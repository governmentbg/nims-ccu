using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.EvalSessionAutomaticProjectEvaluation.Parsers;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Localization;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Projects;
using Eumis.Domain.Projects.DataObjects;
using Eumis.Domain.RioExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Eumis.ApplicationServices.Services.EvalSessionAutomaticProjectEvaluation
{
    internal class EvalSessionAutomaticProjectEvaluationService : IEvalSessionAutomaticProjectEvaluationService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IBlobServerCommunicator blobServerCommunicator;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IEvalSessionsAutomaticProjectEvaluationParser evalSessionsAutomaticProjectEvaluationParser;

        public EvalSessionAutomaticProjectEvaluationService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IEvalSessionsRepository evalSessionsRepository,
            IProjectsRepository projectsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IBlobServerCommunicator blobServerCommunicator,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IEvalSessionsAutomaticProjectEvaluationParser evalSessionsAutomaticProjectEvaluationParser)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectsRepository = projectsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.blobServerCommunicator = blobServerCommunicator;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.evalSessionsAutomaticProjectEvaluationParser = evalSessionsAutomaticProjectEvaluationParser;
        }

        public IList<string> CanExecuteEvalSessionAutomaticProjectEvaluation(int evalSessionId)
        {
            var errors = new List<string>();

            var evalSession = this.evalSessionsRepository.FindWithoutIncludes(evalSessionId);

            if (evalSession.EvalSessionStatus != EvalSessionStatus.Active)
            {
                errors.Add(ApplicationServicesTexts.EvalSessionService_CanCreate_SessionActive);
            }

            return errors;
        }

        public IList<string> ExecuteEvalSessionAutomaticProjectEvaluation(
            int evalSessionId,
            byte[] version,
            Guid blobKey)
        {
            var errors = new List<string>();

            var projectRows = this.ParseExcel(blobKey, errors);

            if (errors.Count > 0)
            {
                return errors;
            }

            var evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, version);

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                foreach (var row in projectRows)
                {
                    var projectRegNumber = row.ProjectRegNumber;
                    var amount = row.Amount;

                    var project = this.projectsRepository.FindByRegNumber(projectRegNumber);
                    if (project == null)
                    {
                        errors.Add(string.Format(ApplicationServicesTexts.EvalSessionService_CanCreateAutomaticProjectVersion_RegNumberNotValid, projectRegNumber));
                        continue;
                    }

                    Domain.Projects.ProjectVersionXml actualProjectVersion;
                    if (amount.HasValue)
                    {
                        var automaticProjectVersionErrors = this.CanCreateAutomaticProjectVersion(
                            evalSession,
                            project);

                        if (automaticProjectVersionErrors.Count > 0)
                        {
                            errors.AddRange(automaticProjectVersionErrors);
                            continue;
                        }

                        actualProjectVersion = this.CreateAutomaticProjectVersion(
                            project,
                            amount);
                    }
                    else
                    {
                        throw new InvalidOperationException("Cannot create automatic ProjectVersionXml.");
                    }
                }

                if (errors.Count == 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }

            return errors;
        }

        #region Private

        #region Excel

        private IList<AutomaticProjectVersionXmlDO> ParseExcel(
            Guid blobKey,
            List<string> errors)
        {
            IList<AutomaticProjectVersionXmlDO> projects = new List<AutomaticProjectVersionXmlDO>();

            try
            {
                using (var excelStream = this.blobServerCommunicator.GetBlobStream(blobKey, true))
                {
                    projects = this.evalSessionsAutomaticProjectEvaluationParser.ParseExcel(
                        excelStream,
                        errors);
                }
            }
            catch (FileFormatException)
            {
                errors.Add(ApplicationServicesTexts.Common_InvalidFileFormat);
            }

            return projects;
        }

        #endregion Excel

        #region ProjectVersionXml

        private IList<string> CanCreateAutomaticProjectVersion(
            Domain.EvalSessions.EvalSession evalSession,
            Project project)
        {
            var errors = new List<string>();

            if (!evalSession.EvalSessionProjects.Where(esp => esp.ProjectId == project.ProjectId).Any())
            {
                errors.Add(string.Format(
                    ApplicationServicesTexts.EvalSessionService_CanCreateAutomaticProjectVersion_NotIncluded,
                    project.RegNumber));
            }

            var hasCommunicationInProgress = this.projectCommunicationsRepository.HasProjectCommunicationInProgress(evalSession.EvalSessionId, project.ProjectId);
            if (hasCommunicationInProgress)
            {
                errors.Add(string.Format(
                    ApplicationServicesTexts.EvalSessionService_CanCreateAutomaticProjectVersion_CommunicationInProgress,
                    project.RegNumber));
            }

            var lastVersionStatus = this.projectVersionXmlsRepository.GetLastVersionStatus(project.ProjectId);
            if (!lastVersionStatus.HasValue)
            {
                errors.Add(string.Format(
                    ApplicationServicesTexts.EvalSessionService_CanCreateAutomaticProjectVersion_MissingLastVersionStatus,
                    project.RegNumber));
            }

            if (lastVersionStatus.HasValue && lastVersionStatus == Domain.Projects.ProjectVersionXmlStatus.Draft)
            {
                errors.Add(string.Format(
                    ApplicationServicesTexts.EvalSessionService_CanCreateAutomaticProjectVersion_DuplicatedDraftStatus,
                    project.RegNumber));
            }

            if (project.RegistrationStatus == ProjectRegistrationStatus.Withdrawn)
            {
                errors.Add(string.Format(ApplicationServicesTexts.EvalSessionService_CanCreateAutomaticProjectVersion_ProjectIsWithdrawn, project.RegNumber));
            }

            if (evalSession.FindEvalSessionProject(project.ProjectId).IsDeleted)
            {
                errors.Add(string.Format(ApplicationServicesTexts.EvalSessionService_CanCreateAutomaticProjectVersion_ProjectIsCanceled, project.RegNumber));
            }

            return errors;
        }

        private Domain.Projects.ProjectVersionXml CreateAutomaticProjectVersion(
            Project project,
            decimal? amount)
        {
            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(project.ProjectId);
            var orderNum = this.projectVersionXmlsRepository.GetNextOrderNum(project.ProjectId);

            var newProjectVersion = new Domain.Projects.ProjectVersionXml(
                actualProjectVersion,
                this.accessContext.UserId,
                string.Format(
                    ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.EvalSessionService_CreateAutomaticProjectVersion_Note), new CultureInfo(SystemLocalization.Bg_BG)),
                    this.accessContext.Username,
                    DateTime.Now),
                string.Format(
                    ApplicationServicesTexts.ResourceManager.GetString(nameof(ApplicationServicesTexts.EvalSessionService_CreateAutomaticProjectVersion_Note), new CultureInfo(SystemLocalization.En_GB)),
                    this.accessContext.Username,
                    DateTime.Now),
                orderNum);

            newProjectVersion.Status = ProjectVersionXmlStatus.Actual;

            var newProjectVersionDoc = newProjectVersion.GetDocument();

            if (amount.HasValue)
            {
                newProjectVersion.SetLastBudgetRowAmounts(newProjectVersionDoc, amount.Value);

                var budget = newProjectVersionDoc.GetBudget();

                var totalBfpAmount = budget.Select(b => b.GrandAmount).Aggregate(0M, (a, b) => a + b);
                var coFinancingAmount = budget.Select(b => b.SelfAmount).Aggregate(0M, (a, b) => a + b);

                newProjectVersion.TotalBfpAmount = totalBfpAmount;
                newProjectVersion.CoFinancingAmount = coFinancingAmount;

                project.TotalBfpAmount = totalBfpAmount;
                project.CoFinancingAmount = coFinancingAmount;
                project.ModifyDate = DateTime.Now;
            }

            this.projectVersionXmlsRepository.Add(newProjectVersion);
            this.unitOfWork.Save();

            this.ArchivePreviousProjectVersions(project.ProjectId, newProjectVersion.ProjectVersionXmlId);

            return newProjectVersion;
        }

        private void ArchivePreviousProjectVersions(int projectId, int projectVersionXmlId)
        {
            var versionsToArchive = this.projectVersionXmlsRepository.GetNonArchivedProjectVersions(projectId)
                .Where(p => p.ProjectVersionXmlId != projectVersionXmlId);

            foreach (var projVersion in versionsToArchive)
            {
                projVersion.ArchiveProjectVersion();
            }

            this.unitOfWork.Save();
        }

        #endregion ProjectVersionXml

        #region EvalSessionEvaluation

        private bool IsInterruptedSheetChain(EvalSessionSheet currentSheet, List<EvalSessionSheet> sheets)
        {
            if (currentSheet.Status == EvalSessionSheetStatus.Paused && !currentSheet.ContinuedEvalSessionSheetId.HasValue)
            {
                return true;
            }

            if (currentSheet.Status == EvalSessionSheetStatus.Paused)
            {
                var nextSheet = sheets.Where(x => x.EvalSessionSheetId == currentSheet.ContinuedEvalSessionSheetId.Value).FirstOrDefault();
                if (nextSheet == null)
                {
                    return true;
                }

                return this.IsInterruptedSheetChain(nextSheet, sheets);
            }

            return false;
        }

        #endregion EvalSessionEvaluation

        #endregion Private
    }
}
