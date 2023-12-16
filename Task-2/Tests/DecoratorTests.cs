using LabelsTask.Decorators;
using LabelsTask.Labels;
using LabelsTask.Transformations;

namespace Tests
{
    [TestClass]
    public class DecoratorTests
    {
        [TestMethod]
        public void TextTransformationDecoratorTest_1()
        {
            string expectedText = "-={ Test label. }=-";
            ILabel label = new SimpleLabel("simple test abc     label.  ");

            label = new TextTransformationDecorator(label, new TrimRightTransformation());
            label = new TextTransformationDecorator(label, new ReplaceTransformation("simple", "abc"));
            label = new TextTransformationDecorator(label, new CensorTransformation("abc"));
            label = new TextTransformationDecorator(label, null);
            label = new TextTransformationDecorator(label, new ReplaceTransformation("***", string.Empty));
            label = new TextTransformationDecorator(label, new TrimLeftTransformation());
            label = new TextTransformationDecorator(label, new NormalizeSpaceTransformation());
            label = new TextTransformationDecorator(label, new CapitalizeTransformation());
            label = new TextTransformationDecorator(label, new DecorateTransformation());

            Assert.AreEqual(expectedText, label.GetText());
        }

        [TestMethod]
        public void TextTransformationDecoratorTest_2_ReturnsBaseLabelIfNoTransformationIsGiven()
        {
            string text = "abc 123 DEF";
            ILabel label = new SimpleLabel(text);

            label = new TextTransformationDecorator(label, null);

            Assert.AreEqual(text, label.GetText());
        }

