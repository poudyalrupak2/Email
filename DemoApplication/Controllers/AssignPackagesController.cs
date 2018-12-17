using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoApplication.Models;
using DemoApplication.Models.DAL;
using Newspaper.Filters;

namespace DemoApplication.Controllers
{
    [SessionCheck]
    public class AssignPackagesController : Controller
    {
        private DemoDbContext db = new DemoDbContext();

        // GET: AssignPackages
        public ActionResult Index()
        {

            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    var assignPackages = db.AssignPackages.Include(a => a.Customer).Include(a => a.Package);
                    return View(assignPackages.ToList());
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);

        }

        // GET: AssignPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    AssignPackage assignPackage = db.AssignPackages.Include(m => m.Customer).Include(a => a.Customer).Include(a => a.Package).Where(m => m.AssignPackageId == id).SingleOrDefault();
                    if (assignPackage == null)
                    {
                        return HttpNotFound();
                    }
                    return View(assignPackage);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        // GET: AssignPackages/Create
        public ActionResult Create()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    ViewBag.CustomerId = new SelectList(db.customers, "CustomerId", "Firstname");
                    ViewBag.PackageId = new SelectList(db.packages, "Id", "PackageCode");
                    return View();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        // POST: AssignPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignPackageId,PackageId,CustomerId,AssignDate")] AssignPackage assignPackage)
        {
            if (ModelState.IsValid)
            {
                assignPackage.AssignDate = DateTime.Now.ToShortDateString();
                db.AssignPackages.Add(assignPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.customers, "CustomerId", "Firstname", assignPackage.CustomerId);
            ViewBag.PackageId = new SelectList(db.packages, "Id", "PackageCode", assignPackage.PackageId);
            return View(assignPackage);
        }

        // GET: AssignPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignPackage assignPackage = db.AssignPackages.Find(id);
            if (assignPackage == null)
            {
                return HttpNotFound();
            }
            string category = Session["ACategory"].ToString();
            ViewBag.CustomerId = new SelectList(db.customers.Where(t=>t.category== "Tour and Travel"), "CustomerId", "Firstname", assignPackage.CustomerId);
            ViewBag.PackageId = new SelectList(db.packages, "Id", "PackageCode", assignPackage.PackageId);
            return View(assignPackage);
        }

        // POST: AssignPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignPackageId,PackageId,CustomerId,AssignDate")] AssignPackage assignPackage)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(assignPackage).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.CustomerId = new SelectList(db.customers, "CustomerId", "Firstname", assignPackage.CustomerId);
                    ViewBag.PackageId = new SelectList(db.packages, "Id", "PackageCode", assignPackage.PackageId);
                    return View(assignPackage);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        // GET: AssignPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    AssignPackage assignPackage = db.AssignPackages.Find(id);
                    if (assignPackage == null)
                    {
                        return HttpNotFound();
                    }
                    return View(assignPackage);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        // POST: AssignPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignPackage assignPackage = db.AssignPackages.Find(id);
            db.AssignPackages.Remove(assignPackage);
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
