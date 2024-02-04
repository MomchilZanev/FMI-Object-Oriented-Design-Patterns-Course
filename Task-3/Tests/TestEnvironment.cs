namespace Tests
{
    [TestClass]
    public class TestEnvironment
    {
        private static string currentDirectory = System.IO.Directory.GetCurrentDirectory();

        [AssemblyInitialize]
        public static void SetUpTestDirectory(TestContext testContext)
        {
            // Create test directory structure
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "Sub Sub Directory"));
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Regular Directory", "Empty Subdirectory"));
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Linked Directory"));

            // Create test files
            System.IO.File.WriteAllText(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Linked File.txt"), "Linked File");
            System.IO.File.WriteAllText(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Regular File.txt"), "Regular File");
            System.IO.File.WriteAllText(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Linked Directory", "File in Linked Directory.txt"), "File in Linked Directory");
            System.IO.File.WriteAllText(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Regular Directory", "Regular File in Regular Directory.txt"), "Regular File in Regular Directory");
            System.IO.File.WriteAllText(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Starting Directory", "File 1.txt"), "File 1");
            System.IO.File.WriteAllText(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Starting Directory", "File 2.txt"), "File 2");
            System.IO.File.WriteAllText(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "File 3.txt"), "File 3");
            System.IO.File.WriteAllText(System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "Sub Sub Directory", "File 4.txt"), "File 4");

            // Create test shortcuts
            CreateShortcut(
                System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Starting Directory", "Directory Shortcut.lnk"),
                System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Linked Directory"));
            CreateShortcut(
                System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "File Shortcut.lnk"),
                System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Linked File.txt"));
            CreateShortcut(
                System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory", "Starting Directory", "Sub Directory", "Sub Sub Directory", "Root Directory Shortcut.lnk"),
                System.IO.Path.Combine(TestEnvironment.currentDirectory, "Test Directory"));
        }

        [AssemblyCleanup]
        public static void CleanUpTestDirectory()
        {
            // Remove test directory
            System.IO.Directory.Delete(System.IO.Path.Combine(currentDirectory, "Test Directory"), true);
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