using System.ComponentModel.DataAnnotations;

namespace SaleSystem.Web.MVC.Models
{
    public class Product : Interfaces.IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength =2)]
        [Display(Name = "Наименование продукта")]
        public string Name { get; set; }
    }
}