        [TestMethod]
        public void TextTransformationDecoratorTest_3_NullLabel()
        {
            ILabel label = new TextTransformationDecorator(null, new CapitalizeTransformation());
            label = new TextTransformationDecorator(null, new NormalizeSpaceTransformation());
            label = new TextTransformationDecorator(null, new TrimRightTransformation());

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void TextTransformationDecoratorTest_4_NullLabelAndTransformation()
        {
            ILabel label = new TextTransformationDecorator(null, null);
            label = new TextTransformationDecorator(null, null);
            label = new TextTransformationDecorator(null, null);

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void TextTransformationDecoratorTest_5_EqualityTest()
        {
            ILabel label1 = new TextTransformationDecorator(null, null);
            ILabel label1Compare = new TextTransformationDecorator(new SimpleLabel(null), null);

            ILabel label2 = new TextTransformationDecorator(new SimpleLabel(null), new CapitalizeTransformation());
            ILabel label2Compare = new TextTransformationDecorator(null, new CapitalizeTransformation());

            ILabel label3 = new TextTransformationDecorator(new SimpleLabel(null), new TrimLeftTransformation());
            label3 = new TextTransformationDecorator(label3, new NormalizeSpaceTransformation());
            ILabel label3Compare = new TextTransformationDecorator(null, new NormalizeSpaceTransformation());

            ILabel label4 = new TextTransformationDecorator(null, new CensorTransformation("asd"));
            ILabel label4Compare = new TextTransformationDecorator(null, new CensorTransformation("asd"));

            ILabel label5 = new TextTransformationDecorator(null, new ReplaceTransformation("asd", "abc"));
            ILabel label5Compare = new TextTransformationDecorator(null, new ReplaceTransformation("asd", "abc"));

            Assert.AreEqual(label1, label1Compare);
            Assert.AreEqual(label2, label2Compare);
            Assert.AreEqual(label3, label3Compare);
            Assert.AreEqual(label4, label4Compare);
            Assert.AreEqual(label5, label5Compare);
        }

        [TestMethod]
        public void TextTransformationDecoratorTest_6_InEqualityTest()
        {
            ILabel label1 = new TextTransformationDecorator(null, null);
            ILabel label1Compare = new TextTransformationDecorator(new SimpleLabel(null), new CapitalizeTransformation());

            ILabel label2 = new TextTransformationDecorator(new SimpleLabel(null), new CapitalizeTransformation());
            ILabel label2Compare = new TextTransformationDecorator(null, new NormalizeSpaceTransformation());

            ILabel label3 = new TextTransformationDecorator(new SimpleLabel(null), new TrimLeftTransformation());
            label3 = new TextTransformationDecorator(label3, new TrimLeftTransformation());
            ILabel label3Compare = new TextTransformationDecorator(null, new TrimRightTransformation());

            ILabel label4 = new TextTransformationDecorator(null, new CensorTransformation("asd"));
            ILabel label4Compare = new TextTransformationDecorator(null, new CensorTransformation("abc"));

            ILabel label5 = new TextTransformationDecorator(null, new ReplaceTransformation("asd", "abc"));
            ILabel label5Compare = new TextTransformationDecorator(null, new ReplaceTransformation("abc", "asd"));

            Assert.AreNotEqual(label1, label1Compare);
            Assert.AreNotEqual(label2, label2Compare);
            Assert.AreNotEqual(label3, label3Compare);
            Assert.AreNotEqual(label4, label4Compare);
            Assert.AreNotEqual(label5, label5Compare);
        }

        [TestMethod]
        public void RandomTransformationDecoratorTest_1()
        {
            string text = "random      decorator  test   ";
            string capitalizeTransformation = "Random      decorator  test   ";
            string trimRightTransformation = "random      decorator  test";
            string normalizeSpaceTransformation = "random decorator test ";
            string censorTransformation = "******      decorator  test   ";
            string replaceTransformation = "random      decorator  TEST   ";

            List<ITextTransformation> transformations = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                new ReplaceTransformation("test", "TEST")
            };

            ILabel label = new SimpleLabel(text);
            label = new RandomTransformationDecorator(label, transformations);

            List<string> results = new List<string>();
            for (int i = 0; i < Math.Pow(transformations.Count, 2); ++i)
            {
                results.Add(label.GetText());
            }

            Assert.IsTrue(results.Contains(capitalizeTransformation));
            Assert.IsTrue(results.Contains(trimRightTransformation));
            Assert.IsTrue(results.Contains(normalizeSpaceTransformation));
            Assert.IsTrue(results.Contains(censorTransformation));
            Assert.IsTrue(results.Contains(replaceTransformation));
        }

        [TestMethod]
        public void RandomTransformationDecoratorTest_2_ReturnsBaseLabelIfNoTransformationsAreGiven()
        {
            string text1 = "abc 123 DEF";
            string text2 = " ! ";
            ILabel label1 = new SimpleLabel(text1);
            ILabel label2 = new SimpleLabel(text2);

            label1 = new RandomTransformationDecorator(label1, null);
            label2 = new RandomTransformationDecorator(label2, new List<ITextTransformation>());

            List<string> results1 = new List<string>();
            List<string> results2 = new List<string>();
            for (int i = 0; i < 10; ++i)
            {
                results1.Add(label1.GetText());
                results2.Add(label2.GetText());
            }

            Assert.IsTrue(results1.All(r => r == text1));
            Assert.IsTrue(results2.All(r => r == text2));
        }

        [TestMethod]
        public void RandomTransformationDecoratorTest_3_OnlyOneTransformationGiven()
        {
            string text = "abc 123 DEF";
            string expectedText = "ABC 123 DEF";
            ILabel label = new SimpleLabel(text);
            ITextTransformation transformation = new ReplaceTransformation("abc", "ABC");

            label = new RandomTransformationDecorator(label, new List<ITextTransformation> { transformation });

            List<string> results = new List<string>();
            for (int i = 0; i < 10; ++i)
            {
                results.Add(label.GetText());
            }

            Assert.IsTrue(results.All(r => r == expectedText));
        }

        [TestMethod]
        public void RandomTransformationDecoratorTest_4_NullLabel()
        {
            ILabel label = new RandomTransformationDecorator(null, new List<ITextTransformation> { new CapitalizeTransformation(), new NormalizeSpaceTransformation() });
            label = new RandomTransformationDecorator(null, new List<ITextTransformation> { new TrimLeftTransformation(), new TrimRightTransformation() });

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void RandomTransformationDecoratorTest_5_NullLabelAndTransformations()
        {
            ILabel label = new RandomTransformationDecorator(null, null);
            label = new RandomTransformationDecorator(null, null);

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void RandomTransformationDecoratorTest_6_EqualityTest()
        {
            List<ITextTransformation> transformations1 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                null,
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                null,
                new ReplaceTransformation("test", "TEST")
            };

            List<ITextTransformation> transformations2 = new List<ITextTransformation>
            {
                new CensorTransformation("random"),
                new TrimLeftTransformation(),
                new ReplaceTransformation("test", "TEST"),
                new CapitalizeTransformation(),
                new NormalizeSpaceTransformation(),
                new TrimRightTransformation()
            };

            ILabel label1 = new RandomTransformationDecorator(null, transformations1);
            ILabel label2 = new RandomTransformationDecorator(null, transformations2);

            Assert.AreEqual(label1, label2);
        }

        [TestMethod] // Checking for strategy count
        public void RandomTransformationDecoratorTest_7_InEqualityTest_1()
        {
            List<ITextTransformation> transformations1 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                null,
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                null,
                new ReplaceTransformation("test", "TEST")
            };

            List<ITextTransformation> transformations2 = new List<ITextTransformation>
            {
                null,
                null,
                null,
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                null,
                new ReplaceTransformation("test", "TEST")
            };

            ILabel label1 = new RandomTransformationDecorator(null, transformations1);
            ILabel label2 = new RandomTransformationDecorator(null, transformations2);

            Assert.AreNotEqual(label1, label2);
        }

        [TestMethod] // Checking for different strategy parameters
        public void RandomTransformationDecoratorTest_8_InEqualityTest_2()
        {
            List<ITextTransformation> transformations1 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                null,
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                null,
                new ReplaceTransformation("test", "TEST")
            };

            List<ITextTransformation> transformations2 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                null,
                null,
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("other"),
                null,
                new ReplaceTransformation("a", "b")
            };

