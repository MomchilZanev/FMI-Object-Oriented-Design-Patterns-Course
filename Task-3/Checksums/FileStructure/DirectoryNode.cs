using Checksums.FileStructure.Visitors;

namespace Checksums.FileStructure
{
    public class DirectoryNode : FileNodeBase
    {
        private List<FileNodeBase> children;

        public DirectoryNode(string path) : base(path)
        {
            this.children = new List<FileNodeBase>();
        }

        public IReadOnlyList<FileNodeBase> Children { get => this.children.AsReadOnly(); }

        public void AddChild(FileNodeBase child)
        {
            this.children.Add(child);
            this.size += child.Size;
        }

        public override void Accept(FileNodeVisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }
}