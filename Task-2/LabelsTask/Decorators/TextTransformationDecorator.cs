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

        public override bool Equals(object? obj)
        {
            if (obj is null || typeof(TextTransformationDecorator) != obj.GetType())
                return false;

            TextTransformationDecorator decoratorToCompare = (TextTransformationDecorator)obj;

            // If one of the strategies is null but the other is not -> return false
            if ((this.textTransformationStrategy is null && decoratorToCompare.textTransformationStrategy is not null)
                ||
                (this.textTransformationStrategy is not null && decoratorToCompare.textTransformationStrategy is null))
                return false;

            // If both strategies are null -> return true
            if (this.textTransformationStrategy is null && decoratorToCompare.textTransformationStrategy is null)
                return true;

            return this.textTransformationStrategy.Equals(decoratorToCompare.textTransformationStrategy);
        }
    }
}