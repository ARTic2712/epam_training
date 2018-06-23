using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesSystem.Repositories;

namespace ConsoleForManager
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            { 
                Console.WriteLine("Enter your first name");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter your last name");
                string secondName = Console.ReadLine();
                Console.WriteLine("Enter product");
                string product = Console.ReadLine();
                var fileName = $"{secondName+ firstName}_{DateTime.Now:ddMMyyyy}";
                try
                {
                    using (var fileStream = new StreamWriter(Properties.Settings.Default.FilePath+ $"{fileName}.csv", false, Encoding.Default))
                    {
                       fileStream.WriteLine($"{firstName},{secondName},{product}");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please, check input format");
                    File.Delete(Properties.Settings.Default.FilePath + $"{fileName}.csv");
                }
                catch(Exception)
                {
                    Console.WriteLine("Error writing file");
                }
            }
            // EFUnitOfWork unitOfWork = new EFUnitOfWork("SaleConnection");
            //Console.WriteLine(unitOfWork.Managers.Get(1).SecondName);
            //SalesSystem.Entities.Manager manager1 = new SalesSystem.Entities.Manager() { FirstName = "Artem", SecondName = "Shumski", BirthDay = new DateTime(1993, 12, 27) };
            //SalesSystem.Entities.Manager manager2 = new SalesSystem.Entities.Manager() { FirstName = "Petr", SecondName = "Romanov", BirthDay = new DateTime(1994, 6, 5) };
            //SalesSystem.Entities.Product product1 = new SalesSystem.Entities.Product() { Name = "Xiaomi Redmi Note4", Price = 400 };
            //SalesSystem.Entities.Product product2 = new SalesSystem.Entities.Product() { Name = "Xiaomi Redmi 5", Price = 560 };
            //unitOfWork.Managers.Create(manager1);
            //unitOfWork.Managers.Create(manager2);
            //unitOfWork.Products.Create(product1);
            //unitOfWork.Products.Create(product2);
            //unitOfWork.Sales.Create(new SalesSystem.Entities.Sale() { Manager = manager1, Product = product1, DateSale = DateTime.Now, Description = "My first sale" });
            //unitOfWork.Save();
        }
    }
}
