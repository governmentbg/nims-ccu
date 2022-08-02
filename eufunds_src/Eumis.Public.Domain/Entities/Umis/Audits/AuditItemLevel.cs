using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public enum AuditItemLevel
    {
        [Description("Приоритетна ос")]
        ProgrammePriority = 1,

        [Description("Процедура")]
        Procedure = 2,

        [Description("Договор за БФП")]
        Contract = 3,

        [Description("Договор с изпълнител")]
        ContractContract = 4
    }
}
