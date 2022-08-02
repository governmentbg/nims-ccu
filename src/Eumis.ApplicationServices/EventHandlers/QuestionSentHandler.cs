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
    public class QuestionSentHandler : EventHandler<QuestionSentEvent>
    {
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IProjectsRepository projectsRepository;
        private IRegistrationsRepository registrationsRepository;
        private IProceduresRepository proceduresRepository;
        private IEmailsRepository emailsRepository;

        public QuestionSentHandler(
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IProjectsRepository projectsRepository,
            IRegistrationsRepository registrationsRepository,
            IProceduresRepository proceduresRepository,
            IEmailsRepository emailsRepository)
        {
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.projectsRepository = projectsRepository;
            this.registrationsRepository = registrationsRepository;
            this.proceduresRepository = proceduresRepository;
            this.emailsRepository = emailsRepository;
        }

        public override void Handle(QuestionSentEvent e)
        {
            var communication = this.projectCommunicationsRepository.Find(e.ProjectCommunicationId);
            var project = this.projectsRepository.Find(communication.ProjectId);
            var registrationEmail = this.registrationsRepository.GetRegistrationEmailForProject(communication.ProjectId);
            var procedureData = this.proceduresRepository.GetProcedureBasicData(project.ProcedureId);

            Email email = new Email(
                registrationEmail,
                EmailTemplate.QuestionSentMessage,
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
                        questionEndingDate = communication.QuestionEndingDate?.ToString(),
                    }));

            this.emailsRepository.Add(email);
        }
    }
}
