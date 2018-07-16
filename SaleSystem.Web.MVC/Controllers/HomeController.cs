using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleSystem.Web.MVC.Repositories;
using SaleSystem.Web.MVC.Interfaces;
using SaleSystem.Web.MVC.Models;
using System.Collections;

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
        public ActionResult GetManagersData()
        {
            using (IUnitOfWork unitOfWork = new EFUnitOfWork())
            {
                var JsonData = new List<ManagerAllSales>();
                var users = unitOfWork.Users.GetAll().ToList();
                foreach (var manager in users)
                {
                    List<Sale> sales = ((unitOfWork as EFUnitOfWork).db.Sales.Include("Manager").Where(x => x.Manager.Id == manager.Id )).ToList();
                    if (sales.Count()>0)
                    {
                        JsonData.Add(new ManagerAllSales());
                        JsonData.Last().Name = manager.SecondName;
                    }
                    foreach (var s in sales)
                    {

                        JsonData.Last().SumPrice += s.Price;
                    }
                }
                return Json(JsonData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}