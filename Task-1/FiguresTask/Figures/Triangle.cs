using System.Globalization;

namespace FiguresTask.Figures
{
    public class Triangle : IFigure
    {
        private double a;
        private double b;
        private double c;

        public Triangle(double a, double b, double c)
        {
            if (!AreValidTriangleSides(a, b, c))
                throw new ArgumentException(string.Format("Sides [ {0} ] do not make a valid triangle", string.Join(", ", a, b, c)));

            this.a = a;
            this.b = b;
            this.c = c;
        }

        public Triangle(Triangle triangle)
        {
            this.a = triangle.a;
            this.b = triangle.b;
            this.c = triangle.c;
        }

        public double Perimeter()
        {
            return Math.Round(this.a + this.b + this.c, IFigure.DecimalPrecision);
        }

        public object Clone()
        {
            return new Triangle(this);
        }

        public override string ToString()
        {
            return string.Join(' ',
                this.GetType().Name.ToLower(),
                this.a.ToString($"F{IFigure.DecimalPrecision}", CultureInfo.InvariantCulture),
                this.b.ToString($"F{IFigure.DecimalPrecision}", CultureInfo.InvariantCulture),
                this.c.ToString($"F{IFigure.DecimalPrecision}", CultureInfo.InvariantCulture)
            );
        }

        // Two triangles are considered equal if they have the same combination of sides (decimal precision of <IFigure.DecimalPrecision>)
        public override bool Equals(object? obj)
        {
            if (obj is not Triangle triangle) return false;

            List<double> current = new List<double> { this.a, this.b, this.c };
            current.Select(x => Math.Round(x, IFigure.DecimalPrecision)).OrderBy(x => x).ToList();

            List<double> other = new List<double> { triangle.a, triangle.b, triangle.c };
            other.Select(x => Math.Round(x, IFigure.DecimalPrecision)).OrderBy(x => x).ToList();

            return current.SequenceEqual(other);
        }

        private static bool AreValidTriangleSides(double a, double b, double c)
        {
            return a > 0
                && b > 0
                && c > 0
                && a + b > c
                && a + c > b
                && b + c > a;
        }
    }
}
