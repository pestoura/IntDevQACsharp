using System.IO;

public static class FileHelper
{
    public static void CopyFile(string sourcePath, string destPath)
    {
        if (!Directory.Exists(Path.GetDirectoryName(destPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(destPath));
        }
        File.Copy(sourcePath, destPath, true);
    }

    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public static void DeleteDirectory(string dirPath)
    {
        if (Directory.Exists(dirPath))
        {
            Directory.Delete(dirPath, true);
        }
    }
}

