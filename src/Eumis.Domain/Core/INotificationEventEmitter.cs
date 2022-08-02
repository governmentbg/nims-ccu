using System.Collections.Generic;

namespace Eumis.Domain.Core
{
    public interface INotificationEventEmitter
    {
        ICollection<INotificationEvent> NotificationEvents { get; set; }
    }
}
