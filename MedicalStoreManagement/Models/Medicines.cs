using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalStoreManagement.Models
{
    public class Medicines
    {
        public int M_Code { get; set; }

        [Required(ErrorMessage ="Medicine Name is required")]

        [DisplayName("Medicine Name")]
        public string M_Name { get; set; }

        [Required(ErrorMessage = "Medicine Type is required")]

        [DisplayName("Medicine Type")]
        public string M_Type { get; set; }

        [Required(ErrorMessage = "Brand Name is required")]
        public string Brand { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Expiry Date is required")]
        public DateTime Exp_Date {get;set;}
   
        [Range(0, int.MaxValue, ErrorMessage = "Quantity should be a Number")]
        [Required(ErrorMessage = "Quantity is required")]
        public string Quantity {get;set;}
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Price is required")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Fill the details of Medicine")]
        public string Information{ get; set; }
        public string choice { get; set; }
    }
}