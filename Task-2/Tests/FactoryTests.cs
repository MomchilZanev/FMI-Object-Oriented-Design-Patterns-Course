using LabelsTask.Factories;
using LabelsTask.Labels;

namespace Tests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void StreamLabelFactoryTest_1()
        {
            string simpleLabelValue = "Simple label.";

            string richLabelValue = "Rich label.";
            string richLabelColor = "DimGray";
            int richLabelSize = 12;
            string richLabelFont = "Times New Roman";

            // Create simple label first and rich label second
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string>
            {
                "invalid type",
                "simple", simpleLabelValue,
                "other invalid type",
                "rich", richLabelValue, richLabelColor, richLabelSize.ToString(), richLabelFont
            })));

            StreamLabelFactory factory = new StreamLabelFactory(Console.In, Console.Out);

            ILabel simpleLabel = factory.CreateLabel();
            ILabel richLabel = factory.CreateLabel();

            Assert.AreEqual(typeof(SimpleLabel), simpleLabel.GetType());
            Assert.AreEqual(simpleLabelValue, simpleLabel.GetText());

            Assert.AreEqual(typeof(RichLabel), richLabel.GetType());
            Assert.AreEqual(richLabelValue, richLabel.GetText());
            Assert.AreEqual(richLabelColor, ((RichLabel)richLabel).Style.Color);
            Assert.AreEqual(richLabelSize, ((RichLabel)richLabel).Style.Size);
            Assert.AreEqual(richLabelFont, ((RichLabel)richLabel).Style.Font);
        }
    }
}