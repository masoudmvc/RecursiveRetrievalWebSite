using System;

namespace RecursiveRetrievalWebSite.Service.Helpers
{
    public static class ConsolLog
    {
        /// <summary>
        /// Showing a message White white color in prompt window.
        /// </summary>
        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }
        /// <summary>
        /// Showing a message with Green color in prompt window.
        /// </summary>
        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }
        /// <summary>
        /// Showing a message with Yellow color in prompt window.
        /// </summary>
        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
        }
        /// <summary>
        /// Showing a message with Red color in prompt window.
        /// </summary>
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }
    }
}
