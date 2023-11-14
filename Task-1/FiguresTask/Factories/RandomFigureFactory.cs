using FiguresTask.Figures;
using System.Reflection;

namespace FiguresTask.Factories
{
    public class RandomFigureFactory : FigureFactoryBase
    {
        const double defaultMinThreshold = 0.01;
        const double defaultMaxThreshold = 10.0;

        private double minThreshold;
        private double maxThreshold;
        private Random random;

        public RandomFigureFactory(double? minArgumentThreshold, double? maxArgumentThreshold) : base()
        {
            this.minThreshold = minArgumentThreshold ?? defaultMinThreshold;
            this.maxThreshold = maxArgumentThreshold ?? defaultMaxThreshold;
            this.random = new Random();
        }

        public override IEnumerable<IFigure> CreateFigures()
        {
            Console.WriteLine("How many figures to create:");
            int count = int.Parse(Console.ReadLine() ?? "");

            string? retryType = null;
            for (int i = 0; i < count; ++i)
            {
                string figureType = retryType ?? this.validTypes.Keys.ElementAt(this.random.Next(0, base.validTypes.Count)).ToLower();
                ConstructorInfo? figureCtor = base.GetFigureConstructor(figureType);

                if (figureCtor is null)
                    throw new ArgumentException(string.Format("Cannot find constructor of type \"{0}\"", this.validTypes[figureType]));

                object[] arguments = this.getRandomArguments(figureCtor.GetParameters().Length).ToArray();

                // Retry until a valid figure is created using the random parameters (currently only triangle fails)
                IFigure figure;
                try
                {
                    figure = (IFigure)figureCtor.Invoke(arguments);
                }
                catch
                {
                    retryType = typeof(Triangle).Name.ToLower();
                    --i;
                    continue;
                }
                retryType = null;
                yield return figure;
            }
        }

        private IEnumerable<object> getRandomArguments(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return this.random.NextDouble() * (this.maxThreshold - this.minThreshold) + this.minThreshold;
            }
        }
    }
}