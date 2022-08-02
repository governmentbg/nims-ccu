using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureMassCommunicationStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureMassCommunicationStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureMassCommunicationStatus_Sent), ResourceType = typeof(DomainEnumTexts))]
        Sent = 2,
    }
}
