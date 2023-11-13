using FiguresTask.Figures;
using System.Globalization;
using System.Reflection;

namespace FiguresTask.Factories
{
    public class StreamFigureFactory : IFigureFactory
    {
        private TextReader textReader;

        public StreamFigureFactory(TextReader textReader)
        {
            this.textReader = textReader;
        }

        public IEnumerable<IFigure> CreateFigures()
        {
            Console.WriteLine("How many figures to create:");
            int count = int.Parse(Console.ReadLine() ?? "");
            while (count > 0)
            {
                string? line = textReader.ReadLine();

                yield return CreateFigure(line);
                count--;
            }
        }

        private IFigure CreateFigure(string? input)
        {
            List<string> tokens = (input ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            Dictionary<string, string> validTypes = Assembly.GetExecutingAssembly().GetTypes()
                                              .Where(t => t.GetInterfaces().Contains(typeof(IFigure)))
                                              .ToDictionary(t => t.Name.ToLower(), t => t.FullName ?? t.Name);

            string figureType = tokens.First().ToLower();
            object[] arguments = tokens.Skip(1).Select(x => (object)double.Parse(x, CultureInfo.InvariantCulture)).ToArray();

            if (!validTypes.ContainsKey(figureType))
                throw new ArgumentException(string.Format("Invalid figure type \"{0}\" passed. Valid types are: [ {1} ]", figureType, string.Join(", ", validTypes)));


            Type? type = Assembly.GetExecutingAssembly().GetType(validTypes[figureType]);
            if (type is null)
                throw new ArgumentException(string.Format("Cannot find type {0}", validTypes[figureType]));

            ConstructorInfo figureCtor = type.GetConstructor(Enumerable.Repeat(typeof(double), arguments.Length).ToArray());

            return (IFigure)figureCtor.Invoke(arguments);
        }
    }
}