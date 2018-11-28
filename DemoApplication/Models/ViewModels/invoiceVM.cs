using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApplication.Models.ViewModels
{
    public class invoiceVM
    {
        public String InvoiceNo { get; set; }
        public string Customer { get; set; }
        public Decimal Vat { get; set; }
        public Decimal Subtotal { get; set; }
        public Decimal Other { get; set; }
        public Decimal GrandTotal { get; set; }
        public string GoodName { get; set; }

        public List<Goods> Goods { get; set; }

    }

    public class Goods {
        public string GoodsName { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal subTotal { get; set; }
    }
}