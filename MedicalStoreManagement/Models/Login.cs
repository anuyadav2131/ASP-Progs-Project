using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;

namespace MedicalStoreManagement.Models
{
    public class LoginM  //ADMIN LOGIN
    {
        public int User_id { get; set; }
        //------------------------------------------------------
        [DisplayName("Email-Id")]
        [Required(ErrorMessage = "Username Required !")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage="E-Mail format is Incorect")]
        public string Email_id { get; set; }
        //------------------------------------------------------
        [Required(ErrorMessage = "Password Required !")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [MembershipPassword(MinRequiredNonAlphanumericCharacters = 1,MinNonAlphanumericCharactersError = "Your password needs to contain at least one special symbol (!, @, #, etc).",ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //------------------------------------------------------
        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Confirm Password Required !")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password did not match")]
        public string RePassword { get; set; }
    }
} 