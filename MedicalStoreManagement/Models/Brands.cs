using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MedicalStoreManagement.Models
{
    public class Brands
    {
        public int B_id { get; set; }

        [Required(ErrorMessage ="Brand Name is Required")]
        [DisplayName("Brand Name")]
        public string B_name { get; set; }
        
    }
}