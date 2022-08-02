using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertReports
{
    public enum YearlyCertReportSummaryTables
    {
        [Description("Допустими разходи по приоритетни оси")]
        EligibleProgrammePriorityExpenses = 1,

        [Description("Отписани и възстановени суми")]
        OffAndRecoveredAmounts = 2,

        [Description("Суми подлежащи на възстановяване")]
        RecoverableAmounts = 3,

        [Description("Възстановени суми")]
        RecoveredAmounts = 4,

        [Description("Невъзстановени суми")]
        UnrecoveredAmounts = 5,

        [Description("Сума на приноса от програмата платен за финансови инструменти")]
        ProgrammePaidContributionAmountForFinancialInstruments = 6,

        [Description("Платени аванси в контекста на държавни помощи")]
        StateAidPaidAdvancePayments = 7,

        [Description("Равнение")]
        Reconciliation = 8
    }
}
