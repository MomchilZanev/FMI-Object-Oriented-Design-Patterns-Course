using Checksums.Progress;
using System.Security.Cryptography;

namespace Checksums.ChecksumCalculators
{
    public class CommonChecksumCalculator : ObservableBase, IChecksumCalculator
    {
        public const string unsupportedHashAlgorithmExceptionMessage = "Unsupported hash algorithm.";
        private const int KB = 1024;

        private List<string> supportedHashAlgorithms = new List<string> { "SHA1", "SHA256", "SHA384", "SHA512", "MD5" };
        private int hashAlgorithmIndex;
        private EventWaitHandle? waitHandle;

        public CommonChecksumCalculator(string hashAlgorithm, EventWaitHandle? waitHandle = null)
        {
            if (!this.supportedHashAlgorithms.Contains(hashAlgorithm))
                throw new ArgumentException(CommonChecksumCalculator.unsupportedHashAlgorithmExceptionMessage);

            this.hashAlgorithmIndex = this.supportedHashAlgorithms.IndexOf(hashAlgorithm);
            this.waitHandle = waitHandle;
        }

        public string Calculate(System.IO.Stream inputStream)
        {
            string result = string.Empty;
            using (HashAlgorithm algorithm = this.GetHashAlgorithm())
            {
                int bytesRead = 0;
                byte[] buffer = new byte[CommonChecksumCalculator.KB];
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (this.waitHandle is not null)
                        this.waitHandle.WaitOne();

                    algorithm.TransformBlock(buffer, 0, bytesRead, buffer, 0);

                    // Notify on last block and for every 5 KBs read
                    if (bytesRead < buffer.Length || inputStream.Position % (5 * CommonChecksumCalculator.KB) == 0)
                        this.Notify(inputStream.Position);
                }
                algorithm.TransformFinalBlock(buffer, 0, 0);
                byte[] hash = algorithm.Hash ?? new byte[0];

                if (hash.Length > 0)
                    result = System.BitConverter.ToString(hash).Replace("-", "");
            }

            return result;
        }

        private HashAlgorithm GetHashAlgorithm()
        {
            switch (this.supportedHashAlgorithms[this.hashAlgorithmIndex])
            {
                case "SHA1":
                    return SHA1.Create();
                case "SHA256":
                    return SHA256.Create();
                case "SHA384":
                    return SHA384.Create();
                case "SHA512":
                    return SHA512.Create();
                case "MD5":
                    return MD5.Create();
                default:
                    throw new ArgumentException(CommonChecksumCalculator.unsupportedHashAlgorithmExceptionMessage);
            }
        }
    }
}