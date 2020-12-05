using Pezeshk.Data;
using Pezeshk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Pezeshk.Controllers
{
    public class SitemapController : Controller
    {
        readonly ApplicationDbContext _context;
        public SitemapController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();


        [OutputCache(Duration = 120, VaryByParam = "none")]
        public SitemapActionResult Index()
        {
            var Website = "https://www.mobilizecloud.com";
            var SitemapItems = new List<SitemapItem>
            {
                new SitemapItem
                {
                    URL = "",
                    Priority = "1",
                    DateAdded = new DateTime(2020, 9, 14)
                },
                new SitemapItem
                {
                    URL = "/Gallery",
                    Priority = ".9",
                    DateAdded = new DateTime(2020, 9, 14)
                },
                new SitemapItem
                {
                    URL = "/Posts",
                    Priority = ".9",
                    DateAdded = new DateTime(2020, 9, 14)
                },
                new SitemapItem
                {
                    URL = "/FAQ",
                    Priority = ".8",
                    DateAdded = new DateTime(2020, 9, 14)
                }
            };

            var posts = _context.Posts.ToList().Where(c => c.Published);

            foreach (var item in posts)
            {
                SitemapItems.Add(new SitemapItem
                {
                    URL = "/Posts/View/" + item.ID,
                    Priority = ".9",
                    DateAdded = Date.ConvertToDateTime(item.PostedOn)
                });
            }

            return new SitemapActionResult(SitemapItems, Website);
        }
    }
}