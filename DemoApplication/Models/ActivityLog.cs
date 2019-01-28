using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Newspaper.Models
{
    
    public class ActivityLog
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public string CreatedBy { get; set; }
        public  string category { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}