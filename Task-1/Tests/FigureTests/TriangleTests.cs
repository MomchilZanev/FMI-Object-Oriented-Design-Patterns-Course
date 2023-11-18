using FiguresTask.Figures;
using System.Drawing;
using System.Globalization;

namespace Tests.FigureTests
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void TriangleInvalidArgumentsTest_1()
        {
            double a = 1;
            double b = 2;
            double c = 3;

            Assert.ThrowsException<ArgumentException>(() => new Triangle(a, b, c));
        }

        [TestMethod]
        public void TriangleInvalidArgumentsTest_2()
        {
            double a = 1;
            double b = 0;
            double c = 1;

            Assert.ThrowsException<ArgumentException>(() => new Triangle(a, b, c));
        }

        [TestMethod]
        public void TriangleInvalidArgumentsTest_3()
        {
            double a = -1;
            double b = 1;
            double c = 1;

            Assert.ThrowsException<ArgumentException>(() => new Triangle(a, b, c));
        }

        [TestMethod]
        public void TrianglePerimeterTest_1()
        {
            double a = 1;
            double b = 2;
            double c = 2;

            Triangle triangle = new Triangle(a, b, c);

            Assert.AreEqual(Math.Round(a + b + c, IFigure.DecimalPrecision), triangle.Perimeter());
        }

        [TestMethod]
        public void TrianglePerimeterTest_2()
        {
            double a = 1.11001;
            double b = 2.220000001;
            double c = 2.2200001;

            Triangle triangle = new Triangle(a, b, c);

            Assert.AreEqual(Math.Round(a + b + c, IFigure.DecimalPrecision), triangle.Perimeter());
        }

        [TestMethod]
        public void TriangleToStringTest_1()
        {
            double a = 1;
            double b = 2;
            double c = 2;

            Triangle triangle = new Triangle(a, b, c);

            string expectedString = string.Format(new NumberFormatInfo() { NumberDecimalDigits = IFigure.DecimalPrecision }, "triangle {0:F} {1:F} {2:F}", a, b, c);
            Assert.AreEqual(expectedString, triangle.ToString());
        }

        [TestMethod]
        public void TriangleToStringTest_2()
        {
            double a = 2;
            double b = 1;
            double c = 2;

            Triangle triangle = new Triangle(a, b, c);

            string expectedString = string.Format(new NumberFormatInfo() { NumberDecimalDigits = IFigure.DecimalPrecision }, "triangle {0:F} {1:F} {2:F}", a, b, c);
            Assert.AreEqual(expectedString, triangle.ToString());
        }

        [TestMethod]
        public void TriangleToStringTest_3()
        {
            double a = 2.223412412412412;
            double b = 2.2219834715239451934;
            double c = 1.1401934712391;

            Triangle triangle = new Triangle(a, b, c);

            string expectedString = string.Format(new NumberFormatInfo() { NumberDecimalDigits = IFigure.DecimalPrecision }, "triangle {0:F} {1:F} {2:F}", a, b, c);
            Assert.AreEqual(expectedString, triangle.ToString());
        }
    }
}