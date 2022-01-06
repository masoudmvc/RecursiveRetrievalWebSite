using HtmlAgilityPack;
using RecursiveRetrievalWebSite.Service.Helpers;
using RecursiveRetrievalWebSite.Service.Services.Contracts;
using System.Net;
using System.Threading.Tasks;

namespace RecursiveRetrievalWebSite.Service.Services
{
    public class InternetService : IInternetService
    {
        /// <summary>
        /// This method downloads the desired file and moves it to the desired path on the disk.
        /// </summary>
        /// <param name="url">source to be downloaded on disk</param>
        /// <param name="destinationFolder">the folder where the downloaded file is located</param>
        /// <param name="destinationFilename">downloaded file name with extension</param>
        public void DownloadFile(string url, string destinationFolder, string destinationFilename)
        {
            using (WebClient Client = new WebClient())
            {
                var path = @"C:\Drive-D\WorkArea\pak\" + destinationFolder ?? "" + @"\";
                FileManager.CreateFolderIfNotExist(path);
                Client.DownloadFile(url, path + destinationFilename);
            }
        }

        /// <summary>
        /// Check if the desired url is valid and available
        /// </summary>
        /// <param name="url">The desired url</param>
        /// <returns>return true if it was valid and available</returns>
        public bool CheckIfUrlIsValidAndAvailable(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                //can also use GET.
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                var result = response.StatusCode == HttpStatusCode.OK;
                response.Close();
                return result;
            }
            catch
            {
                return false;
            }
        }

        public HtmlDocument GetHtml(string url)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(url);
        }
    }
}
