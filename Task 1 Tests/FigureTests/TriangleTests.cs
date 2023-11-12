using Task_1.Figures;

namespace Task_1_Tests.FigureTests
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

            Assert.AreEqual(5, triangle.Perimeter());
        }

        [TestMethod]
        public void TrianglePerimeterTest_2()
        {
            double a = 1.11001;
            double b = 2.220000001;
            double c = 2.2200001;

            Triangle triangle = new Triangle(a, b, c);

            Assert.AreEqual(5.55, triangle.Perimeter());
        }

        [TestMethod]
        public void TriangleToStringTest_1()
        {
            double a = 1;
            double b = 2;
            double c = 2;

            Triangle triangle = new Triangle(a, b, c);

            Assert.AreEqual("triangle 1.00 2.00 2.00", triangle.ToString());
        }

        [TestMethod]
        public void TriangleToStringTest_2()
        {
            double a = 2;
            double b = 1;
            double c = 2;

            Triangle triangle = new Triangle(a, b, c);

            Assert.AreEqual("triangle 2.00 1.00 2.00", triangle.ToString());
        }

        [TestMethod]
        public void TriangleToStringTest_3()
        {
            double a = 2.22;
            double b = 2.22;
            double c = 1.11;

            Triangle triangle = new Triangle(a, b, c);

            Assert.AreEqual("triangle 2.22 2.22 1.11", triangle.ToString());
        }
    }
}