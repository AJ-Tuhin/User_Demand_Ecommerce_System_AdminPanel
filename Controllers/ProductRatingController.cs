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
    public class ProductRatingController : Controller
    {
        private UcommerceDbEntities db = new UcommerceDbEntities();

        // GET: ProductRating
        public ActionResult Index()
        {
            var productRatings = db.ProductRatings.Include(p => p.Product).Include(p => p.User);
            return View(productRatings.ToList());
        }

        // GET: ProductRating/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRating productRating = db.ProductRatings.Find(id);
            if (productRating == null)
            {
                return HttpNotFound();
            }
            return View(productRating);
        }

        // GET: ProductRating/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: ProductRating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,UserId,DateTime,IP,Rating")] ProductRating productRating)
        {
            if (ModelState.IsValid)
            {
                db.ProductRatings.Add(productRating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productRating.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productRating.UserId);
            return View(productRating);
        }

        // GET: ProductRating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRating productRating = db.ProductRatings.Find(id);
            if (productRating == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productRating.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productRating.UserId);
            return View(productRating);
        }

        // POST: ProductRating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,UserId,DateTime,IP,Rating")] ProductRating productRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productRating.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productRating.UserId);
            return View(productRating);
        }

        // GET: ProductRating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRating productRating = db.ProductRatings.Find(id);
            if (productRating == null)
            {
                return HttpNotFound();
            }
            return View(productRating);
        }

        // POST: ProductRating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductRating productRating = db.ProductRatings.Find(id);
            db.ProductRatings.Remove(productRating);
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
