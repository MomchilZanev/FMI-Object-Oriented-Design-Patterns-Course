using Checksums.FileStructure;

namespace Checksums.FileStructureVisitors
{
    public interface IFileStructureVisitor
    {
        void Visit(IFileNode fileNode);
        void Visit(IDirectoryNode directoryNode);
    }
}