            ILabel label1 = new RandomTransformationDecorator(null, transformations1);
            ILabel label2 = new RandomTransformationDecorator(null, transformations2);

            Assert.AreNotEqual(label1, label2);
        }

        [TestMethod]
        public void CyclingTransformationDecoratorTest_1()
        {
            string text = "  abc   123   def    ";
            List<string> expectedTransformations = new List<string>
            {
                "abc   123   def    ",
                "  abc   123   def",
                " abc 123 def ",
                "  abc   ***   def    ",
                "  abc   123   DEF    "
            };

            List<ITextTransformation> transformations = new List<ITextTransformation>
            {
                new TrimLeftTransformation(),
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("123"),
                new ReplaceTransformation("def", "DEF")
            };

            ILabel label = new SimpleLabel(text);
            label = new CyclingTransformationDecorator(label, transformations);

            List<string> results = new List<string>();
            for (int i = 0; i < expectedTransformations.Count * 3; ++i)
            {
                results.Add(label.GetText());
            }

            for (int i = 0; i < results.Count; ++i)
            {
                Assert.AreEqual(expectedTransformations[i % expectedTransformations.Count], results[i]);
            }
        }

        [TestMethod]
        public void CyclingTransformationDecoratorTest_2_ReturnsBaseLabelIfNoTransformationsAreGiven()
        {
            string text1 = "abc 123 DEF";
            string text2 = " ! ";
            ILabel label1 = new SimpleLabel(text1);
            ILabel label2 = new SimpleLabel(text2);

            label1 = new CyclingTransformationDecorator(label1, null);
            label2 = new CyclingTransformationDecorator(label2, new List<ITextTransformation>());

            List<string> results1 = new List<string>();
            List<string> results2 = new List<string>();
            for (int i = 0; i < 10; ++i)
            {
                results1.Add(label1.GetText());
                results2.Add(label2.GetText());
            }

            Assert.IsTrue(results1.All(r => r == text1));
            Assert.IsTrue(results2.All(r => r == text2));
        }

        [TestMethod]
        public void CyclingTransformationDecoratorTest_3_OnlyOneTransformationGiven()
        {
            string text = "abc 123 DEF";
            string expectedText = "ABC 123 DEF";
            ILabel label = new SimpleLabel(text);
            ITextTransformation transformation = new ReplaceTransformation("abc", "ABC");

            label = new CyclingTransformationDecorator(label, new List<ITextTransformation> { transformation });

            List<string> results = new List<string>();
            for (int i = 0; i < 10; ++i)
            {
                results.Add(label.GetText());
            }

            Assert.IsTrue(results.All(r => r == expectedText));
        }

