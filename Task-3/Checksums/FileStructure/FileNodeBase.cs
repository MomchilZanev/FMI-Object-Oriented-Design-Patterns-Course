using Checksums.FileStructure.Visitors;

namespace Checksums.FileStructure
{
    public abstract class FileNodeBase
    {
        protected string path;
        protected ulong size;

        public FileNodeBase(string path, ulong size = 0)
        {
            this.path = path;
            this.size = size;
        }

        public string Name { get => System.IO.Path.GetFileName(this.path); }
        public string Path { get => this.path; }
        public ulong Size { get => this.size; } // in bytes

        public abstract void Accept(FileNodeVisitorBase visitor);
    }
}