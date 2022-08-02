using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemOccupation
    {
        [Description("заето лице")]
        Employed = 1,

        [Description("самонаето лице")]
        SelfEmployed = 2,

        [Description("безработно лице до 6 месеца")]
        UnemployedLessThanSixMonths = 3,

        [Description("безработно лице над 6 месеца")]
        UnemployedMoreThanSixMonths = 4,

        [Description("безработно лице над 12 месеца")]
        UnemployedMoreThanYear = 5,

        [Description("неактивно лице")]
        Unemployed = 6,

        [Description("неактивно лице извън образование или обучение")]
        UnemployedWithoutEducation = 7,

        [Description("Неактивно лице, ангажирано с образование и обучение (учащ)")]
        Student = 8
    }
}