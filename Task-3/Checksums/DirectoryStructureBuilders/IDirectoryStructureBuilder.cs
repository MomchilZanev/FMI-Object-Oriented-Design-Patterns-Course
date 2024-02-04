using Checksums.FileStructure;

namespace Checksums.DirectoryStructureBuilders
{
    public interface IDirectoryStructureBuilder
    {
        void SetupDirectory(string path);
        void Reset(string path);

        void AddFiles();
        void AddSubDirectories();

        IDirectoryNode GetProduct();
    }
}
