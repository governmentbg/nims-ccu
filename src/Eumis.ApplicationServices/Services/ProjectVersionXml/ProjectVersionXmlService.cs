using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.Company;
using Eumis.ApplicationServices.Services.ProjectCommunication;
using Eumis.Common.Db;
using Eumis.Common.Localization;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.Core;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Projects;
using Ionic.Zip;

namespace Eumis.ApplicationServices.Services.ProjectVersionXml
{
    internal class ProjectVersionXmlService : IProjectVersionXmlService
    {
        private IUnitOfWork unitOfWork;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectsRepository projectsRepository;
        private ICompaniesRepository companiesRepository;
        private IProceduresRepository proceduresRepository;
        private ICompanyCreationService companyCreationService;
        private IProjectCommunicationService projectCommunicationService;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IBlobServerCommunicator blobServerCommunicator;

        public ProjectVersionXmlService(
            IUnitOfWork unitOfWork,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectsRepository projectsRepository,
            ICompaniesRepository companiesRepository,
            IProceduresRepository proceduresRepository,
            ICompanyCreationService companyCreationService,
            IProjectCommunicationService projectCommunicationService,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IBlobServerCommunicator blobServerCommunicator)
        {
            this.unitOfWork = unitOfWork;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectsRepository = projectsRepository;
            this.companiesRepository = companiesRepository;
            this.proceduresRepository = proceduresRepository;
            this.companyCreationService = companyCreationService;
            this.projectCommunicationService = projectCommunicationService;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.blobServerCommunicator = blobServerCommunicator;
        }

        public IList<string> CanCreateProjectVersion(int projectId, int evalSessionId)
        {
            var errors = this.CanCreateProjectVersionInt(projectId);

            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(evalSessionId);
            if (sessionStatus != EvalSessionStatus.Active)
            {
                errors.Add(ApplicationServicesTexts.ProjectVersionXmlService_CanCreate_SessionActive);
            }

            var hasCommunicationInProgress = this.projectCommunicationsRepository.HasProjectCommunicationInProgress(evalSessionId, projectId);
            if (hasCommunicationInProgress)
            {
                errors.Add(ApplicationServicesTexts.ProjectVersionXmlService_CanCreate_CommunicationInProgress);
            }

            return errors;
        }

        public bool CanCreateProjectVersionFromRegData(int projectId, int evalSessionId)
        {
            var hasPrevVersions = this.projectVersionXmlsRepository.GetProjectVersions(projectId).Any();
            var sessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(evalSessionId);

            return !hasPrevVersions && sessionStatus == EvalSessionStatus.Active;
        }

        public Domain.Projects.ProjectVersionXml CreateProjectVersion(
            int projectId,
            int userId,
            string createNote,
            string createNoteAlt)
        {
            if (this.CanCreateProjectVersionInt(projectId).Any())
            {
                throw new InvalidOperationException("Cannot create project version.");
            }

            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectId);
            var orderNum = this.projectVersionXmlsRepository.GetNextOrderNum(projectId);

            var newProjectVersion = new Domain.Projects.ProjectVersionXml(
                actualProjectVersion,
                userId,
                createNote,
                createNoteAlt,
                orderNum);

            this.projectVersionXmlsRepository.Add(newProjectVersion);

            this.projectCommunicationService.ApplyAnsweredProjectCommunication(projectId);

            this.unitOfWork.Save();

            this.ArchiveProjectVersions(projectId, newProjectVersion.ProjectVersionXmlId);

            return newProjectVersion;
        }

