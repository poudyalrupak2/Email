using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class TradingGoods
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Goodid is required")]
        public int GoodId { get; set; }
        [Required(ErrorMessage = "GoodName is required")]
        public string GoodName { get; set; }
        [Required]
        public string ShortDetail { get; set; }
        [Required(ErrorMessage = "firstname is required")]
        [MinLength(length:50,ErrorMessage ="minimum 50 letter is required")]
        public string LongDetail { get; set; }
        
        public string Thumbail { get; set; }
        [NotMapped]
        public HttpPostedFileBase TImageFile { get; set; }
        [Required(ErrorMessage ="retail price is required ")]
       public int? RetailRate { get; set; }
        [Required(ErrorMessage = "whilesale rate is required")]

        public decimal WholesaleRate { get; set; }
        [Required(ErrorMessage = "Quantity is   required")]

        public int Quantity { get; set; }
        public string  CoupenCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
      
        public virtual ICollection<Image> Image { get; set; }


    }

}