using FiguresTask.Figures;
using System.Globalization;

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

            Assert.AreEqual(Math.Round(2 * (a + b), IFigure.DecimalPrecision), rectangle.Perimeter());
        }

        [TestMethod]
        public void RectanglePerimeterTest_2()
        {
            double a = 1.11001;
            double b = 2.220000001;

            Rectangle rectangle = new Rectangle(a, b);

            Assert.AreEqual(Math.Round(2 * (a + b), IFigure.DecimalPrecision), rectangle.Perimeter());
        }

        [TestMethod]
        public void RectangleToStringTest_1()
        {
            double a = 1;
            double b = 2;

            Rectangle rectangle = new Rectangle(a, b);

            string expectedString = string.Format(new NumberFormatInfo() { NumberDecimalDigits = IFigure.DecimalPrecision }, "rectangle {0:F} {1:F}", a, b);
            Assert.AreEqual(expectedString, rectangle.ToString());
        }

        [TestMethod]
        public void RectangleToStringTest_2()
        {
            double a = 2.13623;
            double b = 0.123;

            Rectangle rectangle = new Rectangle(a, b);

            string expectedString = string.Format(new NumberFormatInfo() { NumberDecimalDigits = IFigure.DecimalPrecision }, "rectangle {0:F} {1:F}", a, b);
            Assert.AreEqual(expectedString, rectangle.ToString());
        }
    }
}