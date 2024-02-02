using Checksums.ChecksumCalculators;
using Checksums.Progress;

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
            string relativePath = Path.GetRelativePath(this.originPath, fileNode.Path);
            using (FileStream fs = File.OpenRead(fileNode.Path))
            {
                this.Notify(relativePath);
                this.streamWriter.WriteLine(string.Format("{0} {1}", checksumCalculator.Calculate(fs), relativePath));
            }
        }

        public override void Subscribe(Observer subscriber)
        {
            base.Subscribe(subscriber);
            if (checksumCalculator is ObservableBase)
            {
                ((ObservableBase)checksumCalculator).Subscribe(subscriber);
            }
        }
    }
}