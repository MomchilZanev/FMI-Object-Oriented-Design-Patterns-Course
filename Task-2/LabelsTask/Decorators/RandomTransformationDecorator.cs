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
            this.random = new Random();
            this.lastAppliedTransformation = -1;
        }

        public override string GetText()
        {
            if (this.textTransformationStrategies.Count == 0)
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
    }
}