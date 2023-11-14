using FiguresTask.Figures;

namespace FiguresTask.Factories
{
    public class StreamFigureFactory : FigureFactoryBase
    {
        private TextReader textReader;

        public StreamFigureFactory(TextReader textReader)
        {
            this.textReader = textReader;
        }

        public override IEnumerable<IFigure> CreateFigures()
        {
            Console.WriteLine("How many figures to read:");
            int count = int.Parse(Console.ReadLine() ?? "");
            while (count > 0)
            {
                string? line = textReader.ReadLine();

                if (string.IsNullOrEmpty(line)) break;

                yield return this.CreateFigure(line);
                count--;
            }
        }

        protected IFigure CreateFigure(string? input)
        {
            List<string> tokens = (input ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            string figureType = tokens.First().ToLower();
            object[] arguments = tokens.Skip(1).ToArray();

            return base.CreateFigure(figureType, arguments);
        }
    }
}