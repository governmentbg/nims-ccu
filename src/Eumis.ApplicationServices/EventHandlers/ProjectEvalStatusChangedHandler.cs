using Eumis.Common.Email;
using Eumis.Common.Json;
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
    public class ProjectEvalStatusChangedHandler : EventHandler<ProjectEvalStatusChangedEvent>
    {
        private IProjectsRepository projectsRepository;
        private IRegProjectXmlsRepository regProjectXmlsRepository;
        private IRegistrationsRepository registrationsRepository;
        private IProceduresRepository proceduresRepository;
        private IEmailsRepository emailsRepository;

        public ProjectEvalStatusChangedHandler(
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

        public override void Handle(ProjectEvalStatusChangedEvent e)
        {
            var project = this.projectsRepository.Find(e.ProjectId);
            var registrationEmail = this.registrationsRepository.GetEmailByProject(project.ProjectId);

            if (!string.IsNullOrEmpty(registrationEmail))
            {
                var procedureData = this.proceduresRepository.GetProcedureBasicData(e.ProcedureId);

                // create email confirming the registration
                Email email = new Email(
                    registrationEmail,
                    EmailTemplate.ProjectEvalStatusChangedMessage,
                    JObject.FromObject(
                        new
                        {
                            procCode = procedureData.Code,
                            procName = procedureData.Name,
                            programmeName = procedureData.PrimaryProgrammeName,
                            companyName = project.CompanyName,
                            projectName = project.Name,
                            projectRegNumber = project.RegNumber,
                            email = registrationEmail,
                            statusDescription = project.EvalStatus.GetEnumDescription(),
                            status = project.EvalStatus,
                        }));

                this.emailsRepository.Add(email);
            }
        }
    }
}
