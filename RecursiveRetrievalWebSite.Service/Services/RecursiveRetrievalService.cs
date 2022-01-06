using HtmlAgilityPack;
using RecursiveRetrievalWebSite.Service.Extensions;
using RecursiveRetrievalWebSite.Service.Models;
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
            var type = LinkType.Html;
            var htmlDoc = _internetService.GetHtml(rootUrl);
            var allLinks = SelectNodesByType(htmlDoc, type);
            
            GetHtmlLinks(rootUrl, type, allLinks);
        }

        public List<string> GetHtmlLinks(string url, LinkType type, HtmlNodeCollection allLinks)
        {
            var internalLinks = new List<string>();
            var externalLinks = new List<string>();

            foreach (HtmlNode link in allLinks)
            {
                HtmlAttribute att = link.Attributes[SelectAttribute(type)];
                if (att != null && !string.IsNullOrEmpty(att.Value))
                {
                    var hrefValue = type == LinkType.Html
                        ? att.Value.RemoveHashFragment()
                        : att.Value.RemoveBrowserCachingPart();

                    if (hrefValue != null)
                    {
                        if (hrefValue.IsExternalLink()  && !externalLinks.Contains(att.Value))
                            externalLinks.Add(hrefValue);

                        if (!hrefValue.IsExternalLink() && !internalLinks.Contains(att.Value))
                            internalLinks.Add(hrefValue);
                    }
                }
            }

            return internalLinks;
        }

        private HtmlNodeCollection SelectNodesByType(HtmlDocument htmlDoc, LinkType type)
        {
            switch (type)
            {
                case LinkType.Html:
                    return htmlDoc.DocumentNode.SelectNodes("//a[@href]");
                case LinkType.Script:
                    return htmlDoc.DocumentNode.SelectNodes("//script[@src]");
                case LinkType.Image:
                    return htmlDoc.DocumentNode.SelectNodes("//img[@src]");
                case LinkType.StyleImage:
                    return htmlDoc.DocumentNode.SelectNodes("//link[@href]");
                default: 
                    throw new System.Exception();
            }

        }

        private string SelectAttribute(LinkType type)
        {
            switch (type)
            {
                case LinkType.Html:
                    return "href";
                case LinkType.Script:
                    return "src";
                case LinkType.Image:
                    return "src";
                case LinkType.StyleImage:
                    return "href";
                default:
                    throw new System.Exception();
            }

        }
    }
}
