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
    }
}