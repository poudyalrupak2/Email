using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    [Table("Admin1")]
    public class AdminCostumers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string TypeOfService { get; set; }
        public DateTime ProvidedDate { get; set; }
        public string CreatedBy { get; set; }

    }
}