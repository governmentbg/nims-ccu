using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.OperationalMap.Directions
{
    public enum DirectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.DirectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.DirectionStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,
    }
}
