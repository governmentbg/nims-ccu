using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication.Parsers;
using Eumis.Common.Db;
using Eumis.Data.Counters;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication
{
    internal class ProjectManagingAuthorityCommunicationService : IProjectManagingAuthorityCommunicationService
    {
        private IUnitOfWork unitOfWork;
        private ICountersRepository countersRepository;
        private IProjectsRepository projectsRepository;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IBlobServerCommunicator blobServerCommunicator;
        private IProjectMassManagingAuthorityCommunicationRecipientParser projectMassManagingAuthorityCommunicationRecipientParser;

        public ProjectManagingAuthorityCommunicationService(
            IUnitOfWork unitOfWork,
            ICountersRepository countersRepository,
            IProjectsRepository projectsRepository,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IBlobServerCommunicator blobServerCommunicator,
            IProjectMassManagingAuthorityCommunicationRecipientParser projectMassManagingAuthorityCommunicationRecipientParser)
        {
            this.unitOfWork = unitOfWork;
            this.countersRepository = countersRepository;
            this.projectsRepository = projectsRepository;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectMassManagingAuthorityCommunicationsRepository = projectMassManagingAuthorityCommunicationsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.blobServerCommunicator = blobServerCommunicator;
            this.projectMassManagingAuthorityCommunicationRecipientParser = projectMassManagingAuthorityCommunicationRecipientParser;
        }

        #region ProjectManagingAuthorityCommunication

        public IList<string> CanCreate(int projectId)
        {
            var errors = new List<string>();

            var hasAssociatedRegistration = this.projectsRepository.HasAssociatedRegistration(projectId);
            if (!hasAssociatedRegistration)
            {
                errors.Add(ApplicationServicesTexts.ProjectManagingAuthorityCommunicationService_CanCreate_DoesNotHaveAssociatedRegistration);
            }

            return errors;
        }

        public Domain.Projects.ProjectManagingAuthorityCommunication CreateProjectCommunication(int projectId, ProjectManagingAuthorityCommunicationSource source)
        {
            var xml = this.documentRestApiCommunicator.CreateProjectManagingAuthorityCommunicationQuestionXml();

            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectId);

            return new Domain.Projects.ProjectManagingAuthorityCommunication(
                projectId,
                actualProjectVersion.ProjectVersionXmlId,
                this.projectManagingAuthorityCommunicationsRepository.GetNextOrderNumber(projectId),
                xml,
                source);
        }

        public bool CanActivateQuestion(Guid communicationGid)
        {
            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            return communication.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.DraftQuestion;
        }

        public bool CanDelete(int communicationId)
        {
            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationId);

            return communication.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.DraftQuestion;
        }

        public void AssertIsFromBeneficiary(Domain.Projects.ProjectManagingAuthorityCommunication communication)
        {
            if (communication.Source != ProjectManagingAuthorityCommunicationSource.Beneficiary)
            {
                throw new UnauthorizedAccessException("ProjectCommunicationAnswer source must be 'Beneficiary'");
            }
        }

        public void AssertIsFromManagingAuthority(Domain.Projects.ProjectManagingAuthorityCommunication communication)
        {
            if (communication.Source != ProjectManagingAuthorityCommunicationSource.ManagingAuthority)
            {
                throw new UnauthorizedAccessException("ProjectCommunicationAnswer source must be 'ManagingAuthority'");
            }
        }

        #endregion

        #region ProjectCommunicationAnswer

        public IList<string> CanCreateProjectCommunicationAnswer(int communicationId)
        {
            var errors = new List<string>();

            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationId);
            if (communication.Answers.Any(a => a.Status == ProjectCommunicationAnswerStatus.Draft))
            {
                errors.Add("Вече имате създаден отговор в статус 'Чернова' към тази комуникация.");
            }

            return errors;
        }

        public Domain.Projects.ProjectCommunicationAnswer CreateProjectCommunicationAnswer(
            Domain.Projects.ProjectManagingAuthorityCommunication communication,
            int projectId,
            int orderNum)
        {
            var initialXml = communication.Answers.Any() ?
                communication.GetLastAnswerXml() :
                communication.Question.Xml;

            var answerXml = this.documentRestApiCommunicator.CreateProjectManagingAuthorityCommunicationAnswerXml(initialXml);

            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectId);

            return new ProjectCommunicationAnswer(answerXml, actualProjectVersion.ProjectVersionXmlId, orderNum, ProjectCommunicationAnswerSource.ManagingAuthority);
        }

        public void AssertIsBeneficiaryAnswer(ProjectCommunicationAnswer answer)
        {
            if (answer.Source != ProjectCommunicationAnswerSource.Beneficiary)
            {
                throw new UnauthorizedAccessException("ProjectCommunicationAnswer source must be 'Beneficiary'");
            }
        }

        public void AssertIsManagingAuthorityAnswer(ProjectCommunicationAnswer answer)
        {
            if (answer.Source != ProjectCommunicationAnswerSource.ManagingAuthority)
            {
                throw new UnauthorizedAccessException("ProjectCommunicationAnswer source must be 'ManagingAuthority'");
            }
        }

        public void AssertProjectCommunicationAnswerPreconditions(int projectCommunicationId)
        {
            var communication = this.projectManagingAuthorityCommunicationsRepository.FindWithoutIncludes(projectCommunicationId);

            if (communication.ManagingAuthorityCommunicationStatus != ProjectManagingAuthorityCommunicationStatus.Question
                || (communication.QuestionEndingDate.HasValue && communication.QuestionEndingDate.Value < DateTime.Now))
            {
                throw new InvalidOperationException("Cannot create/update/delete ProjectCommunicationAnswer");
            }
        }

        #endregion

        #region ProjectMassManagingAuthorityCommunicationRecipients

        public IList<int> ParseRecipientsExcelFile(Guid blobKey, out IList<string> errors)
        {
            IList<int> projectIds = new List<int>();
            IList<string> projectRegNumbers = new List<string>();
            errors = new List<string>();

            try
            {
                using (var excelStream = this.blobServerCommunicator.GetBlobStream(blobKey, true))
                {
                    projectRegNumbers = this.projectMassManagingAuthorityCommunicationRecipientParser.ParseExcel(excelStream, out errors);
                }
            }
            catch (FileFormatException)
            {
                errors.Add(ApplicationServicesTexts.Common_InvalidFileFormat);
            }

            if (errors.Any() || projectRegNumbers.Count == 0)
            {
                return projectIds;
            }

            foreach (var projectRegNumber in projectRegNumbers)
            {
                var projectId = this.projectsRepository.GetProjectId(projectRegNumber);

                if (!projectId.HasValue)
                {
                    errors.Add(string.Format(ApplicationServicesTexts.ProjectManagingAuthorityCommunicationService_ParseRecipientsExcelFile_ProjectNotFound, projectRegNumber));
                    continue;
                }

                projectIds.Add(projectId.Value);
            }

            return projectIds;
        }

        #endregion ProjectMassManagingAuthorityCommunicationRecipients

        #region ProjectMassManagingAuthorityCommunication

        public void SendProjectMassManagingAuthorityCommunication(int projectMassManagingAuthorityCommunicationId, byte[] version)
        {
            var massCommunication = this.projectMassManagingAuthorityCommunicationsRepository.FindForUpdate(projectMassManagingAuthorityCommunicationId, version);

            if (massCommunication.CanSend().Count > 0)
            {
                throw new InvalidOperationException("Cannot send ProjectMassManagingAuthorityCommunication");
            }

            var xml = this.documentRestApiCommunicator.CreateProjectManagingAuthorityCommunicationQuestionXml(new ProjectMassCommunicationPVO(massCommunication));

            massCommunication.Recipients.ForEach(p =>
            {
                string newXml = xml.Replace(ProjectMassManagingAuthorityCommunication.MassCommunicationTemplateXmlKey, Guid.NewGuid().ToString());
                var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(p.ProjectId);
                var orderNumber = this.projectManagingAuthorityCommunicationsRepository.GetNextOrderNumber(p.ProjectId);

                var newCommunication = new Domain.Projects.ProjectManagingAuthorityCommunication(
                p.ProjectId,
                actualProjectVersion.ProjectVersionXmlId,
                orderNumber,
                newXml,
                ProjectManagingAuthorityCommunicationSource.ManagingAuthority);

                this.projectManagingAuthorityCommunicationsRepository.Add(newCommunication);
                this.unitOfWork.Save();

                newCommunication.QuestionEndingDate = massCommunication.EndingDate.Value.AddDays(1).Date.AddMilliseconds(-1);

                if (massCommunication.Subject.HasValue)
                {
                    newCommunication.SetSubject(massCommunication.Subject.Value);
                }

                this.countersRepository.CreateProjectManagingAuthorityCommunicationCounter(newCommunication.ProjectId);

                var regNumber = this.countersRepository.GetNextProjectManagingAuthorityCommunicationNumber(newCommunication.ProjectId);
                newCommunication.MakeQuestion(regNumber);
            });

            massCommunication.Status = ProjectMassManagingAuthorityCommunicationStatus.Sent;
            this.unitOfWork.Save();
        }

        #endregion ProjectMassManagingAuthorityCommunication
    }
}
