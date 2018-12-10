using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class TicketsController : Controller
    {
        private DemoDbContext db = new DemoDbContext();

        // GET: Tickets

        public ActionResult Index()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    return View(db.Tickets.ToList());
                }
            }
            return RedirectToAction("error");
        }

        // GET: Tickets/Details/5
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
                    Tickets tickets = db.Tickets.Find(id);
                    if (tickets == null)
                    {
                        return HttpNotFound();
                    }
                    return View(tickets);
                }
            }
            return RedirectToAction("error");
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    return View();
                }
            }
            return RedirectToAction("error");
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Tickets tickets)
        {
            if (Session["ACategory"] != null)
            {
                if (Session["ACategory"].ToString() == "Tour and Travel")
                {
                    if (ModelState.IsValid)
                    {
                        tickets.CreatedBy = Session["userEmail"].ToString();
                        tickets.CreatedDate = DateTime.Now.Date;
                        
                        string body;

                        using (var sr = new StreamReader(Server.MapPath("\\App_Data\\HtmlTamplate\\template.html")))
                        {
                            body = sr.ReadToEnd();
                        }
                    
                    

                        
                        

                        try
                        {
                            using (var smtp = new SmtpClient())
                            {
                                
                                    try
                                {
                                        string username =tickets.CostumerName;
                                    string ticketdate =tickets.DepartureDate.ToString("dd MMM yyyy");
                                    string tickettime =tickets.DepartureTime.ToString();

                                    var message = new MailMessage();
                                    message.To.Add(new MailAddress(tickets.Email));
                                    string messageBody = string.Format(body, username,username,ticketdate,tickettime);
                                    string path = Server.MapPath(@"/images/dbug.gif"); // my logo is placed in images folder
                                  AlternateView avHtml = AlternateView.CreateAlternateViewFromString(messageBody, null, MediaTypeNames.Text.Html);
                                    LinkedResource logo = new LinkedResource(path,MediaTypeNames.Image.Gif);
                                    logo.ContentId = "logo";
                                    avHtml.LinkedResources.Add(logo);
                                    message.AlternateViews.Add(avHtml);
                                   
                                   // avHtml.LinkedResources.Add(logo);

                                   



                                    message.Subject = "Tickets Created";
                                    //mail.Body = messageBody;
                                    message.IsBodyHtml = true;
                                    smtp.Send(message);



                                    db.Tickets.Add(tickets);
                                    db.SaveChanges();


                                    return RedirectToAction("Index");

                                }
                                catch
                                {
                                    return RedirectToAction("Error");
                                }

                            }
                        }
                        catch
                        {
                            return RedirectToAction("error");
                        }
                    }

                    return View(tickets);
                }
            }
            return RedirectToAction("error");
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                tickets.EditedBy = Session["userEmail"].ToString();
                tickets.EditedDate = DateTime.Now.Date;
                db.Entry(tickets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tickets);
        }

        // GET: Tickets/Delete/5
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
                    Tickets tickets = db.Tickets.Find(id);
                    if (tickets == null)
                    {
                        return HttpNotFound();
                    }
                    return View(tickets);
                }
            }
            return RedirectToAction("error");
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tickets tickets = db.Tickets.Find(id);
            db.Tickets.Remove(tickets);
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
        public ActionResult Error ()
        {
            return View();
        }
    }
}
