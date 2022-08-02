using Eumis.Common.Email;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ProjectRegisteredHandler : EventHandler<ProjectRegisteredEvent>
    {
        private IProjectsRepository projectsRepository;
        private IRegProjectXmlsRepository regProjectXmlsRepository;
        private IRegistrationsRepository registrationsRepository;
        private IProceduresRepository proceduresRepository;
        private IEmailsRepository emailsRepository;

        public ProjectRegisteredHandler(
            IProjectsRepository projectsRepository,
            IRegProjectXmlsRepository regProjectXmlsRepository,
            IRegistrationsRepository registrationsRepository,
            IProceduresRepository proceduresRepository,
            IEmailsRepository emailsRepository)
        {
            this.projectsRepository = projectsRepository;
            this.regProjectXmlsRepository = regProjectXmlsRepository;
            this.registrationsRepository = registrationsRepository;
            this.proceduresRepository = proceduresRepository;
            this.emailsRepository = emailsRepository;
        }

        public override void Handle(ProjectRegisteredEvent e)
        {
            var regProjectXml = this.regProjectXmlsRepository.Find(e.RegProjectXmlId);
            var project = this.projectsRepository.Find(regProjectXml.ProjectId.Value);
            var registrationEmail = this.registrationsRepository.GetEmail(regProjectXml.RegistrationId);
            var procedureData = this.proceduresRepository.GetProcedureBasicData(regProjectXml.ProcedureId);

            // create email confirming the registration
            Email email = new Email(
                registrationEmail,
                EmailTemplate.ProjectRegisteredMessage,
                JObject.FromObject(
                    new
                    {
                        procCode = procedureData.Code,
                        procName = procedureData.Name,
                        programmeName = procedureData.PrimaryProgrammeName,
                        companyName = regProjectXml.CompanyName,
                        projectName = project.Name,
                        projectRegNumber = project.RegNumber,
                        email = registrationEmail,
                    }));

            this.emailsRepository.Add(email);
        }
    }
}
