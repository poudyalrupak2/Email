using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    [OutputCache(Duration = 60, VaryByParam = "Id")]

    public class PackagesController : Controller
    {
        private DemoDbContext db = new DemoDbContext();

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

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Package package, HttpPostedFileBase TImage, ICollection<HttpPostedFileBase> AImage)
        {
            if (ModelState.IsValid)
            {
                
                        List<PImage> fileDetails = new List<PImage>();
                foreach (var file in AImage)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.Combine(Server.MapPath("/uploads/PIMage/"), Guid.NewGuid() + Path.GetExtension(file.FileName));

                        PImage Pimage = new PImage()
                        {
                            ImageName = "/Uploads/PImage/" + Path.GetFileName(fileName),
                            Path = Path.GetExtension(fileName),

                        };
                        fileDetails.Add(Pimage);
                        file.SaveAs(fileName);

                    }
                }
                package.PImage = fileDetails;




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
                package.CreatedBy = Session["userEmail"].ToString();
                package.CreatedDate = DateTime.Now;
                db.packages.Add(package);
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
            Package package= db.packages.Find(id);
            var model = db.packages.Include(t => t.PImage).SingleOrDefault(x => x.Id == package.Id);
            foreach (var images in model.PImage)
            {
                string imagename = images.ImageName.ToString();
                var fileName = Path.Combine(Server.MapPath("/uploads/PImage/"), Path.GetFileName(imagename));

                System.IO.File.Delete(fileName);

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
