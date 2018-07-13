using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public string Description { get; set; }
        [Required]
        [Range(typeof(double), "0", "10000")]
        [Display(Name = "Стоимость")]
        public double  Price { get; set; }
    }
}