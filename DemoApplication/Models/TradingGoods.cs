using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class TradingGoods
    {
        public int Id { get; set; }

        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public string ShortDetail { get; set; }
        public string LongDetail { get; set; }
        public string Thumbail { get; set; }
        [NotMapped]
        public HttpPostedFileBase TImageFile { get; set; }
       
        public decimal WholesaleRate { get; set; }
        public int Quantity { get; set; }
        public string  CoupenCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; }
      
        public virtual ICollection<Image> Image { get; set; }


    }

}