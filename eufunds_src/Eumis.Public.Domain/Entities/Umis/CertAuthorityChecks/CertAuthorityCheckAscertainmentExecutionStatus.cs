using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public enum CertAuthorityCheckAscertainmentExecutionStatus
    {
        [Description("Изпълнена")]
        Executed = 1,

        [Description("В процес на изпълнение")]
        InProcess = 2,

        [Description("Неизпълнена")]
        Unexecuted = 3,

        [Description("Оттеглена")]
        Removed = 4
    }
}
