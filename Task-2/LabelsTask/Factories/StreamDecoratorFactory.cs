using LabelsTask.Decorators;
using LabelsTask.Labels;
using LabelsTask.Transformations;

namespace LabelsTask.Factories
{
    public class StreamDecoratorFactory
    {
        private StreamTextTransformationFactory textTransformationFactory;
        private TextReader textReader;
        private TextWriter textWriter;
        private List<string> availableTypes;
        private readonly string informationPrompt;

        public StreamDecoratorFactory(TextReader textReader, TextWriter textWriter, StreamTextTransformationFactory textTransformationFactory)
        {
            this.textTransformationFactory = textTransformationFactory;
            this.textReader = textReader;
            this.textWriter = textWriter;
            this.availableTypes = new List<string>() { "text-transformation", "random", "cycling" };
            this.informationPrompt = string.Format("Create a decorator.\nAvailable types: [ {0} ]\nChoose decorator type.", string.Join(", ", this.availableTypes));
        }

        public LabelDecoratorBase CreateDecorator(ILabel label)
        {
            this.textWriter.WriteLine(this.informationPrompt);
            string decoratorType = this.textReader.ReadLine() ?? string.Empty;

            try
            {
                switch (decoratorType)
                {
                    case "text-transformation":
                        return new TextTransformationDecorator(label, this.textTransformationFactory.CreateTextTransformation());
                    case "random":
                        return new RandomTransformationDecorator(label, this.CreateTransformations("random"));
                    case "cycling":
                        return new CyclingTransformationDecorator(label, this.CreateTransformations("cycling"));
                    default:
                        throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                this.textWriter.WriteLine("Invalid input!");
                return this.CreateDecorator(label);
            }
        }

        private List<ITextTransformation> CreateTransformations(string decoratorType)
        {
            List<ITextTransformation> transformations = new List<ITextTransformation>();

            do
            {
                this.textWriter.WriteLine(string.Format("Creating transformation for {0} decorator.\nChoose inner transformation:", decoratorType));
                ITextTransformation transformation = this.textTransformationFactory.CreateTextTransformation();

                transformations.Add(transformation);

                Console.WriteLine(string.Format("Add more transformations to {0} decorator?", decoratorType));
            } while ((Console.ReadLine() ?? string.Empty).Trim().ToLower() == "y");

            return transformations;
        }
    }
}