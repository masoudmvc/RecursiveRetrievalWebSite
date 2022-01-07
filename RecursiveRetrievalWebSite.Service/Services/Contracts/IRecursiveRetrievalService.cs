using HtmlAgilityPack;
using RecursiveRetrievalWebSite.Service.Models;

namespace RecursiveRetrievalWebSite.Service.Services.Contracts
{
    public interface IRecursiveRetrievalService
    {
        void TraverseAndDownload(string rootHddPath);
        LinkModel GetLinks(string url, LinkType type, HtmlNodeCollection allLinks);
    }
}
