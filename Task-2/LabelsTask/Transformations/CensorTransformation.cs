namespace LabelsTask.Transformations
{
    public class CensorTransformation : ITextTransformation
    {
        private string w;

        public CensorTransformation(string w)
        {
            this.w = w ?? string.Empty;
        }

        public string W { get => this.w; }

        public string Transform(string text)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(this.w))
            {
                return text.Replace(this.w, new string('*', this.w.Length));
            }

            return text ?? string.Empty;
        }

        public override bool Equals(object? obj)
        {
            if(obj is null || typeof(CensorTransformation) != obj.GetType())
                return false;

            CensorTransformation transformationToCompare = (CensorTransformation)obj;

            return this.w == transformationToCompare.w;
        }
    }
}