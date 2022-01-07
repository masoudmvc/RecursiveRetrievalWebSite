using System.Collections.Generic;

namespace RecursiveRetrievalWebSite.Service.Models
{
    public class LinkModel
    {
        public LinkModel()
        {
            InternalLinks = new List<string>();
            ExternalLinks = new List<string>();
        }

        /// <summary>
        /// list of internal resourse links like css, js, image
        /// </summary>
        public List<string> InternalLinks { get; set; }

        /// <summary>
        /// list of external resourse links like css, js, image. I don't use this links in this app but I keep them. maybe it use
        /// for logging and some reports
        /// </summary>
        public List<string> ExternalLinks { get; set; }

        /// <summary>
        /// Html, Script, Image, StyleImage
        /// </summary>
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
