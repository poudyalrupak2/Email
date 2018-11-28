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
using Newspaper.Models;

namespace DemoApplication.Controllers
{
    [SessionCheck]

    public class customersController : Controller
    {
        private DemoDbContext db = new DemoDbContext();

        // GET: customers

        public ActionResult Index()
        {
            if (Session["ACategory"] != null)
            {
                string category = Session["ACategory"].ToString();
                    return View(db.customers.Where(t=>t.category==category ).ToList());
                
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: customers/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ACategory"] != null)
            {

                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Customer customer = db.customers.Find(id);
                    if (customer == null)
                    {
                        return HttpNotFound();
                    }
                    return View(customer);
                
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }
        // GET: customers/Create
        public ActionResult Create()
        {
           
                if (Session["ACategory"] != null)
                {
                    return View();
                }
            
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        // POST: customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
            {
            if (ModelState.IsValid)
            {
                customer.CreatedDate = DateTime.Now;
                customer.CreatedBy = Session["userEmail"].ToString(); ;
                try
                {
                    customer.category = Session["ACategory"].ToString();
                    db.customers.Add(customer);
                  
                    String Operation = "Customer Created Sucessfully";
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        Operation = Operation,
                        CreatedBy = Session["userEmail"].ToString(),
                        CreatedDate = DateTime.Now,
                        category= Session["ACategory"].ToString()


                    });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();

                }
            }
            return View(customer);

        }

        // GET: customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ACategory"] != null)
            {
               
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                string category = Session["ACategory"].ToString();

                Customer customer = db.customers.Where(t => t.category ==category).Where(t=>t.CustomerId==id).SingleOrDefault();
                    if (customer == null)
                    {
                        return HttpNotFound();
                    }
                    return View(customer);
                
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Firstname,Middlename,LastName,DOB,email1,email2,phone,phone2,NationalIdNo,tole,homeno,State,Country,ZipCode,CreatedBy,CreatedDate,EditedBy,EditedDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.EditedBy = Session["userEmail"].ToString();
                customer.EditedDate = DateTime.Now;
                try
                {
                    customer.category = Session["ACategory"].ToString();
                    db.Entry(customer).State = EntityState.Modified;

                    
                    String Operation = "Customer edited Sucessfully";

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        Operation = Operation,
                        CreatedBy = Session["userEmail"].ToString(),
                        CreatedDate = DateTime.Now,
                        category =  Session["ACategory"].ToString()


                });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(customer);
        }

        // GET: customers/Delete/5
        public ActionResult Delete(int? id)
        {
            
                if (Session["ACategory"] != null)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                string category = Session["ACategory"].ToString();

                Customer customer = db.customers.Where(t => t.category == category).Where(t => t.CustomerId == id).SingleOrDefault();

                    if (customer == null)
                    {
                        return HttpNotFound();
                    }
                    return View(customer);
                
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string category = Session["ACategory"].ToString();

            Customer customer = db.customers.Where(t => t.category == category).SingleOrDefault();
            try
            {
                db.customers.Remove(customer);
                String Operation = "Customer edited Sucessfully";

                db.ActivityLogs.Add(new ActivityLog
                {
                    Operation = Operation,
                    CreatedBy = Session["userEmail"].ToString(),
                    CreatedDate = DateTime.Now,
                    category =  Session["ACategory"].ToString()


            });
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
           
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
