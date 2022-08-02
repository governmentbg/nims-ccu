using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureKind
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureKind_Budget), ResourceType = typeof(DomainEnumTexts))]
        Budget = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureKind_Schema), ResourceType = typeof(DomainEnumTexts))]
        Schema = 2,
    }
}
