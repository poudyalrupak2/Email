using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApplication.Models.ViewModels
{
    public class ShoppingCartDetails
    {
        public List<AddToCart> AddToCarts { get; set; }
        public List<Customer> Customers { get; set; }
    }
}