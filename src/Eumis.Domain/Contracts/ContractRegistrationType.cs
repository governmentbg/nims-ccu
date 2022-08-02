using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Contracts
{
    public enum ContractRegistrationType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractType_Decision), ResourceType = typeof(DomainEnumTexts))]
        Decision = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractType_Order), ResourceType = typeof(DomainEnumTexts))]
        Order = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractType_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 3,
    }
}
