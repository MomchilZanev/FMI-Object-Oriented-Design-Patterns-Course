namespace Checksums.FileStructure.Builders
{
    public class RespectLinksDirectoryStructureBuilder : DirectoryStructureBuilderBase
    {
        private HashSet<string> visited;

        public RespectLinksDirectoryStructureBuilder()
        {
            this.visited = new HashSet<string>();
        }

        public RespectLinksDirectoryStructureBuilder(HashSet<string> visited)
        {
            this.visited = visited;
        }

        public override void AddFiles()
        {
            if (this.directory is null)
                return;

            foreach (string filePath in Directory.GetFiles(this.directory.Path))
            {
                string targetFilePath = this.getRealTargetPath(filePath);
                if (this.visited.Contains(targetFilePath))
                    continue;

                if (File.GetAttributes(targetFilePath).HasFlag(FileAttributes.Directory))
                {
                    DirectoryStructureBuilderBase builder = new RespectLinksDirectoryStructureBuilder(this.visited);
                    builder.Reset(targetFilePath);
                    builder.AddFiles();
                    builder.AddSubDirectories();

                    this.directory.AddChild(builder.GetProduct());
                    this.visited.Add(targetFilePath);
                }
                else
                {
                    FileNode fileNode = new FileNode(targetFilePath, (ulong)new FileInfo(targetFilePath).Length);
                    this.directory.AddChild(fileNode);
                    this.visited.Add(targetFilePath);
                }
            }
        }

        public override void AddSubDirectories()
        {
            if (this.directory is null)
                return;

            DirectoryStructureBuilderBase builder = new RespectLinksDirectoryStructureBuilder(this.visited);
            foreach (string subdirectoryPath in Directory.GetDirectories(this.directory.Path))
            {
                if (this.visited.Contains(subdirectoryPath))
                    continue;

                builder.Reset(subdirectoryPath);
                builder.AddFiles();
                builder.AddSubDirectories();

                this.directory.AddChild(builder.GetProduct());
                this.visited.Add(subdirectoryPath);
            }
        }

        public override void SetupDirectory(string path)
        {
            base.SetupDirectory(path);
            this.visited.Add(path);
        }

        private string getRealTargetPath(string path)
        {
            if (Path.GetExtension(path).ToLower() == ".lnk")
            {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(path);

                return shortcut.TargetPath;
            }

            return path;
        }
    }
}