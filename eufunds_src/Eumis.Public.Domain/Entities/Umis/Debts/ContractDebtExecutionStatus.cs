using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Debts
{
    public enum ContractDebtExecutionStatus
    {
        [Description("За дълга тече 14 дневен срок за доброволно възстановяване")]
        VoluntaryRepaymentPeriod = 1,

        [Description("Дългът е в процес на принудително възстановяване")]
        InvoluntaryRepaymentProcess = 2,

        [Description("Дългът е в процес на принудително възстановяване от НАП")]
        InvoluntaryRepaymentProcessByNAP = 3,

        [Description("Дългът е напълно възстановен")]
        FullyRepayed = 4,

        [Description("Дългът е закрит, поради наличие на други основания")]
        Closed = 5,

        [Description("Дългът е невъзстановим")]
        Irrecoverable = 6,

        [Description("Дългът е разсрочен")]
        Rescheduled = 7,

        [Description("Друго")]
        Other = 8
    }
}
