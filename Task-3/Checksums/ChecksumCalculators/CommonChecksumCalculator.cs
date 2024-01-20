using System.Security.Cryptography;

namespace Checksums.ChecksumCalculators
{
    public class CommonChecksumCalculator : IChecksumCalculator
    {
        public const string unsupportedHashAlgorithmExceptionMessage = "Unsupported hash algorithm.";

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
                byte[] hash = algorithm.ComputeHash(inputStream);
                result = BitConverter.ToString(hash).Replace("-", "");
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