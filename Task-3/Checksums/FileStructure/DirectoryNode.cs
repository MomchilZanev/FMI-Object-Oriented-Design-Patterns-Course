using Checksums.FileStructureVisitors;

namespace Checksums.FileStructure
{
    public class DirectoryNode : FileNode, IDirectoryNode
    {
        private List<IFileNode> children;

        public DirectoryNode(string path) : base(path)
        {
            this.children = new List<IFileNode>();
        }

        public IReadOnlyList<IFileNode> Children { get => this.children.AsReadOnly(); }

        public void AddChild(IFileNode child)
        {
            this.children.Add(child);
            this.size += child.Size;
        }

        public override void Accept(IFileStructureVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}