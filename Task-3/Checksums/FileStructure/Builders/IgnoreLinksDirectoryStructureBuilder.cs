namespace Checksums.FileStructure.Builders
{
    public class IgnoreLinksDirectoryStructureBuilder : DirectoryStructureBuilderBase
    {
        public override void AddFiles()
        {
            if (this.directory is null)
                return;

            foreach (string filePath in Directory.GetFiles(this.directory.Path))
            {
                FileNode fileNode = new FileNode(filePath, (ulong)new FileInfo(filePath).Length);
                this.directory.AddChild(fileNode);
            }
        }

        public override void AddSubDirectories()
        {
            if (this.directory is null)
                return;

            DirectoryStructureBuilderBase builder = new IgnoreLinksDirectoryStructureBuilder();
            foreach (string subdirectoryPath in Directory.GetDirectories(this.directory.Path))
            {
                builder.Reset(subdirectoryPath);
                builder.AddFiles();
                builder.AddSubDirectories();

                this.directory.AddChild(builder.GetProduct());
            }
        }
    }
}
