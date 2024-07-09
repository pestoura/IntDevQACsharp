using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

public class SyncManager
{
    public void SynchronizeFolders(string sourceDirectory, string replicaDirectory)
    {
        if (!Directory.Exists(sourceDirectory))
        {
            Logger.Log($"Source directory {sourceDirectory} does not exist.");
            return;
        }

        SynchronizeDirectory(new DirectoryInfo(sourceDirectory), new DirectoryInfo(replicaDirectory));
    }

    private void SynchronizeDirectory(DirectoryInfo sourceDir, DirectoryInfo replicaDir)
    {
        try
        {
            if (!replicaDir.Exists)
            {
                replicaDir.Create();
            }

            // Copy new and updated files
            foreach (FileInfo sourceFile in sourceDir.GetFiles())
            {
                string replicaFilePath = Path.Combine(replicaDir.FullName, sourceFile.Name);
                if (!File.Exists(replicaFilePath) || !FilesAreEqual(sourceFile.FullName, replicaFilePath))
                {
                    FileHelper.CopyFileIfExists(sourceFile.FullName, replicaFilePath);
                    Logger.Log($"Copied/Updated file: {sourceFile.FullName} to {replicaFilePath}");
                }
            }

            // Recursively synchronize subdirectories
            foreach (DirectoryInfo sourceSubDir in sourceDir.GetDirectories())
            {
                DirectoryInfo replicaSubDir = replicaDir.CreateSubdirectory(sourceSubDir.Name);
                SynchronizeDirectory(sourceSubDir, replicaSubDir);
            }

            // Delete files and directories that are not in source
            foreach (FileInfo replicaFile in replicaDir.GetFiles())
            {
                string sourceFilePath = Path.Combine(sourceDir.FullName, replicaFile.Name);
                if (!File.Exists(sourceFilePath))
                {
                    FileHelper.DeleteFileIfExists(replicaFile.FullName);
                    Logger.Log($"Deleted file: {replicaFile.FullName}");
                }
            }

            foreach (DirectoryInfo replicaSubDir in replicaDir.GetDirectories())
            {
                DirectoryInfo sourceSubDir = new DirectoryInfo(Path.Combine(sourceDir.FullName, replicaSubDir.Name));
                if (!sourceSubDir.Exists)
                {
                    FileHelper.DeleteDirectoryIfExists(replicaSubDir.FullName);
                    Logger.Log($"Deleted directory: {replicaSubDir.FullName}");
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log($"Error during synchronization: {ex.Message}");
        }
    }

    private bool FilesAreEqual(string filePath1, string filePath2)
    {
        using (var hashAlgorithm = SHA256.Create())
        {
            byte[] hash1 = hashAlgorithm.ComputeHash(File.ReadAllBytes(filePath1));
            byte[] hash2 = hashAlgorithm.ComputeHash(File.ReadAllBytes(filePath2));
            return hash1.SequenceEqual(hash2);
        }
    }
}
