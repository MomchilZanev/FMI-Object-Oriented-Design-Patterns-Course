namespace Checksums.Progress
{
    public abstract class ObservableBase : IObservable
    {
        protected List<IObserver> subscribers;

        public ObservableBase()
        {
            this.subscribers = new List<IObserver>();
        }

        public virtual void Subscribe(IObserver subscriber)
        {
            if (!this.subscribers.Contains(subscriber))
            {
                this.subscribers.Add(subscriber);
            }
        }

        public virtual void Unsubscribe(IObserver subscriber)
        {
            if (this.subscribers.Contains(subscriber))
            {
                this.subscribers.Remove(subscriber);
            }
        }

        public virtual void Notify(object message)
        {
            foreach (IObserver subscriber in this.subscribers)
            {
                subscriber.Update(message);
            }
        }
    }
}