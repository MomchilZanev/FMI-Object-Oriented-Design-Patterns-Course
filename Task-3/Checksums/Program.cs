using Checksums.ChecksumCalculators;
using Checksums.DirectoryStructureBuilders;
using Checksums.FileStructure;
using Checksums.FileStructureVisitors;
using Checksums.Progress;

namespace Checksums
{
    internal class Program
    {
        private static EventWaitHandle waitHandle = new ManualResetEvent(initialState: true);

        static void Main(string[] args)
        {
            Console.WriteLine("Choose directory to scan:");
            string directoryToScan = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Choose output file:");
            string outputFile = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Ignore links? (Y/N)");
            bool ignoreLinks = (Console.ReadLine() ?? string.Empty).Trim().ToLower() == "y";

            Console.WriteLine("Choose hash algorithm: [ SHA1, SHA256, SHA384, SHA512, MD5 ]");
            string hashAlgorithm = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Display report? (Y/N)");
            bool displayReport = (Console.ReadLine() ?? string.Empty).Trim().ToLower() == "y";

            IDirectoryStructureBuilder directoryStructureBuilder = ignoreLinks ?
                new IgnoreLinksDirectoryStructureBuilder() :
                new RespectLinksDirectoryStructureBuilder();
            directoryStructureBuilder.SetupDirectory(directoryToScan);
            directoryStructureBuilder.AddFiles();
            directoryStructureBuilder.AddSubDirectories();
            IDirectoryNode directory = directoryStructureBuilder.GetProduct();

            if (displayReport)
            {
                IFileStructureVisitor reportWriterVisitor = new ReportWriterVisitor(directoryToScan);
                directory.Accept(reportWriterVisitor);
            }

            System.IO.File.Create(outputFile).Close();
            using (System.IO.FileStream outputFileStream = System.IO.File.Open(outputFile, FileMode.Open))
            {
                IFileStructureVisitor hashStreamWriterVisitor = new HashStreamWriterVisitor(
                    directoryToScan,
                    new StreamWriter(outputFileStream),
                    new CommonChecksumCalculator(hashAlgorithm, waitHandle),
                    waitHandle);
                if (hashStreamWriterVisitor is IObservable)
                    ((IObservable)hashStreamWriterVisitor).Subscribe(new ProgressReporter(directory.Size));

                System.Threading.Thread scanThread = new System.Threading.Thread(() => directory.Accept(hashStreamWriterVisitor));
                scanThread.Start();

                bool running = true;
                while (scanThread.IsAlive)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        if (running)
                            waitHandle.Reset();
                        else
                            waitHandle.Set();

                        running = !running;
                    }
                }
            }
        }
    }
}