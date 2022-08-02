using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractCommunicationStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Изпратено съобщение")]
        Sent = 2
    }
}
