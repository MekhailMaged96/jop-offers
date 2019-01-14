using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jopoffers.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required]
        [DisplayName("نوع الوظيفة")]
        public string Name { get; set; }
        [Required]
        [Display(Name="وصف النوع")]
        public string Description { get; set; }


        public virtual ICollection<Jop> jops { get; set; }
    }
}