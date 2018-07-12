namespace SaleSystem.Web.MVC.Models
{
    public class Product : Interfaces.IId
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
