namespace Eumis.Domain.Core
{
    public interface INotificationEventHandler
    {
        void Handle(INotificationEvent notificationEvent);
    }
}
