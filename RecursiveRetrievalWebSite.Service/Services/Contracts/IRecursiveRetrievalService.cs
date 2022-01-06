using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveRetrievalWebSite.Service.Services.Contracts
{
    public interface IRecursiveRetrievalService
    {
        void TraverseAndDownload(string rootUrl, string rootHddPath);
    }
}
