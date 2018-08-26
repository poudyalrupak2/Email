using System;
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
        public ActionResult Create([Bind(Include = "Id,CustumerId,Name,RegisterDate,Categoty,email1,email2,Phone1,PanNo,URL,CustomerType,Detail,WorkingArea,NatureOfWork,CitizenShipPhoto,CompanyDocument,Logo,PpsizePhoto,RandomPass,Password")] CustomerSuperAdmin customerSuperAdmin, Models.Login login, HttpPostedFileBase CImageFile, HttpPostedFileBase PPImageFile, HttpPostedFileBase LImageFile, HttpPostedFileBase CDImageFile)
        {

            if (ModelState.IsValid)
            {
                int data = db.Login.Where(t => t.Email == customerSuperAdmin.email1).Count();
                if (data== 0)
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

                    string from = "dbugtest2016@gmail.com";
                    string fromPassword = "my@test#";
                    string password = Membership.GeneratePassword(6, 1);
                    using (MailMessage mail = new MailMessage(from, customerSuperAdmin.email1))
                    {

                        try
                        {



                            mail.Subject = "Change Password";
                            mail.Body = "Use this Password to login:" + password;

                            mail.IsBodyHtml = false;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            NetworkCredential networkCredential = new NetworkCredential(from, fromPassword);

                            smtp.Credentials = networkCredential;
                            smtp.Port = 587;
                            smtp.Send(mail);

                            login.Email = customerSuperAdmin.email1;
                            login.randompass = password;
                            login.Category = "Admin";
                            db.Login.Add(login);
                            db.Costumers.Add(customerSuperAdmin);

                            db.SaveChanges();


                        }
                        catch
                        {
                            return RedirectToAction("errorpage");
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
