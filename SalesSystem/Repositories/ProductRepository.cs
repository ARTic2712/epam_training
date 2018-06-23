using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesSystem.Entities;
using SalesSystem.EF;
using System.Data.Entity;

namespace SalesSystem.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private SaleContext db;
        public ProductRepository(SaleContext saleContext )
        {
            db = saleContext;
        }
        public void Create(Product item)
        {
            db.Producs.Add(item);
        }

        public void Delete(int id)
        {
            Product product = db.Producs.Find(id);
            if (product != null) db.Producs.Remove(product);
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return db.Producs.Where(predicate).ToList();
        }

        public Product Get(int id)
        {
            return db.Producs.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Producs;
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
