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
    }
}