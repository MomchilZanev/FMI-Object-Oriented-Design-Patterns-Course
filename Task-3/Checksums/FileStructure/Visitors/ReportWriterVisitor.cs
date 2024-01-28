namespace Checksums.FileStructure.Visitors
{
    public class ReportWriterVisitor : FileNodeVisitorBase
    {
        public ReportWriterVisitor(string originPath) : base(originPath)
        { }

        public override void ProcessFile(FileNode fileNode)
        {
            Console.WriteLine(string.Format("{0} : {1} bytes", Path.GetRelativePath(this.originPath, fileNode.Path), fileNode.Size));
        }
    }
}