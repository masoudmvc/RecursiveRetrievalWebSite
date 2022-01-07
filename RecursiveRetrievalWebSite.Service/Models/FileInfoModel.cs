
namespace RecursiveRetrievalWebSite.Service.Models
{
    public class FileInfoModel
    {
        /// <summary>
        /// preparing a model to specify filename and location on disk, depends on the link fragments
        /// </summary>
        public FileInfoModel(string link, string beforePath, string hddRoot)
        {
            FileName = "";
            Path = hddRoot;

            if (!string.IsNullOrEmpty(beforePath))
            {
                var beforeTemp = beforePath.Split('/');
                for (int i = 0; i < beforeTemp.Length - 1; i++)
                    Path += (i == 0) ? beforeTemp[i] : @"\" + beforeTemp[i];
            }

            Url = link.StartsWith("/")
                ? @"https://tretton37.com" + link
                : @"https://tretton37.com" + (beforePath ?? "") + "/" + link;


            if (link.Contains("/"))
            {
                var temp = link.Split("/");
                FileName = temp[temp.Length - 1];

                for (int i = 0; i < temp.Length - 1; i++)
                    Path += (i == 0) ? temp[i] : @"\" + temp[i];

                 Path += @"\";
            }
            else
            {
                FileName = link;
                Path = null;
            }
        }

        public string FileName { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public string Url { get; set; }
    }
}
