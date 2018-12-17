using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using DemoApplication.Models;
using DemoApplication.Models.DAL;
using Newspaper.Filters;

namespace DemoApplication.Controllers
{
    [SessionCheck]

    public class PackagesController : Controller
    {
        private DemoDbContext db = new DemoDbContext();
        private ImageUpload imgesUpload = new ImageUpload();

        // GET: Packages
        public ActionResult Index()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    return View(db.packages.ToList());
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
        }

        // GET: Packages/Details/5
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
                    Package package = db.packages.Find(id);
                    if (package == null)
                    {
                        return HttpNotFound();
                    }
                    return View(package);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    return View();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
        }
        public List<PImage> fileDetails = new List<PImage>();

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Package package, HttpPostedFileBase TImage, ICollection<HttpPostedFileBase> AImage)
        {
            if (ModelState.IsValid)
            {

                foreach (var file in AImage)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        imgesUpload = new ImageUpload { Width = 200 };
                        ImageResult imageResult = imgesUpload.RenameUploadFile(file);
                        fileDetails.Add(imgesUpload.fileDetails);
                    }
                }

                package.PImage = fileDetails;
                package.CreatedBy = Session["userEmail"].ToString();
                package.CreatedDate = DateTime.Now;
                db.packages.Add(package);
                db.SaveChanges();
                db.Entry(package).GetDatabaseValues();

                //string body = "<html><head> <style>body { margin: 0; padding: 0; min - width: 100 % !important; } .content{width:100 %;max-width:600px;}</ style ></head>< body style='color:grey; font-size:15px;'><div style='background-color:#ece8d4;width:600px;height:600px; padding:30px 30px; margin-top:30px;'><p> Dear <b>{0}</b>,<p><p> New package is being created.</p><p> Please Contact soon as possible</p><p>";
                string body;

                using (var sr = new StreamReader(Server.MapPath("\\App_Data\\HtmlTamplate\\PackageTemplate.html")))
                {
                    body = sr.ReadToEnd();
                }
                var charts = db.pImages.Where(m => m.PackageId == package.Id);
                //int j = 0;
                //foreach (var item in charts)
                //{
                //    string img = " <div class='column'><img src='cid:" + j + "'/></div><br/>";
                //    body= body+ img;
                //    j++;
                //}

                

               // body =body + " The package starts from {1} and last {2} days.</p><p> Thank you </p><p> <b>DebugSoft</b> </p></div></body></html>";


                string category = Session["ACategory"].ToString();
                var costumer = db.customers.Where(t => t.category == category).Select(t => t.email1).ToList();
                var rate = db.customers.Where(t => t.category == category).Select(t => t.CustomerCategory).ToList();
               
                    try
                    {
                    using (var smtp = new SmtpClient())
                    {
                        for (int i = 0; i < costumer.Count; i++)
                        {

                            var message = new MailMessage();
                            message.To.Add(new MailAddress(costumer[i]));
                            string email = costumer[i];
                            var name = db.customers.FirstOrDefault(t => t.category == category && t.email1 == email).Firstname;
                            var image = package.thumbnail;
                            string Packagenamename = package.PackageName;
                            string costumername = Convert.ToString(name);
                            string From = package.From;
                            string to = package.TO;
                            string startdate = package.BegainDate.ToString();
                            DateTime endtime = Convert.ToDateTime(startdate).AddDays(package.Duration);
                            string time = endtime.ToShortDateString();
                            decimal rate1;
                            if (rate[i] == "Wholesale")
                            {
                                rate1 = package.Rate1;
                            }
                            else
                            {
                                rate1 = package.Rate2;
                            }


                            string messageBody = string.Format(body, Packagenamename, costumername, startdate, time, From, to, rate1);
                            //int k = 0;
                            //foreach (var item in charts)
                            //{
                            // string path = Server.MapPath(item.ImageName); // my logo is placed in images folder
                            string path = Server.MapPath(@"/images/dbug.gif"); // my logo is placed in images folder
                                                                               // string messageBody = string.Format(body, username, username, ticketdate, tickettime);

                            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);
                            LinkedResource logo = new LinkedResource(path, MediaTypeNames.Image.Jpeg);

                            // logo.ContentId = Convert.ToString(k);
                            avHtml.LinkedResources.Add(logo);
                            message.AlternateViews.Add(avHtml);
                            logo.ContentId = "logo";

                            // k++;
                            // }
                            // avHtml.LinkedResources.Add(logo);






                            message.Subject = "New Package Has Been Added";
                            message.Body = messageBody;
                            message.IsBodyHtml = true;


                            smtp.Send(message);

                        }
                       }
                    }
                


                catch
                {
                    return RedirectToAction("Error");
                }

                if (TImage != null)
                {
                    string fileName3 = TImage.FileName;

                    string filename3 = Path.GetFileNameWithoutExtension(TImage.FileName);
                    string extension3 = Path.GetExtension(TImage.FileName);
                    filename3 = package.Id + extension3;
                    package.thumbnail = "/images/Thumbail/Package/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/Images/Thumbail/Package/"), filename3);
                    TImage.SaveAs(filename3);
                }
                db.packages.Attach(package);
                db.Entry(package).Property(x => x.thumbnail).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(package);
        }




        // GET: Packages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {

                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Package package = db.packages.Find(id);
                    if (package == null)
                    {
                        return HttpNotFound();
                    }
                    return View(package);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
        }


        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PackageCode,PackageName,ShortDescription,LongDescription,From,TO,BegainDate,Duration,Rate1,Rate2,Images,thumbnail,CreatedBy,CreatedDate,EditedBy,EditedDate")] Package package, HttpPostedFileBase TImage, ICollection<HttpPostedFileBase> AImage)
        {
            if (ModelState.IsValid)
            {
                var model = db.packages.Include(t => t.PImage).SingleOrDefault(x => x.Id == package.Id);
                string thumbail = db.packages.Include(t => t.PImage).Where(x => x.Id == package.Id).Select(x => x.thumbnail).SingleOrDefault();
                try
                {

                    if (AImage.Any(m => m.ContentLength > 0))
                    {
                        foreach (var images in model.PImage)
                        {
                            string imagename = images.ImageName.ToString();
                            var fileName = Path.Combine(Server.MapPath("/uploads/PImage/"), Path.GetFileName(imagename));

                            System.IO.File.Delete(fileName);

                            db.Database.ExecuteSqlCommand("delete from PackageImage where ImageName='" + imagename + "'");

                        }
                    }
                }
                catch
                {
                    goto Found;
                }
                Found:
                {
                    List<PImage> fileDetails = new List<PImage>();
                    foreach (var file in AImage)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.Combine(Server.MapPath("/uploads/PImage/"), Guid.NewGuid() + Path.GetExtension(file.FileName));
                            PImage image = new PImage()
                            {
                                ImageName = "/Uploads/PImage/" + Path.GetFileName(fileName),
                                Path = Path.GetExtension(fileName),
                                PackageId = package.Id,

                            };
                            fileDetails.Add(image);
                            file.SaveAs(fileName);
                            db.Entry(image).State = EntityState.Added;
                        }
                    }
                    db.SaveChanges();

                    if (TImage != null)
                    {
                        string fileName3 = TImage.FileName;

                        string filename3 = Path.GetFileNameWithoutExtension(TImage.FileName);
                        string extension3 = Path.GetExtension(TImage.FileName);
                        filename3 = package.Id + extension3;
                        package.thumbnail = "/images/Thumbail/Package/" + filename3;
                        filename3 = Path.Combine(Server.MapPath("~/Images/Thumbail/Package/"), filename3);
                        TImage.SaveAs(filename3);
                    }
                    else
                    {
                        package.thumbnail = thumbail;
                    }
                    // package.Image = fileDetails;
                    var objpackage = db.packages.SingleOrDefault(m => m.Id == package.Id);
                    objpackage.PackageCode = package.PackageCode;
                    objpackage.ShortDescription = package.ShortDescription;
                    objpackage.LongDescription = package.LongDescription;
                    objpackage.PackageName = package.PackageName;
                    objpackage.Rate1 = package.Rate1;
                    objpackage.Rate2 = package.Rate2;
                    objpackage.TO = package.TO;
                    objpackage.From = package.From;
                    objpackage.thumbnail = package.thumbnail;
                    objpackage.EditedBy = Session["userEmail"].ToString();
                    objpackage.EditedDate = DateTime.Now;
                    //objpackage.Image = fileDetails;//
                    // db.Entry(package).State = EntityState.Modified;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(package);
        }

        // GET: Packages/Delete/5
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
                    Package package = db.packages.Find(id);
                    if (package == null)
                    {
                        return HttpNotFound();
                    }
                    return View(package);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
        }


        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.packages.Find(id);
            var model = db.packages.Include(t => t.PImage).SingleOrDefault(x => x.Id == package.Id);
            foreach (var images in model.PImage)
            {
                string imagename = images.ImageName.ToString();
                var fileName = Path.Combine(Server.MapPath("/uploads/PImage/"), Path.GetFileName(imagename));

                System.IO.File.Delete(fileName);
                //Directory.Delete(fileName);
                //  db.Database.ExecuteSqlCommand("delete from Images where ImageName='" + imagename + "'");

            }
            try
            {
                var fileName1 = Path.Combine(Server.MapPath("~/Images/Thumbail/Package/"), Path.GetFileName(package.thumbnail));
                System.IO.File.Delete(fileName1);
                db.packages.Remove(package);

                db.SaveChanges();
            }
            catch
            {
                db.packages.Remove(package);

                db.SaveChanges();

            }
            return RedirectToAction("Index");

        }
        public Size NewImageSize(int OriginalHeight, int OriginalWidth, double thumbSize)
        {
            Size NewSize;
            double tempval;
            if (OriginalHeight < thumbSize && OriginalWidth > thumbSize)
            {
                if (OriginalHeight > OriginalWidth)
                    tempval = thumbSize / Convert.ToDouble(OriginalHeight);
                else
                    tempval = thumbSize / Convert.ToDouble(OriginalWidth);

                NewSize = new Size(Convert.ToInt32(tempval * OriginalWidth),
                                 Convert.ToInt32(tempval * OriginalHeight));
            }
            else
                NewSize = new Size(OriginalWidth, OriginalHeight);
            return NewSize;
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
