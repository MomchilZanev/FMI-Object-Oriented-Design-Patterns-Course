using LabelsTask.Factories;

namespace LabelsTask.Labels
{
    public class CustomLabel : ILabel
    {
        private StreamLabelFactory labelFactory;
        private ILabel? cachedLabel;
        private int timeout;
        private int callsRemaining;

        // timeout = -1 -> never re-read label text
        public CustomLabel(int timeout = -1)
        {
            this.labelFactory = new StreamLabelFactory(Console.In, Console.Out);
            this.timeout = timeout < -1 ? -1 : timeout;
            this.callsRemaining = timeout;
        }

        public string GetText()
        {
            if (this.cachedLabel is null || (this.callsRemaining <= 0 && this.timeout != -1))
            {
                this.cachedLabel = this.labelFactory.CreateLabel();
                this.callsRemaining = this.timeout;
            }
            else
            {
                --this.callsRemaining;
            }

            return this.cachedLabel.GetText();
        }
    }
}