using Eumis.Common.Json;

namespace Eumis.Domain.AnnualAccountReports
{
    public enum AnnualAccountReportAppendixType
    {
        [Description(Description = nameof(DomainEnumTexts.AnnualAccountReportAppendixType_Appendix5), ResourceType = typeof(DomainEnumTexts))]
        Appendix5 = 1,

        [Description(Description = nameof(DomainEnumTexts.AnnualAccountReportAppendixType_Appendix8), ResourceType = typeof(DomainEnumTexts))]
        Appendix8 = 2,
    }
}
