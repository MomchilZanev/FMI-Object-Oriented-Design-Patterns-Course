using LabelsTask.Labels;

namespace LabelsTask.Factories
{
    public class StreamLabelFactory
    {
        private TextReader textReader;
        private TextWriter textWriter;
        private List<string> availableTypes;
        private readonly string informationPrompt;

        public StreamLabelFactory(TextReader textReader, TextWriter textWriter)
        {
            this.textReader = textReader;
            this.textWriter = textWriter;
            this.availableTypes = new List<string>() { "simple", "rich", "custom", "help" };
            this.informationPrompt = string.Format("Create a label.\nAvailable types: [ {0} ]\nChoose label type.", string.Join(", ", this.availableTypes));
        }

        public ILabel CreateLabel()
        {
            this.textWriter.WriteLine(this.informationPrompt);
            string labelType = this.textReader.ReadLine() ?? string.Empty;

            try
            {
                switch (labelType)
                {
                    case "simple":
                        return this.CreateSimpleLabel();
                    case "rich":
                        return this.CreateRichLabel();
                    case "custom":
                        return this.CreateCustomLabel();
                    case "help":
                        return this.CreateHelpLabel();
                    default:
                        throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                textWriter.WriteLine("Invalid input!");
                return this.CreateLabel();
            }
        }

        private ILabel CreateSimpleLabel()
        {
            this.textWriter.WriteLine("Creating simple label.\nChose value:");
            string value = this.textReader.ReadLine() ?? string.Empty;

            return new SimpleLabel(value);
        }

        private ILabel CreateRichLabel()
        {
            this.textWriter.WriteLine("Creating rich label.\nChose value:");
            string value = this.textReader.ReadLine() ?? string.Empty;

            this.textWriter.WriteLine("Chose color:");
            string color = this.textReader.ReadLine() ?? string.Empty;

            this.textWriter.WriteLine("Chose size:");
            int size = int.TryParse(this.textReader.ReadLine(), out int result) ? result : -1;

            this.textWriter.WriteLine("Chose font:");
            string font = this.textReader.ReadLine() ?? string.Empty;

            return new RichLabel(value, color, size, font);
        }

        private ILabel CreateCustomLabel()
        {
            this.textWriter.WriteLine("Creating custom label.\nChoose timeout:");
            int timeout = int.TryParse(this.textReader.ReadLine(), out int result) ? result : -1;

            return new CustomLabel(timeout);
        }

        private ILabel CreateHelpLabel()
        {
            this.textWriter.WriteLine("Creating help label.\nChoose help text:");
            string helpText = this.textReader.ReadLine() ?? string.Empty;

            this.textWriter.WriteLine("Create base label:");
            ILabel innerLabel = this.CreateLabel();

            return new HelpLabel(helpText, innerLabel);
        }
    }
}