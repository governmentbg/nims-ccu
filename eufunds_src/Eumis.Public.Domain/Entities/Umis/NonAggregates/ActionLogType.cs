using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum ActionLogType
    {
        [Description("Вътрешна с-ма")]
        Internal = 1,

        [Description("Портал")]
        Portal = 2,

        [Description("Неуспешен вход в системата")]
        UnsuccessfulLogin = 3,
    }
}
