using NUnit.Framework;
using System;
using System.IO;
using IntDevQACsharp.src;

namespace IntDevQACsharp.tests
{
    [TestFixture]
    public class TestFolderSync
    {
        private static string sourceDir;
        private static string replicaDir;

        [SetUp]
        public void Setup()
        {
            sourceDir = Path.Combine(Path.GetTempPath(), "SourceFolder");
            replicaDir = Path.Combine(Path.GetTempPath(), "ReplicaFolder");

            Directory.CreateDirectory(sourceDir);
            Directory.CreateDirectory(replicaDir);
        }

        [TearDown]
        public void Cleanup()
        {
            Directory.Delete(sourceDir, true);
            Directory.Delete(replicaDir, true);
        }

        [Test]
        public void ShouldCopyFileToReplicaDirectory()
        {
            string testFilePath = Path.Combine(sourceDir, "test_file.txt");
            File.WriteAllText(testFilePath, "This is a test file.");

            SyncManager.Sync(sourceDir, replicaDir);

            string replicaFilePath = Path.Combine(replicaDir, "test_file.txt");
            Assert.IsTrue(File.Exists(replicaFilePath));

            string content = File.ReadAllText(replicaFilePath);
            Assert.AreEqual("This is a test file.", content);
        }

        [Test]
        public void ShouldDeleteFileFromReplicaDirectory()
        {
            string testFilePath = Path.Combine(sourceDir, "test_file.txt");
            string replicaFilePath = Path.Combine(replicaDir, "test_file.txt");
            File.WriteAllText(testFilePath, "This is a test file.");
            File.Copy(testFilePath, replicaFilePath);

            File.Delete(testFilePath);

            SyncManager.Sync(sourceDir, replicaDir);

            Assert.IsFalse(File.Exists(replicaFilePath));
        }

        [Test]
        public void ShouldUpdateFileInReplicaDirectory()
        {
            string testFilePath = Path.Combine(sourceDir, "test_file.txt");
            File.WriteAllText(testFilePath, "Original content.");

            SyncManager.Sync(sourceDir, replicaDir);

            File.WriteAllText(testFilePath, "Updated content.");

            SyncManager.Sync(sourceDir, replicaDir);

            string replicaFilePath = Path.Combine(replicaDir, "test_file.txt");
            string content = File.ReadAllText(replicaFilePath);
            Assert.AreEqual("Updated content.", content);
        }

        [Test]
        public void ShouldCreateDirectoryInReplica()
        {
            string testDirPath = Path.Combine(sourceDir, "test_directory");
            Directory.CreateDirectory(testDirPath);

            SyncManager.Sync(sourceDir, replicaDir);

            string replicaDirPath = Path.Combine(replicaDir, "test_directory");
            Assert.IsTrue(Directory.Exists(replicaDirPath));
        }

        [Test]
        public void ShouldDeleteDirectoryFromReplica()
        {
            string testDirPath = Path.Combine(sourceDir, "test_directory");
            Directory.CreateDirectory(testDirPath);

            SyncManager.Sync(sourceDir, replicaDir);

            Directory.Delete(testDirPath);

            SyncManager.Sync(sourceDir, replicaDir);

            string replicaDirPath = Path.Combine(replicaDir, "test_directory");
            Assert.IsFalse(Directory.Exists(replicaDirPath));
        }
    }
}
