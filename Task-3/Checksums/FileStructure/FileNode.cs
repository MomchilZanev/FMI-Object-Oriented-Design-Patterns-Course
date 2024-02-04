using Checksums.FileStructureVisitors;

namespace Checksums.FileStructure
{
    public class FileNode : IFileNode
    {
        protected string path;
        protected ulong size;

        public FileNode(string path, ulong size = 0)
        {
            this.path = path;
            this.size = size;
        }

        public string Path { get => this.path; }
        public ulong Size { get => this.size; }

        public virtual void Accept(IFileStructureVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}