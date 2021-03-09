using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Models
{
    public class Purchase
    {
        public string PurchaseId { get; set; }
        public string ArticleNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedUTC { get; set; }
    }
}
