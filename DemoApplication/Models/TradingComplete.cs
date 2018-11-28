using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class TradingComplete
    {
        public int Id { get; set; }
        public int  Quantity { get; set; }
        public int?  Rate { get; set; }
        public int TradingGoodsId { get; set; }
        public TradingGoods TradingGoods { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public String CreatedBy{ get; set; }
        public String CreatedDate { get; set; }
    }
}   