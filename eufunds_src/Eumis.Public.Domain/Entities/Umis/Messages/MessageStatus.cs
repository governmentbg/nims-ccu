using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Messages
{
    public enum MessageStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Изпратено")]
        Sent = 2
    }
}
