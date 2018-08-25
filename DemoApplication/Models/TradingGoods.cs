using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class TradingGoods
    {
        public int Id { get; set; }
        public int GoodId { get; set; }
        public string Name { get; set; }
        public string ShortDetail { get; set; }
        public string LongDetail { get; set; }
        public string Thumbail { get; set; }
        public string Images { get; set; }
        public decimal WholesaleRate { get; set; }
        public int Quantity { get; set; }
        public string  CoupenCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedDate { get; set; }

        
    }

}