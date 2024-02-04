using Checksums.ChecksumCalculators;
using Checksums.FileStructure;
using Checksums.Progress;

namespace Checksums.FileStructureVisitors
{
    public class HashStreamWriterVisitor : FileStructureVisitorBase
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

        public override void Visit(IFileNode fileNode)
        {
            if (this.waitHandle is not null)
                this.waitHandle.WaitOne();

            string relativePath = System.IO.Path.GetRelativePath(originPath, fileNode.Path);
            using (System.IO.FileStream fs = System.IO.File.OpenRead(fileNode.Path))
            {
                this.Notify(relativePath);
                this.streamWriter.WriteLine(string.Format("{0} {1}", this.checksumCalculator.Calculate(fs), relativePath));
            }
        }

        public override void Visit(IDirectoryNode directoryNode)
        {
            if (this.waitHandle is not null)
                this.waitHandle.WaitOne();

            base.Visit(directoryNode);
        }

        public override void Subscribe(IObserver subscriber)
        {
            base.Subscribe(subscriber);
            if (this.checksumCalculator is IObservable)
            {
                ((IObservable)this.checksumCalculator).Subscribe(subscriber);
            }
        }
    }
}