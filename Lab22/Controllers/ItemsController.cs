using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab22;
using Lab22.Models;

namespace Lab22.Controllers
{
    public class ItemsController : Controller
    {
        private ShopDBEntities db = new ShopDBEntities();

        // GET: Items
        public ActionResult Index()
        {
            if (Session["CurrentUser"] != null)
            {
                User user = (User)Session["CurrentUser"];
                if (user.Role == "Admin")
                {
                    return View(db.Items.ToList());
                }
            }
            
            return RedirectToAction("Login", "Users");
        }

        // GET: Items/Details/5
        public ActionResult Details(string id)
        {

            if (Session["CurrentUser"] is null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                User user = (User)Session["CurrentUser"];
                if (user.Role != "Admin")
                {
                    return RedirectToAction("Login", "Users");
                }
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            if (Session["CurrentUser"] is null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                User user = (User)Session["CurrentUser"];
                if (user.Role != "Admin")
                {
                    return RedirectToAction("Login", "Users");
                }
            }
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,Quantity,Price,ImageName")] Item item)
        {


            if (Session["CurrentUser"] is null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                User user = (User)Session["CurrentUser"];
                if (user.Role != "Admin")
                {
                    return RedirectToAction("Login", "Users");
                }
            }
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(string id)
        {


            if (Session["CurrentUser"] is null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                User user = (User)Session["CurrentUser"];
                if (user.Role != "Admin")
                {
                    return RedirectToAction("Login", "Users");
                }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Description,Quantity,Price,ImageName")] Item item)
        {


            if (Session["CurrentUser"] is null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                User user = (User)Session["CurrentUser"];
                if (user.Role != "Admin")
                {
                    return RedirectToAction("Login", "Users");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(string id)
        {


            if (Session["CurrentUser"] is null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                User user = (User)Session["CurrentUser"];
                if (user.Role != "Admin")
                {
                    return RedirectToAction("Login", "Users");
                }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {


            if (Session["CurrentUser"] is null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                User user = (User)Session["CurrentUser"];
                if (user.Role != "Admin")
                {
                    return RedirectToAction("Login", "Users");
                }
            }

            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
