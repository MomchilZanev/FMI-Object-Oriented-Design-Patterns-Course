namespace LabelsTask.Transformations
{
    public class DecorateTransformation : ITextTransformation
    {
        private TrimLeftTransformation trimLeftTransformation;
        private TrimRightTransformation trimRightTransformation;

        public DecorateTransformation()
        {
            this.trimLeftTransformation = new TrimLeftTransformation();
            this.trimRightTransformation = new TrimRightTransformation();
        }

        public string Transform(string text)
        {
            string result = text ?? string.Empty;

            result = trimLeftTransformation.Transform(result);
            result = trimRightTransformation.Transform(result);

            result += result == string.Empty ? "}=-" : " }=-";
            result = "-={ " + result;

            return result;
        }

        public override bool Equals(object? obj)
        {
            return obj is not null && typeof(DecorateTransformation) == obj.GetType();
        }
    }
}