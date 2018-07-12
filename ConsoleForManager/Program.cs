using System;
using System.IO;
using System.Text;

namespace ConsoleForManager
{
    class Program
    {
        static void Main(string[] args)
        {
            SalesSystem.Interfaces.IUnitOfWork uw = new SalesSystem.Repositories.EFUnitOfWork("SaleConnection");
            SalesSystem.Entities.User user = new SalesSystem.Entities.User();
            user.FirstName = "test";
            user.SecondName = "Secondtest";
            user.BirthDay = DateTime.Now;
            uw.Users.Create(user);
            Console.WriteLine(uw.Products.Get(91).Name);
            uw.Save();
            while (true)
            {
                Console.WriteLine("-----------------------------");

                

                //Console.WriteLine("Enter your first name");
                //string firstName = Console.ReadLine();
                //Console.WriteLine("Enter your last name");
                //string secondName = Console.ReadLine();
                //Console.WriteLine("Enter product");
                //string product = Console.ReadLine();
                //Console.WriteLine("Enter price");
                //string price = Console.ReadLine();
                //Console.WriteLine("Enter description");
                //string description = Console.ReadLine();
                //Console.WriteLine("Enter client first name");
                //string clientFirstName = Console.ReadLine();
                //Console.WriteLine("Enter client last name");
                //string clientSecondName = Console.ReadLine();
                //var fileName = $"{firstName}_ {secondName}_{DateTime.Now:ddMMyyyy}";
                //try
                //{
                //    using (var fileStream = new StreamWriter(Properties.Settings.Default.FilePath+ $"{fileName}.csv", true, Encoding.Default ))
                //    {
                //       fileStream.WriteLine($"{clientFirstName},{clientSecondName},{product},{price},{description},{DateTime.Now}");
                //    }
                //}
                //catch (FormatException)
                //{
                //    Console.WriteLine("Please, check input format");
                //    File.Delete(Properties.Settings.Default.FilePath + $"{fileName}.csv");
                //}
                //catch(Exception)
                //{
                //    Console.WriteLine("Error writing file");
                //}
            }
        }
    }
}
