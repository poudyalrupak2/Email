using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class INVProduct
    {
        [Key]
        public int Id { get; set; }
        public Invoice Invoice { get; set; }
      
        public string InvoiceId { get; set; }
        public TradingGoods TradingGoods { get; set; }
        public int TradingGoodsId { get; set; }
        public decimal Rate { get; set; }
        public int   Quantity { get; set; }
        public Decimal Total { get; set; }
    }
}