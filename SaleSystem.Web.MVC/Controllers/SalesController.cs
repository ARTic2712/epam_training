using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaleSystem.Web.MVC.Interfaces;
using SaleSystem.Web.MVC.Models;
using SaleSystem.Web.MVC.Repositories;

namespace SaleSystem.Web.MVC.Controllers
{
    public class SalesController : Controller
    {
        private IUnitOfWork unitOfWork = new EFUnitOfWork();
        // private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sales
        public ActionResult Index()
        {
                if (User.IsInRole("admin"))
                {
                    return View(unitOfWork.Sales.GetAll().ToList());
                }
                else if (User.IsInRole("manager"))
                {
                    var manager = unitOfWork.Users.Find(x => x.Email == HttpContext.User.Identity.Name);
                    if (manager.Count() > 0)
                    {
                        return View(unitOfWork.Sales.Find(x => x.Manager == manager.ElementAt(0)));
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(unitOfWork.Sales.GetAll().ToList());

                }
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sale sale = unitOfWork.Sales.Get((int)id);
                if (sale == null)
                {
                    return HttpNotFound();
                }
                return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create(int? idProduct)
        {
            if (idProduct != null)
            {
                SaleViewModel sale = new SaleViewModel();
                sale.Id_Product = (int)idProduct;
                return View(sale);
            }
            return RedirectToAction("Index","Home");
        }

        // POST: Sales/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Price,email,Id_Product")] SaleViewModel sale)
        {
            if (ModelState.IsValid)
            {
                    var user = unitOfWork.Users.Find(x => x.Email == sale.Email);
                    if (user.Count() > 0)
                    {
                        Sale saleInDb = Mappers.Mapper.SaleViewInSale(sale, unitOfWork);
                        saleInDb.Client = user.ElementAt(0);
                        var manager = unitOfWork.Users.Find(x => x.Email == HttpContext.User.Identity.Name);
                        saleInDb.Manager = manager.ElementAt(0) ;
                        unitOfWork.Sales.Create(saleInDb);
                        unitOfWork.Save();
                        return RedirectToAction("Index");
                    }
                    return View(sale);
            }

            return View(sale);
        }

        // GET: Sales/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    using (IUnitOfWork unitOfWork = new EFUnitOfWork())
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Sale saleDB = unitOfWork.Sales.Get((int)id);

        //        SaleViewModel sale = Mappers.Mapper.SaleinSaleView(saleDB, unitOfWork);
        //        if (sale == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(sale);
        //    }
        //}

        // POST: Sales/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Description,Price,email,Id_Product")] SaleViewModel sale)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (IUnitOfWork unitOfWork = new EFUnitOfWork())
        //        {
        //            var user = unitOfWork.Users.Find(x => x.Email == sale.Email);
        //            if (user.Count() > 0)
        //            {
        //                //Sale saleInDb = sale.Id > 0 ? unitOfWork.Sales.Get(sale.Id) : new Sale();
        //                //saleInDb = Mappers.Mapper.SaleViewInSale(saleInDb, sale, unitOfWork);
        //                //saleInDb.ClientId   = user.ElementAt(0).Id;
        //                //var manager = unitOfWork.Users.Find(x => x.Email == HttpContext.User.Identity.Name);
        //                //saleInDb.ManagerId  = manager.ElementAt(0).Id ;
        //                //unitOfWork.Sales.Update(saleInDb);
        //                //unitOfWork.Save();
        //                //return RedirectToAction("Index");
        //            }
        //            return View(sale);
        //        }
        //    }
        //    return View(sale);
        //}

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sale sale = unitOfWork.Sales.Get((int)id);
                if (sale == null)
                {
                    return HttpNotFound();
                }
                return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                unitOfWork.Sales.Delete(id);
                unitOfWork.Save ();
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SearchSales(string searchText,DateTime? dateSearch)
        {
            if (String.IsNullOrEmpty(searchText ))
            {
                if (dateSearch == null) return PartialView(unitOfWork.Sales.GetAll());
                return PartialView(unitOfWork.Sales.Find(x =>x.DateSale.Date ==((DateTime ) dateSearch ).Date  ));
            }
            var products = unitOfWork.Products.Find(x => x.Name.Contains(searchText));
            if (products.Count() > 0)
            {
                int idProd = products.ElementAtOrDefault(0).Id;
                ICollection<Sale> allsales;
                if (dateSearch == null)
                {
                    allsales = ((unitOfWork as EFUnitOfWork).db.Sales.Include("Product").Where(x => x.Product.Id == idProd)).ToList();  //.Sales.Find( x => x.Product.Name.Contains(product));
                }
                else
                {
                    allsales= ((unitOfWork as EFUnitOfWork).db.Sales.Include("Product").Where(x => x.Product.Id == idProd && DbFunctions.TruncateTime(x.DateSale) == DbFunctions.TruncateTime(dateSearch))).ToList();
                } 
                return PartialView(allsales);

            }
            var manager = unitOfWork.Users.Find(x => SearchManager(searchText, x));
            if (manager.Count() > 0)
            {
                string idManager = manager.ElementAtOrDefault(0).Id;
                ICollection<Sale> allsales;
                if (dateSearch == null)
                {
                    allsales = ((unitOfWork as EFUnitOfWork).db.Sales.Include("Manager").Where(x => x.Manager.Id == idManager)).ToList();  //.Sales.Find( x => x.Product.Name.Contains(product));
                }
                else
                {
                    allsales = ((unitOfWork as EFUnitOfWork).db.Sales.Include("Manager").Where(x => x.Manager.Id == idManager && DbFunctions.TruncateTime(x.DateSale)== DbFunctions.TruncateTime(dateSearch))).ToList();  //.Sales.Find( x => x.Product.Name.Contains(product));

                }
                return PartialView(allsales);

            }

            return PartialView(new List<Sale>());
        }

        private bool CheckDate(DateTime dateSale, DateTime dateSearch )
        {
            if (dateSale.Year==dateSearch.Year && dateSale.Month==dateSearch.Month && dateSale.Day == dateSearch.Day ) return true;
            return false;
        }
        private bool  SearchManager(string searchText,ApplicationUser user)
        {
            if (user.Email.Contains(searchText )) return true;
            if (user.FirstName.Contains(searchText)) return true;
            if (user.SecondName.Contains(searchText)) return true;
            return false;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
