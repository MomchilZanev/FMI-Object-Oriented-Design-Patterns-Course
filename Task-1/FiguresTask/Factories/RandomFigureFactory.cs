using FiguresTask.Figures;
using System.Reflection;

namespace FiguresTask.Factories
{
    public class RandomFigureFactory : IFigureFactory
    {
        const double defaultMinThreshold = 0.01;
        const double defaultMaxThreshold = 10.0;

        private double minThreshold;
        private double maxThreshold;
        private Random random;

        public RandomFigureFactory(double? minArgumentThreshold, double? maxArgumentThreshold)
        {
            this.minThreshold = minArgumentThreshold ?? defaultMinThreshold;
            this.maxThreshold = maxArgumentThreshold ?? defaultMaxThreshold;
            this.random = new Random();
        }

        public IEnumerable<IFigure> CreateFigures()
        {
            List<string> validTypes = Assembly.GetExecutingAssembly().GetTypes()
                                              .Where(t => t.GetInterfaces().Contains(typeof(IFigure)))
                                              .Select(t => t.FullName ?? t.Name)
                                              .ToList();

            Console.WriteLine("How many figures to create:");
            int count = int.Parse(Console.ReadLine() ?? "");
            string? retryType = null;
            for (int i = 0; i < count; ++i)
            {
                string figureType = retryType ?? validTypes[this.random.Next(0, validTypes.Count)];

                Type? type = Assembly.GetExecutingAssembly().GetType(figureType);
                if (type is null)
                    throw new ArgumentException(string.Format("Cannot find type {0}", figureType));

                ConstructorInfo figureCtor = type.GetConstructors().First(x => x.GetParameters().Length != 0);
                object[] arguments = this.getRandomArguments(figureCtor.GetParameters().Length).ToArray();

                // Retry until a valid figure is created using the random parameters (currently only triangle fails)
                IFigure figure;
                try
                {
                    figure = (IFigure)figureCtor.Invoke(arguments);
                }
                catch
                {
                    retryType = typeof(Triangle).FullName;
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