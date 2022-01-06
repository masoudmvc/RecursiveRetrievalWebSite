using System.IO;

namespace RecursiveRetrievalWebSite.Service.Helpers
{
    public static class FileManager
    {
        public static void CreateFolderIfNotExist(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static bool CheckIfFolderExist(string path)
        {
            return Directory.Exists(path);
        }
    }
}
