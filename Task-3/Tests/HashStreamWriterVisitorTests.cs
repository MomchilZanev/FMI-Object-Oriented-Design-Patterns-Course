using Checksums.ChecksumCalculators;
using Checksums.FileStructure;
using Checksums.FileStructure.Builders;
using Checksums.FileStructure.Visitors;
using System.Diagnostics;
using System.Text;

namespace Tests
{
    [TestClass]
    public class HashStreamWriterVisitorTests
    {
        private static string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Test Directory");

        [TestInitialize]
        public void CheckEnvironment()
        {
            if (!Directory.Exists(testDirectory) || !Directory.EnumerateFiles(testDirectory).Any())
            {
                throw new DirectoryNotFoundException("Test environment has not been set up.");
            }
        }

        [TestMethod]
        [DataRow("SHA1")]
        [DataRow("SHA256")]
        [DataRow("SHA384")]
        [DataRow("SHA512")]
        [DataRow("MD5")]
        public void IgnoreLinksTest(string hashAlgorithm)
        {
            string startingDirectory = Path.Combine(testDirectory, "Starting Directory");
            Dictionary<string, string> expectedHashedFiles = this.getDirectoryFileHash(startingDirectory, hashAlgorithm)
                                                            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                                            .Select(row => row.Split(',', StringSplitOptions.RemoveEmptyEntries))
                                                            .ToDictionary(tokens => tokens[0], tokens => Path.GetRelativePath(startingDirectory, tokens[1]));

            DirectoryStructureBuilderBase respectLinksDirectoryStructureBuilder = new IgnoreLinksDirectoryStructureBuilder();
            respectLinksDirectoryStructureBuilder.SetupDirectory(startingDirectory);
            respectLinksDirectoryStructureBuilder.AddFiles();
            respectLinksDirectoryStructureBuilder.AddSubDirectories();
            FileNodeBase directory = respectLinksDirectoryStructureBuilder.GetProduct();

            MemoryStream memoryStream = new MemoryStream();
            FileNodeVisitorBase reportWriterVisitor = new HashStreamWriterVisitor(startingDirectory, new StreamWriter(memoryStream), new CommonChecksumCalculator(hashAlgorithm));

            directory.Accept(reportWriterVisitor);

            List<string> results = Encoding.UTF8.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length)
                                                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                                .ToList();

            Assert.AreEqual(expectedHashedFiles.Count, results.Count);
            Assert.IsTrue(expectedHashedFiles.All(kvp => results.Contains(string.Format("{0} {1}", kvp.Key, kvp.Value))));
        }

        [TestMethod]
        [DataRow("SHA1")]
        [DataRow("SHA256")]
        [DataRow("SHA384")]
        [DataRow("SHA512")]
        [DataRow("MD5")]
        public void RespectLinksTest(string hashAlgorithm)
        {
            string startingDirectory = Path.Combine(testDirectory, "Starting Directory");
            Dictionary<string, string> expectedHashedFiles = this.getDirectoryFileHash(testDirectory, hashAlgorithm)
                                                            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                                            .Select(row => row.Split(',', StringSplitOptions.RemoveEmptyEntries))
                                                            .Where(tokens => Path.GetExtension(tokens[1]).ToLower() != ".lnk")
                                                            .ToDictionary(tokens => tokens[0], tokens => Path.GetRelativePath(startingDirectory, tokens[1]));

            DirectoryStructureBuilderBase respectLinksDirectoryStructureBuilder = new RespectLinksDirectoryStructureBuilder();
            respectLinksDirectoryStructureBuilder.SetupDirectory(startingDirectory);
            respectLinksDirectoryStructureBuilder.AddFiles();
            respectLinksDirectoryStructureBuilder.AddSubDirectories();
            FileNodeBase directory = respectLinksDirectoryStructureBuilder.GetProduct();

            MemoryStream memoryStream = new MemoryStream();
            FileNodeVisitorBase reportWriterVisitor = new HashStreamWriterVisitor(startingDirectory, new StreamWriter(memoryStream), new CommonChecksumCalculator(hashAlgorithm));

            directory.Accept(reportWriterVisitor);

            List<string> results = Encoding.UTF8.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length)
                                                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                                .ToList();

            Assert.AreEqual(expectedHashedFiles.Count, results.Count);
            Assert.IsTrue(expectedHashedFiles.All(kvp => results.Contains(string.Format("{0} {1}", kvp.Key, kvp.Value))));
        }

        // Runs Get-FileHash on all files in a directory.
        // Output: <hash>:<file-path>
        private string getDirectoryFileHash(string directoryPath, string hashAlgorithm)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "PowerShell.exe";
            startInfo.Arguments = string.Format("-ExecutionPolicy Bypass -File \"{0}\" -Path \"{1}\" -Algorithm {2}",
                Path.Combine(Directory.GetCurrentDirectory(), "Get-Directory-FileHash.ps1"), directoryPath, hashAlgorithm);
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }
    }
}