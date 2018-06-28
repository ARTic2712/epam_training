using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ServiceSalesWatcher.ModelsBL;
using SalesSystem.Entities;
using SalesSystem.Repositories;


namespace ServiceSalesWatcher.ControlClasses
{
    public class Logger
    {

        FileSystemWatcher watcher;
        object obj = new object();
        bool enabled = true;
        public Logger()
        {
            watcher = new FileSystemWatcher(Properties.Settings.Default.FilePath);
            watcher.Changed += Watcher_Changed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            ParseFile(e.FullPath);
        }
        private void ParseFile(string filePath)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            if (fileNameWithoutExtension == null) return;
            string[] splittedFileName = fileNameWithoutExtension.Split('_');
            SalesPerDay salesPerDay;
            if (splittedFileName.Count() > 1)
            {
                salesPerDay = new SalesPerDay(splittedFileName[0].Trim(), splittedFileName[1].Trim());
            }
            else return;
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        salesPerDay.ParseLine(line);
                        WriteToDb(salesPerDay);

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void WriteToDb(SalesPerDay salesPerDay )
        {
            using (EFUnitOfWork unitOfWork = new EFUnitOfWork(Properties.Settings.Default.connectionString))
            {
                AutoMapper.Mapper.Reset();
                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<SalesPerDay, User>()
                    .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.ManagerFirstName))
                    .ForMember(x => x.SecondName, opt => opt.MapFrom(src => src.ManagerSecondName));
                    cfg.CreateMap<SalesPerDay, Product>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Product));
                });
                User manager = AutoMapper.Mapper.Map<SalesPerDay, User>(salesPerDay);
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(() => CheckUser(manager,  unitOfWork)));

                Product product = AutoMapper.Mapper.Map<SalesPerDay, Product>(salesPerDay);
                tasks.Add(Task.Run(() => CheckProduct(product, unitOfWork)));
                AutoMapper.Mapper.Reset();
                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<SalesPerDay, User>()
                    .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.ClientFirstName))
                    .ForMember(x => x.SecondName, opt => opt.MapFrom(src => src.ClientSecondName));

                    cfg.CreateMap<SalesPerDay, Sale>()
                    .ForMember(x => x.DateSale, opt => opt.MapFrom(src => Convert.ToDateTime(src.DateSale)))
                    .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(x => x.Client, opt => opt.Ignore())
                    .ForMember(x => x.Manager, opt => opt.Ignore())
                    .ForMember(x => x.Product, opt => opt.Ignore());

                });
                User client = AutoMapper.Mapper.Map<SalesPerDay, User>(salesPerDay);
                Sale sale = AutoMapper.Mapper.Map<SalesPerDay, Sale>(salesPerDay);

                Task.WaitAll(tasks.ToArray());
                
                tasks.Add(Task.Run(() => CheckUser(client, unitOfWork)));
                Task.WaitAll(tasks.ToArray());
                unitOfWork.Save();

                sale.Manager = unitOfWork.Users.GetAll().FirstOrDefault(x => x.FirstName.ToLower() == manager.FirstName.ToLower() && x.SecondName.ToLower() == manager.SecondName.ToLower());
                sale.Client = unitOfWork.Users.GetAll().FirstOrDefault(x => x.FirstName.ToLower() == client.FirstName.ToLower() && x.SecondName.ToLower() == client.SecondName.ToLower()); ;
                sale.Product = unitOfWork.Products.GetAll().FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower()); 
                tasks.Add(Task.Run(() => CheckSale(sale, unitOfWork)));
                Task.WaitAll(tasks.ToArray());
                unitOfWork.Save();
            }
        }
        private void CheckUser(User user, EFUnitOfWork unitOfWork)
        {            
                var userInDb = unitOfWork.Users.GetAll().FirstOrDefault(x => x.FirstName.ToLower() == user.FirstName.ToLower() && x.SecondName.ToLower() == user.SecondName.ToLower());
                if (userInDb == null)
                {
                    user.BirthDay = new DateTime(1900, 1, 1);
                    unitOfWork.Users.Create(user);
                }
        }
        private void CheckProduct(Product  product, EFUnitOfWork unitOfWork)
        {
                var productInDb = unitOfWork.Products.GetAll().FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (productInDb == null)
                {
                    unitOfWork.Products.Create(product);
                }
        }
        private void CheckSale(Sale sale, EFUnitOfWork unitOfWork)
        {
                var saleInDb = unitOfWork.Sales.GetAll().FirstOrDefault(x => x.DateSale == sale.DateSale && x.Product == sale.Product && x.Manager == sale.Manager);
                if (saleInDb == null)
                {
                    unitOfWork.Sales.Create(sale);
                }
        }
    }
}
