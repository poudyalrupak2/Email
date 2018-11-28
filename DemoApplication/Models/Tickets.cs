using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoApplication.Models
{
    public class Tickets
    {
        public int Id { get; set; }
        public string CostumerName { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string From { get; set; }
        public string TO { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]

        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DepartureDate { get; set; }
      
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DisplayName("DepartureTime(eg:10:10)")]

        public TimeSpan? DepartureTime { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ReturnDate { get; set; }
      
        [DataType(DataType.Time)]
        [DisplayName("ReturnTime(eg:10:10)")]

        [DisplayFormat( DataFormatString = "{0:HH:mm}")]
        public TimeSpan? ReturnTime { get; set; }
        public int NoOfPersons { get; set; }

        public int? NoOfAdult { get; set; }
        [DisplayName("Children(<5)")]
        public int? NoOfChildern { get; set; }
        [DisplayName("Adult(age>80)")]
        public int? NoOfOldPerson { get; set; }
        public string Nationality { get; set; }
        public Decimal TotalCost { get; set; }
        public string CreatedBy { get; set; }
        [Column(TypeName = "Date")]
        [DisplayFormat( DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }

        public string EditedBy { get; set; }
        [Column(TypeName = "Date")]
        [DisplayFormat( DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime?  EditedDate { get; set; }

    }
}