using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using uCommerceMVC.Models;

namespace uCommerceMVC.Controllers
{
    public class ProductColsedController : Controller
    {
        private UcommerceDbEntities db = new UcommerceDbEntities();

        // GET: ProductColsed
        public ActionResult Index()
        {
            var productColseds = db.ProductColseds.Include(p => p.CloseType).Include(p => p.Product);
            return View(productColseds.ToList());
        }

        // GET: ProductColsed/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductColsed productColsed = db.ProductColseds.Find(id);
            if (productColsed == null)
            {
                return HttpNotFound();
            }
            return View(productColsed);
        }

        // GET: ProductColsed/Create
        public ActionResult Create()
        {
            ViewBag.ClosingTypeId = new SelectList(db.CloseTypes, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: ProductColsed/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,DateTime,ClosingTypeId")] ProductColsed productColsed)
        {
            if (ModelState.IsValid)
            {
                db.ProductColseds.Add(productColsed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClosingTypeId = new SelectList(db.CloseTypes, "Id", "Name", productColsed.ClosingTypeId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productColsed.ProductId);
            return View(productColsed);
        }

        // GET: ProductColsed/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductColsed productColsed = db.ProductColseds.Find(id);
            if (productColsed == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClosingTypeId = new SelectList(db.CloseTypes, "Id", "Name", productColsed.ClosingTypeId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productColsed.ProductId);
            return View(productColsed);
        }

        // POST: ProductColsed/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,DateTime,ClosingTypeId")] ProductColsed productColsed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productColsed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClosingTypeId = new SelectList(db.CloseTypes, "Id", "Name", productColsed.ClosingTypeId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productColsed.ProductId);
            return View(productColsed);
        }

        // GET: ProductColsed/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductColsed productColsed = db.ProductColseds.Find(id);
            if (productColsed == null)
            {
                return HttpNotFound();
            }
            return View(productColsed);
        }

        // POST: ProductColsed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductColsed productColsed = db.ProductColseds.Find(id);
            db.ProductColseds.Remove(productColsed);
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
