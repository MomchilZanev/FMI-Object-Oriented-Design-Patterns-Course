using FiguresTask.Figures;
using System.Globalization;

namespace FiguresTask.Factories
{
    public abstract class FigureFactoryBase : IFigureFactory
    {
        public abstract IEnumerable<IFigure> CreateFigures();

        protected int PromptFigureCount()
        {
            int count = -1;
            do
            {
                Console.WriteLine("Figure count:");
            } while (!(int.TryParse(Console.ReadLine(), out count) && count > 0));
            return count;
        }

        protected IFigure CreateFigure(string figureType, List<string> arguments)
        {
            List<double> parsedArguments = new List<double>();

            foreach (string argument in arguments)
            {
                if (double.TryParse(argument, CultureInfo.InvariantCulture, out double parsedArgument))
                    parsedArguments.Add(parsedArgument);
            }

            return this.CreateFigure(figureType, parsedArguments);
        }

        protected IFigure CreateFigure(string figureType, List<double> arguments)
        {
            switch (figureType.ToLower())
            {
                case "circle":
                    return this.CreateCircle(arguments);
                case "rectangle":
                    return this.CreateRectangle(arguments);
                case "triangle":
                    return this.CreateTriangle(arguments);
                default:
                    throw new ArgumentException("Invalid figure type.");
            }
        }

        protected Circle CreateCircle(List<double> arguments)
        {
            if (arguments.Count < 1) throw new ArgumentException("Insufficient arguments.");
            return new Circle(arguments[0]);
        }

        protected Rectangle CreateRectangle(List<double> arguments)
        {
            if (arguments.Count < 2) throw new ArgumentException("Insufficient arguments.");
            return new Rectangle(arguments[0], arguments[1]);
        }

        protected Triangle CreateTriangle(List<double> arguments)
        {
            if (arguments.Count < 3) throw new ArgumentException("Insufficient arguments.");
            return new Triangle(arguments[0], arguments[1], arguments[2]);
        }
    }
}