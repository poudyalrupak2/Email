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
        public ActionResult Index()
        {
            return View(db.TradingGoods.Include(t=>t.Image).ToList());
        }

        // GET: TradingGoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TradingGoods tradingGoods = db.TradingGoods.Find(id);
            if (tradingGoods == null)
            {
                return HttpNotFound();
            }
            return View(tradingGoods);
        }

        // GET: TradingGoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TradingGoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TradingGoods tradingGoods,HttpPostedFileBase TImage,ICollection<HttpPostedFileBase> AImage)//[Bind(Include = "Id,GoodId,Name,ShortDetail,LongDetail,Thumbail,WholesaleRate,Quantity,CoupenCode,CreatedDate,CreatedBy,EditedBy,EditedDate,Imag )
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
                            ImageName = fileName,
                            Path = Path.GetExtension(fileName),
                            ImageId = Guid.NewGuid()
                            };
                            fileDetails.Add(image);
                        file.SaveAs(Path.Combine(Server.MapPath("/uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName)));

                    }
                    }
                tradingGoods.Image = fileDetails;




                if (TImage != null)
                {
                    string fileName3 = TImage.FileName;

                    string filename3 = Path.GetFileNameWithoutExtension(TImage.FileName);
                    string extension3 = Path.GetExtension(TImage.FileName);
                    filename3 = tradingGoods.GoodId + extension3;
                    tradingGoods.Thumbail = "/images/Thumbail/" + filename3;
                    filename3 = Path.Combine(Server.MapPath("~/Images/Thumbail/"), filename3);
                   TImage.SaveAs(filename3);
                }

                db.TradingGoods.Add(tradingGoods);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                  
            }

            return View(tradingGoods);
        }

        // GET: TradingGoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TradingGoods tradingGoods = db.TradingGoods.Include(s => s.Image).SingleOrDefault(x => x.Id == id);
            if (tradingGoods == null)
            {
                return HttpNotFound();
            }
            return View(tradingGoods);
        }

        // POST: TradingGoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( TradingGoods tradingGoods, HttpPostedFileBase TImage, ICollection<HttpPostedFileBase> AImage)
        {
            if (ModelState.IsValid)
            {

                var model = db.TradingGoods.Include(t => t.Image).SingleOrDefault(x => x.Id == tradingGoods.Id);

                try
                {

                    if (AImage.Any(m => m.ContentLength > 0))
                    {
                        foreach (var images in model.Image)
                        {
                            string imagename = images.ImageName.ToString();
                            System.IO.File.Delete(imagename);

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
                            var fileName = Path.Combine(Server.MapPath("/uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName));
                            Image image = new Image()
                            {
                                ImageName = fileName,
                                Path = Path.GetExtension(fileName),
                                TradingGoodsId = tradingGoods.Id
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
                        filename3 = tradingGoods.GoodId + extension3;
                        tradingGoods.Thumbail = "/images/Thumbail/" + filename3;
                        filename3 = Path.Combine(Server.MapPath("~/Images/Thumbail/"), filename3);
                        TImage.SaveAs(filename3);
                    }

                    // tradingGoods.Image = fileDetails;
                    var objtradingGoods = db.TradingGoods.SingleOrDefault(m => m.Id == tradingGoods.Id);
                    objtradingGoods.GoodId = tradingGoods.GoodId;
                    objtradingGoods.CoupenCode = tradingGoods.CoupenCode;
                    objtradingGoods.GoodName = tradingGoods.GoodName;
                    objtradingGoods.LongDetail = tradingGoods.LongDetail;
                    objtradingGoods.Quantity = tradingGoods.Quantity;
                    objtradingGoods.ShortDetail = tradingGoods.ShortDetail;
                    objtradingGoods.Thumbail = tradingGoods.Thumbail;
                    objtradingGoods.WholesaleRate = tradingGoods.WholesaleRate;

                    //objtradingGoods.Image = fileDetails;//
                    // db.Entry(tradingGoods).State = EntityState.Modified;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(tradingGoods);
        }

        // GET: TradingGoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TradingGoods tradingGoods = db.TradingGoods.Find(id);
            if (tradingGoods == null)
            {
                return HttpNotFound();
            }
            return View(tradingGoods);
        }

        // POST: TradingGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TradingGoods tradingGoods = db.TradingGoods.Find(id);
            db.TradingGoods.Remove(tradingGoods);
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
