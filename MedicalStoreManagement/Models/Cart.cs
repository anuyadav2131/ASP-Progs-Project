using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalStoreManagement.Models
{
    public class Cart
    {
        public int Cart_id { get; set; }
        public int User_id { get; set; }
        public int M_Code { get; set; }
        public string M_Name { get; set; }
        public string Qty { get; set; }
        public int Price { get; set; }

        [Display(Name ="Sale Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd-mm-yyyy}",ApplyFormatInEditMode =true)]
        public string Sale_date {get; set;}
        public int status { get; set; }
        public string Information { get; set; } 
        public string choice { get; set; }
        public string Total_price { get; set; }
    }
}