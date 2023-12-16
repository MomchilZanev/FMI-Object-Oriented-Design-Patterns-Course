using LabelsTask.Labels;

namespace LabelsTask.Decorators
{
    public abstract class LabelDecoratorBase : ILabel
    {
        private ILabel component;

        public LabelDecoratorBase(ILabel component)
        {
            this.component = component ?? new SimpleLabel(null);
        }

        public virtual string GetText()
        {
            return component.GetText();
        }

        public ILabel RemoveDecorator(LabelDecoratorBase decoratorToRemove)
        {
            if (this.Equals(decoratorToRemove))
                return this.component;
            else if (typeof(LabelDecoratorBase).IsAssignableFrom(this.component.GetType()))
                this.component = ((LabelDecoratorBase)this.component).RemoveDecorator(decoratorToRemove);

            return this;
        }

        public static ILabel RemoveDecoratorFrom(ILabel label, LabelDecoratorBase decoratorToRemove)
        {
            if (label is null || decoratorToRemove is null || !typeof(LabelDecoratorBase).IsAssignableFrom(label.GetType()))
                return label;

            LabelDecoratorBase decorator = (LabelDecoratorBase)label;
            return decorator.RemoveDecorator(decoratorToRemove);
        }
    }
}