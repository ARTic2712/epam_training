using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
          //  watcher.Created += Watcher_Created;
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
        // изменение файлов
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            ParseFile(e.FullPath);
        }
        // создание файлов
        //private void Watcher_Created(object sender, FileSystemEventArgs e)
        //{
        //    ParseFile(e.FullPath);
        //}
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
                // Task [] tasks=new Task[4];
                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(() => CheckUser(manager,  unitOfWork)));
                // tasks[0]= Task.Run(() => CheckUser(manager));

                Product product = AutoMapper.Mapper.Map<SalesPerDay, Product>(salesPerDay);
                // tasks[1] = Task.Run(() => CheckProduct(product));
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

                //tasks[2] = Task.Run(() => CheckUser(client));
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
            //tasks[3] = Task.Run(() => CheckSale(sale));
        }
        private void CheckUser(User user, EFUnitOfWork unitOfWork)
        {
            Console.WriteLine("CheckUser" + DateTime.Now.ToString());
            System.Threading.Thread.Sleep(2000);
            
                var userInDb = unitOfWork.Users.GetAll().FirstOrDefault(x => x.FirstName.ToLower() == user.FirstName.ToLower() && x.SecondName.ToLower() == user.SecondName.ToLower());
                if (userInDb == null)
                {
                    user.BirthDay = new DateTime(1900, 1, 1);
                    unitOfWork.Users.Create(user);
                    //unitOfWork.Save();
                }
        }
        private void CheckProduct(Product  product, EFUnitOfWork unitOfWork)
        {
                var productInDb = unitOfWork.Products.GetAll().FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (productInDb == null)
                {
                    unitOfWork.Products.Create(product);
                  //  unitOfWork.Save();
                }
        }
        private void CheckSale(Sale sale, EFUnitOfWork unitOfWork)
        {
                var saleInDb = unitOfWork.Sales.GetAll().FirstOrDefault(x => x.DateSale == sale.DateSale && x.Product == sale.Product && x.Manager == sale.Manager);
                if (saleInDb == null)
                {
                    unitOfWork.Sales.Create(sale);
                   // unitOfWork.Save();
                }
        }
    }
}
