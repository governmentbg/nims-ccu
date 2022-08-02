using Eumis.Common.Json;

namespace Eumis.Domain.CertReports
{
    public enum IntermediateFinalCertReportSummaryTables
    {
        [Description(Description = nameof(DomainEnumTexts.IntermediateFinalCertReportSummaryTables_EligibleProgrammePriorityExpenses), ResourceType = typeof(DomainEnumTexts))]
        EligibleProgrammePriorityExpenses = 1,

        [Description(Description = nameof(DomainEnumTexts.IntermediateFinalCertReportSummaryTables_ApprovedAmountsCorrection), ResourceType = typeof(DomainEnumTexts))]
        ApprovedAmountsCorrection = 2,

        [Description(Description = nameof(DomainEnumTexts.IntermediateFinalCertReportSummaryTables_ReaffirmedCostsByAdministrativeAuthority), ResourceType = typeof(DomainEnumTexts))]
        ReaffirmedCostsByAdministrativeAuthority = 3,

        [Description(Description = nameof(DomainEnumTexts.IntermediateFinalCertReportSummaryTables_ProgrammePaidContributionInfoForFinancialInstruments), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePaidContributionInfoForFinancialInstruments = 4,

        [Description(Description = nameof(DomainEnumTexts.IntermediateFinalCertReportSummaryTables_StateAidPaidAdvancePayments), ResourceType = typeof(DomainEnumTexts))]
        StateAidPaidAdvancePayments = 5,

        [Description(Description = nameof(DomainEnumTexts.IntermediateFinalCertReportSummaryTables_Appendix4A), ResourceType = typeof(DomainEnumTexts))]
        Appendix4A = 6,
    }
}
