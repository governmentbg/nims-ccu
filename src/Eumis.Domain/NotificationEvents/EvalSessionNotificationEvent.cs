using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.NotificationEvents
{
    public class EvalSessionNotificationEvent : DispatchResolver, INotificationEvent
    {
        public EvalSessionNotificationEvent(int evalSessionId, int projectId, int communicationId)
            : base(NonAggregates.NotificationEventType.ProjectCandidateAnswerRegistered, evalSessionId, projectId)
        {
            this.EvalSessionId = evalSessionId;
            this.DispatcherId = communicationId;
        }

        public EvalSessionNotificationEvent(int evalSessionId, int evalSheetId)
           : base(NonAggregates.NotificationEventType.EvalSheetDistributionTypeToContinued, evalSessionId, evalSheetId)
        {
            this.EvalSessionId = evalSessionId;
            this.DispatcherId = evalSheetId;
        }

        public int EvalSessionId { get; set; }

        public int DispatcherId { get; set; }
    }
}
