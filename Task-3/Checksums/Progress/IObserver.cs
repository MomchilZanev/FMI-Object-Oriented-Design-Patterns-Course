namespace Checksums.Progress
{
    public interface IObserver
    {
        void Update(object message);
    }
}