using System;
using System.Collections.Generic;
using System.Linq;
using SalesSystem.Entities;
using SalesSystem.EF;
using System.Data.Entity;

namespace SalesSystem.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private SaleContext db;
        public UserRepository(SaleContext saleContext )
        {
            db = saleContext;
        }
        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null) db.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
           return db.Users;
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
