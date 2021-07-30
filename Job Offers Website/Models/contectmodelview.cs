using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Job_Offers_Website.Models
{
    public class contectmodelview
    {
        [Required]
        public string Name { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Object { set; get; }
        [Required]
        public string Message { set; get; }


    }
}