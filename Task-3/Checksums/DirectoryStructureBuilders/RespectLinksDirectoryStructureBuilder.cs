using Checksums.FileStructure;

namespace Checksums.DirectoryStructureBuilders
{
    public class RespectLinksDirectoryStructureBuilder : DirectoryStructureBuilderBase
    {
        private HashSet<string> visited;

        public RespectLinksDirectoryStructureBuilder(HashSet<string>? visited = null)
        {
            this.visited = visited ?? new HashSet<string>();
        }

        public override void SetupDirectory(string path)
        {
            base.SetupDirectory(path);
            this.visited.Add(path);
        }

        public override void AddFiles()
        {
            if (this.directory is null)
                return;

            foreach (string filePath in System.IO.Directory.GetFiles(this.directory.Path))
            {
                string targetFilePath = getRealTargetPath(filePath);
                if (this.visited.Contains(targetFilePath))
                    continue;

                if (System.IO.File.GetAttributes(targetFilePath).HasFlag(FileAttributes.Directory))
                {
                    IDirectoryStructureBuilder builder = new RespectLinksDirectoryStructureBuilder(this.visited);
                    builder.Reset(targetFilePath);
                    builder.AddFiles();
                    builder.AddSubDirectories();

                    this.directory.AddChild(builder.GetProduct());
                    this.visited.Add(targetFilePath);
                }
                else
                {
                    IFileNode fileNode = new FileNode(targetFilePath, (ulong)new FileInfo(targetFilePath).Length);
                    this.directory.AddChild(fileNode);
                    this.visited.Add(targetFilePath);
                }
            }
        }

        public override void AddSubDirectories()
        {
            if (this.directory is null)
                return;

            IDirectoryStructureBuilder builder = new RespectLinksDirectoryStructureBuilder(this.visited);
            foreach (string subdirectoryPath in System.IO.Directory.GetDirectories(this.directory.Path))
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

        private string getRealTargetPath(string path)
        {
            string realTargetPath = path;

            if (System.IO.Path.GetExtension(path).ToLower() == ".lnk")
            {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(path);

                realTargetPath = shortcut.TargetPath;
            }

            return realTargetPath;
        }
    }
}