using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Job_Offers_Website.Models
{
    public class ApplayForJob
    {

        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime  ApplayDate { get; set; }
        public int jobid { get; set; }
        public string userid { get; set;}



        public virtual Job job { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
} 