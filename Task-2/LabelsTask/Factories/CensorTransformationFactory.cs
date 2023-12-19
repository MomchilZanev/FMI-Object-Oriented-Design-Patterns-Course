using LabelsTask.Transformations;

namespace LabelsTask.Factories
{
    public class CensorTransformationFactory
    {
        private List<string> censorWords;

        public CensorTransformationFactory()
        {
            this.censorWords = new List<string>();
        }

        public CensorTransformation CreateCensorTransformation(string w)
        {
            string word = this.censorWords.FirstOrDefault(t => t == w) ?? w;
            CensorTransformation result = new CensorTransformation(word);

            if (w.Length <= 4 && !this.censorWords.Any(t => t == w))
                this.censorWords.Add(word);

            return result;
        }
    }
}