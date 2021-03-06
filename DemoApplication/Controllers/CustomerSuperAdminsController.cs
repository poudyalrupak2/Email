﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using DemoApplication.Models;
using DemoApplication.Models.DAL;
using Newspaper.Filters;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace DemoApplication.Controllers
{


    [SessionCheck]
    public class CustomerSuperAdminsController : Controller
    {
        private DemoDbContext db = new DemoDbContext();
        // GET: CustomerSuperAdmins

        public ActionResult Index()
        {
            if (Session["ACategory"] == null)
            {

                return View(db.Costumers.ToList());
            }
            return new HttpNotFoundResult();
        }

        // GET: CustomerSuperAdmins/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ACategory"] == null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CustomerSuperAdmin customerSuperAdmin = db.Costumers.Find(id);
                if (customerSuperAdmin == null)
                {
                    return HttpNotFound();
                }
                return View(customerSuperAdmin);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: CustomerSuperAdmins/Create
        public ActionResult Create()
        {
            if (Session["ACategory"] == null)
            {
                return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


        }

        // POST: CustomerSuperAdmins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerSuperAdmin customerSuperAdmin, Models.Login login, HttpPostedFileBase CImageFile, HttpPostedFileBase PPImageFile, HttpPostedFileBase LImageFile, HttpPostedFileBase CDImageFile)
        {

            if (ModelState.IsValid)
            {
                int data = db.Login.Where(t => t.Email == customerSuperAdmin.email1).Count();
                if (data== 0)
                {
                    HttpFileCollectionBase image = Request.Files;
                    if (CImageFile != null)
                    {
                        string firstpath = "/images/";
                        string subPath = "/images/Citizenship/"; // your code goes here
                        bool imageexist = System.IO.Directory.Exists(Server.MapPath(firstpath));
                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                        if(!imageexist)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(firstpath));
                        }
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        }
                        string fileName1 = CImageFile.FileName;
                        string filename1 = Path.GetFileNameWithoutExtension(CImageFile.FileName);
                        string extension1 = Path.GetExtension(CImageFile.FileName);
                        filename1 = customerSuperAdmin.CustumerId + extension1;
                        customerSuperAdmin.CitizenShipPhoto = "/images/Citizenship/" + filename1;
                        filename1 = Path.Combine(Server.MapPath("~/Images/Citizenship/"), filename1);
                        CImageFile.SaveAs(filename1);
                    }
                    if (CDImageFile != null)
                    {
                        string firstpath = "/images/";
                        string subPath = "/images/CompanyDocument/"; // your code goes here
                        bool imageexist = System.IO.Directory.Exists(Server.MapPath(firstpath));
                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                        if (!imageexist)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(firstpath));
                        }
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        }

                        string fileName2 = CDImageFile.FileName;
                        string filename2 = Path.GetFileNameWithoutExtension(CDImageFile.FileName);
                        string extension2 = Path.GetExtension(CDImageFile.FileName);
                        filename2 = customerSuperAdmin.CustumerId + extension2;
                        customerSuperAdmin.CompanyDocument = "/images/CompanyDocument/" + filename2;
                        filename2 = Path.Combine(Server.MapPath("~/Images/CompanyDocument/"), filename2);
                        CDImageFile.SaveAs(filename2);

                    }
                    if (LImageFile != null)
                    {
                        string firstpath = "/images/";
                        string subPath = "/images/Logo/"; // your code goes here
                        bool imageexist = System.IO.Directory.Exists(Server.MapPath(firstpath));
                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                        if (!imageexist)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(firstpath));
                        }
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        }

                        string fileName3 = LImageFile.FileName;

                        string filename3 = Path.GetFileNameWithoutExtension(LImageFile.FileName);
                        string extension3 = Path.GetExtension(LImageFile.FileName);
                        filename3 = customerSuperAdmin.CustumerId + extension3;
                        customerSuperAdmin.Logo = "/images/Logo/" + filename3;
                        filename3 = Path.Combine(Server.MapPath("~/Images/Logo/"), filename3);
                        LImageFile.SaveAs(filename3);
                    }
                    if (PPImageFile != null)
                    {
                        string firstpath = "/images/";
                        string subPath = "/images/PPSizePhoto/"; // your code goes here
                        bool imageexist = System.IO.Directory.Exists(Server.MapPath(firstpath));
                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                        if (!imageexist)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(firstpath));
                        }
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        }


                        string fileName4 = PPImageFile.FileName;

                        string filename4 = Path.GetFileNameWithoutExtension(PPImageFile.FileName);
                        string extension4 = Path.GetExtension(PPImageFile.FileName);


                        filename4 = customerSuperAdmin.CustumerId + extension4;
                        customerSuperAdmin.PpsizePhoto = "/images/PPSizePhoto/" + filename4;
                        filename4 = Path.Combine(Server.MapPath("~/Images/PPSizePhoto/"), filename4);
                        PPImageFile.SaveAs(filename4);
                    }
                    //save new record in database

                 
                    Random generator = new Random();
                    String password = generator.Next(0, 999999).ToString("D6");
                  
                        



                           
                            var message = new MailMessage();
                            message.To.Add(new MailAddress(customerSuperAdmin.email1));
                        message.Subject = "Change Password";
                        message.Body = "Use this Password to login:" + password;
                        using (var smtp = new SmtpClient())
                        {
                        try
                        {
                          
                            smtp.Send(message);

                            login.Email = customerSuperAdmin.email1;
                            login.randompass = password;
                            login.Category = "Admin";
                            db.Login.Add(login);
                            db.Costumers.Add(customerSuperAdmin);

                            db.SaveChanges();


                        }
                        catch(Exception e)
                        {
                            return RedirectToAction("Error");
                        }

                    }

                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Email already exists");
            return View();
           

        }

        // GET: CustomerSuperAdmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ACategory"] == null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CustomerSuperAdmin customerSuperAdmin = db.Costumers.Find(id);
                if (customerSuperAdmin == null)
                {
                    return HttpNotFound();
                }
                return View(customerSuperAdmin);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        // POST: CustomerSuperAdmins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustumerId,Name,RegisterDate,Categoty,email1,email2,Phone1,PanNo,URL,CustomerType,Detail,WorkingArea,NatureOfWork,CitizenShipPhoto,CompanyDocument,Logo,PpsizePhoto,RandomPass,Password")] CustomerSuperAdmin customerSuperAdmin, HttpPostedFileBase CImageFile, HttpPostedFileBase PPImageFile, HttpPostedFileBase LImageFile, HttpPostedFileBase CDImageFile)//[Bind(Include = "Id,Name,RegisterDate,Categoty,email1,email2,Phone1,PanNo,URL,CustomerType,Detail,WorkingArea,NatureOfWork,CitizenShipPhoto,CompanyDocument,Logo,PpsizePhoto,RandomPass,Password")] CustomerSuperAdmin customerSuperAdmin)
        {
            if (ModelState.IsValid)
            {
                HttpFileCollectionBase image = Request.Files;
                if (CImageFile != null)
                {
                    string fileName1 = CImageFile.FileName;
                    string filename1 = Path.GetFileNameWithoutExtension(CImageFile.FileName);
                    string extension1 = Path.GetExtension(CImageFile.FileName);
                    filename1 = customerSuperAdmin.CustumerId + extension1;
                    customerSuperAdmin.CitizenShipPhoto = "/images/Citizenship/" + filename1;
                    filename1 = Path.Combine(Server.MapPath("~/Images/Citizenship/"), filename1);
                    CImageFile.SaveAs(filename1);
                }
                if (CDImageFile != null)
                {
                    string fileName2 = CDImageFile.FileName;
                    string filename2 = Path.GetFileNameWithoutExtension(CDImageFile.FileName);
                    string extension2 = Path.GetExtension(CDImageFile.FileName);
                    filename2 = customerSuperAdmin.CustumerId + extension2;
                    customerSuperAdmin.CompanyDocument = "/images/CompanyDocument/" + filename2;
                    filename2 = Path.Combine(Server.MapPath("~/Images/CompanyDocument/"), filename2);
                    CDImageFile.SaveAs(filename2);

                }
                if (LImageFile != null)
                {
                    string fileName3 = LImageFile.FileName;

                    string filename3 = Path.GetFileNameWithoutExtension(LImageFile.FileName);
                    string extension3 = Path.GetExtension(LImageFile.FileName);
                    filename3 = customerSuperAdmin.CustumerId + extension3;
                    customerSuperAdmin.Logo = "/images/Logo/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/Images/Logo/"), filename3);
                    LImageFile.SaveAs(filename3);
                }
                if (PPImageFile != null)
                {
                    string fileName4 = PPImageFile.FileName;

                    string filename4 = Path.GetFileNameWithoutExtension(PPImageFile.FileName);
                    string extension4 = Path.GetExtension(PPImageFile.FileName);


                    filename4 = customerSuperAdmin.CustumerId + extension4;
                    customerSuperAdmin.PpsizePhoto = "/images/PPSizePhoto/" + filename4;
                    filename4 = Path.Combine(Server.MapPath("~/Images/PPSizePhoto/"), filename4);
                    PPImageFile.SaveAs(filename4);
                }
                //save new record in database


                try
                {

                    db.Entry(customerSuperAdmin).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    //return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }

            try
            {
             
                return RedirectToAction("Index");
            }
            catch
            {
                return View(customerSuperAdmin);
            }
            
            }
            return View();
        }

        // GET: CustomerSuperAdmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSuperAdmin customerSuperAdmin = db.Costumers.Find(id);
            if (customerSuperAdmin == null)
            {
                return HttpNotFound();
            }
            return View(customerSuperAdmin);
        }

        // POST: CustomerSuperAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerSuperAdmin customerSuperAdmin = db.Costumers.Find(id);
            db.Costumers.Remove(customerSuperAdmin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public bool IsEmailExists(string Email)
        {
            var validateName = db.Costumers.FirstOrDefault
                                (x => x.email1 == Email);
            if (validateName != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public ActionResult Error()
        {
            return View();
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
