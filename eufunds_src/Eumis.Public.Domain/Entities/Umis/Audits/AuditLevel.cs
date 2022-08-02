﻿using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public enum AuditLevel
    {
        [Description("Оперативна програма")]
        Programme = 1,

        [Description("Приоритетна ос")]
        ProgrammePriority = 2,

        [Description("Процедура")]
        Procedure = 3,

        [Description("Договор за БФП")]
        Contract = 4,

        [Description("Договор с изпълнител")]
        ContractContract = 5
    }
}
