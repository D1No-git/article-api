using DemoApi.Data;
using DemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public SalesController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task Post(Article article)
        {
            var newPurchase = new Purchase
            {
                PurchaseId = Guid.NewGuid().ToString(),
                ArticleNumber = article.ArticleNumber,
                Price = article.SalesPrice,
                CreatedUTC = DateTime.UtcNow
            };
            _context.Purchases.Add(newPurchase);
            await _context.SaveChangesAsync();
        }


    }
}