        [TestMethod]
        public void CyclingTransformationDecoratorTest_4_NullLabel()
        {
            ILabel label = new CyclingTransformationDecorator(null, new List<ITextTransformation> { new CapitalizeTransformation(), new NormalizeSpaceTransformation() });
            label = new CyclingTransformationDecorator(null, new List<ITextTransformation> { new TrimLeftTransformation(), new TrimRightTransformation() });

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void CyclingTransformationDecoratorTest_5_NullLabelAndTransformations()
        {
            ILabel label = new CyclingTransformationDecorator(null, null);
            label = new CyclingTransformationDecorator(null, null);

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void CyclingTransformationDecoratorTest_6_EqualityTest()
        {
            List<ITextTransformation> transformations1 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                null,
                new TrimRightTransformation(),
                null,
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                null,
                new ReplaceTransformation("test", "TEST")
            };

            List<ITextTransformation> transformations2 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                new ReplaceTransformation("test", "TEST")
            };

            ILabel label1 = new CyclingTransformationDecorator(null, transformations1);
            ILabel label2 = new CyclingTransformationDecorator(null, transformations2);

            Assert.AreEqual(label1, label2);
        }

        [TestMethod] // Checking for strategy order
        public void CyclingTransformationDecoratorTest_7_InEqualityTest_1()
        {
            List<ITextTransformation> transformations1 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                new ReplaceTransformation("test", "TEST")
            };

            List<ITextTransformation> transformations2 = new List<ITextTransformation>
            {
                new TrimLeftTransformation(),
                new CapitalizeTransformation(),
                new NormalizeSpaceTransformation(),
                new TrimRightTransformation(),
                new ReplaceTransformation("test", "TEST"),
                new CensorTransformation("random")
            };

            ILabel label1 = new CyclingTransformationDecorator(null, transformations1);
            ILabel label2 = new CyclingTransformationDecorator(null, transformations2);

