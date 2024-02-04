using Checksums.FileStructureVisitors;

namespace Checksums.FileStructure
{
    public interface IFileNode
    {
        string Path { get; }
        ulong Size { get; } // in bytes

        void Accept(IFileStructureVisitor visitor);
    }
}
