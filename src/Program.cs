using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Usage: FolderSync <source> <replica> <interval> <logFile>");
            return;
        }

        string source = args[0];
        string replica = args[1];
        int interval;
        if (!int.TryParse(args[2], out interval))
        {
            Console.WriteLine("Invalid interval value. It must be an integer representing seconds.");
            return;
        }
        string logFile = args[3];

        Logger.Initialize(logFile);
        SyncManager syncManager = new SyncManager();  // Use a more descriptive class name

        while (true)
        {
            Logger.Log("Starting synchronization...");
            syncManager.SyncFolders(source, replica);
            Logger.Log("Synchronization complete.");
            Thread.Sleep(interval * 1000);  // Consider using async/await for improved responsiveness (async file operations)
        }
    }
}
