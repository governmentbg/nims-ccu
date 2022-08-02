using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Eumis.ApplicationServices.Communicators;
using Eumis.Common;
using Eumis.Common.Auth;
using Eumis.Common.Certificates;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Common.Log;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Monitorstat.Contracts;
using Eumis.Data.Monitorstat.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain;
using Eumis.Domain.Monitorstat;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Print;
using Ionic.Zip;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.Services.Monitorstat
{
    public class MonitorstatService : IMonitorstatService
    {
        private IMonitorstatRestApiCommunicator monitorstatCommunicator;
        private IMonitorstatSurveysRepository surveysRepository;
        private IMonitorstatMapNodesRepository monitorstatMapNodesRepository;
        private IProcedureMonitorstatRequestsRepository procedureMonitorstatRequestsRepository;
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IProcedureMonitorstatDocumentsRepository procedureMonitorstatDocumentsRepository;
        private IProcedureMonitorstatEconomicActivitiesRepository procedureMonitorstatEconomicActivitiesRepository;
        private IProjectsRepository projectsRepository;
        private IBlobServerCommunicator blobServerCommunicator;
        private ILogger logger;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;
        private IPrintManager printManager;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProgrammeAppFormDeclarationsRepository programmeAppFormDeclarationsRepository;
        private IProcedureAppFormDeclarationsRepository procedureAppFormDeclarationsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectFilesRepository projectFilesRepository;

        public MonitorstatService(
            IUnitOfWork unitOfWork,
            IMonitorstatRestApiCommunicator monitorstatCommunicator,
            IMonitorstatSurveysRepository surveysRepository,
            IMonitorstatMapNodesRepository monitorstatMapNodesRepository,
            IProceduresRepository proceduresRepository,
            IProcedureMonitorstatRequestsRepository procedureMonitorstatRequestsRepository,
            IProcedureMonitorstatDocumentsRepository procedureMonitorstatDocumentsRepository,
            IProcedureMonitorstatEconomicActivitiesRepository procedureMonitorstatEconomicActivitiesRepository,
            IProjectsRepository projectsRepository,
            IBlobServerCommunicator blobServerCommunicator,
            ILogger logger,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IPrintManager printManager,
            IEvalSessionsRepository evalSessionsRepository,
            IProgrammeAppFormDeclarationsRepository programmeAppFormDeclarationsRepository,
            IProcedureAppFormDeclarationsRepository procedureAppFormDeclarationsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectFilesRepository projectFilesRepository)
        {
            this.unitOfWork = unitOfWork;
            this.monitorstatCommunicator = monitorstatCommunicator;
            this.surveysRepository = surveysRepository;
            this.monitorstatMapNodesRepository = monitorstatMapNodesRepository;
            this.proceduresRepository = proceduresRepository;
            this.procedureMonitorstatRequestsRepository = procedureMonitorstatRequestsRepository;
            this.procedureMonitorstatDocumentsRepository = procedureMonitorstatDocumentsRepository;
            this.procedureMonitorstatEconomicActivitiesRepository = procedureMonitorstatEconomicActivitiesRepository;
            this.projectsRepository = projectsRepository;
            this.blobServerCommunicator = blobServerCommunicator;
            this.logger = logger;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.printManager = printManager;
            this.evalSessionsRepository = evalSessionsRepository;
            this.programmeAppFormDeclarationsRepository = programmeAppFormDeclarationsRepository;
            this.procedureAppFormDeclarationsRepository = procedureAppFormDeclarationsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectFilesRepository = projectFilesRepository;
    }

        public void LoadExternalSurveys(MonitorstatYear year)
        {
            IDictionary<string, string> surveyList;

            if (this.CanLoadExternalSurveys(year).Any())
            {
                throw new DomainValidationException($"Surveys for {(int)year} already exists");
            }

            surveyList = this.monitorstatCommunicator.GetSurveys((int)year);

            surveyList.ToList().ForEach(x =>
            {
                MonitorstatSurvey survey = new MonitorstatSurvey
                {
                    Code = x.Key,
                    Name = x.Value,
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    Year = year,
                };

                this.surveysRepository.Add(survey);

                var reports = this.monitorstatCommunicator.GetReports((int)year, survey.Code);
                reports.ToList().ForEach(t =>
                {
                    survey.Reports.Add(new MonitorstatReport { Code = t.Key, Name = t.Value });
                });
            });

            this.unitOfWork.Save();
        }

        public IList<string> CanLoadExternalSurveys(MonitorstatYear year)
        {
            var errorList = new List<string>();

            if (this.surveysRepository.GetSurveysByYear(year).Any())
            {
                errorList.Add($"Съществуват Мониторстат отчети за {year.GetEnumDescription()} година.");
            }

            return errorList;
        }

        public void SendProcedureMonitorstatRequest(int procedureId, int procedureMonitorstatRequestId, byte[] version)
        {
            var procedure = this.proceduresRepository.FindWithoutIncludes(procedureId);
            this.CreateMonitorstatProgrammeBranch(procedure);

            if (this.CanSendProcedureMonitorstatRequest(procedureMonitorstatRequestId).Any())
            {
                throw new DomainValidationException("Errors occurred before sending request");
            }

            var request = this.procedureMonitorstatRequestsRepository.FindForUpdate(procedureMonitorstatRequestId, version);

            var inquiryRequest = new ProcedureInquiryDO(request);
            inquiryRequest.ProcedureCode = procedure.Code;
            inquiryRequest.FinishDate = this.proceduresRepository.GetProcedureCurrentEndDate(procedureId);

            inquiryRequest.Reports = this.procedureMonitorstatDocumentsRepository.GetProcedureInquiryReports(procedureId);
            inquiryRequest.Activities = this.procedureMonitorstatEconomicActivitiesRepository.GetProcedureInquiryActivities(procedureId);

            request.MonitorstatInquiryGid = this.monitorstatCommunicator.CreateProcedureInquiryRequest(inquiryRequest);
            request.ModifyDate = DateTime.Now;
            request.Status = ProcedureMonitorstatRequestStatus.Sent;

            this.unitOfWork.Save();
        }

        private void CreateMonitorstatProgrammeBranch(Procedure procedure)
        {
            var procedureParentData = this.proceduresRepository.GetProcedureParentData(procedure.ProcedureId);
            var currentDate = DateTime.Now;
            var programmeGid = this.monitorstatMapNodesRepository.GetOperationalProgrammeGid(procedureParentData.ProgrammeId);

            if (programmeGid == null || programmeGid == Guid.Empty)
            {
                var programme = this.monitorstatMapNodesRepository.GetProgrammeRequest(procedureParentData.ProgrammeId);
                programmeGid = this.monitorstatCommunicator.CreateOperationalProgramme(programme);
                var programmeEntity = new MonitorstatMapNode
                {
                    MapNodeId = procedureParentData.ProgrammeId,
                    CreateDate = currentDate,
                    ModifyDate = currentDate,
                    MonitorstatGid = programmeGid,
                    Type = MonitorstatMapNodeType.Programme,
                };

                this.monitorstatMapNodesRepository.Add(programmeEntity);
                this.unitOfWork.Save();
            }

            if (!this.monitorstatMapNodesRepository.MapNodeHasMapping(procedureParentData.ProgrammePriorityId, MonitorstatMapNodeType.ProgrammePriority))
            {
                var programmePriority = this.monitorstatMapNodesRepository.GetProgrammePriorityRequest(
                    procedureParentData.ProgrammePriorityId,
                    programmeGid);

                Guid gid = this.monitorstatCommunicator.CreateProgrammePriority(programmePriority);
                this.monitorstatMapNodesRepository.Add(new MonitorstatMapNode
                {
                    MapNodeId = procedureParentData.ProgrammePriorityId,
                    CreateDate = currentDate,
                    ModifyDate = currentDate,
                    MonitorstatGid = gid,
                    Type = MonitorstatMapNodeType.ProgrammePriority,
                });
            }

            if (!this.monitorstatMapNodesRepository.MapNodeHasMapping(procedure.ProcedureId, MonitorstatMapNodeType.Procedure))
            {
                var procedureDO = this.monitorstatMapNodesRepository.GetProcedureRequest(procedure.ProcedureId, procedureParentData.ProgrammePriorityId, procedure.ActivationDate);
                Guid gid = this.monitorstatCommunicator.CreateProcedure(procedureDO);
                this.monitorstatMapNodesRepository.Add(new MonitorstatMapNode
                {
                    MapNodeId = procedure.ProcedureId,
                    CreateDate = currentDate,
                    ModifyDate = currentDate,
                    MonitorstatGid = gid,
                    Type = MonitorstatMapNodeType.Procedure,
                });

                this.unitOfWork.Save();
            }
        }

        public IList<string> CanSendProcedureMonitorstatRequest(int procedureMonitorstatRequestId)
        {
            var errors = new List<string>();

            var request = this.procedureMonitorstatRequestsRepository.Find(procedureMonitorstatRequestId);

            if (request.Status != ProcedureMonitorstatRequestStatus.Draft)
            {
                errors.Add("Не можете да изпратите заявка, чийто статус е различен от 'Чернова'");
            }

            if (!request.ExecutionStartDate.HasValue)
            {
                errors.Add("Не е попълнена началната дата за изпълнение на заявката");
            }

            if (!request.ExecutionEndDate.HasValue)
            {
                errors.Add("Не е попълнена крайната дата за изпълнение на заявката");
            }

            if (request.ExecutionStartDate.HasValue && request.ExecutionEndDate.HasValue &&
                request.ExecutionEndDate.Value < request.ExecutionStartDate.Value)
            {
                errors.Add("Началната дата не може да бъде по-голяма от крайната дата");
            }

            return errors;
        }

        public void SendProjectMonitorstatRequest(int projectId, int projectMonitorstatRequestId, byte[] version, int userId)
        {
            if (this.CanSendProjectMonitorstatRequest(projectId, projectMonitorstatRequestId).Any())
            {
                throw new DomainValidationException("Cannot send request");
            }

            var project = this.projectsRepository.FindForUpdate(projectId, version);
            var projectRequest = project.MonitorstatRequests.Single(x => x.ProjectMonitorstatRequestId == projectMonitorstatRequestId);

            var procedureInquiry = this.procedureMonitorstatRequestsRepository.FindWithoutIncludes(projectRequest.ProcedureMonitorstatRequestId);

            var declaration = this.GetProjectMonitorstatRequestDeclaration(projectRequest);

            var zipFileContent = this.ZipContent(declaration.filename, declaration.content);

            projectRequest.AssertSendIsAllowed();

            var request = new SubjectRequestDO()
            {
                ProcedureInquiryGid = procedureInquiry.MonitorstatInquiryGid.Value,
                Uin = projectRequest.CompanyUin,
                UinType = projectRequest.CompanyUinType ?? UinType.Eik,
                File = new FileDO()
                {
                    Content = zipFileContent,
                    Name = "Declaration.zip",
                    Size = zipFileContent.Length,
                },
            };

            try
            {
                projectRequest.ForeignGid = this.monitorstatCommunicator.CreateSubjectRequest(request);
                projectRequest.Status = ProjectMonitorstatRequestStatus.Sent;
            }
            catch (Exception e)
            {
                this.logger.Log(LogLevel.Error, "Error in monitorstat communication", e);
                projectRequest.Status = ProjectMonitorstatRequestStatus.Failed;
            }

            projectRequest.ModifyDate = DateTime.Now;
            projectRequest.UserId = userId;
        }

        public IList<string> CanSendProjectMonitorstatRequest(int projectId, int projectMonitorstatRequestId)
        {
            List<string> errors = new List<string>();

            var project = this.projectsRepository.Find(projectId);
            var projectRequest = project.MonitorstatRequests.Single(x => x.ProjectMonitorstatRequestId == projectMonitorstatRequestId);

            var allowableStatuses = new ProjectMonitorstatRequestStatus[]
            {
                ProjectMonitorstatRequestStatus.Draft,
                ProjectMonitorstatRequestStatus.Failed,
            };

            if (!allowableStatuses.Contains(projectRequest.Status))
            {
                errors.Add("Статусът на заявката към Мониторстат трябва да бъде 'Чернова' или 'Грешка при изпращане'");
            }

            return errors;
        }

        public IList<string> ReceiveMonitorstatFile(string procedureCode, string uin, UinType uinType, string fileName, Guid fileKey, Guid? subjectRequestGuid)
        {
            List<string> errors = new List<string>();

            if (subjectRequestGuid.HasValue)
            {
                var monitorstatRequest = this.projectsRepository.GetMonitorstatRequest(subjectRequestGuid.Value);
                if (monitorstatRequest is null)
                {
                    errors = this.ReceiveFileViaBeneficaryInfo(procedureCode, uin, uinType, fileName, fileKey).ToList();
                    if (errors.Count > 0)
                    {
                        errors.Add($"Neither request with guid: {subjectRequestGuid.Value}");
                        return errors;
                    }
                }
                else
                {
                    monitorstatRequest.AddMonitorstatResponse(fileName, fileKey);
                }

                return errors;
            }
            else
            {
                return this.ReceiveFileViaBeneficaryInfo(procedureCode, uin, uinType, fileName, fileKey);
            }
        }

        public IList<string> SendAutomaticProjectMonitorstatRequests(
            IList<int> projectIds,
            int evalSessionId,
            int procedureMonitorstatRequestId,
            int? procedureApplicationDocId,
            int? programmeDeclarationId)
        {
            var errors = new List<string>();
            var sentProjectMonitorstatRequests = new List<string>();
            string currentProjectRegNumber = null;

            try
            {
                var procedureMonitorstatRequest = this.procedureMonitorstatRequestsRepository.FindWithoutIncludes(procedureMonitorstatRequestId);
                var procedureId = this.evalSessionsRepository.GetEvalSessionProcedure(evalSessionId).ProcedureId;

                ProcedureApplicationDoc procedureApplicationDoc = null;
                if (procedureApplicationDocId.HasValue)
                {
                    procedureApplicationDoc = this.proceduresRepository.FindProcedureAppDoc(procedureApplicationDocId.Value);
                }

                Domain.OperationalMap.ProgrammeDeclarations.ProgrammeDeclaration programmeDeclaration = null;
                if (programmeDeclarationId.HasValue)
                {
                    programmeDeclaration = this.programmeAppFormDeclarationsRepository.FindProgrammeDeclaration(programmeDeclarationId.Value);
                }

                if (procedureId != procedureMonitorstatRequest.ProcedureId)
                {
                    throw new InvalidOperationException("Cannot send automatic project monitorstat requests");
                }

                if (!programmeDeclarationId.HasValue && !procedureApplicationDocId.HasValue)
                {
                    throw new InvalidOperationException("Cannot send automatic project monitorstat requests");
                }

                if (programmeDeclaration != null && !programmeDeclaration.IsConsentForNSIDataProviding)
                {
                    throw new InvalidOperationException("Declararation must be consent for NSI data providing");
                }

                foreach (var projectId in projectIds)
                {
                    var project = this.projectsRepository.FindWithoutIncludes(projectId);
                    currentProjectRegNumber = project.RegNumber;

                    var projectVersionXmlId = this.projectVersionXmlsRepository.GetActualProjectVersionId(project.ProjectId);
                    if (!projectVersionXmlId.HasValue)
                    {
                        errors.Add(string.Format(ApplicationServicesTexts.MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectHasLastNonActualVersion, project.RegNumber));
                        continue;
                    }

                    int? projectVersionXmlFileId = null;
                    if (procedureApplicationDoc != null)
                    {
                        projectVersionXmlFileId = this.projectVersionXmlsRepository.GetProjectVersionXmlFileId(projectVersionXmlId.Value, procedureApplicationDoc.Gid);
                    }

                    Guid? procedureDeclarationGid = null;
                    if (programmeDeclaration != null)
                    {
                        procedureDeclarationGid = this.procedureAppFormDeclarationsRepository.GetProcedureDeclarationGid(programmeDeclaration.ProgrammeDeclarationId, project.ProcedureId);
                    }

                    Guid? projectDeclarationGid = null;
                    if (programmeDeclaration != null && procedureDeclarationGid.HasValue)
                    {
                        projectDeclarationGid = this.projectVersionXmlsRepository.GetProjectVersionDeclarationGid(projectVersionXmlId.Value, procedureDeclarationGid.Value);
                    }

                    var sendErrors = this.CanSendAutomaticProjectMonitorstatRequest(
                        project,
                        programmeDeclarationId,
                        procedureApplicationDocId,
                        procedureApplicationDoc,
                        programmeDeclaration,
                        projectVersionXmlFileId,
                        projectDeclarationGid);

                    if (sendErrors.Count > 0)
                    {
                        errors.AddRange(sendErrors);
                    }
                    else
                    {
                        this.SendAutomaticProjectMonitorstatRequest(
                            project,
                            procedureMonitorstatRequestId,
                            programmeDeclarationId,
                            projectVersionXmlId,
                            projectVersionXmlFileId,
                            projectDeclarationGid);

                        sentProjectMonitorstatRequests.Add(project.RegNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.Log(LogLevel.Error, ex.Message, ex);

                errors.Add(string.Format(ApplicationServicesTexts.MonitorstatService_AutomaticProjectMonitorstatRequest_UnexpectedError, currentProjectRegNumber));
            }

            if (errors.Count > 0 && sentProjectMonitorstatRequests.Count > 0)
            {
                errors.Add(string.Format(
                    ApplicationServicesTexts.MonitorstatService_AutomaticProjectMonitorstatRequest_MonitorstatRequestsSent,
                    $"{Environment.NewLine}    -",
                    string.Join($",{Environment.NewLine}    -", sentProjectMonitorstatRequests)));
            }

            return errors;
        }

        public ProjectMonitorstatRequest CreateProjectMonitorstatRequest(
            int projectId,
            byte[] version,
            int procedureMonitorstatRequestId,
            int projectVersionXmlId,
            int? programmeDeclarationId,
            int? projectVersionXmlFileId)
        {
            var project = this.projectsRepository.FindForUpdate(projectId, version);

            Guid? declarationKey = null;

            if (programmeDeclarationId.HasValue)
            {
                var pdf = this.GenerateDeclarationFile(project, programmeDeclarationId.Value);

                declarationKey = this.UploadDeclarationFile(pdf, project.RegNumber);
            }

            var projectMonitorstatRequest = project.CreateMonitorstatRequest(
                procedureMonitorstatRequestId,
                projectVersionXmlId,
                projectVersionXmlFileId,
                programmeDeclarationId,
                declarationKey,
                project.CompanyUin,
                project.CompanyUinType);

            this.unitOfWork.Save();

            return projectMonitorstatRequest;
        }

        public void UpdateProjectMonitorstatRequest(
            int projectId,
            byte[] version,
            int projectMonitorstatRequestId,
            int procedureMonitorstatRequestId,
            int? projectVersionXmlFileId,
            int? programmeDeclarationId)
        {
            var project = this.projectsRepository.FindForUpdate(projectId, version);
            var projectMonitorstatRequest = project.MonitorstatRequests.Single(r => r.ProjectMonitorstatRequestId == projectMonitorstatRequestId);

            Guid? declarationKey;

            if (programmeDeclarationId.HasValue && projectMonitorstatRequest.ProgrammeDeclarationId != programmeDeclarationId)
            {
                var pdf = this.GenerateDeclarationFile(project, programmeDeclarationId.Value);

                declarationKey = this.UploadDeclarationFile(pdf, project.RegNumber);
            }
            else
            {
                declarationKey = projectMonitorstatRequest.DeclarationBlobKey;
            }

            project.UpdateMonitorstatRequest(
                projectMonitorstatRequestId,
                procedureMonitorstatRequestId,
                projectVersionXmlFileId,
                programmeDeclarationId,
                declarationKey);

            this.unitOfWork.Save();
        }

        private IList<string> ReceiveFileViaBeneficaryInfo(string procedureCode, string uin, UinType uinType, string fileName, Guid fileKey)
        {
            List<string> errors = new List<string>();

            var procedureId = this.proceduresRepository.FindProcedureIdByCode(procedureCode);

            var monitorstatRequests = this.projectsRepository.GetMonitorstatRequests(procedureId, uin);

            if (monitorstatRequests == null || monitorstatRequests.Count == 0)
            {
                errors.Add($"Could not find any ProjectMonitorstatRequests with {uinType}: {uin} for procedure with code: {procedureCode}");
                return errors;
            }

            foreach (var monitorstatRequest in monitorstatRequests)
            {
                monitorstatRequest.AddMonitorstatResponse(fileName, fileKey);
            }

            return errors;
        }

        private byte[] GetFileContent(Guid blobKey)
        {
            using (var blobStream = this.blobServerCommunicator.GetBlobStream(blobKey, true))
            {
                return this.StreamToByteArray(blobStream);
            }
        }

        private byte[] ZipContent(string fileName, byte[] blobContent)
        {
            byte[] zipContent = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (ZipFile zipFile = new ZipFile())
                {
                    zipFile.Encryption = EncryptionAlgorithm.None;

                    zipFile.AddEntry(fileName, blobContent);

                    zipFile.Save(memoryStream);
                }

                zipContent = memoryStream.ToArray();
                return zipContent;
            }
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

        private string GetNsiCompatibleFileName(string realFileName, string newFileName)
        {
            if (realFileName.ContainsNonASCIICharacter())
            {
                var fileExtension = realFileName.Split('.').LastOrDefault();

                if (!string.IsNullOrEmpty(fileExtension))
                {
                    return $"{newFileName}.{fileExtension}";
                }

                throw new DomainValidationException("Unknown declaration file extension");
            }

            return realFileName;
        }

        private IList<string> CanSendAutomaticProjectMonitorstatRequest(
            Project project,
            int? programmeDeclarationId,
            int? procedureApplicationDocId,
            ProcedureApplicationDoc procedureApplicationDoc,
            Domain.OperationalMap.ProgrammeDeclarations.ProgrammeDeclaration programmeDeclaration,
            int? projectVersionXmlFileId,
            Guid? projectDeclarationGid)
        {
            var errors = new List<string>();

            if (procedureApplicationDocId.HasValue && !programmeDeclarationId.HasValue)
            {
                if (!projectVersionXmlFileId.HasValue)
                {
                    errors.Add(string.Format(
                        ApplicationServicesTexts.MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectDoesNotContainDocument,
                        project.RegNumber,
                        procedureApplicationDoc.Name));

                    return errors;
                }
            }
            else if (!procedureApplicationDocId.HasValue && programmeDeclarationId.HasValue)
            {
                if (!projectDeclarationGid.HasValue)
                {
                    errors.Add(string.Format(
                        ApplicationServicesTexts.MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectDoesNotContainDeclaration,
                        project.RegNumber,
                        programmeDeclaration.Name));

                    return errors;
                }
            }
            else if (procedureApplicationDocId.HasValue && programmeDeclarationId.HasValue)
            {
                if (!projectVersionXmlFileId.HasValue && !projectDeclarationGid.HasValue)
                {
                    errors.Add(string.Format(
                        ApplicationServicesTexts.MonitorstatService_AutomaticProjectMonitorstatRequest_ProjectDoesNotContainDocument,
                        project.RegNumber,
                        procedureApplicationDoc.Name));

                    return errors;
                }
            }

            return errors;
        }

        private void SendAutomaticProjectMonitorstatRequest(
            Project project,
            int procedureMonitorstatRequestId,
            int? programmeDeclarationId,
            int? projectVersionXmlId,
            int? projectVersionXmlFileId,
            Guid? projectDeclarationGid)
        {
            var projectMonitorstatRequest = this.CreateProjectMonitorstatRequest(
                project.ProjectId,
                project.Version,
                procedureMonitorstatRequestId,
                projectVersionXmlId.Value,
                projectDeclarationGid.HasValue ? programmeDeclarationId : null,
                projectVersionXmlFileId);

            this.SendProjectMonitorstatRequest(
                project.ProjectId,
                projectMonitorstatRequest.ProjectMonitorstatRequestId,
                project.Version,
                this.accessContext.UserId);

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Projects.MonitorstatRequests.SendAutomaticRequest),
                project.ProjectId,
                projectMonitorstatRequest.ProjectMonitorstatRequestId,
                null,
                null);

            this.unitOfWork.Save();
        }

        private byte[] GenerateDeclarationFile(Project project, int programmeDeclarationId)
        {
            var declaration = this.programmeAppFormDeclarationsRepository.GetProgrammeDeclaration(programmeDeclarationId);

            if (!declaration.IsConsentForNSIDataProviding)
            {
                throw new Exception("Invalid declaration");
            }

            var signatures = this.projectFilesRepository.GetFirstProjectFileSignatures(project.ProjectId);
            var x509Certificates = new List<X509Certificate2>();

            foreach (var encodedMessage in signatures)
            {
                x509Certificates.AddRange(CertificateManager.GetSignerCertificates(encodedMessage));
            }

            var contextData = new
            {
                project.RegDate,
                CompanyName = SystemLocalization.GetDisplayName(project.CompanyName, project.CompanyNameAlt),
                project.CompanyUin,
                Consent = SystemLocalization.GetDisplayName(declaration.Content, declaration.ContentAlt),
                Signatures = x509Certificates.Select(s => new
                {
                    s.SerialNumber,
                    EffectiveDate = s.GetEffectiveDateString(),
                    ExpirationDate = s.GetExpirationDateString(),
                    s.Issuer,
                    s.Subject,
                }).ToArray(),
            };

            JObject context = JObject.FromObject(contextData);
            var pdf = this.printManager.Print(TemplateType.MonitorstatRequestDeclaration, PrintType.PDF, context);

            return pdf;
        }

        private Guid UploadDeclarationFile(byte[] pdf, string regNumber)
        {
            var time = DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat.SortableDateTimePattern).Replace("T", " ");
            var filename = $"{regNumber} {time}.pdf";

            using (var stream = new MemoryStream(pdf))
            {
                var info = this.blobServerCommunicator.UploadBlob(filename, stream);
                return info.FileKey;
            }
        }

        private (string filename, byte[] content) GetProjectMonitorstatRequestDeclaration(ProjectMonitorstatRequest projectMonitorstatRequest)
        {
            Guid blobKey;
            string filename;

            if (projectMonitorstatRequest.ProjectVersionXmlFileId.HasValue)
            {
                var projectVersionXmlFile = this.projectsRepository.GetProjectVersionXmlFile(projectMonitorstatRequest.ProjectId, projectMonitorstatRequest.ProjectVersionXmlFileId.Value);
                blobKey = projectVersionXmlFile.BlobKey;
                filename = projectVersionXmlFile.Name;
            }
            else
            {
                blobKey = projectMonitorstatRequest.DeclarationFile.Key;
                filename = projectMonitorstatRequest.DeclarationFile.FileName;
            }

            var content = this.GetFileContent(blobKey);
            var compatibleFilename = this.GetNsiCompatibleFileName(filename, projectMonitorstatRequest.CompanyUin);

            return (compatibleFilename, content);
        }
    }
}
