namespace Checksums.Progress
{
    public abstract class ObservableBase
    {
        protected List<Observer> subscribers;

        public ObservableBase()
        {
            this.subscribers = new List<Observer>();
        }

        public virtual void Subscribe(Observer subscriber)
        {
            if (!this.subscribers.Contains(subscriber))
            {
                this.subscribers.Add(subscriber);
            }
        }

        public virtual void Unsubscribe(Observer subscriber)
        {
            if (this.subscribers.Contains(subscriber))
            {
                this.subscribers.Remove(subscriber);
            }
        }

        public virtual void Notify(object message)
        {
            foreach (Observer subscriber in this.subscribers)
            {
                subscriber.Update(message);
            }
        }
    }
}