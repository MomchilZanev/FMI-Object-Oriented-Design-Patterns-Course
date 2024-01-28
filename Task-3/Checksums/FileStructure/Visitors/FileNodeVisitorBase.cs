namespace Checksums.FileStructure.Visitors
{
    public abstract class FileNodeVisitorBase
    {
        protected string originPath;

        public FileNodeVisitorBase(string originPath)
        {
            this.originPath = originPath;
        }

        public virtual void Visit(FileNode fileNode)
        {
            this.ProcessFile(fileNode);
        }

        public virtual void Visit(DirectoryNode directoryNode)
        {
            foreach (FileNodeBase child in directoryNode.Children)
            {
                if (child is FileNode)
                {
                    this.ProcessFile((FileNode)child);
                }
                else
                {
                    this.Visit((DirectoryNode)child);
                }
            }
        }

        public abstract void ProcessFile(FileNode fileNode);
    }
}