using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Eumis.ApplicationServices.Communicators;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Projects;
using Ionic.Zip;

namespace Eumis.ApplicationServices.Services.ProjectCommunication
{
    internal class ProjectCommunicationService : IProjectCommunicationService
    {
        private IUnitOfWork unitOfWork;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectsRepository projectsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IBlobServerCommunicator blobServerCommunicator;

        public ProjectCommunicationService(
            IUnitOfWork unitOfWork,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectsRepository projectsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IBlobServerCommunicator blobServerCommunicator)
        {
            this.unitOfWork = unitOfWork;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectsRepository = projectsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.blobServerCommunicator = blobServerCommunicator;
        }

        public IList<string> CanCreate(int projectId, int evalSessionId)
        {
            var errors = new List<string>();

            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(evalSessionId);
            if (sessionStatus != EvalSessionStatus.Active)
            {
                errors.Add(ApplicationServicesTexts.ProjectCommunicationService_CanCreate_EvalSessionNotActive);
            }

            var lastVersionStatus = this.projectVersionXmlsRepository.GetLastVersionStatus(projectId);

            if (lastVersionStatus.HasValue && lastVersionStatus == ProjectVersionXmlStatus.Draft)
            {
                errors.Add(ApplicationServicesTexts.ProjectCommunicationService_CanCreate_ProjectHasDraftVersion);
            }
            else if (!lastVersionStatus.HasValue || lastVersionStatus != ProjectVersionXmlStatus.Actual)
            {
                errors.Add(ApplicationServicesTexts.ProjectCommunicationService_CanCreate_ProjectHasLastNonActualVersion);
            }

            var hasCommunicationInProgress = this.projectCommunicationsRepository.HasProjectCommunicationInProgress(evalSessionId, projectId);
            if (hasCommunicationInProgress)
            {
                errors.Add(ApplicationServicesTexts.ProjectCommunicationService_CanCreate_HasCommunicationInProgress);
            }

            var hasAssociatedRegistration = this.projectsRepository.HasAssociatedRegistration(projectId);
            if (!hasAssociatedRegistration)
            {
                errors.Add(ApplicationServicesTexts.ProjectCommunicationService_CanCreate_DoesNotHaveAssociatedRegistration);
            }

            return errors;
        }

        public IList<string> CanRegisterEvalSessionProjectAnswer(int communicationId, int answerId, string regAnswerHash, DateTime regDate)
        {
            var communication = this.projectCommunicationsRepository.Find(communicationId);
            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(communication.EvalSessionId);

            var answer = communication.FindAnswer(answerId);

            List<string> errors = new List<string>();

            if (answer.Answer.Hash != regAnswerHash)
            {
                errors.Add(ApplicationServicesTexts.ProjectCommunicationService_CanRegisterEvalSessionProjectAnswer_NoAnswerWithThisCode);
            }

            if (regDate > communication.QuestionEndingDate)
            {
                errors.Add(ApplicationServicesTexts.ProjectCommunicationService_CanRegisterEvalSessionProjectAnswer_QuestionTimedOut);
            }

            if (sessionStatus != EvalSessionStatus.Active)
            {
                errors.Add(ApplicationServicesTexts.ProjectCommunicationService_CanRegisterEvalSessionProjectAnswer_SessionInactive);
            }

            return errors;
        }

        public bool CanDelete(int communicationId)
        {
            var communication = this.projectCommunicationsRepository.Find(communicationId);
            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(communication.EvalSessionId);

            return sessionStatus == EvalSessionStatus.Active &&
                Eumis.Domain.Projects.ProjectCommunication.DeletableStatuses.Contains(communication.Status);
        }

        public bool CanCancel(int communicationId)
        {
            var communication = this.projectCommunicationsRepository.Find(communicationId);
            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(communication.EvalSessionId);

            return sessionStatus == EvalSessionStatus.Active &&
                Eumis.Domain.Projects.ProjectCommunication.CancellableStatuses.Contains(communication.Status);
        }

        public bool IsCommunicationEvalSessionStatusActive(int communicationId)
        {
            var communication = this.projectCommunicationsRepository.Find(communicationId);
            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(communication.EvalSessionId);

            return sessionStatus == EvalSessionStatus.Active;
        }

        public bool CanUpdateQuestion(Guid communicationGid)
        {
            var communication = this.projectCommunicationsRepository.Find(communicationGid);
            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(communication.EvalSessionId);

            return sessionStatus == EvalSessionStatus.Active && communication.Status == ProjectCommunicationStatus.DraftQuestion;
        }

        public bool CanActivateQuestion(Guid communicationGid)
        {
            var communication = this.projectCommunicationsRepository.Find(communicationGid);
            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(communication.EvalSessionId);

            return sessionStatus == EvalSessionStatus.Active && communication.Status == ProjectCommunicationStatus.DraftQuestion;
        }

        public Domain.Projects.ProjectCommunication CreateCommunication(int projectId, int evalSessionId)
        {
            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectId);

            var xml = this.documentRestApiCommunicator.CreateProjectCommunicationQuestionXml(actualProjectVersion.Xml);

            return new Domain.Projects.ProjectCommunication(
                projectId,
                actualProjectVersion.ProjectVersionXmlId,
                evalSessionId,
                this.projectCommunicationsRepository.GetNextOrderNumber(projectId),
                xml);
        }

