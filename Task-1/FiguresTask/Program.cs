using FiguresTask.Factories;
using FiguresTask.Figures;

namespace FiguresTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose input method. Available options: [ console, file, random ]");
            string[] tokens = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

            IFigureFactory factory = AbstractFactory.Create(tokens);
            List<IFigure> figures = factory.CreateFigures().ToList();

            while (true)
            {
                Console.WriteLine("Choose command. Available options: [ print, delete <index>, duplicate <index>, save-to-file <file-path>]");
                List<string> commandTokens = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                string? command = commandTokens.FirstOrDefault();

                switch (command)
                {
                    case "print":
                        foreach (IFigure figure in figures)
                        {
                            Console.WriteLine(figure);
                        }
                        break;
                    case "delete":
                        figures.RemoveAt(int.Parse(commandTokens[1]));
                        break;
                    case "duplicate":
                        figures.Add((IFigure)figures[int.Parse(commandTokens[1])].Clone());
                        break;
                    case "save-to-file":
                        StreamWriter streamWriter = new StreamWriter(commandTokens[1]);
                        streamWriter.AutoFlush = true;
                        foreach (IFigure figure in figures)
                        {
                            streamWriter.WriteLine(figure);
                        }
                        break;
                    default:
                        return;
                }
            }
        }
    }
}