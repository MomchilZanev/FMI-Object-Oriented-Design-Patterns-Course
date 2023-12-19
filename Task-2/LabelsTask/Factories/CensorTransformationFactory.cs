using LabelsTask.Transformations;

namespace LabelsTask.Factories
{
    public class CensorTransformationFactory
    {
        private List<CensorTransformation> transformationsCache;

        public CensorTransformationFactory()
        {
            transformationsCache = new List<CensorTransformation>();
        }

        public CensorTransformation CreateCensorTransformation(string w)
        {
            CensorTransformation result = this.transformationsCache.FirstOrDefault(t => t.W == w) ?? new CensorTransformation(w);

            if (w.Length <= 4 && !this.transformationsCache.Any(t => t.W == w))
                this.transformationsCache.Add(result);

            return result;
        }
    }
}