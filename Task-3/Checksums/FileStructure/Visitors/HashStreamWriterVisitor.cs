using Checksums.ChecksumCalculators;

namespace Checksums.FileStructure.Visitors
{
    public class HashStreamWriterVisitor : FileNodeVisitorBase
    {
        private IChecksumCalculator checksumCalculator;
        private StreamWriter streamWriter;

        public HashStreamWriterVisitor(StreamWriter streamWriter, IChecksumCalculator checksumCalculator)
        {
            this.checksumCalculator = checksumCalculator;
            this.streamWriter = streamWriter;
            this.streamWriter.AutoFlush = true;
        }

        public override void ProcessFile(FileNode fileNode)
        {
            using (FileStream fs = File.OpenRead(fileNode.Path))
            {
                this.streamWriter.WriteLine(string.Format("{0} {1}", checksumCalculator.Calculate(fs), fileNode.Path));
            }
        }
    }
}