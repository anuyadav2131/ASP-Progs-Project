using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MedicalStoreManagement.Models
{
    public class UserLogin
    {
        public int User_id { get; set; }

        [DisplayName("Email-Id")]
        [Required(ErrorMessage = "Email-Id is Required !")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "E-Mail format is Incorrect")]
        public string Email_id { get; set; }

        [Required(ErrorMessage = "Password is Required !")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [MembershipPassword(MinRequiredNonAlphanumericCharacters = 1, MinNonAlphanumericCharactersError = "Your password needs to contain at least one special symbol (!, @, #, etc).", ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Name is Required !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Age is Required !")]
        public string Age { get; set; }
        [Required(ErrorMessage = "Gender is Required !")]
        public string Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Entered phone format is not valid.")]
        [DisplayName("Mobile no.")]
        [Required(ErrorMessage = "Phone no. is Required !")]
        public string Phone_no { get; set; }
        [Required(ErrorMessage = "Address is Required !")]
        public string Adderess { get; set; }
    }
}