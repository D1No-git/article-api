using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Models
{
    public class SalesReport
    {
        public DateTime Date { get; set; }
        public int TotalArticlesSold { get; set; }
        public int TotalUniqueArticlesSold { get; set; }
    }
}
