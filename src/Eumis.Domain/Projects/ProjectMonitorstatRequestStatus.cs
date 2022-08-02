using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Projects
{
    public enum ProjectMonitorstatRequestStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectMonitorstatRequestStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectMonitorstatRequestStatus_Sent), ResourceType = typeof(DomainEnumTexts))]
        Sent = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectMonitorstatRequestStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 3,

        [Description(Description = nameof(DomainEnumTexts.ProjectMonitorstatRequestStatus_Finished), ResourceType = typeof(DomainEnumTexts))]
        Finished = 4,

        [Description(Description = nameof(DomainEnumTexts.ProjectMonitorstatRequestStatus_Failed), ResourceType = typeof(DomainEnumTexts))]
        Failed = 5,
    }
}
