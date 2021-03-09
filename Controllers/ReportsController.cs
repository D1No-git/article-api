using DemoApi.Data;
using DemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public ReportsController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("NumberArticlesSoldPerDay")]
        public async Task<IEnumerable<SalesReport>> GetNumberArticlesSoldPerDay(DateTime? reportDate)
        {
            if (reportDate.HasValue)
            {
                var purchases = await _context.Purchases.Where(x => x.CreatedUTC.Date == reportDate.Value.Date).ToListAsync();
                return new List<SalesReport> {
                    new SalesReport
                    {
                        Date = reportDate.Value.Date,
                        TotalArticlesSold = purchases.Count,
                        TotalUniqueArticlesSold = purchases.Select(x => x.ArticleNumber).Distinct().Count()
                    }
                };
            }
            else
            {
                var purchases = await _context.Purchases.ToListAsync();
                var dates = purchases.Select(x => x.CreatedUTC.Date).Distinct();
                var results = new List<SalesReport>();
                foreach (var date in dates)
                {
                    results.Add(new SalesReport
                    {
                        Date = date,
                        TotalArticlesSold = purchases.Where(x => x.CreatedUTC.Date == date).Count(),
                        TotalUniqueArticlesSold = purchases.Where(x => x.CreatedUTC.Date == date).Select(x => x.ArticleNumber).Distinct().Count()
                    });
                }

                return results;
            }
        }

        [HttpGet]
        [Route("TotalRevenuePerDay")]
        public async Task<IEnumerable<RevenueReport>> GetTotalRevenuePerDay(DateTime? reportDate)
        {
            if (reportDate.HasValue)
            {
                var purchases = await _context.Purchases.Where(x => x.CreatedUTC.Date == reportDate.Value.Date).ToListAsync();
                return new List<RevenueReport> {
                    new RevenueReport
                    {
                        Date = reportDate.Value.Date,
                        Revenue = purchases.Sum(x => x.Price)
                    }
                };
            }
            else
            {
                var purchases = await _context.Purchases.ToListAsync();
                var dates = purchases.Select(x => x.CreatedUTC.Date).Distinct();
                var results = new List<RevenueReport>();
                foreach (var date in dates)
                {
                    results.Add(new RevenueReport
                    {
                        Date = date,
                        Revenue = purchases.Where(x => x.CreatedUTC.Date == date).Sum(x => x.Price)
                    });
                }

                return results;
            }
        }

        [HttpGet]
        [Route("Statistics")]
        public async Task<IEnumerable<StatisticsReport>> GetStatistics()
        {
            var purchases = await _context.Purchases.ToListAsync();
            var articles = purchases.Select(x => x.ArticleNumber).Distinct();
            var results = new List<StatisticsReport>();

            foreach (var article in articles)
            {
                results.Add(new StatisticsReport
                {
                    ArticleNumber = article,
                    Revenue = purchases.Where(x => x.ArticleNumber == article).Sum(x => x.Price)
                });
            }

            return results;
        }
    }
}
