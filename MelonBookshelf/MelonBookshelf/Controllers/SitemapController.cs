using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace MelonBookshelf.Controllers
{
    public class SitemapController : Controller
    {
        public ActionResult Index()
        {

            List<string> urls = new List<string>
        {
            Url.Action("Index", "Home"),

            Url.Action("Index", "Category"),
            Url.Action("Create", "Category"),

            Url.Action("Index", "Request"),
            Url.Action("MyRequests", "Request"),
            Url.Action("FollowingRequests", "Request"),
            Url.Action("PendingRequests", "Request"),
            Url.Action("Details", "Request"),
            Url.Action("Create", "Request"),

            Url.Action("Index", "Resource"),
            Url.Action("Create", "Resource"),
            Url.Action("Details", "Resource"),
            Url.Action("DownloadsReport", "Resource"),

            Url.Action("Index", "User"),
            Url.Action("Details", "User"),
            Url.Action("Login", "User"),
            Url.Action("Register", "User"),
            Url.Action("CahngePassword", "User"),
        };

            // Generate the XML sitemap
            string sitemapXml = GenerateSitemapXml(urls);

            return Content(sitemapXml, "application/xml");
        }

        private string GenerateSitemapXml(List<string> urls)
        {
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(ns + "urlset");

            foreach (var url in urls)
            {
                XElement urlElement = new XElement(ns + "url",
                    new XElement(ns + "loc", url),
                    new XElement(ns + "changefreq", "weekly"), // Set the change frequency
                    new XElement(ns + "priority", "0.5"));   // Set the priority

                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }
    }

}
