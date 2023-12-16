namespace LabelsTask.Transformations
{
    public class TrimRightTransformation : ITextTransformation
    {
        public string Transform(string text)
        {
            return (text ?? string.Empty).TrimEnd();
        }

        public override bool Equals(object? obj)
        {
            return obj is not null && typeof(TrimRightTransformation) == obj.GetType();
        }
    }
}