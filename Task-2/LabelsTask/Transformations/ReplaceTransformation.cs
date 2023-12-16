namespace LabelsTask.Transformations
{
    public class ReplaceTransformation : ITextTransformation
    {
        private string a;
        private string b;

        public ReplaceTransformation(string a, string b)
        {
            this.a = a ?? string.Empty;
            this.b = b ?? string.Empty;
        }

        public string A { get => this.a; }
        public string B { get => this.b; }

        public string Transform(string text)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(this.a))
            {
                return text.Replace(this.a, this.b);
            }

            return text ?? string.Empty;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || typeof(ReplaceTransformation) != obj.GetType())
                return false;

            ReplaceTransformation transformationToCompare = (ReplaceTransformation)obj;

            return this.a == transformationToCompare.a && this.b == transformationToCompare.b;
        }
    }
}