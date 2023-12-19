using LabelsTask.Factories;

namespace LabelsTask.Labels
{
    public class CustomLabel : SimpleLabel
    {
        private bool initialState;
        private int timeout;
        private int callsRemaining;

        // timeout = -1 -> never re-read label text
        public CustomLabel(int timeout = -1) : base(string.Empty)
        {
            this.initialState = true;
            this.timeout = timeout < -1 ? -1 : timeout;
            this.callsRemaining = this.timeout;
        }

        public override string GetText()
        {
            if (this.initialState || (this.callsRemaining <= 0 && this.timeout != -1))
            {
                Console.Out.WriteLine("Read custom label value:");
                this.value = Console.In.ReadLine() ?? string.Empty;
                this.callsRemaining = this.timeout;
                this.initialState = false;
            }
            else
            {
                --this.callsRemaining;
            }

            return base.GetText();
        }
    }
}