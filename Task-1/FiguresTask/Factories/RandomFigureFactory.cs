using FiguresTask.Figures;
using System.Collections.Immutable;

namespace FiguresTask.Factories
{
    public class RandomFigureFactory : FigureFactoryBase
    {
        const double defaultMinThreshold = 0.01;
        const double defaultMaxThreshold = 10.0;

        private readonly ImmutableList<string> figureTypes;
        private readonly ImmutableList<string> specialFigureTypes;
        private readonly double minThreshold;
        private readonly double maxThreshold;
        private readonly Random random;

        public RandomFigureFactory(double? minArgumentThreshold, double? maxArgumentThreshold) : base()
        {
            this.figureTypes = ImmutableList.Create("circle", "rectangle", "triangle");
            this.specialFigureTypes = ImmutableList.Create("triangle");
            this.minThreshold = minArgumentThreshold ?? defaultMinThreshold;
            this.maxThreshold = maxArgumentThreshold ?? defaultMaxThreshold;
            this.random = new Random();
        }

        public override IEnumerable<IFigure> CreateFigures()
        {
            int count = base.PromptFigureCount();
            for (int i = 0; i < count; ++i)
            {
                string figureType = this.figureTypes.ElementAt(this.random.Next(0, this.figureTypes.Count));
                yield return this.CreateFigure(figureType);
            }
        }

        protected IFigure CreateFigure(string figureType)
        {
            switch (figureType)
            {
                case "circle":
                    return base.CreateCircle(new List<double> { this.getRandomDouble(this.minThreshold, this.maxThreshold) });
                case "rectangle":
                    return base.CreateRectangle(this.getRandomArguments(2).ToList());
                case "triangle":
                    List<double> triangleArgs = this.getRandomArguments(2).ToList();
                    double lowerBound = Math.Max(triangleArgs[0] - triangleArgs[1], triangleArgs[1] - triangleArgs[0]);
                    double upperBound = Math.Min(triangleArgs[0] + triangleArgs[1], this.maxThreshold);
                    triangleArgs.Add(this.getRandomDouble(lowerBound, upperBound));
                    return base.CreateTriangle(triangleArgs);
                default:
                    throw new ArgumentException("Invalid figure type.");
            }
        }

        private IEnumerable<double> getRandomArguments(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return this.getRandomDouble(this.minThreshold, this.maxThreshold);
            }
        }

        private double getRandomDouble(double min, double max)
        {
            return this.random.NextDouble() * (max - min) + min;
        }
    }
}