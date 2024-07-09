using System;
using System.IO;

public static class Logger
{
    private static string logFilePath;

    public static void Initialize(string filePath)
    {
        logFilePath = filePath;
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine("---- Log Started ----");
        }
    }

    public static void Log(string message)
    {
        string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";  // Use a more descriptive log message format
        Console.WriteLine(logMessage);
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine(logMessage);
        }
    }
}
