using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Job_Offers_Website.Models
{
    
    public class Job
    {
        public int Id { get; set; }
        [DisplayName("أسم الوظيفة")]
        public string Jobtitle { get; set; }
        [DisplayName("وصف الوظيفة")]
        [AllowHtml]
        public string JobContent { get; set; }
        [DisplayName("صورة الوظيفة")]
        public string  JobImage { get; set; }
        [DisplayName("نوع الوظيفة")]
        public int CategoryId { get; set; }
        public string UserID { set; get; }

        public virtual Category category { set; get; }
        public  virtual ApplicationUser User { set; get; } 

    }
}