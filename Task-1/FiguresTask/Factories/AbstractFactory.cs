using System.Globalization;

namespace FiguresTask.Factories
{
    public static class AbstractFactory
    {
        public static IFigureFactory Create(List<string> args)
        {
            if (args == null) throw new ArgumentNullException("Invalid input.");

            string inputMethod = args[0];

            switch (inputMethod)
            {
                case "console":
                    return new StreamFigureFactory(Console.In);
                case "file":
                    string filePath = args.Count > 2 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "dummy-input.txt");
                    if (!File.Exists(filePath)) File.Create(filePath).Close();
                    return new StreamFigureFactory(new StreamReader(filePath));
                case "random":
                    double? minThreshold = args.Count > 1 && double.TryParse(args[1], CultureInfo.InvariantCulture, out double min) ? min : null;
                    double? maxThreshold = args.Count > 2 && double.TryParse(args[1], CultureInfo.InvariantCulture, out double max) ? max : null;
                    return new RandomFigureFactory(minThreshold, maxThreshold);
                default:
                    throw new ArgumentException("Invalid input method");
            }
        }
    }
}