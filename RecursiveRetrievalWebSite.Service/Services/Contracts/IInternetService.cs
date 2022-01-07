using HtmlAgilityPack;
using RecursiveRetrievalWebSite.Service.Models;

namespace RecursiveRetrievalWebSite.Service.Services.Contracts
{
    public interface IInternetService
    {
        void DownloadFile(string url, string destinationFolder, string destinationFilename);
        void DownloadFile(FileInfoModel info);
        bool CheckIfUrlIsValidAndAvailable(string url);
        HtmlDocument GetHtml(string url);
    }
}
