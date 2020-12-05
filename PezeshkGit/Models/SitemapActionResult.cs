using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml;

namespace PezeshkGit.Models
{
    public class SitemapActionResult : ActionResult
    {
        private readonly List<SitemapItem> _SitemapItems;
        private readonly string _Website;
        public SitemapActionResult(List<SitemapItem> SitemapItems, string Website)
        {
            _SitemapItems = SitemapItems;
            _Website = Website;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/xml";
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                foreach (var SiteMapItem in _SitemapItems)
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", string.Format(_Website + "{0}", SiteMapItem.URL));
                    if (SiteMapItem.DateAdded != null)
                        writer.WriteElementString("lastmod", string.Format("{0:yyyy-MM-dd}", SiteMapItem.DateAdded));
                    writer.WriteElementString("changefreq", "daily");
                    writer.WriteElementString("priority", SiteMapItem.Priority);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
            }
        }
    }
}