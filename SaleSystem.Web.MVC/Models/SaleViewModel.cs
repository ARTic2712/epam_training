using System.ComponentModel.DataAnnotations;

namespace SaleSystem.Web.MVC.Models
{
    public class SaleViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты покупателя")]
        public string Email { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public int Id_Product { get; set; }
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Описание покупки")]
        public string Description { get; set; }
        [Required]
        [Range(typeof(double), "0", "10000", ErrorMessage = "Значение должно быть не менее 0 и не более 10000")]
        [Display(Name = "Стоимость")]
        public double  Price { get; set; }
    }
}