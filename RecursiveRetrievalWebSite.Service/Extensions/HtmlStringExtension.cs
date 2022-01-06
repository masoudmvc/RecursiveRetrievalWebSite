using System;
using System.Collections.Generic;
using System.Text;

namespace RecursiveRetrievalWebSite.Service.Extensions
{
    public static class HtmlStringExtension
    {
        /// <summary>
        /// This extension removes the part that added to the href string of links for browser cashing.
        /// </summary>
        /// <param name="href"></param>
        /// <returns>it doesn't change the original value and return a new string result.</returns>
        public static string RemoveBrowserCachingPart(this string href)
        {
            if (href == null) return null;

            var hrefResult = href.Trim();

            if (hrefResult == "/" || string.IsNullOrEmpty(hrefResult))
                return null;

            int index = hrefResult.IndexOf("?");
            if (index >= 0)
                hrefResult = hrefResult.Substring(0, index);

            return hrefResult;
        }

        public static bool IsExternalLink(this string href)
        {
            var hrefValue = href.Trim();

            return hrefValue.ToLower().StartsWith("http") || 
                   hrefValue.ToLower().StartsWith("//") || 
                   hrefValue.ToLower().StartsWith("www.");
        }
    }
}
