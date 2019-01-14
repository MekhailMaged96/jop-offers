using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Jopoffers.Models
{
    public class ApplyForJop
    {
        public int Id { get; set; }
        [DisplayName("الرسالة")]
        public string message { get; set; }
        [DisplayName("تاريخ الطلب")]
        public DateTime ApplyDate { get; set; }
        public int JopId { get; set; }
        public string UserId { get; set; }

        public virtual Jop jop { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
}