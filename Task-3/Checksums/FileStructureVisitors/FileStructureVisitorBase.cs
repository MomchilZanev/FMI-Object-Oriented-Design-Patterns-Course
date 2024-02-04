using Checksums.FileStructure;
using Checksums.Progress;

namespace Checksums.FileStructureVisitors
{
    public abstract class FileStructureVisitorBase : ObservableBase, IFileStructureVisitor
    {
        protected string originPath;

        public FileStructureVisitorBase(string originPath)
        {
            this.originPath = originPath;
        }

        public abstract void Visit(IFileNode fileNode);

        public virtual void Visit(IDirectoryNode directoryNode)
        {
            foreach (IFileNode child in directoryNode.Children)
            {
                if (child is IDirectoryNode)
                    this.Visit((IDirectoryNode)child);
                else
                    this.Visit(child);
            }
        }
    }
}