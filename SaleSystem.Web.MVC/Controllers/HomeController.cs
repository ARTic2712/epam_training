using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleSystem.Web.MVC.Repositories;
using SaleSystem.Web.MVC.Interfaces;
using SaleSystem.Web.MVC.Models;



namespace SaleSystem.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (IUnitOfWork unitOfWork = new EFUnitOfWork())
            {
                var products = unitOfWork.Products.GetAll().ToList();
                ViewBag.Products = products;
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}