using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoApplication.Models;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;
using DemoApplication.Models.DAL;
using Newspaper.Models.ViewModels;
using System.Web.Helpers;

namespace Newspaper.Controllers
{
    public class AccountController : Controller
    {
        DemoDbContext _db = new DemoDbContext();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(DemoApplication.Models.Login l, string ReturnUrl = "")
         {


            var Admin = _db.Login.FirstOrDefault(a => (a.Email ==l.Email));
            
            if (Admin != null)
            {
                string pass = Crypto.Hash(l.Password);
               if(string.Compare(pass,Admin.Password)==0)
                {
                    // FormsAuthentication.SetAuthCookie(l.Email);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        var objAdmin = _db.Login.FirstOrDefault(a => (a.Email == l.Email && a.Password == l.Password));
                        if (objAdmin.Category == "Admin")
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return Redirect(ReturnUrl);
                        }
                    }
                    else
                    {
                        Session.Add("id", Admin.Id);
                        Session.Add("userEmail", Admin.Email);
                        Session.Add("category", Admin.Category);
                        var objAdmin = _db.Login.FirstOrDefault(a => (a.Email == l.Email && a.Password == pass));
                        if (objAdmin != null&& Admin.Category!=null)
                        {
                            string username = _db.Costumers.FirstOrDefault(m => m.email1 == objAdmin.Email).Name;
                            string phone = _db.Costumers.FirstOrDefault(m => m.email1 == objAdmin.Email).Phone1;
                            Session.Add("name", username);
                            Session.Add("phone", phone);
                        }
                        if (objAdmin.Category == "Admin")
                        {
                         
                            var catecory = _db.Costumers.Where(t => t.email1 == Admin.Email)
                                .Select(t => t.CustomerType).FirstOrDefault();
                            Session.Add("ACategory", catecory);
                            if (catecory == "Trading")
                            {
                                string email = Session["userEmail"].ToString();
                                var cartitem = _db.addToCarts.Where(t => t.SessonId == email).ToList();
                              if(cartitem.Count>0)
                                {
                                    _db.Database.ExecuteSqlCommand("Delete From cart where SessonId ='" + email + "'");
                                }
                                ViewBag.message = "Trading";
                                return RedirectToAction("Index", "TradingGoods");
                            }
                            if (catecory == "Food and Beverage")
                            {
                                ViewBag.message = "Food";
                                return RedirectToAction("Index", "Food");
                            }
                            if (catecory == "Tour and Travel")
                            {
                                ViewBag.message = "Tour";
                                return RedirectToAction("Index", "Packages");
                            }
                            if (catecory == "Hotel")
                            {
                                ViewBag.message = "Hotel";
                                return RedirectToAction("Index", "Hotel");
                            }
                          
                        }
                        else
                        {
                            return RedirectToAction("Index", "CustomerSuperAdmins");
                        }

                      //  return RedirectToAction("Index", "CustomerSuperAdmins");
                    }
                }

                else if (l.Password == Admin.randompass)
                {
                    TempData["message"] = Admin.Email;


                    return RedirectToAction("NewPassword");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User");
                }
            }
            return View();

        }

        public ActionResult NewPassword()
        {
            
                return PartialView();
            
        }
        [HttpPost]
        public ActionResult NewPassword(PasswordConform pass)
        {
            if (ModelState.IsValid)
            {
               string message = TempData["message"].ToString();
                var query = (from q in _db.Login
                             where q.Email ==message
                             select q).First();
                string password =pass.Password;
                query.Password = Crypto.Hash(password);
                query.randompass = null;
                _db.SaveChanges();
                return RedirectToAction("Login");



            }
            return PartialView();
        }
        public ActionResult errorpage()
        {
            return PartialView();
        }
        public ActionResult Logout()
        {
           
            var Sesson= Session["userEmail"].ToString();
           string category=_db.Login.Where(t => t.Email == Sesson).Select(t=>t.Category).FirstOrDefault();
            if (category != null)
            {
                _db.Database.ExecuteSqlCommand("Delete From cart where SessonId ='" + Sesson + "'");
                Session.Abandon();
                return RedirectToAction("Login");
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
        }
        public ActionResult ActivityLog()
        {
            var count = _db.ActivityLogs.OrderByDescending(u => u.Id);
            return View(count.ToList());

        }
    }

    //    }
    //}



    ////[NonAction]
    ////public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
    ////{
    ////    var verifyUrl = "/User/" + emailFor + "/" + activationCode;
    ////    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

    ////    var fromEmail = new MailAddress("dotnetawesome@gmail.com", "Dotnet Awesome");
    ////    var toEmail = new MailAddress(emailID);
    ////    var fromEmailPassword = "******"; // Replace with actual password

    ////    string subject = "";
    ////    string body = "";
    ////    if (emailFor == "VerifyAccount")
    ////    {
    ////        subject = "Your account is successfully created!";
    ////        body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
    ////            " successfully created. Please click on the below link to verify your account" +
    ////            " <br/><br/><a href='" + link + "'>" + link + "</a> ";
    ////    }
    ////    else if (emailFor == "ResetPassword")
    ////    {
    ////        subject = "Reset Password";
    ////        body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
    ////            "<br/><br/><a href=" + link + ">Reset Password link</a>";
    ////    }


    ////    var smtp = new SmtpClient
    ////    {
    ////        Host = "smtp.gmail.com",
    ////        Port = 587,
    ////        EnableSsl = true,
    ////        DeliveryMethod = SmtpDeliveryMethod.Network,
    ////        UseDefaultCredentials = false,
    ////        Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
    ////    };

    ////    using (var message = new MailMessage(fromEmail, toEmail)
    ////    {
    ////        Subject = subject,
    ////        Body = body,
    ////        IsBodyHtml = true
    ////    })
    ////        smtp.Send(message);
    ////}
}


