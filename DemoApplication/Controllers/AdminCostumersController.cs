using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using DemoApplication.Models;
using DemoApplication.Models.DAL;
using Newspaper.Filters;

namespace DemoApplication.Controllers
{
    [SessionCheck]
    public class AdminCostumersController : Controller
    {
        private DemoDbContext db = new DemoDbContext();

        // GET: AdminCostumers
        public ActionResult Index()
        {
            var email = Session["userEmail"].ToString();
            var costumer = db.AdminCostumers.Where(t => t.CreatedBy ==email ).ToList();
            return View(costumer);
        }

        // GET: AdminCostumers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminCostumers adminCostumers = db.AdminCostumers.Find(id);
            if (adminCostumers == null)
            {
                return HttpNotFound();
            }
            return View(adminCostumers);
        }

        // GET: AdminCostumers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminCostumers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DOB,Email,Phone,Country,State,TypeOfService,ProvidedDate")] AdminCostumers adminCostumers)
        {
            if (ModelState.IsValid)
            {
                adminCostumers.CreatedBy = Session["userEmail"].ToString();
                
                db.AdminCostumers.Add(adminCostumers);
                db.SaveChanges();
                //ReportDocument rd = new ReportDocument();
                //rd.Load(Path.Combine(Server.MapPath("~/CrystalReport2.rpt")));

                //rd.SetDataSource(db.Costumers.Where(s=>s.email1==adminCostumers.Email)
                //.Select(p => new {
                //    Name = p.Name,
                //    Email1 = p.email1
                //}).ToList());

                //Response.Buffer = false;
                //Response.ClearContent();
                //Response.ClearHeaders();


                //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //stream.Seek(0, SeekOrigin.Begin);
                //return File(stream, "application/pdf", "CustomerList.pdf");
                return RedirectToAction("Index");
            }

            return View(adminCostumers);
        }

        // GET: AdminCostumers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminCostumers adminCostumers = db.AdminCostumers.Find(id);
            if (adminCostumers == null)
            {
                return HttpNotFound();
            }
            return View(adminCostumers);
        }

        // POST: AdminCostumers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DOB,Email,Phone,Country,State,TypeOfService,ProvidedDate")] AdminCostumers adminCostumers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminCostumers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminCostumers);
        }

        // GET: AdminCostumers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminCostumers adminCostumers = db.AdminCostumers.Find(id);
            if (adminCostumers == null)
            {
                return HttpNotFound();
            }
            return View(adminCostumers);
        }

        // POST: AdminCostumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminCostumers adminCostumers = db.AdminCostumers.Find(id);
            db.AdminCostumers.Remove(adminCostumers);
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
