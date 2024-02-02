using Checksums.Progress;
using System.Security.Cryptography;

namespace Checksums.ChecksumCalculators
{
    public class CommonChecksumCalculator : ObservableBase, IChecksumCalculator
    {
        public const string unsupportedHashAlgorithmExceptionMessage = "Unsupported hash algorithm.";

        private const int KB = 1024;
        private List<string> supportedHashAlgorithms;
        private int hashAlgorithmIndex;

        public CommonChecksumCalculator(string hashAlgorithm)
        {
            this.supportedHashAlgorithms = new List<string> { "SHA1", "SHA256", "SHA384", "SHA512", "MD5" };

            if (!this.supportedHashAlgorithms.Contains(hashAlgorithm))
                throw new ArgumentException(unsupportedHashAlgorithmExceptionMessage);

            this.hashAlgorithmIndex = this.supportedHashAlgorithms.IndexOf(hashAlgorithm);
        }

        public string Calculate(Stream inputStream)
        {
            string result = string.Empty;

            long originalPosition = inputStream.Position;
            using (HashAlgorithm algorithm = this.GetHashAlgorithm())
            {
                inputStream.Position = 0;

                int bytesRead = 0;
                byte[] buffer = new byte[KB];
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    algorithm.TransformBlock(buffer, 0, bytesRead, buffer, 0);

                    // Notify for every 5 KBs read and on the last block
                    if (inputStream.Position % (5 * KB) == 0 || bytesRead < KB)
                    {
                        this.Notify(inputStream.Position);
                    }
                }
                algorithm.TransformFinalBlock(buffer, 0, 0);
                byte[] hash = algorithm.Hash ?? new byte[0];

                if (hash.Length > 0)
                {
                    result = BitConverter.ToString(hash).Replace("-", "");
                }
            }
            inputStream.Position = originalPosition;

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
                    throw new ArgumentException(unsupportedHashAlgorithmExceptionMessage);
            }
        }
    }
}