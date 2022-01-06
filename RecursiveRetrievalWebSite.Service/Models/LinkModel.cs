using System;
using System.Collections.Generic;
using System.Text;

namespace RecursiveRetrievalWebSite.Service.Models
{
    public class LinkModel
    {
        public List<string> Links { get; set; }
        public LinkType ListType { get; set; }
    }

    public enum LinkType
    {
        Html,
        Script,
        Image,
        StyleImage
    }
}
