namespace LabelsTask.Labels
{
    public class RichLabel : SimpleLabel
    {
        private LabelStyle style;

        public RichLabel(string value, string color, int size, string font) : base(value)
        {
            this.style = new LabelStyle(color, size, font);
        }

        public LabelStyle Style { get => this.style; }
    }


    public class LabelStyle
    {
        private string color;
        private int size;
        private string font;

        public LabelStyle(string color, int size, string font)
        {
            this.Color = color;
            this.Size = size;
            this.Font = font;
        }

        public string Color
        {
            get => this.color;
            private set => this.color = string.IsNullOrEmpty(value) ? "Black" : value;
        }
        public int Size
        {
            get => this.size;
            private set => this.size = value <= 0 ? 1 : value;
        }
        public string Font
        {
            get => this.font;
            private set => this.font = string.IsNullOrEmpty(value) ? "Arial" : value;
        }
    }
}