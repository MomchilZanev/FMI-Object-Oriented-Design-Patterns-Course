using System.Diagnostics;

namespace Checksums.Progress
{
    public class ProgressReporter : Observer
    {
        private ulong totalBytesExpected;
        private ulong totalBytesRead;
        private string currentFile;
        private long currentFileBytesRead;
        private Stopwatch stopwatch;
        private ulong bytesReadSinceТicking;

        public ProgressReporter(ulong totalBytesExpected)
        {
            this.totalBytesExpected = totalBytesExpected;
            this.totalBytesRead = 0;
            this.currentFile = string.Empty;
            this.currentFileBytesRead = 0;
            this.stopwatch = new Stopwatch();
            this.bytesReadSinceТicking = 0;
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
                ulong newBytesRead = (ulong)(long)message - (ulong)this.currentFileBytesRead;
                this.totalBytesRead += newBytesRead;
                this.bytesReadSinceТicking += newBytesRead;
                this.currentFileBytesRead = (long)message;
            }
            else
                throw new ArgumentException("Unexpected message");

            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            // Refresh ETA time sample every 10 seconds
            if (!this.stopwatch.IsRunning || this.stopwatch.Elapsed.TotalSeconds > 10)
            {
                this.stopwatch.Restart();
                this.bytesReadSinceТicking = 0;
            }

            double completion = (double)totalBytesRead / (double)totalBytesExpected;

            ulong bytesRemaining = this.totalBytesExpected - this.totalBytesRead;
            double bytesPerMillisecond = this.bytesReadSinceТicking / this.stopwatch.Elapsed.TotalMilliseconds;
            double remainingMilliseconds = bytesRemaining / bytesPerMillisecond;

            TimeSpan? remainingTime = null;
            try
            {
                remainingTime = TimeSpan.FromMilliseconds(remainingMilliseconds);
            }
            catch (Exception)
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