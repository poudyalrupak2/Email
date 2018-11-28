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
using DemoApplication.Models.ViewModels;
using Newspaper.Filters;

namespace DemoApplication.Controllers
{
    [SessionCheck]
    public class TradingCompletesController : Controller
    {
        private DemoDbContext db = new DemoDbContext();

        // GET: TradingCompletes
        public ActionResult Index()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Trading" && Session["ACategory"] != null)
                {
                    var customerList = (from trading in db.Invoices
                                        select new
                                        {
                                            CustomerId = trading.CustomerId,
                                            CustomerName = trading.Customer.Firstname +
                                        " " + trading.Customer.Middlename + " " + trading.Customer.LastName
                                        }).ToList();

                    ViewBag.CustomerName = new SelectList(customerList, "CustomerId", "CustomerName", "--Select--");
                    var tradingCompletes = db.Invoices.Include(t => t.Customer);
                    return View(tradingCompletes.ToList());
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost]

        public ActionResult GetCartList(int customerId, string date)
        {
            List<Invoice> tradingCompleteVM = new List<Invoice>();

            if (customerId > 0)
            {
                tradingCompleteVM = db.Invoices.Where(m => m.CustomerId == customerId).ToList();
            }
          
           
            return Json(tradingCompleteVM);
        }
        public ActionResult GetList(string search1, string search2)
        {
           
                    var objTradingGoods = db.Invoices.Include(m => m.Customer).ToList();
            if (search1 != "")
            {
                objTradingGoods = db.Invoices.Include(m => m.Customer).Where(s => s.Customer.Firstname.Contains(search1) || s.Customer.LastName.Contains(search1) || (s.Customer.Firstname + " " + s.Customer.Middlename).Contains(search1) || (s.Customer.Firstname + " " + s.Customer.Middlename + " " + s.Customer.LastName).Contains(search1) || (s.Customer.Firstname + " " + s.Customer.LastName).Contains(search1)).ToList();

                if (search2 != "")
                {
                    objTradingGoods = objTradingGoods.Where(x => x.Customer.email1.Contains(search2)).ToList();// db.TradingCompletes.Include(m => m.Customer).Include(m => m.TradingGoods).Where(x => x.Customer.Firstname.Contains(search1) && x.Customer.email1.Contains(search2)).ToList();
                }
            }
            else
            {
                objTradingGoods = db.Invoices.Include(m => m.Customer).Where(x => x.Customer.email1.Contains(search2)).ToList();
            }
            return View(objTradingGoods);



        }
        [HttpPost]
        public ActionResult Printlist(string name, string email,string invoice)
        {
            var objTradingGoods = db.Invoices.Include(m => m.Customer).ToList();
            if (name != "")
            {
                objTradingGoods = db.Invoices.Include(m => m.Customer).Where(s => s.Customer.Firstname.Contains(name) || s.Customer.LastName.Contains(name) || (s.Customer.Firstname + " " + s.Customer.Middlename).Contains(name) || (s.Customer.Firstname + " " + s.Customer.Middlename + " " + s.Customer.LastName).Contains(name) || (s.Customer.Firstname + " " + s.Customer.LastName).Contains(name)).ToList();

                if (email != "")
                {
                    objTradingGoods = objTradingGoods.Where(x => x.Customer.email1.Contains(email)).ToList();// db.TradingCompletes.Include(m => m.Customer).Include(m => m.TradingGoods).Where(x => x.Customer.Firstname.Contains(search1) && x.Customer.email1.Contains(search2)).ToList();
                }

                if (invoice != "")
                {
                    objTradingGoods = objTradingGoods.Where(x => x.InvoiceNo.Contains(invoice)).ToList();// db.TradingCompletes.Include(m => m.Customer).Include(m => m.TradingGoods).Where(x => x.Customer.Firstname.Contains(search1) && x.Customer.email1.Contains(search2)).ToList();
                }
            }
           else if (email != "")
            {
                objTradingGoods = objTradingGoods.Where(x => x.Customer.email1.Contains(email)).ToList();// db.TradingCompletes.Include(m => m.Customer).Include(m => m.TradingGoods).Where(x => x.Customer.Firstname.Contains(search1) && x.Customer.email1.Contains(search2)).ToList();
                if (name != "")
                {
                      objTradingGoods = db.Invoices.Include(m => m.Customer).Where(s => s.Customer.Firstname.Contains(name) || s.Customer.LastName.Contains(name) || (s.Customer.Firstname + " " + s.Customer.Middlename).Contains(name) || (s.Customer.Firstname + " " + s.Customer.Middlename + " " + s.Customer.LastName).Contains(name) || (s.Customer.Firstname + " " + s.Customer.LastName).Contains(name)).ToList();

                  
                }

                if (invoice != "")
                {
                    objTradingGoods = objTradingGoods.Where(x => x.InvoiceNo.Contains(invoice)).ToList();// db.TradingCompletes.Include(m => m.Customer).Include(m => m.TradingGoods).Where(x => x.Customer.Firstname.Contains(search1) && x.Customer.email1.Contains(search2)).ToList();
                }
            }

            else
            {
                objTradingGoods = db.Invoices.Include(m => m.Customer).Where(x => x.InvoiceNo==invoice).ToList();
            }
            invoiceVM invoiceVM = new invoiceVM();
            invoiceVM.Goods = new List<Goods>();
            if (objTradingGoods.Count() == 1)
            {
                string strInvoiceNo = objTradingGoods[0].InvoiceNo;
                //var listInvoice = (from ip in db.INVProducts
                //                   from iv in db.Invoices
                //                   where ip.InvoiceId == iv.InvoiceNo && ip.InvoiceId == strInvoiceNo
                //                   select new { ip }).ToList();
                var listInvoice= (from ip in db.INVProducts
                                  join iv in db.Invoices
                                  on ip.InvoiceId equals iv.InvoiceNo
                                  where iv.InvoiceNo==strInvoiceNo
                                  select new{Quantity=ip.Quantity,
                                      Rate =ip.Rate,
                                      TradingGoodsId =ip.TradingGoodsId,
                                      subTotal =ip.Total}).ToList();
                invoiceVM.Customer = objTradingGoods[0].Customer.Firstname + " " + objTradingGoods[0].Customer.Middlename + " " + objTradingGoods[0].Customer.LastName;
                invoiceVM.GrandTotal = objTradingGoods[0].GrandTotal;
                invoiceVM.Other = objTradingGoods[0].Other;
                invoiceVM.Subtotal = objTradingGoods[0].Subtotal;
                invoiceVM.GrandTotal = objTradingGoods[0].GrandTotal;
                invoiceVM.Vat = objTradingGoods[0].Vat;
                invoiceVM.InvoiceNo = objTradingGoods[0].InvoiceNo;


                for (int i = 0; i < listInvoice.Count(); i++)
                {
                    var objTrading = db.TradingGoods.Find(listInvoice[i].TradingGoodsId);
                    Goods goods = new Goods { GoodsName = objTrading.GoodName, Quantity = listInvoice[i].Quantity, Rate = listInvoice[i].Rate,subTotal=listInvoice[i].subTotal };
                    invoiceVM.Goods.Add(goods);
                }

            }
            return View(invoiceVM);
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
