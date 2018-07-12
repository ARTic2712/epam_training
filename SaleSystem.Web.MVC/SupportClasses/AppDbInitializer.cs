using SaleSystem.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SaleSystem.Web.MVC.Repositories;
using SaleSystem.Web.MVC.Interfaces;
using Microsoft.AspNet.Identity;

namespace SaleSystem.Web.MVC.SupportClasses
{
    public class AppDbInitializer: DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            using (IUnitOfWork unitOfWork = new EFUnitOfWork())
            {
                string password = "Art_9398";
                unitOfWork.UserManager.Create( new ApplicationUser{ Email = "artic27@mail.ru", UserName = "artic27@mail.ru" }, password);
                unitOfWork.Roles.Create(new ApplicationRole { Name = "admin" });
                unitOfWork.Roles.Create(new ApplicationRole { Name = "client" });
                unitOfWork.Roles.Create(new ApplicationRole { Name = "manager" });
                unitOfWork.Save();
                unitOfWork.UserRoles.Create(new ApplicationUserRoles(unitOfWork.UserManager.Find("artic27@mail.ru",password ), unitOfWork.Roles.Find(x => x.Name.Contains("admin")).ElementAt(0)));
                unitOfWork.UserRoles.Create(new ApplicationUserRoles(unitOfWork.UserManager.Find("artic27@mail.ru", password), unitOfWork.Roles.Find(x => x.Name.Contains("client")).ElementAt(0)));
                unitOfWork.UserRoles.Create(new ApplicationUserRoles(unitOfWork.UserManager.Find("artic27@mail.ru", password), unitOfWork.Roles.Find(x => x.Name.Contains("manager")).ElementAt(0)));

                unitOfWork.Products.Create(new Product { Name = "Product1" });
                unitOfWork.Save();
                
            }

            base.Seed(context);
        }
    }
}