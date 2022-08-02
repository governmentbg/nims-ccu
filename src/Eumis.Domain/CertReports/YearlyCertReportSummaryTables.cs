using Eumis.Common.Json;

namespace Eumis.Domain.CertReports
{
    public enum YearlyCertReportSummaryTables
    {
        [Description(Description = nameof(DomainEnumTexts.YearlyCertReportSummaryTables_EligibleProgrammePriorityExpenses), ResourceType = typeof(DomainEnumTexts))]
        EligibleProgrammePriorityExpenses = 1,

        [Description(Description = nameof(DomainEnumTexts.YearlyCertReportSummaryTables_OffAndRecoveredAmounts), ResourceType = typeof(DomainEnumTexts))]
        OffAndRecoveredAmounts = 2,

        [Description(Description = nameof(DomainEnumTexts.YearlyCertReportSummaryTables_RecoverableAmounts), ResourceType = typeof(DomainEnumTexts))]
        RecoverableAmounts = 3,

        [Description(Description = nameof(DomainEnumTexts.YearlyCertReportSummaryTables_RecoveredAmounts), ResourceType = typeof(DomainEnumTexts))]
        RecoveredAmounts = 4,

        [Description(Description = nameof(DomainEnumTexts.YearlyCertReportSummaryTables_UnrecoveredAmounts), ResourceType = typeof(DomainEnumTexts))]
        UnrecoveredAmounts = 5,

        [Description(Description = nameof(DomainEnumTexts.YearlyCertReportSummaryTables_ProgrammePaidContributionAmountForFinancialInstruments), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePaidContributionAmountForFinancialInstruments = 6,

        [Description(Description = nameof(DomainEnumTexts.YearlyCertReportSummaryTables_StateAidPaidAdvancePayments), ResourceType = typeof(DomainEnumTexts))]
        StateAidPaidAdvancePayments = 7,

        [Description(Description = nameof(DomainEnumTexts.YearlyCertReportSummaryTables_Reconciliation), ResourceType = typeof(DomainEnumTexts))]
        Reconciliation = 8,

        [Description(Description = nameof(DomainEnumTexts.YearlyCertReportSummaryTables_Appendix4A), ResourceType = typeof(DomainEnumTexts))]
        Appendix4A = 9,
    }
}
