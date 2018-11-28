using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    [Table("cart")]
    public class AddToCart
    {
        [Key]
        public int CartId{ get; set; }
        [Required]
        public string GoodName { get; set; } 
        [Required]
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public int ProductId { get; set; }
        public decimal  VatAmount { get; set; }
        public decimal vat { get; set; }
        public string SessonId { get; set; }

    }
}