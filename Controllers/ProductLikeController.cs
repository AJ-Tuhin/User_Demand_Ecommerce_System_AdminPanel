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
    public class ProductLikeController : Controller
    {
        private UcommerceDbEntities db = new UcommerceDbEntities();

        // GET: ProductLike
        public ActionResult Index()
        {
            var productLikes = db.ProductLikes.Include(p => p.Product).Include(p => p.User);
            return View(productLikes.ToList());
        }

        // GET: ProductLike/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLike productLike = db.ProductLikes.Find(id);
            if (productLike == null)
            {
                return HttpNotFound();
            }
            return View(productLike);
        }

        // GET: ProductLike/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: ProductLike/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,UserId,DateTime,IP")] ProductLike productLike)
        {
            if (ModelState.IsValid)
            {
                db.ProductLikes.Add(productLike);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productLike.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productLike.UserId);
            return View(productLike);
        }

        // GET: ProductLike/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLike productLike = db.ProductLikes.Find(id);
            if (productLike == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productLike.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productLike.UserId);
            return View(productLike);
        }

        // POST: ProductLike/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,UserId,DateTime,IP")] ProductLike productLike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productLike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productLike.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productLike.UserId);
            return View(productLike);
        }

        // GET: ProductLike/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLike productLike = db.ProductLikes.Find(id);
            if (productLike == null)
            {
                return HttpNotFound();
            }
            return View(productLike);
        }

        // POST: ProductLike/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductLike productLike = db.ProductLikes.Find(id);
            db.ProductLikes.Remove(productLike);
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
