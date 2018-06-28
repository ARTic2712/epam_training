using System;
using System.Data.Entity;
using SalesSystem.Entities;

namespace SalesSystem.EF
{
   public class SaleContext:DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product > Producs { get; set; }
        public DbSet<Sale > Sales { get; set; }
        public SaleContext(String connectionString)
            : base(connectionString)
        {

        }
    }
}
