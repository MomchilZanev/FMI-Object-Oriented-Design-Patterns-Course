using FiguresTask.Figures;

namespace FiguresTask.Factories
{
    public interface IFigureFactory
    {
        IEnumerable<IFigure> CreateFigures();
    }
}