using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaleSystem.Web.MVC.Interfaces;
using SaleSystem.Web.MVC.Models;
using SaleSystem.Web.MVC.Repositories;

namespace SaleSystem.Web.MVC.Mappers
{
    public static class Mapper
    {
        public static Sale SaleViewInSale(SaleViewModel saleView, IUnitOfWork unitOfWork )
        {
            Sale sale = new Sale();
            sale.Description = saleView.Description;
            sale.Price = saleView.Price;
            sale.Product = unitOfWork.Products.Get(saleView.Id_Product);
            return sale;
        }
        public static SaleViewModel SaleinSaleView(Sale sale, IUnitOfWork unitOfWork)
        {
            SaleViewModel  saleview = new SaleViewModel();
            saleview.Description = sale.Description;
            saleview.Price = sale.Price;
            saleview.Email = sale.Client.Email;
            saleview.Id = sale.Id;
            saleview.Id_Product = sale.Product.Id;
            return saleview;
        }
    }
}