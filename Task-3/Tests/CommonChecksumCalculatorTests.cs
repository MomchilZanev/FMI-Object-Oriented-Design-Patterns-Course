using Checksums.ChecksumCalculators;
using System.Text;

namespace Tests
{
    [TestClass]
    public class CommonChecksumCalculatorTests
    {
        [TestMethod]
        public void ThrowsIfUnsupported()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new CommonChecksumCalculator("Invalid");
            }, CommonChecksumCalculator.unsupportedHashAlgorithmExceptionMessage);
        }

        [TestMethod]
        public void SHA1()
        {
            string input1 = "abc";
            string input2 = "Hello world!";

            string expectedResult1 = "A9993E364706816ABA3E25717850C26C9CD0D89D";
            string expectedResult2 = "D3486AE9136E7856BC42212385EA797094475802";

            IChecksumCalculator md5CheckSumCalculator = new CommonChecksumCalculator("SHA1");

            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input1)))
            {
                Assert.AreEqual(expectedResult1, md5CheckSumCalculator.Calculate(stream));
            }
            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input2)))
            {
                Assert.AreEqual(expectedResult2, md5CheckSumCalculator.Calculate(stream));
            }
        }

        [TestMethod]
        public void SHA256()
        {
            string input1 = "abc";
            string input2 = "Hello world!";

            string expectedResult1 = "BA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD";
            string expectedResult2 = "C0535E4BE2B79FFD93291305436BF889314E4A3FAEC05ECFFCBB7DF31AD9E51A";

            IChecksumCalculator md5CheckSumCalculator = new CommonChecksumCalculator("SHA256");

            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input1)))
            {
                Assert.AreEqual(expectedResult1, md5CheckSumCalculator.Calculate(stream));
            }
            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input2)))
            {
                Assert.AreEqual(expectedResult2, md5CheckSumCalculator.Calculate(stream));
            }
        }

        [TestMethod]
        public void SHA384()
        {
            string input1 = "abc";
            string input2 = "Hello world!";

            string expectedResult1 = "CB00753F45A35E8BB5A03D699AC65007272C32AB0EDED1631A8B605A43FF5BED8086072BA1E7CC2358BAECA134C825A7";
            string expectedResult2 = "86255FA2C36E4B30969EAE17DC34C772CBEBDFC58B58403900BE87614EB1A34B8780263F255EB5E65CA9BBB8641CCCFE";

            IChecksumCalculator md5CheckSumCalculator = new CommonChecksumCalculator("SHA384");

            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input1)))
            {
                Assert.AreEqual(expectedResult1, md5CheckSumCalculator.Calculate(stream));
            }
            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input2)))
            {
                Assert.AreEqual(expectedResult2, md5CheckSumCalculator.Calculate(stream));
            }
        }

        [TestMethod]
        public void SHA512()
        {
            string input1 = "abc";
            string input2 = "Hello world!";

            string expectedResult1 = "DDAF35A193617ABACC417349AE20413112E6FA4E89A97EA20A9EEEE64B55D39A2192992A274FC1A836BA3C23A3FEEBBD454D4423643CE80E2A9AC94FA54CA49F";
            string expectedResult2 = "F6CDE2A0F819314CDDE55FC227D8D7DAE3D28CC556222A0A8AD66D91CCAD4AAD6094F517A2182360C9AACF6A3DC323162CB6FD8CDFFEDB0FE038F55E85FFB5B6";

            IChecksumCalculator md5CheckSumCalculator = new CommonChecksumCalculator("SHA512");

            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input1)))
            {
                Assert.AreEqual(expectedResult1, md5CheckSumCalculator.Calculate(stream));
            }
            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input2)))
            {
                Assert.AreEqual(expectedResult2, md5CheckSumCalculator.Calculate(stream));
            }
        }

        [TestMethod]
        public void MD5()
        {
            string input1 = "abc";
            string input2 = "Hello world!";

            string expectedResult1 = "900150983CD24FB0D6963F7D28E17F72";
            string expectedResult2 = "86FB269D190D2C85F6E0468CECA42A20";

            IChecksumCalculator md5CheckSumCalculator = new CommonChecksumCalculator("MD5");

            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input1)))
            {
                Assert.AreEqual(expectedResult1, md5CheckSumCalculator.Calculate(stream));
            }
            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(input2)))
            {
                Assert.AreEqual(expectedResult2, md5CheckSumCalculator.Calculate(stream));
            }
        }
    }
}