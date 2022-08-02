using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.NotificationEvents
{
    public class ProgrammeIndependentEvent : DispatchResolver, INotificationEvent
    {
        public ProgrammeIndependentEvent(NotificationEventType notificationEventType, int dispatcherId)
            : base(notificationEventType, dispatcherId)
        {
        }
    }
}
