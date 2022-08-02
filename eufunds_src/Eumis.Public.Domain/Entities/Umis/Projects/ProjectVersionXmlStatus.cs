using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public enum ProjectVersionXmlStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Текущ")]
        Actual = 2,

        [Description("Архивирано")]
        Archive = 3
    }
}
