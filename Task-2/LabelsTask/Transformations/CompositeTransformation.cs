namespace LabelsTask.Transformations
{
    public class CompositeTransformation : ITextTransformation
    {
        private List<ITextTransformation> textTransformationStrategies;

        public CompositeTransformation(List<ITextTransformation> textTransformations)
        {
            this.textTransformationStrategies = textTransformations ?? new List<ITextTransformation>();
            this.textTransformationStrategies.RemoveAll(t => t is null);
        }

        public string Transform(string text)
        {
            if (this.textTransformationStrategies is null || !this.textTransformationStrategies.Any())
                return text ?? string.Empty;

            foreach (var transformation in this.textTransformationStrategies)
            {
                text = transformation.Transform(text);
            }

            return text;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || typeof(CompositeTransformation) != obj.GetType())
                return false;

            CompositeTransformation transformationToCompare = (CompositeTransformation)obj;

            // If one of the strategy collections is null/empty but the other is not -> return false
            if (((this.textTransformationStrategies is null || !this.textTransformationStrategies.Any())
                && transformationToCompare.textTransformationStrategies is not null
                && transformationToCompare.textTransformationStrategies.Any())
                ||
                ((transformationToCompare.textTransformationStrategies is null || !transformationToCompare.textTransformationStrategies.Any())
                && this.textTransformationStrategies is not null
                && this.textTransformationStrategies.Any()))
                return false;

            // If both of the strategy collections are null/empty -> return true
            if ((this.textTransformationStrategies is null || !this.textTransformationStrategies.Any())
                && (transformationToCompare.textTransformationStrategies is null || !transformationToCompare.textTransformationStrategies.Any()))
                return true;

            if (this.textTransformationStrategies.Count != transformationToCompare.textTransformationStrategies.Count)
                return false;

            // Strategy collection order is relevant but the next transformation to be applied is not
            for (int i = 0; i < this.textTransformationStrategies.Count; ++i)
            {
                if (!this.textTransformationStrategies[i].Equals(transformationToCompare.textTransformationStrategies[i]))
                    return false;
            }
            return true;
        }
    }
}
