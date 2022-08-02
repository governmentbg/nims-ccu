using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public enum ProjectRegistrationStatus
    {
        [Description("Регистрирано")]
        Registered = 1,

        [Description("Регистрирано извън срок")]
        RegisteredLate = 2,

        [Description("Оттеглено")]
        Withdrawn = 3
    }
}
