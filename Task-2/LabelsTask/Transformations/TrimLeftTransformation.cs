namespace LabelsTask.Transformations
{
    public class TrimLeftTransformation : ITextTransformation
    {
        public string Transform(string text)
        {
            return (text ?? string.Empty).TrimStart();
        }
    }
}