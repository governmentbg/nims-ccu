using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractSubcontractType
    {
        [Description("Подизпълнител")]
        Subcontractor = 1,

        [Description("Член на обединението")]
        Member = 2
    }
}
