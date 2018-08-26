using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApplication.Models
{
    [Table("Admin1")]
    public class AdminCostumers
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter NationalId number ")]
        public string NationalIdType { get; set; }
        [Required(ErrorMessage = "Please enter NationalId number ")]
        public string NationalIdNo { get; set; }

        [Required(ErrorMessage = "Please enter Date")]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter phone number")]
        [DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }
        [Required(ErrorMessage = "Please select country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please select state")]
        public string State { get; set; }
        public string TypeOfService { get; set; }
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ServiceProvidedDate { get; set; }
        public string CreatedBy { get; set; }

    }
    
}