using System;
using System.ComponentModel.DataAnnotations;

namespace SaleSystem.Web.MVC.Models 
{
    public class Sale : Interfaces.IId
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Описание покупки")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime )]
        [Display(Name = "Дата")]
        public DateTime DateSale { get; set; }
        [Required]
        [Range(typeof(double), "0", "100000")]
        [Display(Name = "Стоимость")]
        public double Price { get; set; }
        
        public  ApplicationUser  Manager  { get; set; }
        public ApplicationUser Client { get; set; }
        public Product Product { get; set; }

        public Sale()
        {
            DateSale = DateTime.Now;
        }
    }
}
