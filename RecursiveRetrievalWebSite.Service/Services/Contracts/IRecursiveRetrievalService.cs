using HtmlAgilityPack;
using RecursiveRetrievalWebSite.Service.Models;
using System.Collections.Generic;

namespace RecursiveRetrievalWebSite.Service.Services.Contracts
{
    public interface IRecursiveRetrievalService
    {
        void TraverseAndDownload(string rootUrl, string rootHddPath);
        List<string> GetHtmlLinks(string url, LinkType type, HtmlNodeCollection allLinks);
    }
}
