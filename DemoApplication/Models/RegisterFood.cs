using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class RegisterFood
    {
        public int Id { get; set; }
        public string Beverage { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string ShortDetail { get; set; }
        public string ingredient { get; set; }
        public string  Thumbnail { get; set; }
        public int Rate1 { get; set; }
        public int Rate2 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBY { get; set; }
        public DateTime? EditedDate { get; set; }



    }
}