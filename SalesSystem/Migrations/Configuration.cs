namespace SalesSystem.Migrations
{
    using Microsoft.AspNet.Identity;
    using SalesSystem.Interfaces;
    using SalesSystem.Repositories;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesSystem.Entities.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SalesSystem.Entities.ApplicationDbContext";
        }

        protected override void Seed(SalesSystem.Entities.ApplicationDbContext context)
        {
            using (IUnitOfWork unitOfWork = new EFUnitOfWork())
            {
                string password = "Art_9398";
                unitOfWork.UserManager.Create(new SalesSystem.Entities.ApplicationUser("artic27@mail.ru", "artic27@mail.ru", "Artem", "Shumski"), password);
                unitOfWork.Roles.Create(new SalesSystem.Entities.ApplicationRole { Name = "admin" });
                unitOfWork.Roles.Create(new SalesSystem.Entities.ApplicationRole { Name = "client" });
                unitOfWork.Roles.Create(new SalesSystem.Entities.ApplicationRole { Name = "manager" });
                unitOfWork.Save();
                unitOfWork.UserRoles.Create(new SalesSystem.Entities.ApplicationUserRoles(unitOfWork.Users.GetAll().ElementAt(0), unitOfWork.Roles.Find(x => x.Name.Contains("admin")).ElementAt(0)));
                unitOfWork.UserRoles.Create(new SalesSystem.Entities.ApplicationUserRoles(unitOfWork.Users.GetAll().ElementAt(0), unitOfWork.Roles.Find(x => x.Name.Contains("client")).ElementAt(0)));
                unitOfWork.UserRoles.Create(new SalesSystem.Entities.ApplicationUserRoles(unitOfWork.Users.GetAll().ElementAt(0), unitOfWork.Roles.Find(x => x.Name.Contains("manager")).ElementAt(0)));

                unitOfWork.Products.Create(new SalesSystem.Entities.Product { Name = "Product1" });
                unitOfWork.Save();



            }
        }
    }
}
