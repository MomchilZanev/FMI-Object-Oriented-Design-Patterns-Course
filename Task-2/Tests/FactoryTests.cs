using LabelsTask.Decorators;
using LabelsTask.Factories;
using LabelsTask.Labels;
using LabelsTask.Transformations;

namespace Tests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void StreamLabelFactoryTest_1_CreateSimpleLabel()
        {
            string simpleLabelValue = "Simple label.";

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string>
            {
                "invalid type",
                "simple", simpleLabelValue,
            })));

            StreamLabelFactory factory = new StreamLabelFactory(Console.In, Console.Out);

            ILabel simpleLabel = factory.CreateLabel();

            Assert.AreEqual(typeof(SimpleLabel), simpleLabel.GetType());
            Assert.AreEqual(simpleLabelValue, simpleLabel.GetText());
        }

        [TestMethod]
        public void StreamLabelFactoryTest_2_CreateRichLabel()
        {
            string richLabelValue = "Rich label.";
            string richLabelColor = "DimGray";
            int richLabelSize = 12;
            string richLabelFont = "Times New Roman";

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string>
            {
                "other invalid type",
                "rich", richLabelValue, richLabelColor, richLabelSize.ToString(), richLabelFont
            })));

            StreamLabelFactory factory = new StreamLabelFactory(Console.In, Console.Out);

            ILabel richLabel = factory.CreateLabel();

            Assert.AreEqual(typeof(RichLabel), richLabel.GetType());
            Assert.AreEqual(richLabelValue, richLabel.GetText());
            Assert.AreEqual(richLabelColor, ((RichLabel)richLabel).Style.Color);
            Assert.AreEqual(richLabelSize, ((RichLabel)richLabel).Style.Size);
            Assert.AreEqual(richLabelFont, ((RichLabel)richLabel).Style.Font);
        }

        [TestMethod]
        public void StreamLabelFactoryTest_3_CreateCustomLabel()
        {
            int customLabelTimeout = 2;
            string proxyLabelValue1 = "creating a custom label";
            string proxyLabelValue2 = "custom label working?";

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string>
            {
                null,
                "custom", customLabelTimeout.ToString(),
                "simple", proxyLabelValue1,
                "rich", proxyLabelValue2, "", "", ""
            })));

            StreamLabelFactory factory = new StreamLabelFactory(Console.In, Console.Out);
            ILabel customLabel = factory.CreateLabel();

            Assert.AreEqual(typeof(CustomLabel), customLabel.GetType());
            for (int i = 0; i < customLabelTimeout + 1; ++i)
            {
                Assert.AreEqual(proxyLabelValue1, customLabel.GetText());
            }
            for (int i = 0; i < customLabelTimeout + 1; ++i)
            {
                Assert.AreEqual(proxyLabelValue2, customLabel.GetText());
            }
        }

        [TestMethod]
        public void StreamLabelFactoryTest_4_CreateHelpLabel()
        {
            string helpText = "Help text 123";
            string baseLabelText = "ABC-def-123";

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string>
            {
                "help", helpText,
                "simple", baseLabelText,
            })));

            StreamLabelFactory factory = new StreamLabelFactory(Console.In, Console.Out);
            ILabel helpLabel = factory.CreateLabel();

            Assert.AreEqual(typeof(HelpLabel), helpLabel.GetType());
            Assert.AreEqual(baseLabelText, helpLabel.GetText());
            Assert.AreEqual(helpText, ((HelpLabel)helpLabel).GetHelpText());
        }

        [TestMethod]
        public void StreamTextTransformationFactoryTest_1()
        {
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string>
            {
                "INVALID",
                "capitalize",
                "trim-left",
                "trim-right",
                "OTHER-INVALID",
                "normalize-space",
                "decorate",
                "censor", "123",
                "replace", "abc", "def",
                null,
                "composite", "trim-left", "y", "trim-right", "y", "censor", "a"
            })));

            StreamTextTransformationFactory factory = new StreamTextTransformationFactory(Console.In, Console.Out);

            ITextTransformation capitalize = factory.CreateTextTransformation();
            ITextTransformation trimLeft = factory.CreateTextTransformation();
            ITextTransformation trimRight = factory.CreateTextTransformation();
            ITextTransformation normalizeSpace = factory.CreateTextTransformation();
            ITextTransformation decorate = factory.CreateTextTransformation();
            ITextTransformation censor = factory.CreateTextTransformation();
            ITextTransformation replace = factory.CreateTextTransformation();
            ITextTransformation composite = factory.CreateTextTransformation();

            Assert.AreEqual(typeof(CapitalizeTransformation), capitalize.GetType());
            Assert.AreEqual(typeof(TrimLeftTransformation), trimLeft.GetType());
            Assert.AreEqual(typeof(TrimRightTransformation), trimRight.GetType());
            Assert.AreEqual(typeof(NormalizeSpaceTransformation), normalizeSpace.GetType());
            Assert.AreEqual(typeof(DecorateTransformation), decorate.GetType());

            Assert.AreEqual(typeof(CensorTransformation), censor.GetType());
            Assert.AreEqual("***", censor.Transform("123"));

            Assert.AreEqual(typeof(ReplaceTransformation), replace.GetType());
            Assert.AreEqual("def", replace.Transform("abc"));

            Assert.AreEqual(typeof(CompositeTransformation), composite.GetType());
            Assert.AreEqual("A*", composite.Transform(" Aa "));
        }

        [TestMethod]
        public void StreamDecoratorFactoryTest_1()
        {
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, new List<string>
            {
                "INVALID",
                "text-transformation", "capitalize", "n",
                "OTHER-INVALID",
                "random", "trim-right", "n",
                null,
                "cycling", "censor", "abc", "y", "replace", "abc", "ABC", "n"
            })));

            StreamDecoratorFactory factory = new StreamDecoratorFactory(Console.In, Console.Out, new StreamTextTransformationFactory(Console.In, Console.Out));

            ILabel label = new SimpleLabel("abc ");

            ILabel textTransformationDecorator = factory.CreateDecorator(label);
            ILabel randomDecorator = factory.CreateDecorator(label);
            ILabel cyclingDecorator = factory.CreateDecorator(label);

            Assert.AreEqual(typeof(TextTransformationDecorator), textTransformationDecorator.GetType());
            Assert.AreEqual("Abc ", textTransformationDecorator.GetText());

            Assert.AreEqual(typeof(RandomTransformationDecorator), randomDecorator.GetType());
            Assert.AreEqual("abc", randomDecorator.GetText());

            Assert.AreEqual(typeof(CyclingTransformationDecorator), cyclingDecorator.GetType());
            Assert.AreEqual("*** ", cyclingDecorator.GetText());
            Assert.AreEqual("ABC ", cyclingDecorator.GetText());
        }

        [TestMethod]
        public void CensorTransformationFactoryTest_1_SmallObjectsAreReused()
        {
            CensorTransformationFactory factory = new CensorTransformationFactory();

            string censorWord1 = "abc";
            string censorWord2 = "1234";
            string censorWord3 = string.Empty;

            ITextTransformation transformation11 = factory.CreateCensorTransformation(censorWord1);
            ITextTransformation transformation12 = factory.CreateCensorTransformation(censorWord1);

            ITextTransformation transformation21 = factory.CreateCensorTransformation(censorWord2);
            ITextTransformation transformation22 = factory.CreateCensorTransformation(censorWord2);

            ITextTransformation transformation31 = factory.CreateCensorTransformation(censorWord3);
            ITextTransformation transformation32 = factory.CreateCensorTransformation(censorWord3);

            Assert.AreSame(transformation11, transformation12);
            Assert.AreSame(transformation21, transformation22);
            Assert.AreSame(transformation31, transformation32);
        }

        [TestMethod]
        public void CensorTransformationFactoryTest_1_LargeObjectsAreNotReused()
        {
            CensorTransformationFactory factory = new CensorTransformationFactory();

            string censorWord = "abc-123";

            ITextTransformation transformation1 = factory.CreateCensorTransformation(censorWord);
            ITextTransformation transformation2 = factory.CreateCensorTransformation(censorWord);

            Assert.AreNotSame(transformation1, transformation2);
        }
    }
}