using System.Diagnostics;

namespace Checksums.Progress
{
    public class ProgressReporter : Observer
    {
        private Stopwatch stopwatch;

        private ulong totalBytesExpected;
        private ulong totalBytesRead;

        private long currentFileBytesRead;
        private string currentFile;

        public ProgressReporter(ulong totalBytesExpected)
        {
            this.stopwatch = new Stopwatch();

            this.totalBytesExpected = totalBytesExpected;
            this.totalBytesRead = 0;

            this.currentFileBytesRead = 0;
            this.currentFile = "(nothing)";
        }

        public void Update(object message)
        {
            if (message is string)
            {
                this.currentFile = (string)message;
                this.currentFileBytesRead = 0;
                Console.WriteLine();
            }
            else if (message is long)
            {
                this.totalBytesRead += (ulong)(long)message - (ulong)this.currentFileBytesRead;
                this.currentFileBytesRead = (long)message;
            }
            else
            {
                throw new ArgumentException("Unexpected message");
            }
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            if (!this.stopwatch.IsRunning)
            {
                this.stopwatch.Start();
            }

            double completion = (double)totalBytesRead / (double)totalBytesExpected;

            ulong bytesRemaining = this.totalBytesExpected - this.totalBytesRead;
            double bytesPerMillisecond = this.totalBytesRead / this.stopwatch.Elapsed.TotalMilliseconds;
            double remainingMilliseconds = bytesRemaining / bytesPerMillisecond;

            TimeSpan? remainingTime = null;
            try
            {
                remainingTime = TimeSpan.FromMilliseconds(remainingMilliseconds);
            }
            catch (OverflowException)
            {
                remainingTime = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59).Add(TimeSpan.FromSeconds(59)));
            }

            Console.SetCursorPosition(0, Console.CursorTop - 1); // Overwrite progress line
            Console.Write(string.Format("\rProcessing: {0}... {1} byte(s) read{2}Progress: {3:P2} done, ETA: {4:hh\\:mm\\:ss}",
                this.currentFile,
                this.currentFileBytesRead,
                Environment.NewLine,
                completion,
                remainingTime));
        }

        public override bool Equals(object? other)
        {
            return other is null ? false : other is ProgressReporter;
        }
    }
}