using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum ActiveStatus
    {
        [Description("Неактивиран")]
        NotActivated = 1,

        [Description("Активен")]
        Active = 2,

        [Description("Неактивен")]
        Inactive = 3
    }
}