            Assert.AreNotEqual(label1, label2);
        }

        [TestMethod] // Checking for strategy count
        public void CyclingTransformationDecoratorTest_8_InEqualityTest_2()
        {
            List<ITextTransformation> transformations1 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                new ReplaceTransformation("test", "TEST")
            };

            List<ITextTransformation> transformations2 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                new ReplaceTransformation("test", "TEST")
            };

            ILabel label1 = new CyclingTransformationDecorator(null, transformations1);
            ILabel label2 = new CyclingTransformationDecorator(null, transformations2);

            Assert.AreNotEqual(label1, label2);
        }

        [TestMethod] // Checking for different strategy parameters
        public void CyclingTransformationDecoratorTest_9_InEqualityTest_3()
        {
            List<ITextTransformation> transformations1 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("random"),
                new ReplaceTransformation("test", "TEST")
            };

            List<ITextTransformation> transformations2 = new List<ITextTransformation>
            {
                new CapitalizeTransformation(),
                new TrimLeftTransformation(),
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
                new CensorTransformation("other"),
                new ReplaceTransformation("a", "b")
            };

            ILabel label1 = new CyclingTransformationDecorator(null, transformations1);
            ILabel label2 = new CyclingTransformationDecorator(null, transformations2);

            Assert.AreNotEqual(label1, label2);
        }

        [TestMethod]
        public void MultipleDecoratorsTest_1()
        {
            string text = "abc  123  def ";
            List<string> call1ExpectedResults = new List<string> { "Abc  ***  def", "Abc  123  DEF" };
            List<string> call2PotentialResults = new List<string> { "Abc *** def ", "Abc 123 DEF " };

            ILabel label = new SimpleLabel(text);
            label = new TextTransformationDecorator(label, new CapitalizeTransformation());
            label = new CyclingTransformationDecorator(label, new List<ITextTransformation>
            {
                new TrimRightTransformation(),
                new NormalizeSpaceTransformation(),
            });
            label = new RandomTransformationDecorator(label, new List<ITextTransformation>
            {
                new CensorTransformation("123"),
                new ReplaceTransformation("def", "DEF"),
            });


            string call1Result = label.GetText();
            Assert.IsTrue(call1ExpectedResults.Contains(call1Result));

            string call2Result = label.GetText();
            Assert.IsTrue(call2PotentialResults.Contains(call2Result));

            // Verify that RandomTransformationDecorator does not repeat transforations
            string call2ExpectedResult = call1Result == call1ExpectedResults.First() ? call2PotentialResults.Last() : call2PotentialResults.First();
            Assert.AreEqual(call2ExpectedResult, call2Result);
        }

        [TestMethod]
        public void RemoveDecoratorTest_1_CallingRemoveOnLabel()
        {
            string text = "test";
            ILabel label1 = new SimpleLabel(text);
            ILabel label2 = new RichLabel(text, "Grey", 1, "Calibri");

            label1 = LabelDecoratorBase.RemoveDecoratorFrom(label1, null);
            label1 = LabelDecoratorBase.RemoveDecoratorFrom(label1, new TextTransformationDecorator(null, new CapitalizeTransformation()));
            label2 = LabelDecoratorBase.RemoveDecoratorFrom(label2, null);
            label2 = LabelDecoratorBase.RemoveDecoratorFrom(label2, new TextTransformationDecorator(null, new NormalizeSpaceTransformation()));

            Assert.AreEqual(text, label1.GetText());
            Assert.AreEqual(text, label2.GetText());
        }

        [TestMethod]
        public void RemoveDecoratorTest_2_DecoratorDoesNotExist()
        {
            string text = "test";
            string label1ExpectedResult = "-={ Test }=-";
            string label2ExpectedResult = "ABC***";

            ILabel label1 = new SimpleLabel(text);
            label1 = new TextTransformationDecorator(label1, new CapitalizeTransformation());
            label1 = new CyclingTransformationDecorator(label1, new List<ITextTransformation>() { new DecorateTransformation() });
            ILabel label2 = new RichLabel(text, "Grey", 1, "Calibri");
            label2 = new TextTransformationDecorator(label2, new ReplaceTransformation("test", "ABCdef"));
            label2 = new RandomTransformationDecorator(label2, new List<ITextTransformation>() { new CensorTransformation("def") });

            label1 = LabelDecoratorBase.RemoveDecoratorFrom(label1, new RandomTransformationDecorator(null, new List<ITextTransformation>() { new DecorateTransformation() }));
            label1 = ((LabelDecoratorBase)label1).RemoveDecorator(null);
            label1 = ((LabelDecoratorBase)label1).RemoveDecorator(new TextTransformationDecorator(null, new TrimLeftTransformation()));
            label2 = ((LabelDecoratorBase)label2).RemoveDecorator(new RandomTransformationDecorator(null, new List<ITextTransformation>() { new ReplaceTransformation("test", "ABCdef") }));
            label2 = ((LabelDecoratorBase)label2).RemoveDecorator(null);
            label2 = LabelDecoratorBase.RemoveDecoratorFrom(label2, new CyclingTransformationDecorator(null, new List<ITextTransformation>() { new NormalizeSpaceTransformation() }));

            Assert.AreEqual(label1ExpectedResult, label1.GetText());
            Assert.AreEqual(label2ExpectedResult, label2.GetText());
        }

        [TestMethod]
        public void RemoveDecoratorTest_3_BasicRemove()
        {
            string text = "test";
            string label1ExpectedResultBeforeRemove = "-={ Test }=-";
            string label1ExpectedResultAfterRemove = "-={ test }=-";
            string label2ExpectedResultBeforeRemove = "ABC***";
            string label2ExpectedResultAfterRemove = "ABCdef";

            ILabel label1 = new SimpleLabel(text);
            label1 = new TextTransformationDecorator(label1, new CapitalizeTransformation());
            label1 = new CyclingTransformationDecorator(label1, new List<ITextTransformation>() { new DecorateTransformation() });
            ILabel label2 = new RichLabel(text, "Grey", 1, "Calibri");
            label2 = new TextTransformationDecorator(label2, new ReplaceTransformation("test", "ABCdef"));
            label2 = new RandomTransformationDecorator(label2, new List<ITextTransformation>() { new CensorTransformation("def") });

            Assert.AreEqual(label1ExpectedResultBeforeRemove, label1.GetText());
            Assert.AreEqual(label2ExpectedResultBeforeRemove, label2.GetText());

            label1 = LabelDecoratorBase.RemoveDecoratorFrom(label1, new TextTransformationDecorator(label1, new CapitalizeTransformation()));
            label2 = ((LabelDecoratorBase)label2).RemoveDecorator(new RandomTransformationDecorator(label2, new List<ITextTransformation>() { new CensorTransformation("def") }));

            Assert.AreEqual(label1ExpectedResultAfterRemove, label1.GetText());
            Assert.AreEqual(label2ExpectedResultAfterRemove, label2.GetText());
        }

        [TestMethod] // Decorators are treated as a stack -> "first matching" = "added at a later point" 
        public void RemoveDecoratorTest_4_FirstMatchingDecoratorIsRemoved()
        {
            string text = "Test";
            string labelExpectedResult = "test";

            ILabel label = new SimpleLabel(text);
            label = new TextTransformationDecorator(label, new CapitalizeTransformation()); // If this decorator is removed -> Test
            label = new TextTransformationDecorator(label, new ReplaceTransformation("T", "t"));
            label = new TextTransformationDecorator(label, new CapitalizeTransformation()); // If this decorator is removed -> test

            label = LabelDecoratorBase.RemoveDecoratorFrom(label, new TextTransformationDecorator(label, new CapitalizeTransformation()));

            Assert.AreEqual(labelExpectedResult, label.GetText());
        }

        [TestMethod]
        public void RemoveDecoratorTest_5_CyclingDecoratorStrategyOrderIsRespected()
        {
            string text = " test ";
            string labelFirstCallExpectedResult = " test";
            string labelSecondCallExpectedResult = "test ";

            ILabel label = new SimpleLabel(text);
            label = new CyclingTransformationDecorator(label, new List<ITextTransformation>() { new TrimLeftTransformation(), new TrimRightTransformation() });
            label = new CyclingTransformationDecorator(label, new List<ITextTransformation>() { new TrimRightTransformation(), new TrimLeftTransformation() });

            label = LabelDecoratorBase.RemoveDecoratorFrom(label, new CyclingTransformationDecorator(label, new List<ITextTransformation>()
                                                                  {
                                                                    new TrimLeftTransformation(),
                                                                    new TrimRightTransformation()
                                                                  }));

            Assert.AreEqual(labelFirstCallExpectedResult, label.GetText());
            Assert.AreEqual(labelSecondCallExpectedResult, label.GetText());
        }

        [TestMethod]
        public void RemoveDecoratorTest_6_RandomDecoratorStrategyOrderIsIgnored()
        {
            string text = " test ";

            ILabel label = new RichLabel(text, null, 0, null);
            label = new RandomTransformationDecorator(label, new List<ITextTransformation>() { new TrimLeftTransformation(), new TrimRightTransformation() });

            label = LabelDecoratorBase.RemoveDecoratorFrom(label, new RandomTransformationDecorator(label, new List<ITextTransformation>()
                                                                  {
                                                                    new TrimRightTransformation(),
                                                                    new TrimLeftTransformation()
                                                                  }));

            Assert.AreEqual(text, label.GetText());
        }

        [TestMethod]
        public void RemoveDecoratorTest_7_RemoveAllDecorators()
        {
            string text = " test ";
            string labelExpectedResultBeforeRemove = "test";

            ILabel label = new RichLabel(text, null, 0, null);
            label = new TextTransformationDecorator(label, new TrimLeftTransformation());
            label = new TextTransformationDecorator(label, new TrimRightTransformation());

            Assert.AreEqual(labelExpectedResultBeforeRemove, label.GetText());

            label = ((LabelDecoratorBase)label).RemoveDecorator(new TextTransformationDecorator(label, new TrimLeftTransformation()));

            Assert.IsFalse(label.GetType() == typeof(RichLabel));

            label = ((LabelDecoratorBase)label).RemoveDecorator(new TextTransformationDecorator(label, new TrimRightTransformation()));

            Assert.IsTrue(label.GetType() == typeof(RichLabel));
            Assert.AreEqual(text, label.GetText());
        }
    }
}