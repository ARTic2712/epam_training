using System;
using SaleSystem.Web.MVC.Models;
namespace SaleSystem.Web.MVC.Interfaces
{
    public interface IUnitOfWork:IDisposable 
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository <Product > Products { get;}
        IRepository<Sale> Sales { get; }
        IRepository<ApplicationRole > Roles { get; }
        IRepository<ApplicationUserRoles > UserRoles { get; }
        ApplicationUserManager UserManager { get; }
        void Save();
    }
}
