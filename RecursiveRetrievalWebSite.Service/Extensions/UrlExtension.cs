
using System.Linq;

namespace RecursiveRetrievalWebSite.Service.Extensions
{
    public static class UrlExtension
    {
        /// <summary>
        /// This extension removes hash fragments of a url.
        /// </summary>
        /// <param name="url">string type of an url.</param>
        /// <returns>it doesn't change the original value and return a new string result.</returns>
        public static string RemoveHashFragment(this string url)
        {
            if (url == null) return null;

            var hrefResult = url.Trim();

            if (hrefResult == "/" || string.IsNullOrEmpty(hrefResult) || hrefResult.StartsWith("#"))
                return null;

            int index = hrefResult.IndexOf("#");
            if (index >= 0)
                hrefResult = hrefResult.Substring(0, index);

            return hrefResult;
        }

        public static string RemoveBeginingSlash(this string url)
        {
            if (url == null) return null;

            var hrefResult = url.Trim();

            while(hrefResult.StartsWith("/"))
                hrefResult = hrefResult.Substring(1, hrefResult.Length - 1);

            return hrefResult;
        }

        public static string LastFragment(this string url)
        {
            if (url == null) return null;

            var hrefResult = url.Trim();

            if (!hrefResult.Contains("/")) return hrefResult;

            var temp = hrefResult.Replace("//","/").Split("/").Where(x => x != string.Empty).ToArray();

            return temp[temp.Length - 1];
        }

        public static string MakeDiskPath(this string url)
        {
            if (url == null) return null;

            var hrefResult = url.Trim().Replace("//", "/");

            if (!hrefResult.Contains("/")) return hrefResult;

            var temp = hrefResult.Split("/").Where(x => x != string.Empty).ToArray();

            var result = "";
            for (int i = 0; i < temp.Length - 1; i++)
            {
                result += temp[i] + @"\";
            }

            return result;
        }
    }
}
