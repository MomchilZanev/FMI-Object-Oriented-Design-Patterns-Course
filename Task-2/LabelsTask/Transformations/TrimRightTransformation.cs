namespace LabelsTask.Transformations
{
    public class TrimRightTransformation : ITextTransformation
    {
        public string Transform(string text)
        {
            return (text ?? string.Empty).TrimEnd();
        }
    }
}