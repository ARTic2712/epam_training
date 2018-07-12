using System;

namespace SalesSystem.Entities
{
    public class Sale : Interfaces.IId
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateSale { get; set; }
        public double Price { get; set; }
        public ApplicationUser  Manager  { get; set; }
        public ApplicationUser Client { get; set; }
        public Product Product { get; set; }
    }
}
