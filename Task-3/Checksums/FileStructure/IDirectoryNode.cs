namespace Checksums.FileStructure
{
    public interface IDirectoryNode : IFileNode
    {
        IReadOnlyList<IFileNode> Children { get; }

        void AddChild(IFileNode child);
    }
}
