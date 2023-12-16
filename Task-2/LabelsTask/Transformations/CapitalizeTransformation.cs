namespace LabelsTask.Transformations
{
    public class CapitalizeTransformation : ITextTransformation
    {
        public string Transform(string text)
        {
            if (!string.IsNullOrEmpty(text) && char.IsLetter(text.First()))
            {
                string result = string.Empty;

                result += char.ToUpper(text.First());
                result += text.Substring(1);

                return result.ToString();
            }

            return text ?? string.Empty;
        }

        public override bool Equals(object? obj)
        {
            return obj is not null && typeof(CapitalizeTransformation) == obj.GetType();
        }
    }
}