        public byte[] GetProjectCommunicationAttachedDocumentsZip(int communicationId, Guid answerGid, bool isQuestion)
        {
            var projectCommunication = this.projectCommunicationsRepository.Find(communicationId);

            IList<Rio.AttachedDocument> attachedDocuments = new List<Rio.AttachedDocument>();
            if (isQuestion)
            {
                attachedDocuments = projectCommunication
                    .Question
                    .GetDocument()
                    .Project
                    .AttachedDocuments
                    .AttachedDocumentCollection
                    .Where(f => f.AttachedDocumentContent != null && !string.IsNullOrEmpty(f.AttachedDocumentContent.BlobContentId))
                    .ToList();
            }
            else
            {
                var answer = projectCommunication.FindAnswer(answerGid);

                attachedDocuments = answer
                    .Answer
                    .GetDocument()
                    .Project
                    .AttachedDocuments
                    .AttachedDocumentCollection
                    .Where(f => f.AttachedDocumentContent != null && !string.IsNullOrEmpty(f.AttachedDocumentContent.BlobContentId))
                    .ToList();
            }

            return this.GetDocumentsZip(attachedDocuments);
        }

        public byte[] GetCommunicationAttachedDocumentsZip(int communicationId)
        {
            var projectCommunication = this.projectCommunicationsRepository.Find(communicationId);

            var attachedDocuments = projectCommunication
                .Question
                .GetDocument()
                .ContentAttachedDocumentCollection
                .Where(f => f.AttachedDocumentContent != null && !string.IsNullOrEmpty(f.AttachedDocumentContent.BlobContentId))
                .ToList();

            return this.GetDocumentsZip(attachedDocuments);
        }

        public byte[] GetCommunicationAnswerAttachedDocumentsZip(int communicationId, Guid answerGid)
        {
            var projectCommunication = this.projectCommunicationsRepository.Find(communicationId);
            var answer = projectCommunication.FindAnswer(answerGid);

            var attachedDocuments = answer
                .Answer
                .GetDocument()
                .ReplyAttachedDocumentCollection
                .Where(f => f.AttachedDocumentContent != null && !string.IsNullOrEmpty(f.AttachedDocumentContent.BlobContentId))
                .ToList();

            return this.GetDocumentsZip(attachedDocuments);
        }

        public void DeleteProjectCommunicationAnswerMessageFiles(int projectCommunicationAnswerId)
        {
            this.unitOfWork.BulkDelete<ProjectCommunicationMessageFile>(p => p.ProjectCommunicationAnswerId == projectCommunicationAnswerId);
        }

        public void ApplyAnsweredProjectCommunication(int projectId)
        {
            var projectCommunication = this.projectCommunicationsRepository.GetActiveProjectCommunication(projectId);

            if (projectCommunication != null)
            {
                projectCommunication.Status = ProjectCommunicationStatus.Applied;
                projectCommunication.ModifyDate = DateTime.Now;
            }
        }

        private byte[] GetDocumentsZip(IEnumerable<Rio.AttachedDocument> attachedDocuments)
        {
            var files = new Dictionary<string, string>();

            var attachedFiles = new Dictionary<string, int>();

            foreach (var document in attachedDocuments)
            {
                var fileName = this.GetFileName(attachedFiles, document.AttachedDocumentContent.FileName);

                files.Add(fileName, document.AttachedDocumentContent.BlobContentId);

                if (document.SignatureContentCollection?.Count > 0)
                {
                    var signatures = document.SignatureContentCollection.Where(d => !string.IsNullOrEmpty(d.BlobContentId));

                    foreach (var signature in document.SignatureContentCollection)
                    {
                        var signatureName = this.GetFileName(attachedFiles, signature.FileName);

                        files.Add(signatureName, signature.BlobContentId);
                    }
                }
            }

            var zipFile = this.ZipFiles(files);

            return zipFile;
        }

        private string GetFileName(IDictionary<string, int> attachedFiles, string documentFileName)
        {
            string fullFileName = null;

            if (!attachedFiles.ContainsKey(documentFileName))
            {
                attachedFiles.Add(documentFileName, 0);
                fullFileName = documentFileName;
            }
            else
            {
                attachedFiles[documentFileName]++;

                var fileNameEndIndex = documentFileName.LastIndexOf('.');

                var fileName = documentFileName.Substring(0, fileNameEndIndex);
                var extension = documentFileName.Substring(fileNameEndIndex);

                fullFileName = $"{fileName} ({attachedFiles[documentFileName]}){extension}";
            }

            return fullFileName;
        }

        private byte[] ZipFiles(IDictionary<string, string> files)
        {
            byte[] zipContent = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (ZipFile zipFile = new ZipFile())
                {
                    zipFile.Encryption = EncryptionAlgorithm.None;

                    zipFile.AlternateEncoding = Encoding.UTF8;
                    zipFile.AlternateEncodingUsage = ZipOption.AsNecessary;

                    foreach (var file in files)
                    {
                        var blobKey = Guid.Parse(file.Value);

                        using (var blobStream = this.blobServerCommunicator.GetBlobStream(blobKey, true))
                        {
                            var blobContent = this.StreamToByteArray(blobStream);
                            zipFile.AddEntry(file.Key, blobContent);
                        }
                    }

                    zipFile.Save(memoryStream);
                }

                zipContent = memoryStream.ToArray();
            }

            return zipContent;
        }

        private byte[] StreamToByteArray(Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];

            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
            {
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            }

            return buffer;
        }
    }
}
