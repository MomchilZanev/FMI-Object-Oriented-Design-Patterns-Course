using LabelsTask.Labels;
using LabelsTask.Transformations;

namespace LabelsTask.Decorators
{
    public class CyclingTransformationDecorator : LabelDecoratorBase
    {
        private List<ITextTransformation> textTransformationStrategies;
        private int nextTransformation;

        public CyclingTransformationDecorator(ILabel component, List<ITextTransformation> textTransformations) : base(component)
        {
            this.textTransformationStrategies = textTransformations ?? new List<ITextTransformation>();
            this.textTransformationStrategies.RemoveAll(t => t is null);
            this.nextTransformation = this.textTransformationStrategies.Count - 1;
        }

        public override string GetText()
        {
            if (!this.textTransformationStrategies.Any())
                return base.GetText();

            this.nextTransformation = (this.nextTransformation + 1) % this.textTransformationStrategies.Count;

            return this.textTransformationStrategies[this.nextTransformation].Transform(base.GetText());
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || typeof(CyclingTransformationDecorator) != obj.GetType())
                return false;

            CyclingTransformationDecorator decoratorToCompare = (CyclingTransformationDecorator)obj;

            // If one of the strategy collections is null/empty but the other is not -> return false
            if (((this.textTransformationStrategies is null || !this.textTransformationStrategies.Any())
                && decoratorToCompare.textTransformationStrategies is not null
                && decoratorToCompare.textTransformationStrategies.Any())
                ||
                ((decoratorToCompare.textTransformationStrategies is null || !decoratorToCompare.textTransformationStrategies.Any())
                && this.textTransformationStrategies is not null
                && this.textTransformationStrategies.Any()))
                return false;

            // If both of the strategy collections are null/empty -> return true
            if ((this.textTransformationStrategies is null || !this.textTransformationStrategies.Any())
                && (decoratorToCompare.textTransformationStrategies is null || !decoratorToCompare.textTransformationStrategies.Any()))
                return true;

            if (this.textTransformationStrategies.Count != decoratorToCompare.textTransformationStrategies.Count)
                return false;

            // Strategy collection order is relevant but the next transformation to be applied is not
            for (int i = 0; i < this.textTransformationStrategies.Count; ++i)
            {
                if (!this.textTransformationStrategies[i].Equals(decoratorToCompare.textTransformationStrategies[i]))
                    return false;
            }
            return true;
        }
    }
}