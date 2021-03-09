using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Models
{
    public class Article
    {
        [Required]
        [MaxLength(32, ErrorMessage = "Article number must be 32 characters or less")]
        public string ArticleNumber { get; set; }
        public decimal SalesPrice { get; set; }
    }
}
