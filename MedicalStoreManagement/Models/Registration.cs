using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalStoreManagement.Models
{
    public class Registration   //VALIDATIONS ARE ALWAYS DEFINED AT MODEL LEVEL, model first verifies the values & then goes & sends the data from DB accordingly 
    {   //Validations are handled from both the sides: Server side and Client side
        [Required(ErrorMessage = "Email ID is Required !")]
        [Display(Name = "Email-ID")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "E-mail format is Incorect")]
       public string Email_id { get; set; }

        [Required(ErrorMessage = "Password is Required !")]
        [Display(Name="Set Password")]
        [DataType(DataType.Password)]
       public string Password { get; set; }

        [Required(ErrorMessage = "Re-Enter Password !")]
        [DataType(DataType.Password)]
        [Compare("Password")]
       public string RePassword { get; set; }

        [Required(ErrorMessage = "Name is Required !")]
       public string Name { get; set; }

        [Required(ErrorMessage = "Age is Required !")]
        [Range(18,200, ErrorMessage="Age should be above 18")]
       public string Age { get; set; }

        [Required(ErrorMessage = "Please select your Gender !")]
       public string Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone no. is Required !")]
        [MaxLength(10, ErrorMessage = "Phone number should be of 10 digits")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Phone_no { get; set; }

        [Required(ErrorMessage = "Address is Required !")]
        public string Address { get; set; }
    }
}