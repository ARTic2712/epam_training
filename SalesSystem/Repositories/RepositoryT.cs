using System;
using System.Collections.Generic;
using System.Linq;
using SalesSystem.Entities;
using SalesSystem.EF;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SalesSystem.Repositories
{
    public class RepositoryT<T>:IRepository<T> where T: class
    {
        private DbContext _context;
        private DbSet<T> db;
        public RepositoryT(DbContext  saleContext)
        {
            _context = saleContext;
            db = saleContext.Set<T>();
        }
        public void Create(T item)
        {
            db.Add(item);
        }
        public void CreateUser(ApplicationUser user, string password)
        {
            var userManager = new SalesSystem.ApplicationUserManager(new UserStore<SalesSystem.Entities.ApplicationUser>(_context));
            var result = userManager.Create(user, password);

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
