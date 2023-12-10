namespace LabelsTask.Labels
{
    public class SimpleLabel : ILabel
    {
        private string value;

        public SimpleLabel(string value)
        {
            this.value = value ?? string.Empty;
        }

        public string GetText()
        {
            return this.value;
        }
    }
}