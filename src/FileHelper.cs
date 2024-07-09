using System;
using System.IO;

public static class FileHelper
{
    public static void CopyFileIfExists(string sourcePath, string destPath)  // Use more descriptive method names
    {
        try
        {
            if (!Directory.Exists(Path.GetDirectoryName(destPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destPath));
            }
            File.Copy(sourcePath, destPath, true);
        }
        catch (Exception ex)
        {
            Logger.Log($"Error copying file: {ex.Message}");
        }
    }

    public static void DeleteFileIfExists(string filePath)  // Use more descriptive method names
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception ex)
        {
            Logger.Log($"Error deleting file: {ex.Message}");
        }
    }

    public static void DeleteDirectoryIfExists(string dirPath)  // Use more descriptive method names
    {
        try
        {
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath, true);
            }
        }
        catch (Exception ex)
        {
            Logger.Log($"Error deleting directory: {ex.Message}");
        }
    }
}
