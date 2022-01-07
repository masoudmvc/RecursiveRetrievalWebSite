using HtmlAgilityPack;
using RecursiveRetrievalWebSite.Service.Extensions;
using RecursiveRetrievalWebSite.Service.Helpers;
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

        /// <summary>
        /// this is the landing method that calls main recursive method. hear we check everything or do any preparation before
        /// the process start.
        /// </summary>
        public RecursiveRetrievalService(IInternetService internetService)
        {
            _internetService = internetService;
            _allAppliedLinked = new LinkModel();
            _htmlLinks = new List<string>();
            _urlRoot = @"https://tretton37.com/";
        }

        public void TraverseAndDownload(string rootHddPath)
        {
            _hddRoot = rootHddPath.EndsWith(@"\") ?  rootHddPath : rootHddPath + @"\";

            if (_internetService.CheckIfUrlIsValidAndAvailable(_urlRoot))
            {
                ConsolLog.Success($"Targeted website ({_urlRoot}) is valid and available.");
                ConsolLog.Info($"The process is starting...");
                RecursiveMethod(_hddRoot, true);
            }
            else
                ConsolLog.Error($"Targeted website ({_urlRoot}) is invalid or there is any problem with your internet connection!");

        }

        /// <summary>
        /// This method is the main method that recursively load root url and its child until the last child doesn't have any
        /// related link (page).
        /// </summary>
        public void RecursiveMethod(string hddPath, bool isWebsiteRoot = false, string relatedUrl = null)
        {
            var currentUrl = _urlRoot + relatedUrl;
            DownloadHtml(hddPath, isWebsiteRoot, relatedUrl);

            ConsolLog.Warn($"scaning {currentUrl}...");
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
                // todo: this loop should be parallel loop (Parallel.For)
                resultHtml.InternalLinks.ForEach(link =>
                {
                    RecursiveMethod(hddPath, false, link);
                });
            }

        }

        /// <summary>
        /// Getting required elements in the loaded Html.
        /// </summary>
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

        /// <summary>
        /// Html element selector depends on link type. 
        /// </summary>
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

        /// <summary>
        /// specify the required attribute of the html element (tag)
        /// </summary>
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

        /// <summary>
        /// concentrated method to download assets except html file
        /// </summary>
        private void DownloadPreparedLink(LinkModel list)
        {
            list.InternalLinks.ForEach(x =>
            {
                var fileInfo = new FileInfoModel(x, null, _hddRoot);
                try
                {
                    _internetService.DownloadFile(fileInfo);
                    ConsolLog.Info($"{fileInfo.FileName} downloded to {fileInfo.Path}");
                }
                catch
                {
                    ConsolLog.Error($"Error occured while downloding {fileInfo.FileName}!");
                }
            });
        }

        /// <summary>
        /// concentrated method to download Htmls
        /// </summary>
        private void DownloadHtml(string hddPath, bool isWebsiteRoot = false, string relatedUrl = null)
        {
            if (!isWebsiteRoot)
            {
                if (relatedUrl.StartsWith("/"))
                {
                    var folder = hddPath + relatedUrl.MakeDiskPath();
                    try
                    {
                        _internetService.DownloadFile(_urlRoot + relatedUrl + @"/", folder, relatedUrl.LastFragment() + ".html");
                        ConsolLog.Info($"{relatedUrl.LastFragment()} downloded to {folder}");
                    }
                    catch
                    {
                        ConsolLog.Error($"Error occured while downloding {relatedUrl.LastFragment()}!");
                    }
                }
                else
                {
                    // not implement yet (relative path);
                }
            }
            else
            {
                _internetService.DownloadFile(_urlRoot, hddPath, @"index.html");
                ConsolLog.Warn($"index.html downloded to {hddPath}");
            }
        }
    }
}
