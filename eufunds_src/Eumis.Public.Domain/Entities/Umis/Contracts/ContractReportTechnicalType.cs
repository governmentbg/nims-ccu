using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportTechnicalType
    {
        [Description("Междинен")]
        Intermediate = 1,

        [Description("Окончателен")]
        Final = 2
    }
}