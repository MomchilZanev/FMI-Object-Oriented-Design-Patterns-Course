namespace Checksums.ChecksumCalculators
{
    public interface IChecksumCalculator
    {
        string Calculate(System.IO.Stream inputStream);
    }
}