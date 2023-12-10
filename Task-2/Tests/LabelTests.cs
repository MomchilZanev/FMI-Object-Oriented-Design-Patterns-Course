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

            SimpleLabel label = new SimpleLabel(text);

            Assert.AreEqual(text, label.GetText());
        }

        [TestMethod]
        public void SimpleLabelTest_2_EmptyValue()
        {
            string text = "";

            SimpleLabel label = new SimpleLabel(text);

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void SimpleLabelTest_3_NullValue()
        {
            SimpleLabel label = new SimpleLabel(null);

            Assert.AreEqual(string.Empty, label.GetText());
        }

        [TestMethod]
        public void RichLabelTest_1()
        {
            string text = "Rich label test.";
            string color = "Green";
            int size = 12;
            string font = "Tahoma";

            RichLabel richLabel = new RichLabel(text, color, size, font);

            Assert.AreEqual(text, richLabel.GetText());
            Assert.AreEqual(color, richLabel.Style.Color);
            Assert.AreEqual(size, richLabel.Style.Size);
            Assert.AreEqual(font, richLabel.Style.Font);
        }

        [TestMethod]
        public void RichLabelTest_2_DefaultValues()
        {
            RichLabel richLabel = new RichLabel(null, null, -10, null);

            Assert.AreEqual(string.Empty, richLabel.GetText());
            Assert.AreEqual("Black", richLabel.Style.Color);
            Assert.AreEqual(1, richLabel.Style.Size);
            Assert.AreEqual("Arial", richLabel.Style.Font);
        }
    }
}