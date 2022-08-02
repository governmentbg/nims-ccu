using System.ComponentModel;

namespace Eumis.Print
{
    public enum TemplateType
    {
        // Регистрация на проектно предложение
        ProjectRegistration = 1,

        // Регистрация на отговор
        AnswerRegistration = 2,

        // Данни за САП към пакет отчетни документи
        ContractReportSAPData = 3,

        // Данни за САП към версия на договор
        ContractVersionSAPData = 4,

        MonitorstatRequestDeclaration = 5,
    }
}
