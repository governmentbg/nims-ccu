using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportRevalidationType
    {
        [Description("Препотвърдени на ниво искане за плащане")]
        PaymentRevalidated = 1,

        [Description("Препотвърдени на ниво договор")]
        ContractRevalidated = 2,

        [Description("Препотвърдени на ниво програма")]
        ProgrameRevalidated = 3,

        [Description("Препотвърдени на ниво приоритетна ос")]
        ProgramePriorityRevalidated = 4,

        [Description("Препотвърдени на ниво процедура")]
        ProcedureRevalidated = 5
    }
}