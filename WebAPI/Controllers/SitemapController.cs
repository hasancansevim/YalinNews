using System.Text;
using System.Xml.Linq;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class SitemapController : ControllerBase
    {
        private const string SiteBaseUrl = "https://yalinnews.com";
        private static readonly XNamespace SitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

        private readonly INewsService _newsService;

        public SitemapController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("sitemap.xml")]
        [Produces("application/xml")]
        public IActionResult GetSitemap()
        {
            var result = _newsService.GetPublishedNewsForSitemap();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var urlSet = new XElement(SitemapNamespace + "urlset",
                result.Data.Select(item =>
                    new XElement(SitemapNamespace + "url",
                        new XElement(SitemapNamespace + "loc", $"{SiteBaseUrl}/news/{item.Slug}"),
                        new XElement(SitemapNamespace + "lastmod", item.LastModified.ToString("yyyy-MM-dd"))
                    )
                )
            );

            var document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), urlSet);
            var xml = new StringBuilder();
            xml.AppendLine(document.Declaration.ToString());
            xml.Append(document.ToString(SaveOptions.DisableFormatting));

            return Content(xml.ToString(), "application/xml", Encoding.UTF8);
        }
    }
}
