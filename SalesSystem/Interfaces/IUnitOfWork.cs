using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesSystem.Entities;

namespace SalesSystem.Interfaces
{
    public interface IUnitOfWork:IDisposable 
    {
        IRepository<Manager> Managers { get; }
        IRepository <Product > Products { get;}
        IRepository<Sale> Sales { get; }
        void Save();
    }
}
