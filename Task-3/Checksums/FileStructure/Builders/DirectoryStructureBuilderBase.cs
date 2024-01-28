namespace Checksums.FileStructure.Builders
{
    public abstract class DirectoryStructureBuilderBase
    {
        protected DirectoryNode? directory;

        public virtual void SetupDirectory(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(string.Format("Directory \"{0}\" does not exist.", path));

            this.directory = new DirectoryNode(path);
        }

        public abstract void AddFiles();

        public abstract void AddSubDirectories();

        public void Reset(string path)
        {
            this.directory = null;
            this.SetupDirectory(path);
        }

        public DirectoryNode GetProduct()
        {
            if (this.directory is null)
                throw new Exception("No product has been built yet.");

            return this.directory;
        }
    }
}