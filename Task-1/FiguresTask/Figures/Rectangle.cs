using System.Globalization;

namespace FiguresTask.Figures
{
    public class Rectangle : IFigure
    {
        private double a;
        private double b;

        public Rectangle(double a, double b)
        {
            if (a <= 0 || b <= 0)
                throw new ArgumentException("Rectangle sides must be greater than zero");

            this.a = a;
            this.b = b;
        }

        public Rectangle(Rectangle rectangle)
        {
            this.a = rectangle.a;
            this.b = rectangle.b;
        }

        public double Perimeter()
        {
            return Math.Round(2 * (this.a + this.b), IFigure.DecimalPrecision);
        }

        public object Clone()
        {
            return new Rectangle(this);
        }

        public override string ToString()
        {
            return string.Join(' ',
                this.GetType().Name.ToLower(),
                this.a.ToString($"F{IFigure.DecimalPrecision}", CultureInfo.InvariantCulture),
                this.b.ToString($"F{IFigure.DecimalPrecision}", CultureInfo.InvariantCulture)
            );
        }

        // Two rectangles are considered equal if they have the same combination of sides (decimal precision of <IFigure.DecimalPrecision>)
        public override bool Equals(object? obj)
        {
            if (obj is not Rectangle rectangle) return false;

            List<double> current = new List<double> { this.a, this.b };
            current.Select(x => Math.Round(x, IFigure.DecimalPrecision)).OrderBy(x => x).ToList();

            List<double> other = new List<double> { rectangle.a, rectangle.b };
            other.Select(x => Math.Round(x, IFigure.DecimalPrecision)).OrderBy(x => x).ToList();

            return current.SequenceEqual(other);
        }
    }
}
