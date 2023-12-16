using System.Text.RegularExpressions;

namespace LabelsTask.Transformations
{
    public class NormalizeSpaceTransformation : ITextTransformation
    {
        public string Transform(string text)
        {
            if (!string.IsNullOrEmpty(text) && Regex.IsMatch(text, @"\s{2,}"))
            {
                return Regex.Replace(text, @"\s{2,}", " ");
            }

            return text ?? string.Empty;
        }

        public override bool Equals(object? obj)
        {
            return obj is not null && typeof(NormalizeSpaceTransformation) == obj.GetType();
        }
    }
}