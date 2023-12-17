using LabelsTask.Labels;

namespace LabelsTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LabelCreator creator = new LabelCreator();
            foreach (var label in creator.InteractiveCreate())
            {
                if (label.GetType() == typeof(HelpLabel))
                    LabelPrinter.PrintWithHelpText((HelpLabel)label);
                else
                    LabelPrinter.Print(label);
            }
        }
    }
}