namespace Checksums.Progress
{
    public interface IObservable
    {
        void Subscribe(IObserver subscriber);
        void Unsubscribe(IObserver subscriber);
        void Notify(object message);
    }
}