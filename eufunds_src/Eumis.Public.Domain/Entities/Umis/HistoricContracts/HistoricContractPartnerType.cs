using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.HistoricContracts
{
    public enum HistoricContractPartnerType
    {
        [Description("Партньор")]
        Partner = 1,

        [Description("Изпълнител")]
        Contractor = 2,

        [Description("Подизпълнител")]
        Subcontractor = 3,

        [Description("Членове на обединение")]
        Member = 4,
    }
}
