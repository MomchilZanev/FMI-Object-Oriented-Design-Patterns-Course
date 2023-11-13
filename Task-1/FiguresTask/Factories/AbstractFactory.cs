namespace FiguresTask.Factories
{
    public static class AbstractFactory
    {
        public static IFigureFactory Create(string[]? args)
        {
            if (args == null) throw new ArgumentNullException("args");

            string inputMethod = args[0];

            switch (inputMethod)
            {
                case "console":
                    return new StreamFigureFactory(Console.In);
                case "file":
                    if (args.Length < 2) throw new ArgumentException("The file input method requires an additional argument - file path.");
                    string filePath = args[1];
                    return new StreamFigureFactory(new StreamReader(filePath));
                case "random":
                    double? minThreshold = args.Length > 1 ? double.Parse(args[1]) : null;
                    double? maxThreshold = args.Length > 2 ? double.Parse(args[2]) : null;
                    return new RandomFigureFactory(minThreshold, maxThreshold);
                default:
                    throw new ArgumentException("Invalid input method");
            }
        }
    }
}