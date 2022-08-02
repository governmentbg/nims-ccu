using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemEducation
    {
        [Description("Без образование")]
        NoEducation = 1,

        [Description("Основно образование (начално  или прогимназиално)")]
        ElementaryEducation = 2,

        [Description("Средно образование (гимназия, техникум и т.н.)")]
        SecondaryEducation = 3,

        [Description("Следгимназиално (професионално обучение след средно образование – ІV степен)")]
        PostSecondaryEducation = 4,

        [Description("Висше образование")]
        HigherEducation = 5
    }
}
