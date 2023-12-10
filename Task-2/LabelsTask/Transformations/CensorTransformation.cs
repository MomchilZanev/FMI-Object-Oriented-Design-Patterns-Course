namespace LabelsTask.Transformations
{
    public class CensorTransformation : ITextTransformation
    {
        private string w;
        private ReplaceTransformation replaceTransformation;

        public CensorTransformation(string w)
        {
            this.w = w ?? string.Empty;
            string censor = new string('*', this.w.Length) ?? string.Empty;
            this.replaceTransformation = new ReplaceTransformation(this.w, censor);
        }

        public string W { get => this.w; }

        public string Transform(string text)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(this.w))
            {
                return this.replaceTransformation.Transform(text);
            }

            return text ?? string.Empty;
        }
    }
}