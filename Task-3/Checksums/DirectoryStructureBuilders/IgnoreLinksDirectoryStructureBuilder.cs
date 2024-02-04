using Checksums.FileStructure;

namespace Checksums.DirectoryStructureBuilders
{
    public class IgnoreLinksDirectoryStructureBuilder : DirectoryStructureBuilderBase
    {
        public override void AddFiles()
        {
            if (this.directory is null)
                return;

            foreach (string filePath in System.IO.Directory.GetFiles(this.directory.Path))
            {
                IFileNode fileNode = new FileNode(filePath, (ulong)new FileInfo(filePath).Length);
                this.directory.AddChild(fileNode);
            }
        }

        public override void AddSubDirectories()
        {
            if (this.directory is null)
                return;

            IDirectoryStructureBuilder builder = new IgnoreLinksDirectoryStructureBuilder();
            foreach (string subdirectoryPath in System.IO.Directory.GetDirectories(this.directory.Path))
            {
                builder.Reset(subdirectoryPath);
                builder.AddFiles();
                builder.AddSubDirectories();

                this.directory.AddChild(builder.GetProduct());
            }
        }
    }
}