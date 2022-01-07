using System.IO;

namespace RecursiveRetrievalWebSite.Service.Helpers
{
    public static class FileManager
    {
        /// <summary>
        /// this method creates a folder if not exist.
        /// </summary>
        public static void CreateFolderIfNotExist(string path)
        {
            // it doen't need to check if the folder exist.
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Check if a folder exist
        /// </summary>
        public static bool CheckIfFolderExist(string path)
        {
            return Directory.Exists(path);
        }
    }
}
