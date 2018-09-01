using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "firstname is required")]
        public string Firstname { get; set; }

        public string Middlename { get; set; }
        [Required(ErrorMessage = "LastName is required")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "Dob is required")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "email is required")]

        public string email1 { get; set; }

        public string  email2 { get; set; }
        [Required(ErrorMessage = "Phone is required")]

        public long phone { get; set; }
        public long phone2 { get; set; }
        [Required(ErrorMessage = "NationalId is required")]

        public string NationalIdNo { get; set; }
        public string tole { get; set; }
        public string homeno { get; set; }
        [Required(ErrorMessage = "State is required")]

        public string State { get; set; }
        [Required(ErrorMessage = "counrty is required")]

        public string Country { get; set; }
        [Required(ErrorMessage = "ZipCode is required")]

        public string ZipCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }


    }
}