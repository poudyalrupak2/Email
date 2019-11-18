using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "firstname is required")]
        public string Firstname { get; set; }

        public string Middlename { get; set; }
        [Required(ErrorMessage = "LastName is required")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "Dob is required")]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "email is required")]

        public string email1 { get; set; }

        public string  email2 { get; set; }

        public long phone { get; set; }
        public long phone2 { get; set; }

        public string NationalIdNo { get; set; }
        public string tole { get; set; }
        public string homeno { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public string category { get; set; }
        public string  CustomerCategory { get; set; }

    }
}