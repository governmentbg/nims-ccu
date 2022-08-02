using Eumis.Common.Email;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Emails;
using Eumis.Domain.Events;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class ProjectMACommunicationQuestionSentHandler : Domain.Core.EventHandler<ProjectMACommunicationQuestionSentEvent>
    {
        private IEmailsRepository emailsRepository;
        private IProjectsRepository projectsRepository;
        private IRegistrationsRepository registrationsRepository;
        private IProceduresRepository proceduresRepository;
        private IProjectManagingAuthorityCommunicationsRepository managingAuthorityCommunicationsRepository;

        public ProjectMACommunicationQuestionSentHandler(
            IRegistrationsRepository registrationsRepos,
            IProceduresRepository proceduresRepository,
            IProjectManagingAuthorityCommunicationsRepository managingAuthorityCommunicationsRepository,
            IProjectsRepository projectsRepository,
            IEmailsRepository emailsRepository)
        {
            this.emailsRepository = emailsRepository;
            this.projectsRepository = projectsRepository;
            this.registrationsRepository = registrationsRepos;
            this.proceduresRepository = proceduresRepository;
            this.managingAuthorityCommunicationsRepository = managingAuthorityCommunicationsRepository;
        }

        public override void Handle(ProjectMACommunicationQuestionSentEvent e)
        {
            var communication = this.managingAuthorityCommunicationsRepository.FindWithoutIncludes(e.ProjectCommunicationId);
            var project = this.projectsRepository.FindWithoutIncludes(communication.ProjectId);
            var registrationEmail = this.registrationsRepository.GetRegistrationEmailForProject(communication.ProjectId);
            var procedureData = this.proceduresRepository.GetProcedureBasicData(project.ProcedureId);

            Email email = new Email(
                registrationEmail,
                EmailTemplate.ProjectMACommunicationQuestionSentMessage,
                JObject.FromObject(
                    new
                    {
                        procCode = procedureData.Code,
                        procName = procedureData.Name,
                        programmeName = procedureData.PrimaryProgrammeName,
                        companyName = project.CompanyName,
                        projectName = project.Name,
                        regNumber = communication.RegNumber,
                        email = registrationEmail,
                    }));

            this.emailsRepository.Add(email);
        }
    }
}
