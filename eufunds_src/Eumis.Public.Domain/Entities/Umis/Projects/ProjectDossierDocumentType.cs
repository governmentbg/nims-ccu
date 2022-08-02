using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Projects
{
    public enum ProjectDossierDocumentType
    {
        [Description("Версия на ПП")]
        ProjectVersion = 1,

        [Description("Оценителен лист")]
        EvalSessionSheet = 2,

        [Description("Комуникация към ПП")]
        ProjectCommunication = 3,

        [Description("Версия на договор")]
        ContractVersion = 4,

        [Description("Декларация за присъединяване на профил за достъп")]
        ContractsContractRegistration = 5,

        [Description("Процедура за избор на изпълнител и сключени договори")]
        ContractProcurement = 6,

        [Description("Искане за плащане")]
        ContractReportPayment = 7,

        [Description("Финансов отчет")]
        ContractReportFinancial = 8,

        [Description("Технически отчет")]
        ContractReportTechnical = 9,

        [Description("Микроданни участници (ФЕПНЛ)")]
        ContractReportMicroType1 = 10,

        [Description("Микроданни участници (ЕСФ)")]
        ContractReportMicroType2 = 11,

        [Description("Микроданни хранителни продукти")]
        ContractReportMicroType3 = 12,

        [Description("Микроданни на АСП")]
        ContractReportMicroType4 = 13,

        [Description("Комуникация към Договор")]
        ContractAdminAuthorityCommunication = 113
    }
}
