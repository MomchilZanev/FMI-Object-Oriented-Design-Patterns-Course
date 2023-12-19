namespace LabelsTask.Transformations
{
    public class TrimLeftTransformation : ITextTransformation
    {
        public string Transform(string text)
        {
            return (text ?? string.Empty).TrimStart();
        }

        public override bool Equals(object? obj)
        {
            return obj is not null && typeof(TrimLeftTransformation) == obj.GetType();
        }
    }
}