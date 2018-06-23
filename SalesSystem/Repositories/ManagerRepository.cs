using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesSystem.Interfaces;
using SalesSystem.Entities;
using SalesSystem.EF;
using System.Data.Entity;

namespace SalesSystem.Repositories
{
    public class ManagerRepository : IRepository<Manager>
    {
        private SaleContext db;
        public ManagerRepository(SaleContext saleContext )
        {
            db = saleContext;
        }
        public void Create(Manager item)
        {
            db.Managers.Add(item);
        }

        public void Delete(int id)
        {
            Manager manager = db.Managers.Find(id);
            if (manager != null) db.Managers.Remove(manager);
        }

        public IEnumerable<Manager> Find(Func<Manager, bool> predicate)
        {
            return db.Managers.Where(predicate).ToList();
        }

        public Manager Get(int id)
        {
            return db.Managers.Find(id);
        }

        public IEnumerable<Manager> GetAll()
        {
           return db.Managers;
        }

        public void Update(Manager item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
