namespace SaleSystem.Web.MVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SaleSystem.Web.MVC.Repositories;
    using SaleSystem.Web.MVC.Interfaces;
    using Microsoft.AspNet.Identity;
    using SaleSystem.Web.MVC.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SaleSystem.Web.MVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SaleSystem.Web.MVC.Models.ApplicationDbContext";
        }

        protected override void Seed(SaleSystem.Web.MVC.Models.ApplicationDbContext context)
        {
            using (IUnitOfWork unitOfWork = new EFUnitOfWork())
            {
                string password = "Art_9398";
                unitOfWork.UserManager.Create(new ApplicationUser { Email = "artic27@mail.ru", UserName = "artic27@mail.ru"}, password);
                ApplicationUser user=unitOfWork.Users.Find(x => x.UserName == "artic27@mail.ru").ElementAt(0);
                user.FirstName = "Artem";
                user.SecondName  = "Shumski";
                unitOfWork.Users.Update(user);
                unitOfWork.Roles.Create(new ApplicationRole { Name = "admin" });
                unitOfWork.Roles.Create(new ApplicationRole { Name = "client" });
                unitOfWork.Roles.Create(new ApplicationRole { Name = "manager" });
                unitOfWork.Save();
                unitOfWork.UserManager.AddToRole(user.Id, "admin");
                unitOfWork.UserManager.AddToRole(user.Id, "client");
                unitOfWork.UserManager.AddToRole(user.Id, "manager");
                
                unitOfWork.Products.Create(new Product { Name = "Product1" });
                unitOfWork.Save();
            }
        }
    }
}
