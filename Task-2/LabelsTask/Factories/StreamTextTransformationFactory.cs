using LabelsTask.Transformations;

namespace LabelsTask.Factories
{
    public class StreamTextTransformationFactory
    {
        private CensorTransformationFactory? censorTransformationFactory;
        private TextReader textReader;
        private TextWriter textWriter;
        private List<string> availableTypes;
        private readonly string informationPrompt;

        public StreamTextTransformationFactory(TextReader textReader, TextWriter textWriter)
        {
            this.textReader = textReader;
            this.textWriter = textWriter;
            this.availableTypes = new List<string>() { "capitalize", "trim-left", "trim-right", "normalize-space", "decorate", "censor", "replace", "composite" };
            this.informationPrompt = string.Format("Create a text transformation.\nAvailable types: [ {0} ]\nChoose transformation type.", string.Join(", ", this.availableTypes));
        }

        public void SetCensorTransformationFactory(CensorTransformationFactory censorTransformationFactory)
        {
            this.censorTransformationFactory = censorTransformationFactory;
        }

        public ITextTransformation CreateTextTransformation()
        {
            this.textWriter.WriteLine(this.informationPrompt);
            string transformationType = this.textReader.ReadLine() ?? string.Empty;

            try
            {
                switch (transformationType)
                {
                    case "capitalize":
                        return new CapitalizeTransformation();
                    case "trim-left":
                        return new TrimLeftTransformation();
                    case "trim-right":
                        return new TrimRightTransformation();
                    case "normalize-space":
                        return new NormalizeSpaceTransformation();
                    case "decorate":
                        return new DecorateTransformation();
                    case "censor":
                        this.textWriter.WriteLine("w:");
                        string w = this.textReader.ReadLine() ?? string.Empty;
                        return this.censorTransformationFactory is null ? 
                            new CensorTransformation(w) : 
                            this.censorTransformationFactory.CreateCensorTransformation(w);
                    case "replace":
                        this.textWriter.WriteLine("a:");
                        string a = this.textReader.ReadLine() ?? string.Empty;
                        this.textWriter.WriteLine("b:");
                        string b = this.textReader.ReadLine() ?? string.Empty;
                        return new ReplaceTransformation(a, b);
                    case "composite":
                        return this.CreateCompositeTransformation();
                    default:
                        throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                this.textWriter.WriteLine("Invalid input!");
                return this.CreateTextTransformation();
            }
        }

        private ITextTransformation CreateCompositeTransformation()
        {
            List<ITextTransformation> transformations = new List<ITextTransformation>();

            do
            {
                this.textWriter.WriteLine("Creating transformation for composite.\nChoose inner transformation:");
                ITextTransformation transformation = this.CreateTextTransformation();

                transformations.Add(transformation);

                Console.WriteLine("Add more transformations to composite?");
            } while ((Console.ReadLine() ?? string.Empty).Trim().ToLower() == "y");

            return new CompositeTransformation(transformations);
        }
    }
}