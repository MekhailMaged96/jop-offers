using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jopoffers.Models
{
    public class JopsView
    {
        public string JopTitle { get; set; }

        public IEnumerable<ApplyForJop> Items  { get; set; }
    }
}