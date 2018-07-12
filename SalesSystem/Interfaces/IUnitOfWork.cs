using System;
using SalesSystem.Entities;

namespace SalesSystem.Interfaces
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
