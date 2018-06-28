using System;
using SalesSystem.Entities;

namespace SalesSystem.Interfaces
{
    public interface IUnitOfWork:IDisposable 
    {
        IRepository<User> Users { get; }
        IRepository <Product > Products { get;}
        IRepository<Sale> Sales { get; }
        void Save();
    }
}
