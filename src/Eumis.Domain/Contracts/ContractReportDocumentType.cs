using Eumis.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportDocumentType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportDocumentType_Payment), ResourceType = typeof(DomainEnumTexts))]
        ContractReportPayment = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportDocumentType_Financial), ResourceType = typeof(DomainEnumTexts))]
        ContractReportFinancial = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportDocumentType_Technical), ResourceType = typeof(DomainEnumTexts))]
        ContractReportTechnical = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportDocumentType_MicrodataType1), ResourceType = typeof(DomainEnumTexts))]
        ContractReportMicrodataType1 = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportDocumentType_MicrodataType2), ResourceType = typeof(DomainEnumTexts))]
        ContractReportMicrodataType2 = 5,

        [Description(Description = nameof(DomainEnumTexts.ContractReportDocumentType_MicrodataType3), ResourceType = typeof(DomainEnumTexts))]
        ContractReportMicrodataType3 = 6,

        [Description(Description = nameof(DomainEnumTexts.ContractReportDocumentType_MicrodataType4), ResourceType = typeof(DomainEnumTexts))]
        ContractReportMicrodataType4 = 7,
    }
}
