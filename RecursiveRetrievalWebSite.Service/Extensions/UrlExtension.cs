
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
    }
}
