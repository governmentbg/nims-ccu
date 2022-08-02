using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum ApplicationFormType
    {
        [Description("Стандартен")]
        Standard = 1,

        [Description("За предварителен подбор")]
        PreliminarySelection = 2,

        [Description("Стандартен (разширен по концепция)")]
        StandardWithPreliminarySelection = 3,

        [Description("Стандартен (бюджетна линия)")]
        StandardForBudgetLine = 4,

        [Description("Информация за фонд на фондовете и ЕИФ/за финансови посредници")]
        FOFFinancialAgentsInfo = 5,

        [Description("Информация за крайни получатели")]
        EndUsersInfo = 6
    }
}
