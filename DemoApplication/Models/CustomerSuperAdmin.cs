using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApplication.Models
{
    [Table("Admin")]
    public class CustomerSuperAdmin
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter CustumerId")]

        public String CustumerId { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]

        public String Name { get; set; }
        [Required(ErrorMessage = "Please Enter Date")]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name="Register date or Date of Birth")]
        public DateTime RegisterDate { get; set; }
        [Required(ErrorMessage = "Please select Category")]
        public string Categoty { get; set; }
        [Required(ErrorMessage = "Please enter email")]

     
        public string email1 { get; set; }
        public string email2 { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        public string Phone1 { get; set; }
        public long? PanNo { get; set; }
        public string URL { get; set; }
        [Required(ErrorMessage ="Please select costumer type")]
        public string CustomerType { get; set; }
        public string Detail { get; set; }
        public string WorkingArea { get; set; }
        public string NatureOfWork { get; set; }
        public string CitizenShipPhoto { get; set; }
        public string CompanyDocument { get; set; }
        public string Logo { get; set; }
        public string PpsizePhoto { get; set; }

        [NotMapped]
        public HttpPostedFileBase CImageFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase PPImageFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase LImageFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase CDImageFile { get; set; }



    }
}