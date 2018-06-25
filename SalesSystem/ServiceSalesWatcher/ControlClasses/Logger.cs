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
        EFUnitOfWork unitOfWork;

        FileSystemWatcher watcher;
        object obj = new object();
        bool enabled = true;
        public Logger()
        {
            watcher = new FileSystemWatcher(Properties.Settings.Default.FilePath);
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            unitOfWork = new EFUnitOfWork(Properties.Settings.Default.connectionString);
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
        private void Watcher_Created(object sender, FileSystemEventArgs e)
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
                salesPerDay = new SalesPerDay(splittedFileName[0], splittedFileName[1]);
            }
            else return;
                
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var lineContent = line.Split(',');
                    salesPerDay.ClientFirstName = lineContent[0];
                    salesPerDay.ClientSecondName = lineContent[1];
                    salesPerDay.Product = lineContent[2];
                    salesPerDay.Price = Convert.ToDouble( lineContent[3]);
                    salesPerDay.Description  = lineContent[5];
                    salesPerDay.DateSale = Convert.ToDateTime(lineContent[5]);

                    AutoMapper.Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<SalesPerDay, User>()
                        .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.ManagerFirstName))
                        .ForMember(x => x.SecondName, opt => opt.MapFrom(src => src.ManagerSecondName));
                    });
                    User manager = AutoMapper.Mapper.Map<SalesPerDay, User>(salesPerDay);
                    Task [] tasks=new Task[4];
                    tasks[0]= Task.Run(() => CheckUser(manager));
                    AutoMapper.Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<SalesPerDay, User>()
                        .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.ClientFirstName ))
                        .ForMember(x => x.SecondName, opt => opt.MapFrom(src => src.ClientSecondName ));
                        cfg.CreateMap<SalesPerDay, Product>()
                        .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Product));
                        cfg.CreateMap<SalesPerDay, Sale>()
                        .ForMember(x => x.DateSale, opt => opt.MapFrom(src => src.DateSale))
                        .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                        .ForMember(x => x.Price , opt => opt.MapFrom(src => src.Price));

                    });
                    User client = AutoMapper.Mapper.Map<SalesPerDay, User>(salesPerDay);
                    Product product = AutoMapper.Mapper.Map<SalesPerDay, Product >(salesPerDay);
                    Sale sale= AutoMapper.Mapper.Map<SalesPerDay, Sale >(salesPerDay);
                    tasks[1] = Task.Run(() => CheckProduct(product));
                    Task.WaitAll(tasks);
                    tasks[2] = Task.Run(() => CheckUser(client));
                    Task.WaitAll(tasks);
                    sale.Manager = manager;
                    sale.Product = product;
                    tasks[3] = Task.Run(() => CheckSale(sale));
                }
                unitOfWork.Save();
            }
        }
        private void CheckUser(User user)
        {
            var userInDb = unitOfWork.Users.GetAll().FirstOrDefault(x => x.FirstName == user.FirstName && x.SecondName == user.SecondName);
            if (userInDb == null)
            {
                unitOfWork.Users.Create(user);
            }
        }
        private void CheckProduct(Product  product)
        {
            var productInDb = unitOfWork.Products .GetAll().FirstOrDefault(x => x.Name==product.Name );
            if (productInDb == null)
            {
                unitOfWork.Products.Create(product);
            }
        }
        private void CheckSale(Sale sale)
        {
            var saleInDb = unitOfWork.Sales .GetAll().FirstOrDefault(x =>x.DateSale ==sale.DateSale && x.Product ==sale.Product && x.Manager ==sale.Manager );
            if (saleInDb == null)
            {
                unitOfWork.Sales .Create(sale);
            }
        }
    }
}
