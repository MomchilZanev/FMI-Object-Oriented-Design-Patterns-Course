using LabelsTask.Transformations;

namespace Tests
{
    [TestClass]
    public class TextTransformationTests
    {
        [TestMethod]
        public void CapitalizeTransformationTest_1()
        {
            string text = "abcDEF123";
            string capitalizedText = "AbcDEF123";

            CapitalizeTransformation transformation = new CapitalizeTransformation();

            Assert.AreEqual(capitalizedText, transformation.Transform(text));
        }

        [TestMethod]
        public void CapitalizeTransformationTest_2_AlreadyCapitalized()
        {
            string text = "AbcDEF123";

            CapitalizeTransformation transformation = new CapitalizeTransformation();

            Assert.AreEqual(text, transformation.Transform(text));
        }

        [TestMethod]
        public void CapitalizeTransformationTest_3_NothingToCapitalize()
        {
            string text1 = " abcDEF123";
            string text2 = "!abcDEF123";
            string text3 = "0abcDEF123";

            CapitalizeTransformation transformation = new CapitalizeTransformation();

            Assert.AreEqual(text1, transformation.Transform(text1));
            Assert.AreEqual(text2, transformation.Transform(text2));
            Assert.AreEqual(text3, transformation.Transform(text3));
        }

        [TestMethod]
        public void CapitalizeTransformationTest_4_ReturnsEmptyIfNullIsGiven()
        {
            CapitalizeTransformation transformation = new CapitalizeTransformation();

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }

        [TestMethod]
        public void TrimLeftTransformationTest_1()
        {
            string text1 = " abc 123 ";
            string text1Trimmed = "abc 123 ";

            string text2 = "   abc 123";
            string text2Trimmed = "abc 123";

            string text3 = "     abc 123  ";
            string text3Trimmed = "abc 123  ";

            TrimLeftTransformation transformation = new TrimLeftTransformation();

            Assert.AreEqual(text1Trimmed, transformation.Transform(text1));
            Assert.AreEqual(text2Trimmed, transformation.Transform(text2));
            Assert.AreEqual(text3Trimmed, transformation.Transform(text3));
        }

        [TestMethod]
        public void TrimLeftTransformationTest_2_NothingToTrim()
        {
            string text = "abc 123 ";

            TrimLeftTransformation transformation = new TrimLeftTransformation();

            Assert.AreEqual(text, transformation.Transform(text));
        }

        [TestMethod]
        public void TrimLeftTransformationTest_3_ReturnsEmptyIfNullIsGiven()
        {
            TrimLeftTransformation transformation = new TrimLeftTransformation();

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }

        [TestMethod]
        public void TrimRightTransformationTest_1()
        {
            string text1 = " abc 123 ";
            string text1Trimmed = " abc 123";

            string text2 = "abc 123  ";
            string text2Trimmed = "abc 123";

            string text3 = "     abc 123      ";
            string text3Trimmed = "     abc 123";

            TrimRightTransformation transformation = new TrimRightTransformation();

            Assert.AreEqual(text1Trimmed, transformation.Transform(text1));
            Assert.AreEqual(text2Trimmed, transformation.Transform(text2));
            Assert.AreEqual(text3Trimmed, transformation.Transform(text3));
        }

        [TestMethod]
        public void TrimRightTransformationTest_2_NothingToTrim()
        {
            string text = "     abc 123";

            TrimRightTransformation transformation = new TrimRightTransformation();

            Assert.AreEqual(text, transformation.Transform(text));
        }

        [TestMethod]
        public void TrimRightTransformationTest_3_ReturnsEmptyIfNullIsGiven()
        {
            TrimRightTransformation transformation = new TrimRightTransformation();

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }

        [TestMethod]
        public void NormalizeSpaceTransformationTest_1()
        {
            string text = "     abc   123   _ - _  ";
            string normalizedText = " abc 123 _ - _ ";

            NormalizeSpaceTransformation transformation = new NormalizeSpaceTransformation();

            Assert.AreEqual(normalizedText, transformation.Transform(text));
        }

        [TestMethod]
        public void NormalizeSpaceTransformationTest_2_NothingToNormalize()
        {
            string text = "abc 123";

            NormalizeSpaceTransformation transformation = new NormalizeSpaceTransformation();

            Assert.AreEqual(text, transformation.Transform(text));
        }

