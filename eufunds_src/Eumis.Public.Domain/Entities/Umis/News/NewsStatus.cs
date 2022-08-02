using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.News
{
    public enum NewsStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Публикувана")]
        Published = 2,

        [Description("Архивирана")]
        Archived = 3
    }
}
