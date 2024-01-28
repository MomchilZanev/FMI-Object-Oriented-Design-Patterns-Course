namespace Tests
{
    [TestClass]
    public class TestEnvironment
    {
        private static string currentDirectory = Directory.GetCurrentDirectory();

        [AssemblyInitialize]
        public static void SetUpTestDirectory(TestContext testContext)
        {
            // Create test directory structure
            Directory.CreateDirectory(Path.Combine(currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "Sub Sub Directory"));
            Directory.CreateDirectory(Path.Combine(currentDirectory, "Test Directory", "Regular Directory", "Empty Subdirectory"));
            Directory.CreateDirectory(Path.Combine(currentDirectory, "Test Directory", "Linked Directory"));

            // Create test files
            File.WriteAllText(Path.Combine(currentDirectory, "Test Directory", "Linked File.txt"), "Linked File");
            File.WriteAllText(Path.Combine(currentDirectory, "Test Directory", "Regular File.txt"), "Regular File");
            File.WriteAllText(Path.Combine(currentDirectory, "Test Directory", "Linked Directory", "File in Linked Directory.txt"), "File in Linked Directory");
            File.WriteAllText(Path.Combine(currentDirectory, "Test Directory", "Regular Directory", "Regular File in Regular Directory.txt"), "Regular File in Regular Directory");
            File.WriteAllText(Path.Combine(currentDirectory, "Test Directory", "Starting Directory", "File 1.txt"), "File 1");
            File.WriteAllText(Path.Combine(currentDirectory, "Test Directory", "Starting Directory", "File 2.txt"), "File 2");
            File.WriteAllText(Path.Combine(currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "File 3.txt"), "File 3");
            File.WriteAllText(Path.Combine(currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "Sub Sub Directory", "File 4.txt"), "File 4");

            // Create test shortcuts
            CreateShortcut(
                Path.Combine(currentDirectory, "Test Directory", "Starting Directory", "Directory Shortcut.lnk"),
                Path.Combine(currentDirectory, "Test Directory", "Linked Directory"));
            CreateShortcut(
                Path.Combine(currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "File Shortcut.lnk"),
                Path.Combine(currentDirectory, "Test Directory", "Linked File.txt"));
            CreateShortcut(
                Path.Combine(currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "Sub Sub Directory", "Root Directory Shortcut.lnk"), 
                Path.Combine(currentDirectory, "Test Directory"));
        }

        [AssemblyCleanup]
        public static void CleanUpTestDirectory()
        {
            // Remove test directory
            Directory.Delete(Path.Combine(currentDirectory, "Test Directory"), true);
        }

        private static void CreateShortcut(string shortcutPath, string targetPath)
        {
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);

            shortcut.TargetPath = targetPath;
            shortcut.Save();
        }
    }
}
