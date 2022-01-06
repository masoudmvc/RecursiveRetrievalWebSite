using HtmlAgilityPack;
using RecursiveRetrievalWebSite.Service.Extensions;
using RecursiveRetrievalWebSite.Service.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecursiveRetrievalWebSite.Service.Services
{
    public class RecursiveRetrievalService : IRecursiveRetrievalService
    {
        private readonly IInternetService _internetService;

        public RecursiveRetrievalService(IInternetService internetService)
        {
            _internetService = internetService;
        }

        public void TraverseAndDownload(string rootUrl, string rootHddPath)
        {
            GetHtmlLinks(rootUrl);
        }

        public List<string> GetHtmlLinks(string url)
        {
            var internalLinks = new List<string>();
            var externalLinks = new List<string>();

            var htmlDoc = _internetService.GetHtml(url);

            var allLinks = htmlDoc.DocumentNode.SelectNodes("//a[@href]");

            foreach (HtmlNode link in allLinks)
            {
                HtmlAttribute att = link.Attributes["href"];
                if (att != null && !string.IsNullOrEmpty(att.Value))
                {
                    var hrefValue = att.Value.RemoveHashFragment();
                    if (hrefValue != null)
                    {
                        if ((hrefValue.ToLower().StartsWith("http") || hrefValue.ToLower().StartsWith("//") || hrefValue.ToLower().StartsWith("www.")) && !externalLinks.Contains(att.Value))
                            externalLinks.Add(hrefValue);

                        if (!(hrefValue.ToLower().StartsWith("http") || hrefValue.ToLower().StartsWith("//") || hrefValue.ToLower().StartsWith("www.")) && !internalLinks.Contains(att.Value))
                            internalLinks.Add(hrefValue);
                    }
                }
            }

            return internalLinks;
        }
    }
}
