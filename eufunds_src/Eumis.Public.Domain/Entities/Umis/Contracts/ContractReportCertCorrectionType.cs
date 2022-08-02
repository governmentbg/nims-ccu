using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportCertCorrectionType
    {
        [Description("Сертифицирани на ниво искане за плащане")]
        PaymentCertified = 1,

        [Description("Сертифицирани на ниво договор")]
        ContractCertified = 2,

        [Description("Сертифицирани на ниво програма")]
        ProgrameCertified = 3,

        [Description("Сертифицирани на ниво приоритетна ос")]
        ProgramePriorityCertified = 4,

        [Description("Сертифицирани на ниво процедура")]
        ProcedureCertified = 5
    }
}