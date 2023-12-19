namespace LabelsTask.Labels
{
    public class SimpleLabel : ILabel
    {
        protected string value;

        public SimpleLabel(string value)
        {
            this.value = value ?? string.Empty;
        }

        public virtual string GetText()
        {
            return this.value;
        }
    }
}