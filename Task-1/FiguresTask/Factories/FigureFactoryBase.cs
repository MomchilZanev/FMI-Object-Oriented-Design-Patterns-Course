using FiguresTask.Figures;
using System.Globalization;
using System.Reflection;

namespace FiguresTask.Factories
{
    public abstract class FigureFactoryBase : IFigureFactory
    {
        protected Dictionary<string, string> validTypes;

        public FigureFactoryBase()
        {
            this.validTypes = Assembly.GetExecutingAssembly()
                                      .GetTypes()
                                      .Where(t => t.GetInterfaces().Contains(typeof(IFigure)))
                                      .ToDictionary(t => t.Name.ToLower(), t => t.FullName ?? t.Name);
        }

        public abstract IEnumerable<IFigure> CreateFigures();

        protected IFigure CreateFigure(string figureType, object[] arguments)
        {
            if (!this.validTypes.ContainsKey(figureType))
                throw new ArgumentException(string.Format("Invalid figure type \"{0}\" passed. Valid types are: [ {1} ]", figureType, string.Join(", ", validTypes)));

            ConstructorInfo? figureCtor = this.GetFigureConstructor(figureType, arguments);

            if (figureCtor is null)
                throw new ArgumentException(string.Format("Cannot find constructor of type \"{0}\" with arguments [ {1} ]", this.validTypes[figureType], string.Join(", ", arguments)));

            // Assuming that all figure constructors have arguments of type 'double'
            return (IFigure)figureCtor.Invoke(arguments.Select(a => (object)double.Parse((string)a, CultureInfo.InvariantCulture)).ToArray());
        }

        protected ConstructorInfo? GetFigureConstructor(string figureType, object[]? arguments = null)
        {
            Type? type = Assembly.GetExecutingAssembly().GetType(this.validTypes[figureType]);
            if (type is null)
                throw new ArgumentException(string.Format("Cannot find type {0}", this.validTypes[figureType]));

            if (arguments is null)
            {
                return type.GetConstructors().FirstOrDefault(x => x.GetParameters().Length != 0);
            }
            else
            {
                // Assuming that all figure constructors have arguments of type 'double'
                return type.GetConstructor(Enumerable.Repeat(typeof(double), arguments.Length).ToArray());
            }
        }
    }
}