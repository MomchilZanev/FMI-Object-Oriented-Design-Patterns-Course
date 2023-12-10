namespace FiguresTask.Figures
{
    public interface IFigure : ICloneable
    {
        const int DecimalPrecision = 2;

        double Perimeter();
    }
}
