using LabelsTask.Labels;

namespace LabelsTask
{
    public class LabelPrinter
    {
        public static void Print(ILabel label)
        {
            Console.WriteLine(label.GetText());
        }
    }
}
