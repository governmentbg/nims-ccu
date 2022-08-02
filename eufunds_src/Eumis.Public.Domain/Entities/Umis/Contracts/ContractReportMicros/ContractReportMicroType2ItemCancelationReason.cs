using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemCancelationReason
    {
        [Description("Получих предложение за друга работа")]
        JobOffer = 1,

        [Description("Нямах възможност да продължа участие поради лични причини")]
        Personal = 2,

        [Description("Получих предложение за продължаване на образование/обучение")]
        Education = 3,

        [Description("Друго")]
        Other = 4
    }
}
