namespace LabelsTask.Labels
{
    public class HelpLabel : ILabel
    {
        private string helpText;
        private ILabel label;

        public HelpLabel(string value, string helpText)
        {
            this.helpText = helpText ?? string.Empty;
            this.label = new SimpleLabel(value);
        }

        public HelpLabel(string helpText, ILabel? label = null)
        {
            this.helpText = helpText ?? string.Empty;
            this.label = label ?? new SimpleLabel(string.Empty);
        }

        public string GetText()
        {
            return this.label.GetText();
        }

        public string GetHelpText()
        {
            return this.helpText;
        }
    }
}