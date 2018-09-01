using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    [Table("PackageImage")]
    public class PImage
    {
        public int PImageId { get; set; }
        [NotMapped]
        public IEnumerable<HttpPostedFile> Img { get; set; }
        public string ImageName { get; set; }     
        public long Size { get; set; }
        public string Path { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
    }
}