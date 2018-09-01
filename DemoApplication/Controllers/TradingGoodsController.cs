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

    public class TradingGoodsController : Controller
    {
        private DemoDbContext db = new DemoDbContext();

        // GET: TradingGoods
        [OutputCache(Duration = 60, VaryByParam = "Id")]

        public ActionResult Index()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading")
                {
                    return View(db.TradingGoods.Include(t => t.Image).ToList());
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        // GET: TradingGoods/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    TradingGoods TradingGoods = db.TradingGoods.Find(id);
                    if (TradingGoods == null)
                    {
                        return HttpNotFound();
                    }
                    return View(TradingGoods);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        // GET: TradingGoods/Create
        public ActionResult Create()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    return View();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        // POST: TradingGoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TradingGoods TradingGoods,HttpPostedFileBase TImage,ICollection<HttpPostedFileBase> AImage)//[Bind(Include = "Id,GoodId,Name,ShortDetail,LongDetail,Thumbail,WholesaleRate,Quantity,CoupenCode,CreatedDate,CreatedBy,EditedBy,EditedDate,Imag )
        {
            if (ModelState.IsValid)
            {
               
                    List<Image> fileDetails = new List<Image>();
                    foreach (var file in AImage)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.Combine(Server.MapPath("/uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName));

                       Image image = new Image()
                        {
                            ImageName = "/Uploads/Trading"+Path.GetFileName(fileName),
                            Path = Path.GetExtension(fileName),
                           
                            };
                            fileDetails.Add(image);
                        file.SaveAs(fileName);

                    }
                    }
                TradingGoods.Image = fileDetails;




                if (TImage != null)
                {
                    string fileName3 = TImage.FileName;

                    string filename3 = Path.GetFileNameWithoutExtension(TImage.FileName);
                    string extension3 = Path.GetExtension(TImage.FileName);
                    filename3 = TradingGoods.Id + extension3;
                    TradingGoods.Thumbail = "/images/Thumbail/Trading/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/Images/Thumbail/Trading/"), filename3);
                   TImage.SaveAs(filename3);
                }
                TradingGoods.CreatedBy = Session["userEmail"].ToString();
                TradingGoods.CreatedDate = DateTime.Now;
                db.TradingGoods.Add(TradingGoods);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                  
            }

            return View(TradingGoods);
        }

        // GET: TradingGoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    TradingGoods TradingGoods = db.TradingGoods.Include(s => s.Image).SingleOrDefault(x => x.Id == id);
                    if (TradingGoods == null)
                    {
                        return HttpNotFound();
                    }
                    return View(TradingGoods);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        // POST: TradingGoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( TradingGoods TradingGoods, HttpPostedFileBase TImage, ICollection<HttpPostedFileBase> AImage)
        {
            if (ModelState.IsValid)
            {

                var model = db.TradingGoods.Include(t => t.Image).SingleOrDefault(x => x.Id == TradingGoods.Id);
                string thumbail = db.TradingGoods.Include(t=>t.Image).Where(x => x.Id == TradingGoods.Id).Select(x => x.Thumbail).SingleOrDefault();
                try
                {

                    if (AImage.Any(m => m.ContentLength > 0))
                    {
                        foreach (var images in model.Image)
                        {
                            string imagename = images.ImageName.ToString();
                            var fileName = Path.Combine(Server.MapPath("/uploads/Trading/"),Path.GetFileName(imagename));

                            System.IO.File.Delete(fileName);

                            db.Database.ExecuteSqlCommand("delete from Images where ImageName='" + imagename + "'");

                        }
                    }
                }
                catch
                {
                    goto Found;
                }
                Found:
                {
                    List<Image> fileDetails = new List<Image>();
                    foreach (var file in AImage)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.Combine(Server.MapPath("/uploads/Trading/"), Guid.NewGuid() + Path.GetExtension(file.FileName));
                            Image image = new Image()
                            {
                                ImageName = "/Uploads/Trading/" + Path.GetFileName(fileName),
                                Path = Path.GetExtension(fileName),
                                TradingGoodsId = TradingGoods.Id,

                            };
                            fileDetails.Add(image);
                            file.SaveAs(fileName);
                            db.Entry(image).State = EntityState.Added;
                        }
                    }

                 
                        if (TImage != null)
                        {
                            string fileName3 = TImage.FileName;

                            string filename3 = Path.GetFileNameWithoutExtension(TImage.FileName);
                            string extension3 = Path.GetExtension(TImage.FileName);
                            filename3 = TradingGoods.Id + extension3;
                            TradingGoods.Thumbail = "/images/Thumbail/Trading/" + filename3;
                            filename3 = Path.Combine(Server.MapPath("~/Images/Thumbail/Trading/"), filename3);
                            TImage.SaveAs(filename3);


                        }
                        else
                    {
                        TradingGoods.Thumbail = thumbail;
                    }
                        db.SaveChanges();
                        // TradingGoods.Image = fileDetails;
                        var objTradingGoods = db.TradingGoods.SingleOrDefault(m => m.Id == TradingGoods.Id);
                        objTradingGoods.GoodId = TradingGoods.GoodId;
                        objTradingGoods.CoupenCode = TradingGoods.CoupenCode;
                        objTradingGoods.GoodName = TradingGoods.GoodName;
                        objTradingGoods.LongDetail = TradingGoods.LongDetail;
                        objTradingGoods.Quantity = TradingGoods.Quantity;
                        objTradingGoods.ShortDetail = TradingGoods.ShortDetail;
                        objTradingGoods.Thumbail = TradingGoods.Thumbail;
                        objTradingGoods.WholesaleRate = TradingGoods.WholesaleRate;
                        objTradingGoods.EditedBy = Session["userEmail"].ToString();
                        objTradingGoods.EditedDate = DateTime.Now;
                        //objTradingGoods.Image = fileDetails;//
                        // db.Entry(TradingGoods).State = EntityState.Modified;
                        db.Entry(objTradingGoods).Property(x => x.Thumbail).IsModified = true;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                  
                    }

            }
            return View(TradingGoods);
        }

        // GET: TradingGoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    TradingGoods TradingGoods = db.TradingGoods.Find(id);
                    if (TradingGoods == null)
                    {
                        return HttpNotFound();
                    }
                    return View(TradingGoods);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        // POST: TradingGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TradingGoods TradingGoods = db.TradingGoods.Find(id);
            var model = db.TradingGoods.Include(t => t.Image).SingleOrDefault(x => x.Id == TradingGoods.Id);
            foreach (var images in model.Image)
            {
                string imagename = images.ImageName.ToString();
                var fileName = Path.Combine(Server.MapPath("/uploads/Trading"), Path.GetFileName(imagename));

                System.IO.File.Delete(fileName);

              //  db.Database.ExecuteSqlCommand("delete from Images where ImageName='" + imagename + "'");

            }
            try
            {
                var fileName1 = Path.Combine(Server.MapPath("~/Images/Thumbail/Trading/"), Path.GetFileName(TradingGoods.Thumbail));
                System.IO.File.Delete(fileName1);
                db.TradingGoods.Remove(TradingGoods);

                db.SaveChanges();
            }
            catch
            {
                db.TradingGoods.Remove(TradingGoods);

                db.SaveChanges();

            }
            return RedirectToAction("Index");

        }
        public ActionResult ImageViewer(int? id)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var image = db.Images.Where(s => s.TradingGoodsId == id).ToList();
                    if (image == null)
                    {
                        return HttpNotFound();
                    }
                    return View(image);
                }
            }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
        }
        public ActionResult ActivityLog()
        {
            var count = db.ActivityLogs.OrderByDescending(u => u.Id);
            return View(count.ToList());

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

