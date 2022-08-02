using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportCorrectionType
    {
        [Description("Верифицирани на ниво искане за плащане")]
        PaymentVerified = 1,

        [Description("Верифицирани на ниво договор")]
        ContractVerified = 2,

        [Description("Верифицирани на ниво програма")]
        ProgrameVerified = 3,

        [Description("Верифицирани на ниво приоритетна ос")]
        ProgramePriorityVerified = 4,

        [Description("Верифицирани на ниво процедура")]
        ProcedureVerified = 5
    }
}