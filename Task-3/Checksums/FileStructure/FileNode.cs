using Checksums.FileStructure.Visitors;

namespace Checksums.FileStructure
{
    public class FileNode : FileNodeBase
    {
        public FileNode(string path, ulong size) : base(path, size)
        { }

        public string Extension { get => System.IO.Path.GetExtension(this.path) ?? string.Empty; }

        public override void Accept(FileNodeVisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }
}