        public Domain.Projects.ProjectVersionXml CreateProjectVersionFromProjectData(int projectId, int userId)
        {
            var project = this.projectsRepository.Find(projectId);
            var procedureGid = this.proceduresRepository.GetGid(project.ProcedureId);
            var company = this.companiesRepository.Find(project.CompanyId);

            var projectXml = this.documentRestApiCommunicator.CreateFirstProjectVersionXml(procedureGid);
            var rioProject = new RioXmlDocument<Eumis.Rio.Project>();
            rioProject.SetXml(projectXml);
            var projectDoc = rioProject.GetDocument();

            projectDoc.ProjectBasicData.Name = project.Name;
            projectDoc.Candidate = this.companyCreationService.CreateRioCompany(company);

            var newProjectVersion = new Domain.Projects.ProjectVersionXml(
                project.ProjectId,
                projectDoc,
                userId,
                ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectVersionXmlService_CreateProjectVersion), new CultureInfo(SystemLocalization.Bg_BG)),
                ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectVersionXmlService_CreateProjectVersion), new CultureInfo(SystemLocalization.En_GB)));

            this.projectVersionXmlsRepository.Add(newProjectVersion);
            this.unitOfWork.Save();

            return newProjectVersion;
        }

        public Domain.Projects.ProjectVersionXml CreateProjectServiceVersion(Project project, int companyId, int userId)
        {
            var procedureGid = this.proceduresRepository.GetGid(project.ProcedureId);

            var company = this.companiesRepository.Find(companyId);

            var projectXml = this.documentRestApiCommunicator.CreateFirstProjectVersionXml(procedureGid);
            var rioProject = new RioXmlDocument<Eumis.Rio.Project>();
            rioProject.SetXml(projectXml);
            var projectDoc = rioProject.GetDocument();

            projectDoc.ProjectBasicData.Name = project.Name;
            projectDoc.ProjectBasicData.NameEN = project.NameAlt;

            projectDoc.Candidate = this.companyCreationService.CreateRioCompany(company);

            var newProjectVersion = new Domain.Projects.ProjectVersionXml(
                project.ProjectId,
                projectDoc,
                userId,
                ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectVersionXmlService_CreateProjectServiceVersion), new CultureInfo(SystemLocalization.Bg_BG)),
                ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectVersionXmlService_CreateProjectServiceVersion), new CultureInfo(SystemLocalization.En_GB)));

            newProjectVersion.ActivateProjectVersion(true);

            this.projectVersionXmlsRepository.Add(newProjectVersion);
            this.unitOfWork.Save();

            return newProjectVersion;
        }

        public void CreateProjectVersionFromCommunication(Eumis.Domain.Projects.ProjectCommunication communication, string answerXml)
        {
            if (this.CanCreateProjectVersionInt(communication.ProjectId).Any())
            {
                throw new InvalidOperationException("Cannot create project version.");
            }

            var orderNum = this.projectVersionXmlsRepository.GetNextOrderNum(communication.ProjectId);
            var xml = this.documentRestApiCommunicator.CreateProjectVersionXmlFromCommunication(answerXml);

            var newProjectVersion = new Domain.Projects.ProjectVersionXml(
                communication.ProjectId,
                orderNum,
                string.Format("{0} {1}.", ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectVersionXmlService_CreateProjectVersionFromCommunication), new CultureInfo(SystemLocalization.Bg_BG)), communication.RegNumber),
                string.Format("{0} {1}.", ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.ProjectVersionXmlService_CreateProjectVersionFromCommunication), new CultureInfo(SystemLocalization.En_GB)), communication.RegNumber),
                xml);

            this.projectVersionXmlsRepository.Add(newProjectVersion);
            this.unitOfWork.Save();

            this.ArchiveProjectVersions(communication.ProjectId, newProjectVersion.ProjectVersionXmlId);
        }

        public bool CanUpdateProjectVersionData(int versionId, int evalSessionId)
        {
            var projectVersion = this.projectVersionXmlsRepository.Find(versionId);
            var evalSessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(evalSessionId);

            return projectVersion.Status == Domain.Projects.ProjectVersionXmlStatus.Draft && evalSessionStatus == EvalSessionStatus.Active;
        }

        public bool CanDeleteProjectVersion(int versionId, int evalSessionId)
        {
            var projectVersion = this.projectVersionXmlsRepository.Find(versionId);
            var evalSessionStatus = this.evalSessionsRepository.GetEvalSessionStatus(evalSessionId);

            return projectVersion.Status == Domain.Projects.ProjectVersionXmlStatus.Draft && evalSessionStatus == EvalSessionStatus.Active;
        }

        private void ArchiveProjectVersions(int projectId, int newVersionId)
        {
            var versionsToArchive = this.projectVersionXmlsRepository.GetNonArchivedProjectVersions(projectId)
                .Where(p => p.ProjectVersionXmlId != newVersionId);

            foreach (var projVersion in versionsToArchive)
            {
                projVersion.ArchiveProjectVersion();
            }

            this.unitOfWork.Save();
        }

        private IList<string> CanCreateProjectVersionInt(int projectId)
        {
            var errors = new List<string>();

            var lastVersionStatus = this.projectVersionXmlsRepository.GetLastVersionStatus(projectId);
            if (!lastVersionStatus.HasValue)
            {
                errors.Add(ApplicationServicesTexts.ProjectVersionXmlService_CanCreate_MissingLastVersionStatus);
            }

            if (lastVersionStatus.HasValue && lastVersionStatus == Domain.Projects.ProjectVersionXmlStatus.Draft)
            {
                errors.Add(ApplicationServicesTexts.ProjectVersionXmlService_CanCreate_DuplicatedDraftStatus);
            }

            return errors;
        }

        public byte[] GetProjectAttachedDocumentsZip(int projectVersionId)
        {
            var projectVersion = this.projectVersionXmlsRepository.Find(projectVersionId);

            var attachedDocuments = projectVersion.GetDocument().AttachedDocuments.AttachedDocumentCollection
                .Where(f => f.AttachedDocumentContent != null && !string.IsNullOrEmpty(f.AttachedDocumentContent.BlobContentId));

            var files = new Dictionary<string, string>();

            var attachedFiles = new Dictionary<string, int>();

            foreach (var document in attachedDocuments)
            {
                var fileName = this.GetFileName(attachedFiles, document.AttachedDocumentContent.FileName);

                files.Add(fileName, document.AttachedDocumentContent.BlobContentId);

                if (document.SignatureContentCollection.Count > 0)
                {
                    var signatures = document.SignatureContentCollection.Where(d => !string.IsNullOrEmpty(d.BlobContentId));

                    foreach (var signature in document.SignatureContentCollection)
                    {
                        var signatureName = this.GetFileName(attachedFiles, signature.FileName);

                        files.Add(signatureName, signature.BlobContentId);
                    }
                }
            }

            var zipFile = this.ZipProjectFiles(files);

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

        private byte[] ZipProjectFiles(IDictionary<string, string> files)
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
