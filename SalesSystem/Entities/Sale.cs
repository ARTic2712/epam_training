using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateSale { get; set; }
        public double Price { get; set; }
        public User Manager  { get; set; }
        public User Client { get; set; }
        public Product Product { get; set; }
    }
}
