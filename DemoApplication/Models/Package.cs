using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string From { get; set; }
        public string TO { get; set; }
        public string BegainDate { get; set; }
        public string Duration { get; set; }
        public decimal Rate1 { get; set; }
        public decimal Rate2 { get; set; }
        public string thumbnail { get; set; }
        [NotMapped]
        public HttpPostedFileBase TImageFile { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public virtual ICollection<PImage> PImage { get; set; }

    }
}