using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertReports
{
    public enum IntermediateFinalCertReportSummaryTables
    {
        [Description("Допустими разходи по приоритетни оси")]
        EligibleProgrammePriorityExpenses = 1,

        [Description("Корекция на верифицирани разходи")]
        ApprovedAmountsCorrection = 2,

        [Description("Препотвърдени от УО разходи")]
        ReaffirmedCostsByAdministrativeAuthority = 3,

        [Description("Информация на приноса от програмата платен за финансови инструменти")]
        ProgrammePaidContributionInfoForFinancialInstruments = 4,

        [Description("Платени аванси в контекста на държавни помощи")]
        StateAidPaidAdvancePayments = 5,
    }
}
