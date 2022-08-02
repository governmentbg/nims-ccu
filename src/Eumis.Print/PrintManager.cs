using System;
using Eumis.Print.AnswerRegistration;
using Eumis.Print.ContractVersionSAPData;
using Eumis.Print.MonitorstatRequestDeclaration;
using Eumis.Print.ProjectRegistration;
using Newtonsoft.Json.Linq;

namespace Eumis.Print
{
    public class PrintManager : IPrintManager
    {
        public byte[] Print(TemplateType templateType, PrintType printType, JObject context)
        {
            ITemplate template = null;

            switch (templateType)
            {
                case TemplateType.ProjectRegistration:
                    template = new ProjectRegistrationTemplate();
                    break;
                case TemplateType.AnswerRegistration:
                    template = new AnswerRegistrationTemplate();
                    break;
                case TemplateType.ContractReportSAPData:
                    template = new ContractReportSAPDataTemplate();
                    break;
                case TemplateType.ContractVersionSAPData:
                    template = new ContractVersionSAPDataTemplate();
                    break;
                case TemplateType.MonitorstatRequestDeclaration:
                    template = new MonitorstatRequestDeclarationTemplate();
                    break;
                default:
                    throw new ArgumentException();
            }

            return template.Print(printType, context);
        }
    }
}
