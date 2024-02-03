using Checksums.ChecksumCalculators;
using Checksums.Progress;

namespace Checksums.FileStructure.Visitors
{
    public class HashStreamWriterVisitor : FileNodeVisitorBase
    {
        private IChecksumCalculator checksumCalculator;
        private StreamWriter streamWriter;
        private EventWaitHandle? waitHandle;

        public HashStreamWriterVisitor(string originPath, StreamWriter streamWriter, IChecksumCalculator checksumCalculator, EventWaitHandle? waitHandle = null)
            : base(originPath)
        {
            this.checksumCalculator = checksumCalculator;
            this.streamWriter = streamWriter;
            this.streamWriter.AutoFlush = true;
            this.waitHandle = waitHandle;
        }

        public override void Visit(DirectoryNode directoryNode)
        {
            if (this.waitHandle is not null)
                this.waitHandle.WaitOne();

            base.Visit(directoryNode);
        }

        public override void Visit(FileNode fileNode)
        {
            if (this.waitHandle is not null)
                this.waitHandle.WaitOne();

            base.Visit(fileNode);
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