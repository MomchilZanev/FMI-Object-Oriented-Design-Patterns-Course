using Task_1.Figures;

namespace Task_1_Tests.FigureTests
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

            Assert.AreEqual(6.28, circle.Perimeter());
        }

        [TestMethod]
        public void CirclePerimeterTest_2()
        {
            double r = 2.768964;

            Circle circle = new Circle(r);

            Assert.AreEqual(17.40, circle.Perimeter());
        }

        [TestMethod]
        public void CircleToStringTest_1()
        {
            double r = 1;

            Circle circle = new Circle(r);

            Assert.AreEqual("circle 1.00", circle.ToString());
        }

        [TestMethod]
        public void CircleToStringTest_2()
        {
            double r = 2.768964;

            Circle circle = new Circle(r);

            Assert.AreEqual("circle 2.77", circle.ToString());
        }
    }
}