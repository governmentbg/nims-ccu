using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public enum CertAuthorityCheckLevel
    {
        [Description("Оперативна програма")]
        Programme = 1,

        [Description("Приоритетна ос")]
        ProgrammePriority = 2,

        [Description("Процедура")]
        Procedure = 3,

        [Description("Договор за БФП")]
        Contract = 4
    }
}
