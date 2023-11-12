using FiguresTask.Figures;

namespace Tests.FigureTests
{
    [TestClass]
    public class RectangleTests
    {
        [TestMethod]
        public void RectangleInvalidArgumentsTest_1()
        {
            double a = 0;
            double b = 2;

            Assert.ThrowsException<ArgumentException>(() => new Rectangle(a, b));
        }

        [TestMethod]
        public void RectangleInvalidArgumentsTest_2()
        {
            double a = 1;
            double b = -2;

            Assert.ThrowsException<ArgumentException>(() => new Rectangle(a, b));
        }

        [TestMethod]
        public void RectangleInvalidArgumentsTest_3()
        {
            double a = -1;
            double b = -0.1231;

            Assert.ThrowsException<ArgumentException>(() => new Rectangle(a, b));
        }

        [TestMethod]
        public void RectanglePerimeterTest_1()
        {
            double a = 1;
            double b = 2;

            Rectangle rectangle = new Rectangle(a, b);

            Assert.AreEqual(6, rectangle.Perimeter());
        }

        [TestMethod]
        public void RectanglePerimeterTest_2()
        {
            double a = 1.11001;
            double b = 2.220000001;

            Rectangle rectangle = new Rectangle(a, b);

            Assert.AreEqual(6.66, rectangle.Perimeter());
        }

        [TestMethod]
        public void RectangleToStringTest_1()
        {
            double a = 1;
            double b = 2;

            Rectangle rectangle = new Rectangle(a, b);

            Assert.AreEqual("rectangle 1.00 2.00", rectangle.ToString());
        }

        [TestMethod]
        public void RectangleToStringTest_2()
        {
            double a = 2.13623;
            double b = 0.123;

            Rectangle rectangle = new Rectangle(a, b);

            Assert.AreEqual("rectangle 2.14 0.12", rectangle.ToString());
        }
    }
}