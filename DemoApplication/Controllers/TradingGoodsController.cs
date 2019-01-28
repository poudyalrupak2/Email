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
using DemoApplication.Models.ViewModels;
using Newspaper.Filters;
using Newspaper.Models;
using Newspaper.Models.ViewModels;

namespace DemoApplication.Controllers
{
    [SessionCheck]

    public class TradingGoodsController : Controller
    {
        private DemoDbContext db = new DemoDbContext();

        // GET: TradingGoods

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCart(ShoppingCartDetails cartDetails,FormCollection col)//[Bind(Include = "Id,GoodId,Name,ShortDetail,LongDetail,Thumbail,WholesaleRate,Quantity,CoupenCode,CreatedDate,CreatedBy,EditedBy,EditedDate,Imag )
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {


                            if (Convert.ToInt32(col["customerId"]) != 0)
                            {
                                int customerId = Convert.ToInt32(col["customerId"]);
                                decimal Vat = Convert.ToDecimal(col["vatTotal"]);
                                decimal Subtotal = Convert.ToDecimal(col["Total"]);
                                decimal GrandTotal = Convert.ToDecimal(col["GrandTotal"]);
                                decimal other = Convert.ToDecimal(col["Other"]);

                                var objInvoiceGoods = new Invoice();
                                var objInvoice = new INVProduct();
                                objInvoiceGoods.CustomerId = customerId;
                                objInvoiceGoods.Vat = Vat;
                                objInvoiceGoods.Subtotal = Subtotal;
                                objInvoiceGoods.GrandTotal = GrandTotal;
                                objInvoiceGoods.Other = other;
                                InvoiceGenerate invoice = db.InvoiceGenerates.FirstOrDefault();
                                if (invoice == null)
                                {
                                    db.InvoiceGenerates.Add(new InvoiceGenerate { InvoiceId = 1 });
                                    db.SaveChanges();
                                    invoice = db.InvoiceGenerates.FirstOrDefault();
                                }
                                else
                                {
                                    invoice.InvoiceId = invoice.InvoiceId + 1;
                                    db.SaveChanges();
                                }
                                string invoicet = invoice.InvoiceId.ToString();
                                objInvoiceGoods.InvoiceNo = invoice.InvoiceId.ToString();
                                db.Invoices.Add(objInvoiceGoods);
                                db.SaveChanges();
                                try
                                {
                                    foreach (var item in cartDetails.AddToCarts)
                                    {

                                        objInvoice.InvoiceId = invoicet;
                                        objInvoice.Rate = item.price;
                                        objInvoice.Quantity = item.Quantity;
                                        objInvoice.TradingGoodsId = item.ProductId;
                                        objInvoice.Total = item.Subtotal;
                                        var goods = db.TradingGoods.Where(t => t.Id == item.ProductId).FirstOrDefault();
                                        goods.Quantity -= item.Quantity;
                                        db.INVProducts.Add(objInvoice);
                                        db.SaveChanges();
                                    }
                                    var Sesson = Session["userEmail"].ToString();

                                    db.Database.ExecuteSqlCommand("Delete From cart where SessonId ='" + Sesson + "'");
                                    return RedirectToAction("Index", "TradingCompletes");
                                }
                                catch
                                {

                                    return RedirectToAction("ViewCart");
                                }

                            }
                            TempData["message"] = "Please select costumer";
                            return RedirectToAction("ViewCart");
                        }
                        catch
                        {
                            return RedirectToAction("ViewCart");
                        }
                        }

