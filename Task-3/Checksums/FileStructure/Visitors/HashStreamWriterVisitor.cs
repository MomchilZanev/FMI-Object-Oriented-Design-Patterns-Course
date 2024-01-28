using Checksums.ChecksumCalculators;

namespace Checksums.FileStructure.Visitors
{
    public class HashStreamWriterVisitor : FileNodeVisitorBase
    {
        private IChecksumCalculator checksumCalculator;
        private StreamWriter streamWriter;

        public HashStreamWriterVisitor(string originPath, StreamWriter streamWriter, IChecksumCalculator checksumCalculator) : base(originPath)
        {
            this.checksumCalculator = checksumCalculator;
            this.streamWriter = streamWriter;
            this.streamWriter.AutoFlush = true;
        }

        public override void ProcessFile(FileNode fileNode)
        {
            using (FileStream fs = File.OpenRead(fileNode.Path))
            {
                this.streamWriter.WriteLine(string.Format("{0} {1}", checksumCalculator.Calculate(fs), Path.GetRelativePath(this.originPath, fileNode.Path)));
            }
        }
    }
}