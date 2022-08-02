using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemActivity
    {
        [Description("Потребител на услуги")]
        User = 1,

        [Description("Личен асистент")]
        Assistant = 2,

        [Description("Обучение на служители от администрацията по ПО 2 по ОПДУ")]
        TeachingAdministration = 3,

        [Description("Съпътстващо обучение на служители от администрацията по ПО 2 по ОПДУ")]
        ConcomitantTeachingAdministration = 4,

        [Description("Обучение на магистрати, съдебни служители и служители на разследващите органи по НПК по ПО 3 по ОПДУ")]
        TeachingInvestigationAuth = 5,

        [Description("Съпътстващо обучение на магистрати, съдебни служители и служители на разследващите органи по НПК по ПО 3 по ОПДУ")]
        ConcomitantTeachingInvestigationAuth = 6,

        [Description("Обучение на служители на НПО и социално-икономически партньори по ОПДУ")]
        TeachingNgo = 7,

        [Description("Обучение на служители по ПО 4 по ОПДУ")]
        TeachingProgrammePriority4 = 8,

        [Description("Обучение на служители на УО и членове на КН по ПО 5 по ОПДУ")]
        TeachingManagingAuth = 9,

        [Description("Обучение по ПО 1 по ОПДУ")]
        TeachingProgrammePriority1 = 10
    }
}
