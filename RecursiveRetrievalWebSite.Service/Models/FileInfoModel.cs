using System;
using System.Collections.Generic;
using System.Text;

namespace RecursiveRetrievalWebSite.Service.Models
{
    public class FileInfoModel
    {
        public FileInfoModel(string link, string beforePath)
        {
            FileName = "";
            Path = @"C:\Drive-D\WorkArea\pak\";

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

            //if (!string.IsNullOrEmpty(beforePath))
            //{
            //    Path = beforePath + Path;
            //    var temp = link.Split("/");
            //}
        }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public string Url { get; set; }
    }
}
