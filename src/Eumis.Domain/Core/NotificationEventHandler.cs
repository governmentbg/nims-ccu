namespace Eumis.Domain.Core
{
    public abstract class NotificationEventHandler<TEvent> : INotificationEventHandler
        where TEvent : INotificationEvent
    {
        public void Handle(INotificationEvent e)
        {
            if (e is TEvent)
            {
                this.Handle((TEvent)e);
            }
        }

        public abstract void Handle(TEvent e);
    }
}
