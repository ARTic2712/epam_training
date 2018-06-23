using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesSystem.Interfaces;
using SalesSystem.Repositories;
using SalesSystem.Entities;

namespace SalesSystem.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EF.SaleContext db;
        private ManagerRepository _managers;
        private ProductRepository  _products;
        private SaleRepository  _sales;
        private bool disposed = false;
        public EFUnitOfWork(string connectionString )
        {
            db = new EF.SaleContext(connectionString);
        }
        public IRepository<Manager> Managers
        {
            get
            {
                if (_managers == null) _managers = new ManagerRepository(db);
                return _managers;
            }
        }
        public IRepository<Product> Products
        {
            get
            {
                if (_products == null) _products = new ProductRepository(db);
                return _products;
            }
        }
        public IRepository<Sale> Sales
        {
            get
            {
                if (_sales == null) _sales = new SaleRepository(db);
                return _sales;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
