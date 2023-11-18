using FiguresTask.Factories;
using FiguresTask.Figures;

namespace Tests.FactoryTests
{
    [TestClass]
    public class RandomFigureFactoryTests
    {
        [TestMethod]
        public void RandomFactoryCreatesValidFiguresTest()
        {
            int count = 100000;
            RandomFigureFactory factory = new RandomFigureFactory(null, null, count);

            try
            {
                List<IFigure> figures = factory.CreateFigures().ToList();
                Assert.AreEqual(count, figures.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("RandomFigureFactory throws exception \"{0}\"", ex.Message));
            }
        }

        [TestMethod]
        public void RandomFactoryCreatesRandomFigureTypes()
        {
            int count = 100000;
            RandomFigureFactory factory = new RandomFigureFactory(null, null, count);

            List<IFigure> figures = factory.CreateFigures().ToList();

            int circles = figures.Where(f => f.GetType() == typeof(Circle)).Count();
            int rectangles = figures.Where(f => f.GetType() == typeof(Rectangle)).Count();
            int triangles = figures.Where(f => f.GetType() == typeof(Triangle)).Count();

            // Factory is considered random if all figure types are reoughly 1/3 of the total figures.
            // Where roughly is assumed to be +/- 1% of the total figures.
            int lowerThreshold = (count / 3) - (count / 100);
            int upperThreshold = (count / 3) + (count / 100);

            Assert.IsTrue(lowerThreshold <= circles && circles <= upperThreshold,
                          string.Format("lower: {0}, upper: {1}, circles: {2}", lowerThreshold, upperThreshold, circles));
            Assert.IsTrue(lowerThreshold <= rectangles && rectangles <= upperThreshold,
                          string.Format("lower: {0}, upper: {1}, rectangles: {2}", lowerThreshold, upperThreshold, rectangles));
            Assert.IsTrue(lowerThreshold <= triangles && triangles <= upperThreshold,
                          string.Format("lower: {0}, upper: {1}, triangles: {2}", lowerThreshold, upperThreshold, triangles));
        }

        [TestMethod]
        public void RandomFactoryCreatesCirclesWithPerimeterInRange()
        {
            int count = 300000;
            double lowerThreshold = 0.01;
            double upperThreshold = 100.9;
            RandomFigureFactory factory = new RandomFigureFactory(lowerThreshold, upperThreshold, count);

            List<IFigure> figures = factory.CreateFigures().ToList();

            List<IFigure> circles = figures.Where(f => f.GetType() == typeof(Circle)).ToList();

            double lowerPerimeter = Math.Round(2.0 * Math.PI * lowerThreshold, IFigure.DecimalPrecision);
            double upperPerimeter = Math.Round(2.0 * Math.PI * upperThreshold, IFigure.DecimalPrecision);

            Assert.IsTrue(circles.All(c => lowerPerimeter <= c.Perimeter() && c.Perimeter() <= upperPerimeter));
        }

        [TestMethod]
        public void RandomFactoryCreatesCirclesWithRandomPerimeter()
        {
            int count = 300000;
            double lowerThreshold = 50.0;
            double upperThreshold = 200.0;
            RandomFigureFactory factory = new RandomFigureFactory(lowerThreshold, upperThreshold, count);

            List<IFigure> figures = factory.CreateFigures().ToList();

            List<IFigure> circles = figures.Where(f => f.GetType() == typeof(Circle)).ToList();

            // Factory is considered random if the circles it creates have an average perimeter close to the perimeter of a
            // circle with radius equal to the average of the radius thresholds.
            // Where close is assumed to be expected +/- 1% of expected.
            double expectedAveragePerimeter = Math.Round(Math.PI * (lowerThreshold + upperThreshold), IFigure.DecimalPrecision);
            double lowerPerimeter = expectedAveragePerimeter - (expectedAveragePerimeter / 100);
            double upperPerimeter = expectedAveragePerimeter + (expectedAveragePerimeter / 100);

            double averagePermieter = circles.Select(c => c.Perimeter()).Average();

            Assert.IsTrue(lowerPerimeter <= averagePermieter && averagePermieter <= upperPerimeter,
                          string.Format("lower: {0}, upper: {1}, actual: {2}", lowerPerimeter, upperPerimeter, averagePermieter));
        }

