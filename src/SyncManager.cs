using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

public class SyncManager
{
    public void SyncFolders(string source, string replica)
    {
        if (!Directory.Exists(source))
        {
            Logger.Log($"Source directory {source} does not exist.");
            return;
        }

        SyncDirectory(new DirectoryInfo(source), new DirectoryInfo(replica));
    }

    private void SyncDirectory(DirectoryInfo source, DirectoryInfo replica)
    {
        if (!replica.Exists)
        {
            replica.Create();
        }

        // Copy new and updated files
        foreach (FileInfo sourceFile in source.GetFiles())
        {
            string replicaFilePath = Path.Combine(replica.FullName, sourceFile.Name);
            if (!File.Exists(replicaFilePath) || !FilesAreEqual(sourceFile.FullName, replicaFilePath))
            {
                FileHelper.CopyFile(sourceFile.FullName, replicaFilePath);
                Logger.Log($"Copied/Updated file: {sourceFile.FullName} to {replicaFilePath}");
            }
        }

        // Recursively sync subdirectories
        foreach (DirectoryInfo sourceSubDir in source.GetDirectories())
        {
            DirectoryInfo replicaSubDir = replica.CreateSubdirectory(sourceSubDir.Name);
            SyncDirectory(sourceSubDir, replicaSubDir);
        }

        // Delete files and directories that are not in source
        foreach (FileInfo replicaFile in replica.GetFiles())
        {
            string sourceFilePath = Path.Combine(source.FullName, replicaFile.Name);
            if (!File.Exists(sourceFilePath))
            {
                FileHelper.DeleteFile(replicaFile.FullName);
                Logger.Log($"Deleted file: {replicaFile.FullName}");
            }
        }

        foreach (DirectoryInfo replicaSubDir in replica.GetDirectories())
        {
            DirectoryInfo sourceSubDir = new DirectoryInfo(Path.Combine(source.FullName, replicaSubDir.Name));
            if (!sourceSubDir.Exists)
            {
                FileHelper.DeleteDirectory(replicaSubDir.FullName);
                Logger.Log($"Deleted directory: {replicaSubDir.FullName}");
            }
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
