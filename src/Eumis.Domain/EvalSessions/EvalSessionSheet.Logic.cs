using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Domain.NotificationEvents;

namespace Eumis.Domain.EvalSessions
{
    public partial class EvalSessionSheet
    {
        public void ContinueEvalSheet()
        {
            ((INotificationEventEmitter)this).NotificationEvents.Add(new EvalSessionNotificationEvent(this.EvalSessionId, this.EvalSessionSheetId));
        }
    }
}
