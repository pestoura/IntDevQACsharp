using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace DirectorySyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: DirectorySyncApp <source_dir> <replica_dir> <interval_seconds> <log_file>");
                return;
            }

            string sourceDir = args[0];
            string replicaDir = args[1];
            int intervalSeconds = int.Parse(args[2]);
            string logFile = args[3];

            Console.WriteLine($"Starting synchronization from {sourceDir} to {replicaDir} with interval of {intervalSeconds} seconds.");

            while (true)
            {
                try
                {
                    SyncFolders(sourceDir, replicaDir, logFile);
                    Console.WriteLine("Synchronization complete.");
                    Thread.Sleep(intervalSeconds * 1000); // Convert seconds to milliseconds
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during synchronization: {ex.Message}");
                    throw;
                }
            }
        }

        static void SyncFolders(string sourceDir, string replicaDir, string logFile)
        {
            // Set up logging
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine($"Starting synchronization from {sourceDir} to {replicaDir}.");

                // Traverse source directory recursively
                foreach (string srcFilePath in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        string relPath = Path.GetRelativePath(sourceDir, srcFilePath);
                        string dstFilePath = Path.Combine(replicaDir, relPath);

                        string srcHash = CalculateFileHash(srcFilePath);
                        string dstHash = File.Exists(dstFilePath) ? CalculateFileHash(dstFilePath) : null;

                        if (srcHash != dstHash)
                        {
                            File.Copy(srcFilePath, dstFilePath, true);
                            logWriter.WriteLine($"File copied: {srcFilePath} to {dstFilePath}");
                        }
                        else
                        {
                            logWriter.WriteLine($"File {srcFilePath} unchanged, skipping copy.");
                        }
                    }
                    catch (Exception ex)
                    {
                        logWriter.WriteLine($"Error processing file {srcFilePath}: {ex.Message}");
                    }
                }

                logWriter.WriteLine($"Synchronization completed at {DateTime.Now}");
                logWriter.WriteLine(); // Add blank line for separation
            }
        }

        static string CalculateFileHash(string filePath)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hash = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
