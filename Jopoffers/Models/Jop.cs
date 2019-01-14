using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Jopoffers.Models
{
    public class Jop
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("اسم الوظيفة")]
        public string title { get; set; }

        [Required]
        [AllowHtml]
        [DisplayName("وصف الوظيفة")]
        public string content { get; set; }

 
        [DisplayName("صورة الوظيفة")]
        public string image { get; set; }


        [Required]
        [DisplayName(" نوع الوظيفة")]
        public int CategoryId { get; set; }

       
        public string UserID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ApplicationUser user { get; set; }


    }
}