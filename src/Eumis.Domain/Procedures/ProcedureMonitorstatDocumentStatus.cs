using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureMonitorstatDocumentStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureMonitorstatDocumentStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureMonitorstatDocumentStatus_Activated), ResourceType = typeof(DomainEnumTexts))]
        Activated = 2,
    }
}
