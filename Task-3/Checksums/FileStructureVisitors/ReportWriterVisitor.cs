using Checksums.FileStructure;

namespace Checksums.FileStructureVisitors
{
    public class ReportWriterVisitor : FileStructureVisitorBase
    {
        public ReportWriterVisitor(string originPath) : base(originPath)
        { }

        public override void Visit(IFileNode fileNode)
        {
            Console.WriteLine(string.Format("{0} : {1} bytes", Path.GetRelativePath(this.originPath, fileNode.Path), fileNode.Size));
        }
    }
}