using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.OperationalMap.ProgrammePriorities
{
    public enum ProgrammePriorityType
    {
        [Description(Description = nameof(DomainEnumTexts.ProgrammePriorityType_FirstClass), ResourceType = typeof(DomainEnumTexts))]
        FirstClass = 1,

        [Description(Description = nameof(DomainEnumTexts.ProgrammePriorityType_SecondClass), ResourceType = typeof(DomainEnumTexts))]
        SecondClass = 2,

        [Description(Description = nameof(DomainEnumTexts.ProgrammePriorityType_ThirdClass), ResourceType = typeof(DomainEnumTexts))]
        ThirdClass = 3,
    }
}
