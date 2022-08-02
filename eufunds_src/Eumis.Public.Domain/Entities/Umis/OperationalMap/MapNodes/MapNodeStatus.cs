using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes
{
    public enum MapNodeStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Анулиран")]
        Canceled = 3
    }
}
