using FiguresTask.Figures;
using System.Globalization;

namespace Tests.FigureTests
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        public void CircleInvalidArgumentsTest_1()
        {
            double r = 0;

            Assert.ThrowsException<ArgumentException>(() => new Circle(r));
        }

        [TestMethod]
        public void CircleInvalidArgumentsTest_2()
        {
            double r = -0.123;

            Assert.ThrowsException<ArgumentException>(() => new Circle(r));
        }


        [TestMethod]
        public void CirclePerimeterTest_1()
        {
            double r = 1;

            Circle circle = new Circle(r);

            Assert.AreEqual(Math.Round(2.0 * Math.PI * r, IFigure.DecimalPrecision), circle.Perimeter());
        }

        [TestMethod]
        public void CirclePerimeterTest_2()
        {
            double r = 2.768964;

            Circle circle = new Circle(r);

            Assert.AreEqual(Math.Round(2.0 * Math.PI * r, IFigure.DecimalPrecision), circle.Perimeter());
        }

        [TestMethod]
        public void CircleToStringTest_1()
        {
            double r = 1;

            Circle circle = new Circle(r);

            string expectedString = string.Format(new NumberFormatInfo() { NumberDecimalDigits = IFigure.DecimalPrecision }, "circle {0:F}", r);
            Assert.AreEqual(expectedString, circle.ToString());
        }

        [TestMethod]
        public void CircleToStringTest_2()
        {
            double r = 2.768964;

            Circle circle = new Circle(r);

            string expectedString = string.Format(new NumberFormatInfo() { NumberDecimalDigits = IFigure.DecimalPrecision }, "circle {0:F}", r);
            Assert.AreEqual(expectedString, circle.ToString());
        }
    }
}