        [TestMethod]
        public void NormalizeSpaceTransformationTest_3_ReturnsEmptyIfNullIsGiven()
        {
            NormalizeSpaceTransformation transformation = new NormalizeSpaceTransformation();

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }

        [TestMethod]
        public void DecorateTransformationTest_1()
        {
            string text = "abc 123";
            string decoratedText = "-={ abc 123 }=-";

            DecorateTransformation transformation = new DecorateTransformation();

            Assert.AreEqual(decoratedText, transformation.Transform(text));
        }

        [TestMethod]
        public void DecorateTransformationTest_2_HasLeadingAndTrailingWhiteSpaces()
        {
            string text = "     abc 123  ";
            string decoratedText = "-={ abc 123 }=-";

            DecorateTransformation transformation = new DecorateTransformation();

            Assert.AreEqual(decoratedText, transformation.Transform(text));
        }

        [TestMethod]
        public void DecorateTransformationTest_3_EmptyOrBlankText()
        {
            string empty = string.Empty;
            string whiteSpace = " ";
            string blank = "       ";

            string decoratedText = "-={ }=-";

            DecorateTransformation transformation = new DecorateTransformation();

            Assert.AreEqual(decoratedText, transformation.Transform(empty));
            Assert.AreEqual(decoratedText, transformation.Transform(whiteSpace));
            Assert.AreEqual(decoratedText, transformation.Transform(blank));
        }

        [TestMethod]
        public void DecorateTransformationTest_4_NullText()
        {
            string decoratedText = "-={ }=-";

            DecorateTransformation transformation = new DecorateTransformation();

            Assert.AreEqual(decoratedText, transformation.Transform(null));
        }

        [TestMethod]
        public void ReplaceTransformationTest_1()
        {
            string a = "abc";
            string b = "0";
            string text = " abc 123 aBc ABCdef aabcc";
            string replacedText = " 0 123 aBc ABCdef a0c";

            ReplaceTransformation transformation = new ReplaceTransformation(a, b);

            Assert.AreEqual(replacedText, transformation.Transform(text));
        }

        [TestMethod]
        public void ReplaceTransformationTest_2_NullOrEmptyA()
        {
            string b = "0";
            string text = " abc 123 aBc ABCdef aabcc";

            ReplaceTransformation transformation1 = new ReplaceTransformation(string.Empty, b);
            ReplaceTransformation transformation2 = new ReplaceTransformation(null, b);

            Assert.AreEqual(text, transformation1.Transform(text));
            Assert.AreEqual(text, transformation2.Transform(text));
        }

        [TestMethod]
        public void ReplaceTransformationTest_3_NullOrEmptyB()
        {
            string a = "abc";
            string text = " abc 123 aBc ABCdef aabcc";
            string replacedText = "  123 aBc ABCdef ac";

            ReplaceTransformation transformation1 = new ReplaceTransformation(a, string.Empty);
            ReplaceTransformation transformation2 = new ReplaceTransformation(a, null);

            Assert.AreEqual(replacedText, transformation1.Transform(text));
            Assert.AreEqual(replacedText, transformation2.Transform(text));
        }

        [TestMethod]
        public void ReplaceTransformationTest_4_ReturnsEmptyIfNullIsGiven()
        {
            string a = "abc";
            string b = "0";

            ReplaceTransformation transformation = new ReplaceTransformation(a, b);

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }

        [TestMethod]
        public void CensorTransformationTest_1()
        {
            string w = "abc";
            string text = "abc AbC aabbcc abcabc 123 DEF";
            string censoredText = "*** AbC aabbcc ****** 123 DEF";

            CensorTransformation transformation = new CensorTransformation(w);

            Assert.AreEqual(censoredText, transformation.Transform(text));
        }

        [TestMethod]
        public void CensorTransformationTest_2_NullOrEmptyW()
        {
            string text = "abc AbC aabbcc abcabc 123 DEF";

            CensorTransformation transformation1 = new CensorTransformation(string.Empty);
            CensorTransformation transformation2 = new CensorTransformation(null);

            Assert.AreEqual(text, transformation1.Transform(text));
            Assert.AreEqual(text, transformation2.Transform(text));
        }

        [TestMethod]
        public void CensorTransformationTest_3_ReturnsEmptyIfNullIsGiven()
        {
            string w = "abc";

            CensorTransformation transformation = new CensorTransformation(w);

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }
    }
}