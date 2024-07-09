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
        string logMessage = $"{DateTime.Now} - {message}";
        Console.WriteLine(logMessage);
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine(logMessage);
        }
    }
}

