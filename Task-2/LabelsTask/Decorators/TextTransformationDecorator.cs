using LabelsTask.Labels;
using LabelsTask.Transformations;

namespace LabelsTask.Decorators
{
    public class TextTransformationDecorator : LabelDecoratorBase
    {
        private ITextTransformation textTransformationStrategy;

        public TextTransformationDecorator(ILabel component, ITextTransformation textTransformation) : base(component)
        {
            this.textTransformationStrategy = textTransformation;
        }

        public override string GetText()
        {
            return this.textTransformationStrategy is not null ? textTransformationStrategy.Transform(base.GetText()) : base.GetText();
        }
    }
}