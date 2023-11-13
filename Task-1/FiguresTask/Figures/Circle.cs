using System.Globalization;

namespace FiguresTask.Figures
{
    public class Circle : IFigure
    {
        private double r;

        public Circle(double r)
        {
            if (r <= 0)
                throw new ArgumentException("Circle radius must be greater than zero");

            this.r = r;
        }

        public Circle(Circle circle)
        {
            this.r = circle.r;
        }

        public double Perimeter()
        {
            return Math.Round(2 * Math.PI * this.r, IFigure.DecimalPrecision);
        }

        public object Clone()
        {
            return new Circle(this);
        }

        public override string ToString()
        {
            return string.Join(' ',
                this.GetType().Name.ToLower(),
                this.r.ToString($"F{IFigure.DecimalPrecision}", CultureInfo.InvariantCulture)
            );
        }

        // Two circles are considered equal if they have the same radius (decimal precision of <IFigure.DecimalPrecision>)
        public override bool Equals(object? obj)
        {
            if (obj is not Circle circle) return false;

            return Math.Round(this.r, IFigure.DecimalPrecision) == Math.Round(circle.r, IFigure.DecimalPrecision);
        }
    }
}