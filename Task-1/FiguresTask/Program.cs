using FiguresTask.Factories;
using FiguresTask.Figures;

namespace FiguresTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> tokens = new List<string>();

            do
            {
                Console.WriteLine("Choose input method. [ console, file <file-path>, random <min-threshold = 0.01> <max-threshold = 10>]");
                tokens = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            } while (tokens.Count < 1);

            IFigureFactory factory = AbstractFactory.Create(tokens);
            List<IFigure> figures = factory.CreateFigures().ToList();

            while (true)
            {
                Console.WriteLine("Choose command. [ print, delete <index>, duplicate <index>, save-file <file-path>]");
                List<string> commandTokens = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                string? command = commandTokens.FirstOrDefault();

                switch (command)
                {
                    case "print":
                        printFigures(Console.Out, figures);
                        break;
                    case "delete":
                        if (commandTokens.Count > 1
                            && int.TryParse(commandTokens[1], out int deleteIindex)
                            && deleteIindex > 0
                            && figures.Count > deleteIindex)
                            figures.RemoveAt(deleteIindex);
                        break;
                    case "duplicate":
                        if (commandTokens.Count > 1
                            && int.TryParse(commandTokens[1], out int duplicateIindex)
                            && duplicateIindex > 0
                            && figures.Count > duplicateIindex)
                            figures.Add((IFigure)figures[duplicateIindex].Clone());
                        break;
                    case "save-file":
                        if (commandTokens.Count > 1)
                        {
                            StreamWriter streamWriter = new StreamWriter(commandTokens[1]) { AutoFlush = true };
                            printFigures(streamWriter, figures);
                        }
                        break;
                    default:
                        return;
                }
            }
        }

        private static void printFigures(TextWriter textWriter, List<IFigure> figures)
        {
            textWriter.WriteLine(string.Join(Environment.NewLine, figures));
        }
    }
}