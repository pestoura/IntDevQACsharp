using System;
using System.IO;

namespace IntDevQACsharp.src
{
    public static class Logger
    {
        private static readonly object lockObject = new object();
        private static string logFilePath;

        public static void Initialize(string filePath)
        {
            logFilePath = filePath;
            WriteLog("---- Log Started ----");
        }

        public static void Log(string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

            Console.WriteLine(logMessage);
            WriteLog(logMessage);
        }

        private static void WriteLog(string logMessage)
        {
            lock (lockObject)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(logFilePath, true))
                    {
                        writer.WriteLine(logMessage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing to log file: {ex.Message}");
                }
            }
        }
    }
}
