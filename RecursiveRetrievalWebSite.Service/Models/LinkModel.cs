using System;
using System.Collections.Generic;
using System.Text;

namespace RecursiveRetrievalWebSite.Service.Models
{
    public class LinkModel
    {
        public LinkModel()
        {
            InternalLinks = new List<string>();
            ExternalLinks = new List<string>();
        }

        public List<string> InternalLinks { get; set; }
        public List<string> ExternalLinks { get; set; }
        public LinkType ListType { get; set; }
    }

    //public class CombinedLinkModel
    //{
    //    public string link { get; set; }
    //    public LinkType type { get; set; }
    //}

    public enum LinkType
    {
        Html,
        Script,
        Image,
        StyleImage
    }
}
