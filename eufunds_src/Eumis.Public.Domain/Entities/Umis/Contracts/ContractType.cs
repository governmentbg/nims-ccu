using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractType
    {
        [Description("Решение")]
        Decision = 1,

        [Description("Заповед")]
        Order = 2,

        [Description("Договор")]
        Contract = 3,

        [Description("Споразумение с ФФ и ЕИМ")]
        FOFAgreement = 4,

        [Description("Споразумение с ФП")]
        AgentAgreement = 5,

        [Description("Споразумение с КП")]
        EndUserAgreement = 6

    }
}
