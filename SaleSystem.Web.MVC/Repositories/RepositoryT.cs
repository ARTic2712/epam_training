using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SaleSystem.Web.MVC.Interfaces;
using SaleSystem.Web.MVC.Models;

namespace SaleSystem.Web.MVC.Repositories
{
    public class RepositoryT<T>:IRepository<T> where T: class
    {
        private ApplicationDbContext _context;
        private DbSet<T> db;
        public RepositoryT(ApplicationDbContext saleContext)
        {
            _context = saleContext;
            db = saleContext.Set<T>();
        }
        public void Create(T item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            T product = db.Find(id);
            if (product != null) db.Remove(product);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return db.Where(predicate).ToList();
        }

        public T Get(int id)
        {
            return db.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return db;
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
