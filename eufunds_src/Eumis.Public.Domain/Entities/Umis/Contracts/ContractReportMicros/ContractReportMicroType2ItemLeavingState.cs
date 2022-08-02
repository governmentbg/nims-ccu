using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemLeavingState
    {
        [Description("което не е получило предложение за работа, обучение или образование")]
        NoJobOfferOrEducation = 1,

        [Description("ангажирано с образование/обучение")]
        Student = 2,

        [Description("получило предложение за работа, обучение или образование")]
        JobOfferOrEducation = 3,

        [Description("което е заето при същия работодател")]
        EmployedFromTheSameEmployer = 4,

        [Description("което е заето при друг работодател")]
        EmployedFromDifferentEmployer = 5,

        [Description("което е самонаето")]
        SelfEmployed = 6
    }
}
