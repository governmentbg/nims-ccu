using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public enum SpotCheckRecommendationStatus
    {
        [Description("Изпълнена")]
        Executed = 1,

        [Description("В процес на изпълнение")]
        InProcess = 2,

        [Description("Неизпълнена")]
        Unexecuted = 3,

        [Description("Отпаднала")]
        Removed = 4
    }
}
