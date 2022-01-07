using HtmlAgilityPack;
using RecursiveRetrievalWebSite.Service.Extensions;
using RecursiveRetrievalWebSite.Service.Models;
using RecursiveRetrievalWebSite.Service.Services.Contracts;
using System.Collections.Generic;

namespace RecursiveRetrievalWebSite.Service.Services
{
    public class RecursiveRetrievalService : IRecursiveRetrievalService
    {
        private readonly IInternetService _internetService;
        private LinkModel _allAppliedLinked;
        private List<string> _htmlLinks;
        private string _urlRoot;
        private string _hddRoot;

        public RecursiveRetrievalService(IInternetService internetService)
        {
            _internetService = internetService;
            _allAppliedLinked = new LinkModel();
            _htmlLinks = new List<string>();
            _urlRoot = @"https://tretton37.com/";
            _hddRoot = @"C:\Drive-D\WorkArea\pak\";
        }

        public void TraverseAndDownload(string rootUrl, string rootHddPath)
        {
            RecursiveMethod(_hddRoot, true);
        }

        public void RecursiveMethod(string hddPath, bool websiteRoot = false, string relatedUrl = null)
        {
            var currentUrl = _urlRoot + relatedUrl;
            DownloadHtml(hddPath, websiteRoot, relatedUrl);

            var htmlDoc = _internetService.GetHtml(currentUrl);

            var allLinksScript = SelectNodesByType(htmlDoc, LinkType.Script);
            var resultScript = GetLinks(currentUrl, LinkType.Script, allLinksScript);
            DownloadPreparedLink(resultScript);

            var allLinksStyleImage = SelectNodesByType(htmlDoc, LinkType.StyleImage);
            var resultStyleImage = GetLinks(currentUrl, LinkType.StyleImage, allLinksStyleImage);
            DownloadPreparedLink(resultStyleImage);

            var allLinksImage = SelectNodesByType(htmlDoc, LinkType.Image);
            var resultImage = GetLinks(currentUrl, LinkType.Image, allLinksImage);
            DownloadPreparedLink(resultImage);

            var allLinksHtml = SelectNodesByType(htmlDoc, LinkType.Html);
            var resultHtml = GetLinks(currentUrl, LinkType.Html, allLinksHtml);

            if (resultHtml.InternalLinks.Count > 0)
            {
                //var tee = resultHtml.InternalLinks[0];
                //RecursiveMethod(hddPath, false, tee);

                resultHtml.InternalLinks.ForEach(link =>
                {
                    RecursiveMethod(hddPath, false, link);
                });
            }

        }

        public LinkModel GetLinks(string url, LinkType type, HtmlNodeCollection allLinks)
        {
            var result = new LinkModel { ListType = type };
            if (allLinks != null)
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
                            if (hrefValue.IsExternalLink() &&
                                !result.ExternalLinks.Contains(hrefValue) && !_allAppliedLinked.ExternalLinks.Contains(hrefValue))
                            {
                                result.ExternalLinks.Add(hrefValue);
                                _allAppliedLinked.ExternalLinks.Add(hrefValue);
                            }

                            if (!hrefValue.IsExternalLink() &&
                                !result.InternalLinks.Contains(hrefValue) && !_allAppliedLinked.InternalLinks.Contains(hrefValue))
                            {
                                result.InternalLinks.Add(hrefValue);
                                _allAppliedLinked.InternalLinks.Add(hrefValue);
                                if (type == LinkType.Html) _htmlLinks.Add(hrefValue);
                            }
                        }
                    }
                }

            return result;
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

        private void DownloadPreparedLink(LinkModel list)
        {
            list.InternalLinks.ForEach(x =>
            {
                var fileInfo = new FileInfoModel(x, null);
                _internetService.DownloadFile(fileInfo);
            });
        }

        private void DownloadHtml(string hddPath, bool websiteRoot = false, string relatedUrl = null)
        {
            if (!websiteRoot)
            {
                if (relatedUrl.StartsWith("/"))
                {
                    var folder = hddPath + relatedUrl.MakeDiskPath();

                    _internetService.DownloadFile(_urlRoot + relatedUrl + @"/", folder, relatedUrl.LastFragment() + ".html");
                }
                else
                {
                    // not implement yet (relative path);
                }
            }
            else
            {
                _internetService.DownloadFile(_urlRoot, hddPath, @"index.html");
            }
        }
    }
}
