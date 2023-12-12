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
            this.nextTransformation = this.textTransformationStrategies.Count - 1;
        }

        public override string GetText()
        {
            if (this.textTransformationStrategies.Count == 0)
                return base.GetText();

            this.nextTransformation = (this.nextTransformation + 1) % this.textTransformationStrategies.Count;

            return this.textTransformationStrategies[this.nextTransformation].Transform(base.GetText());
        }
    }
}