using LabelsTask.Labels;

namespace LabelsTask.Decorators
{
    public abstract class LabelDecoratorBase : ILabel
    {
        private ILabel component;

        public LabelDecoratorBase(ILabel component)
        {
            this.component = component;
        }

        public virtual string GetText()
        {
            return component.GetText();
        }
    }
}