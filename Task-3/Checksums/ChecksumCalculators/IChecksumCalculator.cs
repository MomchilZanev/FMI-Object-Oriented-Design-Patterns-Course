namespace Checksums.ChecksumCalculators
{
    public interface IChecksumCalculator
    {
        public string Calculate(Stream inputStream);
    }
}