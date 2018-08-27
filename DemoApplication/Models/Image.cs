using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        [NotMapped]
        public IEnumerable<HttpPostedFile> Img { get; set; }
        public string ImageName { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
        public int TradingGoodsId { get; set; }
        public virtual TradingGoods tradingGoods { get; set; }
    }
}