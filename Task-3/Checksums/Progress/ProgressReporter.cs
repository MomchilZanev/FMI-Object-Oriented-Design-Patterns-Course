using System.Diagnostics;

namespace Checksums.Progress
{
    public class ProgressReporter : IObserver
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

        public double Completion { get => (double)this.totalBytesRead / (double)this.totalBytesExpected; }
        public TimeSpan ETA
        {
            get
            {
                ulong bytesRemaining = this.totalBytesExpected - this.totalBytesRead;
                double bytesPerMillisecond = this.bytesReadSinceТicking / this.stopwatch.Elapsed.TotalMilliseconds;
                double remainingMilliseconds = bytesRemaining / bytesPerMillisecond;

                TimeSpan eta;
                try
                {
                    eta = TimeSpan.FromMilliseconds(remainingMilliseconds);
                }
                catch (Exception)
                {
                    eta = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59).Add(TimeSpan.FromSeconds(59)));
                }

                return eta;
            }
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
                throw new ArgumentException("Unexpected message.");

            this.RefreshStopwatch();
            this.RefreshDisplay();
        }

        // Refresh ETA time sample roughly every 10 seconds
        private void RefreshStopwatch()
        {
            if (!this.stopwatch.IsRunning || this.stopwatch.Elapsed.TotalSeconds > 10)
            {
                this.stopwatch.Restart();
                this.bytesReadSinceТicking = 0;
            }
        }

        private void RefreshDisplay()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 2); // Overwrite last progress line

            string processingLine = string.Format("Processing: {0}... {1} byte(s) read", this.currentFile, this.currentFileBytesRead);
            string progressLine = string.Format("Progress: {0:P2} done, ETA: {1:hh\\:mm\\:ss}", this.Completion, this.ETA);

            // Compensate for console word wrap
            processingLine = processingLine.Substring(0, int.Min(Console.BufferWidth, processingLine.Length));
            progressLine = progressLine.Substring(0, int.Min(Console.BufferWidth, progressLine.Length));

            Console.WriteLine(processingLine);
            Console.WriteLine(progressLine);
        }

        public override bool Equals(object? other)
        {
            return other is null ? false : other is ProgressReporter;
        }

        public override int GetHashCode()
        {
            return this.GetType().Name.GetHashCode();
        }
    }
}