        [TestMethod]
        public void RandomFactoryCreatesRectanglesWithPerimeterInRange()
        {
            int count = 300000;
            double lowerThreshold = 1.5;
            double upperThreshold = 110.6;
            RandomFigureFactory factory = new RandomFigureFactory(lowerThreshold, upperThreshold, count);

            List<IFigure> figures = factory.CreateFigures().ToList();

            List<IFigure> rectangles = figures.Where(f => f.GetType() == typeof(Rectangle)).ToList();

            double lowerPerimeter = Math.Round(4.0 * lowerThreshold, IFigure.DecimalPrecision);
            double upperPerimeter = Math.Round(4.0 * upperThreshold, IFigure.DecimalPrecision);

            Assert.IsTrue(rectangles.All(c => lowerPerimeter <= c.Perimeter() && c.Perimeter() <= upperPerimeter));
        }

        [TestMethod]
        public void RandomFactoryCreatesRectanglesWithRandomPerimeter()
        {
            int count = 300000;
            double lowerThreshold = 13.0;
            double upperThreshold = 325.0;
            RandomFigureFactory factory = new RandomFigureFactory(lowerThreshold, upperThreshold, count);

            List<IFigure> figures = factory.CreateFigures().ToList();

            List<IFigure> rectangles = figures.Where(f => f.GetType() == typeof(Rectangle)).ToList();

            // Factory is considered random if the rectangles it creates have an average perimeter close to the perimeter of a
            // rectangle with both sides equal to the average of the side thresholds.
            // Where close is assumed to be expected +/- 1% of expected.
            double expectedAveragePerimeter = Math.Round(2.0 * (lowerThreshold + upperThreshold), IFigure.DecimalPrecision);
            double lowerPerimeter = expectedAveragePerimeter - (expectedAveragePerimeter / 100);
            double upperPerimeter = expectedAveragePerimeter + (expectedAveragePerimeter / 100);

            double averagePermieter = rectangles.Select(c => c.Perimeter()).Average();

            Assert.IsTrue(lowerPerimeter <= averagePermieter && averagePermieter <= upperPerimeter,
                          string.Format("lower: {0}, upper: {1}, actual: {2}", lowerPerimeter, upperPerimeter, averagePermieter));
        }

        [TestMethod]
        public void RandomFactoryCreatesTrianglesWithPerimeterInRange()
        {
            int count = 300000;
            double lowerThreshold = 132.0;
            double upperThreshold = 624.0;
            RandomFigureFactory factory = new RandomFigureFactory(lowerThreshold, upperThreshold, count);

            List<IFigure> figures = factory.CreateFigures().ToList();

            List<IFigure> triangles = figures.Where(f => f.GetType() == typeof(Triangle)).ToList();

            double lowerPerimeter = Math.Round(3.0 * lowerThreshold, IFigure.DecimalPrecision);
            double upperPerimeter = Math.Round(3.0 * upperThreshold, IFigure.DecimalPrecision);

            Assert.IsTrue(triangles.All(c => lowerPerimeter <= c.Perimeter() && c.Perimeter() <= upperPerimeter));
        }

        [TestMethod]
        public void RandomFactoryCreatesTriagnlesWithRandomPerimeter()
        {
            int count = 300000;
            double lowerThreshold = 1231.124;
            double upperThreshold = 98237562938.1634;
            RandomFigureFactory factory = new RandomFigureFactory(lowerThreshold, upperThreshold, count);

            List<IFigure> figures = factory.CreateFigures().ToList();

            List<IFigure> triangles = figures.Where(f => f.GetType() == typeof(Triangle)).ToList();

            // Factory is considered random if the triangles it creates have an average perimeter close to the perimeter of a
            // triangle whose sides are equal to the average of the side thresholds (adjusted for the bias introduced by the inequality rule).
            // Where close is assumed to be expected +/- 1% of expected.
            double expectedAveragePerimeter = Math.Round((3.0 / 2.0 * (lowerThreshold + upperThreshold)) * 1.053, IFigure.DecimalPrecision);
            double lowerPerimeter = expectedAveragePerimeter - (expectedAveragePerimeter / 100);
            double upperPerimeter = expectedAveragePerimeter + (expectedAveragePerimeter / 100);

            double averagePermieter = triangles.Select(c => c.Perimeter()).Average();

            Assert.IsTrue(lowerPerimeter <= averagePermieter && averagePermieter <= upperPerimeter,
                          string.Format("lower: {0}, upper: {1}, actual: {2}", lowerPerimeter, upperPerimeter, averagePermieter));
        }
    }
}