using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Events
{
    public class ProjectEvalStatusChangedEvent : IDomainEvent
    {
        public ProjectEvalStatusChangedEvent()
        {
        }

        public int ProjectId { get; set; }

        public int ProcedureId { get; set; }

        public int RegProjectXmlId { get; set; }
    }
}
