namespace SalesSystem.Entities
{
    public class Product : Interfaces.IId
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
