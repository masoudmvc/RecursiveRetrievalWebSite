using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveRetrievalWebSite.Service.Services.Contracts
{
    public interface IInternetService
    {
        void DownloadFile(string url, string destinationFolder, string destinationFilename);
        bool CheckIfUrlIsValidAndAvailable(string url);
        HtmlDocument GetHtml(string url);
    }
}
