using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class AssignPackage
    {
        public int AssignPackageId { get; set; }
        public Package Package{ get; set; }
        public int PackageId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public String AssignDate { get; set; }
    }
}