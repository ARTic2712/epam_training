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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sales
        public ActionResult Index()
        {
            return View(db.Sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
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
                using (IUnitOfWork unitOfWork = new EFUnitOfWork())
                {
                    var user = unitOfWork.Users.Find(x=>x.Email==sale.Email);
                    if (user.Count()>0)
                    {
                        Sale saleInDb = Mappers.Mapper.SaleViewInSale(sale,unitOfWork );
                        saleInDb.Client = user.ElementAt(0);
                        var manager = unitOfWork.Users.Find(x => x.Email == HttpContext.User.Identity.Name);
                        saleInDb.Manager = manager.ElementAt(0);
                        unitOfWork.Sales.Create(saleInDb);
                        unitOfWork.Save();
                        return RedirectToAction("Index");
                    }
                    return View(sale);
                }
            }

            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,DateSale,Price")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
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
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