                    return RedirectToAction("ViewCart");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost]
        
        public ActionResult DeleteCart(int CartId)
        {
            try
            {
                AddToCart cout = db.addToCarts.Find(CartId);
                db.addToCarts.Remove(cout);
                db.SaveChanges();
                return RedirectToAction("ViewCart");

            }
            catch
            {
                return RedirectToAction("ViewCart");
            }
            }


        // POST: TradingGoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TradingGoods TradingGoods,HttpPostedFileBase TImage,ICollection<HttpPostedFileBase> AImage)//[Bind(Include = "Id,GoodId,Name,ShortDetail,LongDetail,Thumbail,WholesaleRate,Quantity,CoupenCode,CreatedDate,CreatedBy,EditedBy,EditedDate,Imag )
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    if (ModelState.IsValid)
                    {

                        List<Image> fileDetails = new List<Image>();
                        foreach (var file in AImage)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                string firstpath = "/uploads/";
                                string subPath = "/uploads/Trading/"; // your code goes here
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
                                var fileName = Path.Combine(Server.MapPath("/uploads/Trading"), Guid.NewGuid() + Path.GetExtension(file.FileName));

                                Image image = new Image()
                                {

                                    ImageName = "/uploads/Trading" + Path.GetFileName(fileName),
                                    Path = Path.GetExtension(fileName),

                                };
                                fileDetails.Add(image);
                                file.SaveAs(fileName);

                            }
                        }
                        TradingGoods.Image = fileDetails;

                        db.TradingGoods.Add(TradingGoods);
                        db.SaveChanges();
                        db.Entry(TradingGoods).GetDatabaseValues();


                        if (TImage != null)
                        {
                            string firstpath = "/Images/";
                            string subPath = "/Images/Thumbail/"; // your code goes here
                            string TPAth = "/Images/Thumbail/Trading/";
                            bool imageexist = System.IO.Directory.Exists(Server.MapPath(firstpath));
                            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                            bool Texists = System.IO.Directory.Exists(Server.MapPath(TPAth));
                            if (!imageexist)
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath(firstpath));
                            }
                            if (!exists)
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                            }
                            if(!Texists)
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath(TPAth));
                            }
                            string fileName3 = TImage.FileName;

                            string filename3 = Path.GetFileNameWithoutExtension(TImage.FileName);
                            string extension3 = Path.GetExtension(TImage.FileName);
                            filename3 = TradingGoods.Id + extension3;
                            TradingGoods.Thumbail = "/Images/Thumbail/Trading/" + filename3;
                            filename3 = Path.Combine(Server.MapPath("~/Images/Thumbail/Trading/"), filename3);
                            TImage.SaveAs(filename3);
                        }

                        TradingGoods.CreatedBy = Session["userEmail"].ToString();
                        TradingGoods.CreatedDate = DateTime.Now;
                        try
                        {
                            db.TradingGoods.Attach(TradingGoods);
                            db.Entry(TradingGoods).Property(x => x.Thumbail).IsModified = true;
                            db.SaveChanges();

                            String Operation = "Goods created successfully";
                            db.ActivityLogs.Add(new ActivityLog
                            {
                                Operation = Operation,
                                CreatedBy = Session["userEmail"].ToString(),
                                CreatedDate = DateTime.Now

                            });
                            return RedirectToAction("Index");
                        }
                        catch
                        {
                            return View(TradingGoods);
                        }

                    }
                    var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);

                    return View(TradingGoods);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
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
                    try { db.SaveChanges();
                        String Operation = "Goods Edited Sucessfully";
                        db.ActivityLogs.Add(new ActivityLog
                        {
                            Operation = Operation,
                            CreatedBy = Session["userEmail"].ToString(),
                            CreatedDate = DateTime.Now

                        });
                        // TradingGoods.Image = fileDetails;
                        var objTradingGoods = db.TradingGoods.SingleOrDefault(m => m.Id == TradingGoods.Id);
                        objTradingGoods.GoodId = TradingGoods.GoodId;
                        objTradingGoods.Vat = TradingGoods.Vat;
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
                    catch
                    {
                        return View(TradingGoods);
                    }
                    }

            }
            return View(TradingGoods);
        }
        public ActionResult AddGoods()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    ViewBag.Id = new SelectList(db.TradingGoods, "Id", "GoodName");
                    return View();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost]
        public ActionResult AddGoods([Bind(Include ="Id,Quantity")] TradingGoods tradingGoods)
        {
            var goods = db.TradingGoods.Where(t => t.Id == tradingGoods.Id).FirstOrDefault();
            goods.Quantity += tradingGoods.Quantity;
           // db.Entry(tradingGoods).Property(x => x.Quantity).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("index");

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
                string Operation = "Goods deleted successfully";
                db.ActivityLogs.Add(new ActivityLog
                {
                    Operation = Operation,
                    CreatedBy = Session["userEmail"].ToString(),
                    CreatedDate = DateTime.Now

                });

                db.SaveChanges();
            }
            catch
            {
                db.TradingGoods.Remove(TradingGoods);
                string Operation = "Goods deleted successfully";
                db.ActivityLogs.Add(new ActivityLog
                {
                    Operation = Operation,
                    CreatedBy = Session["userEmail"].ToString(),
                    CreatedDate = DateTime.Now

                });
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
        public ActionResult AddToCart(int ?id)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    var good = db.TradingGoods.Find(id);

                    AddToCart add = new AddToCart();
                    var sesssonid = Session["userEmail"].ToString();

                    var cartitem = db.addToCarts.FirstOrDefault(t => t.ProductId == id && t.SessonId == sesssonid);

                    if (cartitem == null)
                    {
                        if (good.Quantity > 0)
                        {
                            add.GoodName = good.GoodName;
                            add.price = good.RetailRate;
                            add.Quantity = good.Quantity;
                            add.Subtotal = Convert.ToDecimal(good.Quantity * good.RetailRate);
                            add.ProductId = good.Id;
                            add.SessonId = Session["userEmail"].ToString();
                            if (good.Vat == true)
                            {
                                var vat = db.vat.Where(t => t.Id == 1).FirstOrDefault();
                                add.VatAmount = vat.VatPercent;
                                add.vat = vat.VatPercent * add.Subtotal / 100;
                            }
                            else
                            {
                                add.VatAmount = 0;
                                add.vat = 0;
                            }
                            db.addToCarts.Add(add);
                            db.SaveChanges();
                            TempData["Message"] = "Added to cart Successfully";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["Message"] = " Please update quantity and try again";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Already exist in cart";
                        return RedirectToAction("Index");

                    }
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            
        }
      
        public ActionResult EditVats (int id=1)
        {
            Vat TradingGoods = db.vat.Find(id);
            return View(TradingGoods);
        }
        [HttpPost]
        public ActionResult EditVats(Vat vat)
        {
            if (ModelState.IsValid)
            {
            
                db.Entry(vat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            return View(vat);
        }
        public ActionResult ViewCart()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    ShoppingCartDetails mymodel = new ShoppingCartDetails();
                    string Sesson = Session["userEmail"].ToString();
                    mymodel.AddToCarts = db.addToCarts.Where(t => t.SessonId == Sesson).ToList();
                    mymodel.Customers = db.customers.ToList();

                    return View(mymodel);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        public ActionResult UserProfile()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    var customerEmail = Session["UserEmail"].ToString();
                    CustomerSuperAdmin CustomerSuperAdmin = db.Costumers.Where(m => m.email1 == customerEmail).FirstOrDefault();

                    return View(CustomerSuperAdmin);

                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }


            public ActionResult Dashboard() {
            var Customer = db.customers.Where(t => t.CustomerCategory == "Trading");
             ViewBag.good = db.TradingGoods.Count();
            var sesson = Session["userEmail"].ToString();
             ViewBag.cartitem = db.addToCarts.Where(t => t.SessonId ==sesson).Count();
            ViewBag.Message = db.Costumers.Count();
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

