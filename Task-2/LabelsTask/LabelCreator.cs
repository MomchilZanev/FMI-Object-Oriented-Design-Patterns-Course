using LabelsTask.Decorators;
using LabelsTask.Factories;
using LabelsTask.Labels;

namespace LabelsTask
{
    public class LabelCreator
    {
        private StreamTextTransformationFactory textTransformationFactory;
        private StreamLabelFactory labelFactory;
        private StreamDecoratorFactory decoratorFactory;

        public LabelCreator()
        {
            this.textTransformationFactory = new StreamTextTransformationFactory(Console.In, Console.Out);
            this.labelFactory = new StreamLabelFactory(Console.In, Console.Out);
            this.decoratorFactory = new StreamDecoratorFactory(Console.In, Console.Out, this.textTransformationFactory);
        }

        public List<ILabel> InteractiveCreate()
        {
            List<ILabel> labels = new List<ILabel>();
            do
            {
                ILabel label = this.labelFactory.CreateLabel();

                Console.WriteLine("Decorate label?");
                if ((Console.ReadLine() ?? string.Empty).Trim().ToLower() == "y")
                    label = this.DecorateLabel(label);

                Console.WriteLine("Remove decorator from label?");
                if ((Console.ReadLine() ?? string.Empty).Trim().ToLower() == "y")
                    label = this.RemoveDecorator(label);

                labels.Add(label);

                Console.WriteLine("Create more labels?");
            } while ((Console.ReadLine() ?? string.Empty).Trim().ToLower() == "y");

            return labels;
        }

        private ILabel RemoveDecorator(ILabel label)
        {
            do
            {
                Console.WriteLine("Choose what kind of decorator to remove");
                LabelDecoratorBase decoratorToRemove = this.decoratorFactory.CreateDecorator(null);

                label = LabelDecoratorBase.RemoveDecoratorFrom(label, decoratorToRemove);

                Console.WriteLine("Remove more decorators?");
            } while ((Console.ReadLine() ?? string.Empty).Trim().ToLower() == "y");

            return label;
        }

        private ILabel DecorateLabel(ILabel label)
        {
            do
            {
                label = this.decoratorFactory.CreateDecorator(label);


                Console.WriteLine("Add more decorators?");
            } while ((Console.ReadLine() ?? string.Empty).Trim().ToLower() == "y");

            return label;
        }
    }
}