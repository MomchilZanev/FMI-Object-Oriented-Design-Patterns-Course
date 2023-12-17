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

            ITextTransformation transformation = new CapitalizeTransformation();

            Assert.AreEqual(capitalizedText, transformation.Transform(text));
        }

        [TestMethod]
        public void CapitalizeTransformationTest_2_AlreadyCapitalized()
        {
            string text = "AbcDEF123";

            ITextTransformation transformation = new CapitalizeTransformation();

            Assert.AreEqual(text, transformation.Transform(text));
        }

        [TestMethod]
        public void CapitalizeTransformationTest_3_NothingToCapitalize()
        {
            string text1 = " abcDEF123";
            string text2 = "!abcDEF123";
            string text3 = "0abcDEF123";

            ITextTransformation transformation = new CapitalizeTransformation();

            Assert.AreEqual(text1, transformation.Transform(text1));
            Assert.AreEqual(text2, transformation.Transform(text2));
            Assert.AreEqual(text3, transformation.Transform(text3));
        }

        [TestMethod]
        public void CapitalizeTransformationTest_4_ReturnsEmptyIfNullIsGiven()
        {
            ITextTransformation transformation = new CapitalizeTransformation();

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

            ITextTransformation transformation = new TrimLeftTransformation();

            Assert.AreEqual(text1Trimmed, transformation.Transform(text1));
            Assert.AreEqual(text2Trimmed, transformation.Transform(text2));
            Assert.AreEqual(text3Trimmed, transformation.Transform(text3));
        }

        [TestMethod]
        public void TrimLeftTransformationTest_2_NothingToTrim()
        {
            string text = "abc 123 ";

            ITextTransformation transformation = new TrimLeftTransformation();

            Assert.AreEqual(text, transformation.Transform(text));
        }

        [TestMethod]
        public void TrimLeftTransformationTest_3_ReturnsEmptyIfNullIsGiven()
        {
            ITextTransformation transformation = new TrimLeftTransformation();

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

            ITextTransformation transformation = new TrimRightTransformation();

            Assert.AreEqual(text1Trimmed, transformation.Transform(text1));
            Assert.AreEqual(text2Trimmed, transformation.Transform(text2));
            Assert.AreEqual(text3Trimmed, transformation.Transform(text3));
        }

        [TestMethod]
        public void TrimRightTransformationTest_2_NothingToTrim()
        {
            string text = "     abc 123";

            ITextTransformation transformation = new TrimRightTransformation();

            Assert.AreEqual(text, transformation.Transform(text));
        }

        [TestMethod]
        public void TrimRightTransformationTest_3_ReturnsEmptyIfNullIsGiven()
        {
            ITextTransformation transformation = new TrimRightTransformation();

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }

        [TestMethod]
        public void NormalizeSpaceTransformationTest_1()
        {
            string text = "     abc   123   _ - _  ";
            string normalizedText = " abc 123 _ - _ ";

            ITextTransformation transformation = new NormalizeSpaceTransformation();

            Assert.AreEqual(normalizedText, transformation.Transform(text));
        }

        [TestMethod]
        public void NormalizeSpaceTransformationTest_2_NothingToNormalize()
        {
            string text = "abc 123";

            ITextTransformation transformation = new NormalizeSpaceTransformation();

            Assert.AreEqual(text, transformation.Transform(text));
        }

        [TestMethod]
        public void NormalizeSpaceTransformationTest_3_ReturnsEmptyIfNullIsGiven()
        {
            ITextTransformation transformation = new NormalizeSpaceTransformation();

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }

        [TestMethod]
        public void DecorateTransformationTest_1()
        {
            string text = "abc 123";
            string decoratedText = "-={ abc 123 }=-";

            ITextTransformation transformation = new DecorateTransformation();

            Assert.AreEqual(decoratedText, transformation.Transform(text));
        }

        [TestMethod]
        public void DecorateTransformationTest_2_HasLeadingAndTrailingWhiteSpaces()
        {
            string text = "     abc 123  ";
            string decoratedText = "-={ abc 123 }=-";

            ITextTransformation transformation = new DecorateTransformation();

            Assert.AreEqual(decoratedText, transformation.Transform(text));
        }

        [TestMethod]
        public void DecorateTransformationTest_3_EmptyOrBlankText()
        {
            string empty = string.Empty;
            string whiteSpace = " ";
            string blank = "       ";

            string decoratedText = "-={ }=-";

            ITextTransformation transformation = new DecorateTransformation();

            Assert.AreEqual(decoratedText, transformation.Transform(empty));
            Assert.AreEqual(decoratedText, transformation.Transform(whiteSpace));
            Assert.AreEqual(decoratedText, transformation.Transform(blank));
        }

        [TestMethod]
        public void DecorateTransformationTest_4_NullText()
        {
            string decoratedText = "-={ }=-";

            ITextTransformation transformation = new DecorateTransformation();

            Assert.AreEqual(decoratedText, transformation.Transform(null));
        }

        [TestMethod]
        public void ReplaceTransformationTest_1()
        {
            string a = "abc";
            string b = "0";
            string text = " abc 123 aBc ABCdef aabcc";
            string replacedText = " 0 123 aBc ABCdef a0c";

            ITextTransformation transformation = new ReplaceTransformation(a, b);

            Assert.AreEqual(replacedText, transformation.Transform(text));
        }

        [TestMethod]
        public void ReplaceTransformationTest_2_NullOrEmptyA()
        {
            string b = "0";
            string text = " abc 123 aBc ABCdef aabcc";

            ITextTransformation transformation1 = new ReplaceTransformation(string.Empty, b);
            ITextTransformation transformation2 = new ReplaceTransformation(null, b);

            Assert.AreEqual(text, transformation1.Transform(text));
            Assert.AreEqual(text, transformation2.Transform(text));
        }

        [TestMethod]
        public void ReplaceTransformationTest_3_NullOrEmptyB()
        {
            string a = "abc";
            string text = " abc 123 aBc ABCdef aabcc";
            string replacedText = "  123 aBc ABCdef ac";

            ITextTransformation transformation1 = new ReplaceTransformation(a, string.Empty);
            ITextTransformation transformation2 = new ReplaceTransformation(a, null);

            Assert.AreEqual(replacedText, transformation1.Transform(text));
            Assert.AreEqual(replacedText, transformation2.Transform(text));
        }

        [TestMethod]
        public void ReplaceTransformationTest_4_ReturnsEmptyIfNullIsGiven()
        {
            string a = "abc";
            string b = "0";

            ITextTransformation transformation = new ReplaceTransformation(a, b);

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }

        [TestMethod]
        public void CensorTransformationTest_1()
        {
            string w = "abc";
            string text = "abc AbC aabbcc abcabc 123 DEF";
            string censoredText = "*** AbC aabbcc ****** 123 DEF";

            ITextTransformation transformation = new CensorTransformation(w);

            Assert.AreEqual(censoredText, transformation.Transform(text));
        }

        [TestMethod]
        public void CensorTransformationTest_2_NullOrEmptyW()
        {
            string text = "abc AbC aabbcc abcabc 123 DEF";

            ITextTransformation transformation1 = new CensorTransformation(string.Empty);
            ITextTransformation transformation2 = new CensorTransformation(null);

            Assert.AreEqual(text, transformation1.Transform(text));
            Assert.AreEqual(text, transformation2.Transform(text));
        }

        [TestMethod]
        public void CensorTransformationTest_3_ReturnsEmptyIfNullIsGiven()
        {
            string w = "abc";

            ITextTransformation transformation = new CensorTransformation(w);

            Assert.AreEqual(string.Empty, transformation.Transform(null));
        }

        [TestMethod]
        public void CompositeTransformationTest_1()
        {
            string text = "abc def";
            string transformedText = "-={ Abc def }=-";

            ITextTransformation transformation = new CompositeTransformation(new List<ITextTransformation>() 
            {
                new CapitalizeTransformation(),
                null,
                new DecorateTransformation(),
                new ReplaceTransformation("abc", "def"),
                null
            });

            Assert.AreEqual(transformedText, transformation.Transform(text));
        }

        [TestMethod]
        public void CompositeTransformationTest_2_NullTransformations()
        {
            string text = "abc def";

            ITextTransformation transformation = new CompositeTransformation(new List<ITextTransformation>()
            {
                null,
                null,
                null
            });

            Assert.AreEqual(text, transformation.Transform(text));
        }

        [TestMethod]
        public void CompositeTransformationTest_3_NestedComposite()
        {
            string text = "abc def";
            string transformedText = "* Abc def *";

            ITextTransformation transformation = new CompositeTransformation(new List<ITextTransformation>()
            {
                new CapitalizeTransformation(),
                new DecorateTransformation(),
                new CompositeTransformation(new List<ITextTransformation>() 
                {
                    new ReplaceTransformation("{", " "),
                    new ReplaceTransformation("-", " "),
                    new ReplaceTransformation("}", " ")
                }),
                new NormalizeSpaceTransformation(),
                new CompositeTransformation(new List<ITextTransformation>()
                {
                    new TrimLeftTransformation(),
                    new CompositeTransformation(new List<ITextTransformation>() 
                    {
                        new TrimRightTransformation(),
                        new CensorTransformation("=")
                    })
                })
            });

            Assert.AreEqual(transformedText, transformation.Transform(text));
        }

        [TestMethod]
        public void TextTransformationEqualityTest_1()
        {
            Assert.AreEqual(new CapitalizeTransformation(), new CapitalizeTransformation());
            Assert.AreEqual(new TrimLeftTransformation(), new TrimLeftTransformation());
            Assert.AreEqual(new TrimRightTransformation(), new TrimRightTransformation());
            Assert.AreEqual(new NormalizeSpaceTransformation(), new NormalizeSpaceTransformation());
            Assert.AreEqual(new DecorateTransformation(), new DecorateTransformation());
            Assert.AreEqual(new CensorTransformation("asd"), new CensorTransformation("asd"));
            Assert.AreEqual(new CensorTransformation(""), new CensorTransformation(""));
            Assert.AreEqual(new CensorTransformation(""), new CensorTransformation(null));
            Assert.AreEqual(new CensorTransformation(null), new CensorTransformation(null));
            Assert.AreEqual(new ReplaceTransformation("ab", "cd"), new ReplaceTransformation("ab", "cd"));
            Assert.AreEqual(new ReplaceTransformation("", ""), new ReplaceTransformation("", ""));
            Assert.AreEqual(new ReplaceTransformation("", ""), new ReplaceTransformation(null, null));
            Assert.AreEqual(new ReplaceTransformation(null, null), new ReplaceTransformation(null, null));
        }

        [TestMethod]
        public void TextTransformationEqualityTest_2_Composite()
        {
            ITextTransformation transformation1 = new CompositeTransformation(new List<ITextTransformation>()
            {
                new CapitalizeTransformation(),
                new DecorateTransformation(),
                new CompositeTransformation(new List<ITextTransformation>()
                {
                    new ReplaceTransformation("{", " "),
                    new ReplaceTransformation("-", " "),
                    new ReplaceTransformation("}", " ")
                }),
                new NormalizeSpaceTransformation(),
                new CompositeTransformation(new List<ITextTransformation>()
                {
                    new TrimLeftTransformation(),
                    new CompositeTransformation(new List<ITextTransformation>()
                    {
                        new TrimRightTransformation(),
                        new CensorTransformation("=")
                    })
                })
            });

            ITextTransformation transformation2 = new CompositeTransformation(new List<ITextTransformation>()
            {
                new CapitalizeTransformation(),
                new DecorateTransformation(),
                new CompositeTransformation(new List<ITextTransformation>()
                {
                    new ReplaceTransformation("{", " "),
                    new ReplaceTransformation("-", " "),
                    new ReplaceTransformation("}", " ")
                }),
                new NormalizeSpaceTransformation(),
                new CompositeTransformation(new List<ITextTransformation>()
                {
                    new TrimLeftTransformation(),
                    new CompositeTransformation(new List<ITextTransformation>()
                    {
                        new TrimRightTransformation(),
                        new CensorTransformation("=")
                    })
                })
            });

            Assert.AreEqual(transformation1, transformation2);
        }

        [TestMethod]
        public void TextTransformationInEqualityTest_1()
        {
            List<ITextTransformation> transformations = new List<ITextTransformation>()
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new DecorateTransformation(),
                new CensorTransformation("asd"),
                new ReplaceTransformation("ab","cd")
            };

            for (int i = 0; i < transformations.Count; ++i)
            {
                for (int j = 0; j < transformations.Count; ++j)
                {
                    if (i == j)
                        continue;

                    Assert.AreNotEqual(transformations[i], transformations[j]);
                }
            }

            Assert.AreNotEqual(new CensorTransformation("a"), new CensorTransformation("b"));
            Assert.AreNotEqual(new CensorTransformation("a"), new CensorTransformation(" a "));
            Assert.AreNotEqual(new CensorTransformation("asd"), new CensorTransformation("ASD"));

            Assert.AreNotEqual(new ReplaceTransformation("a", "b"), new ReplaceTransformation("b", "a"));
            Assert.AreNotEqual(new ReplaceTransformation("a", "b"), new ReplaceTransformation("a", "c"));
            Assert.AreNotEqual(new ReplaceTransformation("a", "b"), new ReplaceTransformation("c", "b"));
            Assert.AreNotEqual(new ReplaceTransformation("a", "b"), new ReplaceTransformation(" a ", " b "));
            Assert.AreNotEqual(new ReplaceTransformation("a", "b"), new ReplaceTransformation("A", "B"));
            Assert.AreNotEqual(new ReplaceTransformation("a", "b"), new ReplaceTransformation("A", "B"));
        }

        [TestMethod]
        public void TextTransformationInEqualityTest_2_Composite()
        {
            string leafParam1 = "=";
            string leafParam2 = "~";

            ITextTransformation transformation1 = new CompositeTransformation(new List<ITextTransformation>()
            {
                new CapitalizeTransformation(),
                new DecorateTransformation(),
                new CompositeTransformation(new List<ITextTransformation>()
                {
                    new ReplaceTransformation("{", " "),
                    new ReplaceTransformation("-", " "),
                    new ReplaceTransformation("}", " ")
                }),
                new NormalizeSpaceTransformation(),
                new CompositeTransformation(new List<ITextTransformation>()
                {
                    new TrimLeftTransformation(),
                    new CompositeTransformation(new List<ITextTransformation>()
                    {
                        new TrimRightTransformation(),
                        new CensorTransformation(leafParam1)
                    })
                })
            });

            ITextTransformation transformation2 = new CompositeTransformation(new List<ITextTransformation>()
            {
                new CapitalizeTransformation(),
                new DecorateTransformation(),
                new CompositeTransformation(new List<ITextTransformation>()
                {
                    new ReplaceTransformation("{", " "),
                    new ReplaceTransformation("-", " "),
                    new ReplaceTransformation("}", " ")
                }),
                new NormalizeSpaceTransformation(),
                new CompositeTransformation(new List<ITextTransformation>()
                {
                    new TrimLeftTransformation(),
                    new CompositeTransformation(new List<ITextTransformation>()
                    {
                        new TrimRightTransformation(),
                        new CensorTransformation(leafParam2)
                    })
                })
            });

            Assert.AreNotEqual(transformation1, transformation2);
        }
    }
}