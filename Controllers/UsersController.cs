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
    public class UsersController : Controller
    {
        private UcommerceDbEntities db = new UcommerceDbEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.AdminUser).Include(u => u.City).Include(u => u.Country);
            return View(users.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(u => u.Email.ToLower() == loginModel.Email.ToLower() && u.Password == loginModel.Password).First();
                           
                if (user == null)
                {
                    ViewBag.error = "Invalid Login";
                }
                else if (user.AdminUser == null)
                {
                    ViewBag.error = "You need to login with admin account";
                }

                else
                {
                    Session["Id"] = user.Id;
                    Session["Name"] = user.Name;
                    Session["Type"] = "Admin";
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["Id"] = "";
            Session["Name"] = "";
            Session["Type"] = "";

            return View();
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.AdminUsers, "UserId", "UserId");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, HttpPostedFileBase image)
        {
            user.IP = Request.UserHostAddress;
            user.JoinDate = DateTime.Now;
            user.Image = System.IO.Path.GetFileName(image.FileName);
            
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                
                db.SaveChanges();

                image.SaveAs(Server.MapPath("../Upload/UserImages/" + user.Id + "_" +user.Image));

                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AdminUsers, "UserId", "UserId", user.Id);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", user.CityId);  
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", user.CountryId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AdminUsers, "UserId", "UserId", user.Id);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", user.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", user.CountryId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, HttpPostedFileBase image)
        {
            user.IP = Request.UserHostAddress;
            user.JoinDate = DateTime.Now;
            user.Image = System.IO.Path.GetFileName(image.FileName);

            if (ModelState.IsValid)
            {   
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                image.SaveAs(Server.MapPath("../Upload/UserImages/" + user.Id + "_" + user.Image));

                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AdminUsers, "UserId", "UserId", user.Id);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", user.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", user.CountryId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
