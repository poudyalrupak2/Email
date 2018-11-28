using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class BuyersList
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public TradingGoods  TradingGoods { get; set; }
        public int MyProperty { get; set; }
    }
}