using LabelsTask.Labels;

namespace LabelsTask
{
    public class LabelPrinter
    {
        public static void Print(ILabel label)
        {
            Console.WriteLine("Label: " + label.GetText());
        }

        public static void PrintWithHelpText(HelpLabel label)
        {
            LabelPrinter.Print(label);
            Console.WriteLine("Help information: " + label.GetHelpText());
        }
    }
}