using FiguresTask.Figures;

namespace FiguresTask.Factories
{
    public class StreamFigureFactory : FigureFactoryBase
    {
        private TextReader textReader;
        private readonly int? count;

        public StreamFigureFactory(TextReader textReader, int? count = null)
        {
            this.textReader = textReader;
            this.count = count;
        }

        public override IEnumerable<IFigure> CreateFigures()
        {
            int count = this.count ?? base.PromptFigureCount();
            for (int i = 0; i < count; ++i)
            {
                string? line = textReader.ReadLine();

                if (this.ValidateInput(line, out List<string> tokens))
                {
                    IFigure? figure = null;

                    try { figure = base.CreateFigure(tokens.First(), tokens.Skip(1).ToList()); }
                    catch (ArgumentException) { continue; } // Argument exceptions expected, continue reading

                    yield return figure;
                }
            }
        }

        private bool ValidateInput(string? input, out List<string> tokens)
        {
            if (string.IsNullOrEmpty(input))
            {
                tokens = new List<string>();
                return false;
            }

            tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            return tokens.Count > 0;
        }
    }
}