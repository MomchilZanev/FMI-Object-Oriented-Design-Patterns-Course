using LabelsTask.Labels;

namespace Tests
{
    [TestClass]
    public class LabelTests
    {
        [TestMethod]
        public void SimpleLabelTest_1()
        {
            string text = "Simple label test.";

            ILabel label = new SimpleLabel(text);

            Assert.AreEqual(text, label.GetText());
        }

        [TestMethod]
        public void SimpleLabelTest_2_EmptyValue()
        {
            string text = "";

            ILabel label = new SimpleLabel(text);

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void SimpleLabelTest_3_NullValue()
        {
            ILabel label = new SimpleLabel(null);

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void RichLabelTest_1()
        {
            string text = "Rich label test.";
            string color = "Green";
            int size = 12;
            string font = "Tahoma";

            ILabel richLabel = new RichLabel(text, color, size, font);

            Assert.AreEqual(text, richLabel.GetText());
            Assert.AreEqual(color, ((RichLabel)richLabel).Style.Color);
            Assert.AreEqual(size, ((RichLabel)richLabel).Style.Size);
            Assert.AreEqual(font, ((RichLabel)richLabel).Style.Font);
        }

        [TestMethod]
        public void RichLabelTest_2_DefaultValues()
        {
            ILabel richLabel = new RichLabel(null, null, -10, null);

            Assert.AreEqual(string.Empty, richLabel.GetText());
            Assert.AreEqual("Black", ((RichLabel)richLabel).Style.Color);
            Assert.AreEqual(1, ((RichLabel)richLabel).Style.Size);
            Assert.AreEqual("Arial", ((RichLabel)richLabel).Style.Font);
        }

        [TestMethod]
        public void CustomLabelTest_1_NoTimeout()
        {
            string text = "Label value";

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string> { "simple", text, "simple", "label that will never be read" })));
            CustomLabel label = new CustomLabel();

            for (int i = 0; i < 100; ++i)
            {
                Assert.AreEqual(text, label.GetText());
            }
        }

        [TestMethod]
        public void CustomLabelTest_2_NTimeout()
        {
            string text1 = "Label 1";
            string text2 = "Label 2";
            string text3 = "Label 3";

            for (int N = 0; N <= 10; ++N)
            {
                Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string> { "simple", text1, "simple", text2, "simple", text3 })));
                CustomLabel label = new CustomLabel(N);

                Assert.AreEqual(text1, label.GetText());
                for (int i = 0; i < N; ++i)
                {
                    Assert.AreEqual(text1, label.GetText());
                }
                Assert.AreEqual(text2, label.GetText());
                for (int i = 0; i < N; ++i)
                {
                    Assert.AreEqual(text2, label.GetText());
                }
                Assert.AreEqual(text3, label.GetText());
                for (int i = 0; i < N; ++i)
                {
                    Assert.AreEqual(text3, label.GetText());
                }
            }
        }

        [TestMethod]
        public void CustomLabelTest_3_NegativeTimeoutWorksAsNoTimeout()
        {
            string text = "Label value";

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string> { "simple", text, "simple", "label that will never be read" })));
            CustomLabel label = new CustomLabel(-5);

            for (int i = 0; i < 100; ++i)
            {
                Assert.AreEqual(text, label.GetText());
            }
        }

        [TestMethod]
        public void HelpLabelTest_1()
        {
            string text = "Label value";
            string helpText = "Label help text";

            HelpLabel label1 = new HelpLabel(text, helpText);
            HelpLabel label2 = new HelpLabel(helpText, new RichLabel(text, null, 1, null));

            Assert.AreEqual(text, label1.GetText());
            Assert.AreEqual(helpText, label1.GetHelpText());

            Assert.AreEqual(text, label2.GetText());
            Assert.AreEqual(helpText, label2.GetHelpText());
        }

        [TestMethod]
        public void HelpLabelTest_2_NullLabel()
        {
            string helpText = "Label help text";

            HelpLabel label = new HelpLabel(helpText);

            Assert.AreEqual(string.Empty, label.GetText());
            Assert.AreEqual(helpText, label.GetHelpText());
        }

        [TestMethod]
        public void HelpLabelTest_3_WithCustomLabel()
        {
            string helpText = "Custom help text";
            string simpleText = "custom 1";
            string richText = "custom 2";

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string> { "simple", simpleText, "rich", richText, "", "", "" })));
            HelpLabel label = new HelpLabel(helpText, new CustomLabel(1));

            Assert.AreEqual(simpleText, label.GetText());
            Assert.AreEqual(helpText, label.GetHelpText());
            Assert.AreEqual(simpleText, label.GetText());

            Assert.AreEqual(richText, label.GetText());
            Assert.AreEqual(helpText, label.GetHelpText());
            Assert.AreEqual(richText, label.GetText());
        }
    }
}