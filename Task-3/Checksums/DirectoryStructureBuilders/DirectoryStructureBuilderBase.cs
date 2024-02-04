using Checksums.FileStructure;

namespace Checksums.DirectoryStructureBuilders
{
    public abstract class DirectoryStructureBuilderBase : IDirectoryStructureBuilder
    {
        protected IDirectoryNode? directory;

        public virtual void SetupDirectory(string path)
        {
            if (!System.IO.Directory.Exists(path))
                throw new DirectoryNotFoundException(string.Format("Directory \"{0}\" does not exist.", path));

            this.directory = new DirectoryNode(path);
        }

        public void Reset(string path)
        {
            this.directory = null;
            SetupDirectory(path);
        }

        public abstract void AddFiles();
        public abstract void AddSubDirectories();


        public virtual IDirectoryNode GetProduct()
        {
            if (this.directory is null)
                throw new Exception("No product has been built yet.");

            return this.directory;
        }
    }
}