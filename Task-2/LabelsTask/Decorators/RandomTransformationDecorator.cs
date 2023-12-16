using LabelsTask.Labels;
using LabelsTask.Transformations;

namespace LabelsTask.Decorators
{
    public class RandomTransformationDecorator : LabelDecoratorBase
    {
        private List<ITextTransformation> textTransformationStrategies;
        private Random random;
        private int lastAppliedTransformation;

        public RandomTransformationDecorator(ILabel component, List<ITextTransformation> textTransformations) : base(component)
        {
            this.textTransformationStrategies = textTransformations ?? new List<ITextTransformation>();
            this.textTransformationStrategies.RemoveAll(t => t is null);
            this.random = new Random();
            this.lastAppliedTransformation = -1;
        }

        public override string GetText()
        {
            if (!this.textTransformationStrategies.Any())
                return base.GetText();
            else if (this.textTransformationStrategies.Count == 1)
                return this.textTransformationStrategies.First().Transform(base.GetText());

            int randomIndex = this.random.Next(this.textTransformationStrategies.Count);
            while (randomIndex == this.lastAppliedTransformation)
            {
                randomIndex = this.random.Next(this.textTransformationStrategies.Count);
            }
            this.lastAppliedTransformation = randomIndex;

            return this.textTransformationStrategies[randomIndex].Transform(base.GetText());
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || typeof(RandomTransformationDecorator) != obj.GetType())
                return false;

            RandomTransformationDecorator decoratorToCompare = (RandomTransformationDecorator)obj;

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

            // Strategy collection order is irrelevant
            return this.textTransformationStrategies.Count == decoratorToCompare.textTransformationStrategies.Count
                && this.textTransformationStrategies.All(s => decoratorToCompare.textTransformationStrategies.Contains(s));
        }
    }
}