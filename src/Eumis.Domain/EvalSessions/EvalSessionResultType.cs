using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionResultType
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionResultType_Preliminary), ResourceType = typeof(DomainEnumTexts))]
        Preliminary = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionResultType_AdminAdmiss), ResourceType = typeof(DomainEnumTexts))]
        AdminAdmiss = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionResultType_Standing), ResourceType = typeof(DomainEnumTexts))]
        Standing = 3,
    }
}
