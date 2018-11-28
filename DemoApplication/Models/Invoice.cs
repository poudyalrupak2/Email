using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public String InvoiceNo { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Decimal Vat { get; set; }
        public Decimal Subtotal { get; set; }
        public Decimal Other { get; set; }
        public Decimal GrandTotal { get; set; }
    }
}