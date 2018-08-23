using CrystalDecisions.CrystalReports.Engine;
using DemoApplication.Models.DAL;
using Newspaper.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApplication.Controllers
{
    [SessionCheck]
    public class ReportController : Controller
    {
        private DemoDbContext db = new DemoDbContext();

        // GET: Report
        public ActionResult Index()
        {
            ViewBag.Costumers = db.Costumers.ToList();
            return View();
        }
        public ActionResult ExportReport()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReport2.rpt")));

            rd.SetDataSource(db.Costumers.Select(p=> new {
                Name=p.Name,
                Email1 = p.email1
            }).ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerList.pdf");
           
        }
    }
}