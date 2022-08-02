using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum EmailStatus
    {
        [Description("Предстоящо изпращане")]
        Pending = 1,

        [Description("Изпратен")]
        Sent = 2,

        [Description("Грешка")]
        UknownError = 3,
    }
}