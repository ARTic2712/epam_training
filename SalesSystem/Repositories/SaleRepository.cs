//using System;
//using System.Collections.Generic;
//using System.Linq;
//using SalesSystem.Entities;
//using SalesSystem.EF;
//using System.Data.Entity;

//namespace SalesSystem.Repositories
//{
//    class SaleRepository : IRepository<Sale>
//    {
//        private SaleContext db;
//        public SaleRepository (SaleContext saleContext )
//        {
//            db = saleContext;
//        }
//        public void Create(Sale item)
//        {
//            db.Sales.Add(item);
//        }

//        public void Delete(int id)
//        {
//            Sale sale = db.Sales.Find(id);
//            if (sale != null) db.Sales.Remove(sale);
//        }

//        public IEnumerable<Sale> Find(Func<Sale, bool> predicate)
//        {
//            return db.Sales.Where(predicate).ToList();
//        }

//        public Sale Get(int id)
//        {
//            return db.Sales.Find(id);
//        }

//        public IEnumerable<Sale> GetAll()
//        {
//            return db.Sales.ToList();
//        }

//        public void Update(Sale item)
//        {
//            db.Entry(item).State= EntityState.Modified;
//        }
//    }
//}
