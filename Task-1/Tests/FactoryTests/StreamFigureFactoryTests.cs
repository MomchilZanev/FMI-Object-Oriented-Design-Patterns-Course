using FiguresTask.Factories;
using FiguresTask.Figures;
using System.Text;

namespace Tests.FactoryTests
{
    [TestClass]
    public class StreamFigureFactoryTests
    {
        [TestMethod]
        public void StreamFactoryReadsFileStreamCorrectly()
        {
            string testFile = Path.Combine(Directory.GetCurrentDirectory(), "test-input.txt");
            List<IFigure> originalFigures = new List<IFigure>()
            {
                new Circle(1.25312),
                new Rectangle(2.5, 5.6),
                new Triangle(4.5, 4.6, 5.3)
            };
            File.WriteAllText(testFile, string.Join(Environment.NewLine, originalFigures));

            StreamReader streamReader = new StreamReader(testFile);
            StreamFigureFactory factory = new StreamFigureFactory(streamReader, originalFigures.Count);
            List<IFigure> readFigures = factory.CreateFigures().ToList();

            for (int i = 0; i < originalFigures.Count; ++i)
            {
                Assert.AreEqual(originalFigures[i], readFigures[i]);
            }
        }

        [TestMethod]
        public void StreamFactoryReadsConsoleStreamCorrectly()
        {
            List<IFigure> originalFigures = new List<IFigure>()
            {
                new Circle(1.2),
                new Rectangle(2.3, 4.5),
                new Triangle(5, 5, 6)
            };

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, originalFigures)));

            StreamFigureFactory factory = new StreamFigureFactory(Console.In, originalFigures.Count);
            List<IFigure> readFigures = factory.CreateFigures().ToList();

            for (int i = 0; i < originalFigures.Count; ++i)
            {
                Assert.AreEqual(originalFigures[i], readFigures[i]);
            }
        }

        [TestMethod]
        public void StreamFactoryReadsMemoryStreamCorrectly()
        {
            List<IFigure> originalFigures = new List<IFigure>()
            {
                new Circle(9.1231),
                new Rectangle(21.3, 1234.5),
                new Triangle(4.98, 3.67, 6.01)
            };

            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, originalFigures)));

            StreamFigureFactory factory = new StreamFigureFactory(new StreamReader(memoryStream), originalFigures.Count);
            List<IFigure> readFigures = factory.CreateFigures().ToList();

            for (int i = 0; i < originalFigures.Count; ++i)
            {
                Assert.AreEqual(originalFigures[i], readFigures[i]);
            }
        }

        [TestMethod]
        public void StreamFactoryReadsNoFiguresIfGivenEmptyStream()
        {
            StreamFigureFactory factory = new StreamFigureFactory(new StreamReader(new MemoryStream()), 10);
            List<IFigure> readFigures = factory.CreateFigures().ToList();

            Assert.AreEqual(readFigures.Count, 0);
        }

        [TestMethod]
        public void StreamFactoryIgnoresInvalidInput()
        {
            List<IFigure> originalFigures = new List<IFigure>()
            {
                new Circle(1.2),
                new Rectangle(2.3, 4.5),
                new Triangle(5, 5, 6)
            };

            List<string> testInput = new List<string>()
            {
                "123987",
                originalFigures[0].ToString() ?? "",
                "triangle 1 1 999",
                "circle -12",
                originalFigures[1].ToString() ?? "",
                "ASD 123 QWERTY",
                originalFigures[2].ToString() ?? "",
                "\r\n"
            };

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, testInput)));

            StreamFigureFactory factory = new StreamFigureFactory(Console.In, testInput.Count);
            List<IFigure> readFigures = factory.CreateFigures().ToList();

            for (int i = 0; i < originalFigures.Count; ++i)
            {
                Assert.AreEqual(originalFigures[i], readFigures[i]);
            }
        }

        [TestMethod]
        public void StreamFactoryReadsCountTimesAndNoMore()
        {
            List<IFigure> originalFigures = new List<IFigure>()
            {
                new Circle(1.2),
                new Rectangle(2.3, 4.5),
                new Triangle(5, 5, 6)
            };
            int count = 2;

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, originalFigures)));

            StreamFigureFactory factory = new StreamFigureFactory(Console.In, count);
            List<IFigure> readFigures = factory.CreateFigures().ToList();

            Assert.AreEqual(count, readFigures.Count);
            for (int i = 0; i < count; ++i)
            {
                Assert.AreEqual(originalFigures[i], readFigures[i]);
            }
        }

        [TestMethod]
        public void StreamFactoryReadsAllStreamIfCountIsGreaterThanStreamSize()
        {
            List<IFigure> originalFigures = new List<IFigure>()
            {
                new Circle(1.2),
                new Rectangle(2.3, 4.5),
                new Triangle(5, 5, 6)
            };
            int count = 12;

            Console.SetIn(new StringReader(string.Join(Environment.NewLine, originalFigures)));

            StreamFigureFactory factory = new StreamFigureFactory(Console.In, count);
            List<IFigure> readFigures = factory.CreateFigures().ToList();

            Assert.AreEqual(originalFigures.Count, readFigures.Count);
            for (int i = 0; i < originalFigures.Count; ++i)
            {
                Assert.AreEqual(originalFigures[i], readFigures[i]);
            }
        }
    }
}