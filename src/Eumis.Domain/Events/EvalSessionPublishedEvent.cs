using Eumis.Domain.Core;
using Eumis.Domain.EvalSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Events
{
    public class EvalSessionPublishedEvent : IDomainEvent
    {
        public EvalSessionPublishedEvent()
        {
        }

        public int EvalSessionId { get; set; }

        public int ProcedureId { get; set; }

        public int EvalSessionResultId { get; set; }

        public EvalSessionResultType EvalSessionResultType { get; set; }
    }
}
