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
                if (l.Password == Admin.Password)
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
                    
                        var objAdmin = _db.Login.FirstOrDefault(a => (a.Email == l.Email && a.Password == l.Password));
                        if (objAdmin.Category == "Admin")
                        {
                         
                            var catecory = _db.Costumers.Where(t => t.email1 == Admin.Email)
                                .Select(t => t.CustomerType).FirstOrDefault();
                            Session.Add("ACategory", catecory);
                            if (catecory == "Trading")
                            {
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

        //    var Admin = _db.Login.FirstOrDefault(a => (a.Email));
        //                    if (Admin != null)
        //                    {
        //                               // var pass = Crypto.Hash(l.Password);
        //                                if (string.Compare(pass, Admin.Password) == 0)
        //                                {

        //                                    FormsAuthentication.SetAuthCookie(l.UserName, l.RememberMe);
        //                                    if (Url.IsLocalUrl(ReturnUrl))
        //                                    {
        //                                        // var objAdmin = _db.tblAdmins.FirstOrDefault(a=>(a.UserName == l.UserName || a.Email == l.UserName) && a.Password == l.Password);

        //                                        return Redirect(ReturnUrl);
        //}
        //                                    else
        //                                    {
        //                                        Session.Add("id", Admin.Id);
        //                                        Session.Add("userName", Admin.UserName);
        //                                        Session.Add("userEmail", Admin.Email);
        //                                        Session.Add("FullName", Admin.FullName);
        //                                        Session.Add("Category", Admin.Category);
        //                                        return RedirectToAction("Index", "DashBoard");
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                ModelState.AddModelError("", "Invalid User");
        //                            }
        //                        }
        //                        return View();
        //                    }
        //                }
        //            }
        //                [Authorize]
        //        //        public ActionResult Logout()
        //        //        {

        //        //            Session.Abandon();
        //        //            return RedirectToAction("Login", "Account");
        //        //        }
        //        //        public ActionResult ForgetPassword()
        //        //        {
        //        //            return View();
        //        //        }
        //        //        [HttpPost]

        //        //        public ActionResult ForgetPassword(Admin admin)
        //        //        {

        //        //            if (ModelState.IsValid)
        //        //            {
        //        //                //https://www.google.com/settings/security/lesssecureapps
        //        //                //Make Access for less secure apps=true

        //        //                string from = "dbugtest2016@gmail.com";
        //        //                string fromPassword = "my@test#";
        //        //                string password = Membership.GeneratePassword(6, 1);

        //        //                if (admin.Email != null)
        //        //                {
        //                    using (MailMessage mail = new MailMessage(from, admin.Email))
        //                    {

        //                        try
        //                        {
        //                            var tb = _db.Admin.Where(u => u.Email == admin.Email).FirstOrDefault();

        //                            if (tb == null)
        //                            {
        //                                ViewBag.Message = "email Doesnot Exist Please enter valid email";
        //                            }
        //                            else
        //                            {
        //                                mail.Subject = "Password Recovery";
        //                                mail.Body = "Use this Password to login:" + password;

        //                                mail.IsBodyHtml = false;
        //                                SmtpClient smtp = new SmtpClient();
        //                                smtp.Host = "smtp.gmail.com";
        //                                smtp.EnableSsl = true;
        //                                smtp.UseDefaultCredentials = false;
        //                                NetworkCredential networkCredential = new NetworkCredential(from, fromPassword);

        //                                smtp.Credentials = networkCredential;
        //                                smtp.Port = 587;
        //                                smtp.Send(mail);
        //                                ViewBag.Message = "Your Password Is Sent to your email";
        //                                var query = (from q in _db.Admin
        //                                             where q.Email == admin.Email
        //                                             select q).First();
        //                                query.randompass = password;
        //                                _db.SaveChanges();

        //                            }
        //                        }
        //                        catch
        //                        {
        //                            return RedirectToAction("errorpage");
        //                        }
        //                        TempData["Message"] = admin.Email;
        //                        TempData.Keep();
        //                        return RedirectToAction("conformation");


        //                    }
        //                }


        //            }


        //            return View();


        //            //return RedirectToAction("Index", "Home");
        //        }
        //        public ActionResult conformation()
        //        {
        //            if (TempData["Message"] != null)
        //            {
        //                string message = TempData["Message"].ToString();
        //                TempData.Keep();
        //                if (message != null)
        //                {
        //                    return PartialView();
        //                }
        //            }
        //            return RedirectToAction("ForgetPassword");

        //        }
        //        [HttpPost]
        //        public ActionResult conformation(Admin admin)
        //        {
        //            if (ModelState.IsValid)
        //            {

        //                string message = TempData["Message"].ToString();
        //                TempData.Keep();
        //                TempData["info"] = (_db.Admin.Any(u => u.Email == message && u.randompass == admin.randompass));
        //                if (admin.randompass != null)
        //                {
        //                    if (_db.Admin.Any(u => u.Email == message && u.randompass == admin.randompass))
        //                    {
        //                        return RedirectToAction("NewPassword");
        //                    }
        //                }
        //                return PartialView();

        //            }
        //            ViewBag.message = "conformation code donot match";
        //            return PartialView();
        //        }
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
                query.Password = password;
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
            Session.Abandon();
            return RedirectToAction("Login");
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


