using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core.Relations;
using Eumis.Data.Counters;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Projects.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/standings")]
    public class EvalSessionStandingsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private ICountersRepository countersRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProceduresRepository proceduresRepository;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IRelationsRepository relationsRepository;

        public EvalSessionStandingsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            ICountersRepository countersRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProceduresRepository proceduresRepository,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.countersRepository = countersRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.proceduresRepository = proceduresRepository;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<EvalSessionStandingsVO> GetEvalSessionStandings(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionStandings(evalSessionId);
        }

        [Route("{standingId:int}")]
        public EvalSessionStandingDO GetEvalSessionStanding(int evalSessionId, int standingId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);
            var procedure = this.proceduresRepository.FindWithoutIncludes(evalSession.ProcedureId);

            var evalSessionStanding = evalSession.FindEvalSessionStanding(standingId);

            var projects = this.evalSessionsRepository.GetEvalSessionStandingProjects(evalSessionId, standingId);

            bool canRearrange = this.evalSessionsRepository.CanRearrangeStanding(evalSessionId, standingId);

            return new EvalSessionStandingDO(evalSessionStanding, projects, evalSession.Version, canRearrange);
        }

        [HttpGet]
        [Route("new")]
        public NewEvalSessionStandingDO NewEvalSessionStanding(int evalSessionId, NewEvalSessionStandingType type)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var projects = this.evalSessionsRepository.GetNewEvalSessionStandingProjects(evalSessionId);

            return new NewEvalSessionStandingDO(evalSessionId, type, projects, evalSession.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Standings.Create), IdParam = "evalSessionId")]
        public EvalSessionStandingDO AddEvalSessionStanding(int evalSessionId, NewEvalSessionStandingDO newEvalSessionStandingDO)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            this.countersRepository.CreateEvalSessionStandingCounter(evalSessionId);

            EvalSessionStanding newEvalSessionStanding;

            var regNumber = this.countersRepository.GetNextEvalSessionStandingNumber(evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, newEvalSessionStandingDO.Version);

            var procedureOldProjects = this.evalSessionsRepository.GetProcedurePreviousEvalSessionStandingProjects(evalSession.ProcedureId, evalSessionId);
            var procedureSpentAmounts = this.projectVersionXmlsRepository.GetProgrammeBudgetSpentAmount(procedureOldProjects);

            var projects = this.evalSessionsRepository.GetNewEvalSessionStandingProjects(evalSessionId);
            var projectsIds = projects.Select(t => t.ProjectId).ToArray();

            var noProjectVersion = this.projectVersionXmlsRepository.HasProjectWithoutActualProjectVersion(projectsIds);
            var hasProjectCommunicationInProgress = this.projectCommunicationsRepository.HasProjectCommunicationInProgress(evalSessionId);

            Procedure procedure = this.proceduresRepository.FindWithoutIncludes(evalSession.ProcedureId);

            Dictionary<int, Dictionary<string, decimal>> projectsAmounts = new Dictionary<int, Dictionary<string, decimal>>();
            Dictionary<int, int> projectsVersions = new Dictionary<int, int>();
            Dictionary<int, bool> projectsHaveCommunicationInProgress = new Dictionary<int, bool>();

            if (!hasProjectCommunicationInProgress && !noProjectVersion)
            {
                var programmeBudgetGrandAmounts = this.projectVersionXmlsRepository.GetProgrammeBudgetGrandAmountForActualProjectVersions(projectsIds);

                foreach (var projectId in projectsIds)
                {
                    projectsHaveCommunicationInProgress.Add(projectId, false);
                    var programmeBudgetGrandAmount = programmeBudgetGrandAmounts.Where(t => t.ProjectId == projectId);

                    var grandAmounts = programmeBudgetGrandAmount
                        .GroupBy(g => g.ProgrammePriorityCode)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(t => t.GrandAmount).Aggregate(0M, (a, b) => a + b));

                    projectsVersions.Add(projectId, programmeBudgetGrandAmount.First().ProjectVersionXmlId);
                    projectsAmounts.Add(projectId, grandAmounts);
                }
            }

            var hasPreliminaryEvalTable = this.proceduresRepository.HasPreliminaryEvalTable(evalSession.ProcedureId);

            newEvalSessionStanding = evalSession.AddEvalSessionStanding(
                regNumber,
                newEvalSessionStandingDO.Type,
                newEvalSessionStandingDO.PreliminaryBudgetPercentage,
                projects,
                hasProjectCommunicationInProgress,
                noProjectVersion,
                hasPreliminaryEvalTable);

            this.unitOfWork.Save();

            decimal budgetCoef = (newEvalSessionStandingDO.PreliminaryBudgetPercentage ?? 100m) / 100;
            var procedureAmounts = this.proceduresRepository.GetProcedureShares(evalSession.ProcedureId)
                .GroupBy(g => g.ProgrammePriorityCode)
                .ToDictionary(
                    g => g.Key,
                    g =>
                        decimal.Round(
                            g.Select(p => p.EuAmount + p.BgAmount).Aggregate(0M, (a, b) => a + b) * budgetCoef,
                            2));

            Func<Dictionary<string, decimal>,
                 IList<ProcedureGrandAmountsVO>,
                 Dictionary<string, decimal>> reduceProcedureAmounts = (pa, sa) =>
                 {
                     foreach (var spentAmount in sa)
                     {
                         var key = spentAmount.ProgrammePriorityCode;
                         pa[key] = pa[key] - spentAmount.GrandAmount;
                     }

                     return pa;
                 };

            procedureAmounts = reduceProcedureAmounts(procedureAmounts, procedureSpentAmounts);

            evalSession.GenerateProjectStandings(
                newEvalSessionStanding,
                newEvalSessionStandingDO.Type,
                projectsHaveCommunicationInProgress,
                projectsVersions,
                projects,
                projectsAmounts,
                procedureAmounts,
                hasPreliminaryEvalTable);

            this.unitOfWork.Save();

            return new EvalSessionStandingDO(newEvalSessionStanding.EvalSessionStandingId);
        }

        [HttpPost]
        [Route("{standingId:int}/canRefuse")]
        public ErrorsDO CanRefuseEvalSessionStanding(int evalSessionId, int standingId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            this.relationsRepository.AssertЕvalSessionHasStanding(evalSessionId, standingId);

            IList<string> errorList = new List<string>();

            var projectRegNumbers = this.evalSessionsRepository.GetEvalSessionProjectsWithContracts(standingId);

            if (projectRegNumbers.Any())
            {
                errorList.Add($"За следните ПП вече има създадени договори:\r\n\t{string.Join(";\r\n\t", projectRegNumbers.ToArray())}");
            }

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{standingId:int}/refuse")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Standings.Refuse), IdParam = "evalSessionId", ChildIdParam = "standingId")]
        public void RefuseEvalSessionStanding(int evalSessionId, int standingId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.RefuseEvalSessionStanding(standingId, confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateEvalSessionStanding(int evalSessionId, NewEvalSessionStandingType type)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.GetWithIncludedProjectStandingsAndEvaluations(evalSessionId);

            var availableEvalSessionActions = this.evalSessionsRepository.GetEvalSessionAvailableActions(evalSession.ProcedureId, evalSessionId);

            var procedure = this.proceduresRepository.FindWithoutIncludes(evalSession.ProcedureId);

            var projects = this.evalSessionsRepository.GetNewEvalSessionStandingProjects(evalSessionId);
            var projectsIds = projects.Select(t => t.ProjectId).ToArray();

            var hasProjectCommunicationInProgress = this.projectCommunicationsRepository.HasProjectCommunicationInProgress(evalSessionId);
            var noProjectVersion = this.projectVersionXmlsRepository.HasProjectWithoutActualProjectVersion(projectsIds);
            var hasPreliminaryEvalTable = this.proceduresRepository.HasPreliminaryEvalTable(evalSession.ProcedureId);
            var errorList = evalSession.CanCreateEvalSessionStanding(projects, type, hasProjectCommunicationInProgress, noProjectVersion, hasPreliminaryEvalTable);

            return new ErrorsDO(errorList);
        }

        [Route("{standingId:int}/rearrange")]
        public EvalSessionStandingDO GetRearrangeableEvalSessionStandingProjects(int evalSessionId, int standingId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);
            var procedure = this.proceduresRepository.FindWithoutIncludes(evalSession.ProcedureId);

            var evalSessionStanding = evalSession.FindEvalSessionStanding(standingId);

            var projects = this.evalSessionsRepository.GetEvalSessionRearrangedStandingProjects(evalSessionId, standingId);
            var standingType = this.evalSessionsRepository.GetEvalSessionStandingType(standingId);

            projects.DeterminateMoveableProjects(standingType);

            return new EvalSessionStandingDO(evalSessionStanding, projects, evalSession.Version, true);
        }

        [HttpPost]
        [Route("{standingId:int}/rearrange/{projectId:int}/moveUp")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Standings.Rearrange.MoveUp), IdParam = "standingId", ChildIdParam = "projectId")]
        public void MoveUpEvalSessionStandingProject(int evalSessionId, int standingId, int projectId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);
            var standing = evalSession.FindEvalSessionStanding(standingId);

            var projects = this.evalSessionsRepository.GetEvalSessionRearrangedStandingProjects(evalSessionId, standingId);
            var standingType = this.evalSessionsRepository.GetEvalSessionStandingType(standingId);

            projects.DeterminateMoveableProjects(standingType);

            var currentProject = projects.Where(x => x.ProjectId == projectId).Single();

            if (!currentProject.CanMoveUp)
            {
                throw new DomainException("Operation is not supported");
            }

            var previousProject = projects.Where(x => x.ManualOrderNum == currentProject.ManualOrderNum - 1).Single();

            if (!previousProject.CanMoveDown)
            {
                throw new DomainException("Operation is not supported");
            }

            var procedureBudgetAmount = this.GetProcedureBudgetLeft(evalSession.ProcedureId, evalSessionId, standing.PreliminaryBudgetPercentage);
            var projectsAmounts = this.GetProjectsAmounts(projects.Select(x => x.ProjectId).Distinct().ToArray());

            evalSession.SwitchEvalSessionStandingProjectsOrder(standingId, currentProject.ProjectId, previousProject.ProjectId, procedureBudgetAmount, projectsAmounts);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{standingId:int}/rearrange/{projectId:int}/moveDown")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Standings.Rearrange.MoveUp), IdParam = "standingId", ChildIdParam = "projectId")]
        public void MoveDownEvalSessionStandingProject(int evalSessionId, int standingId, int projectId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);
            var standing = evalSession.FindEvalSessionStanding(standingId);

            var projects = this.evalSessionsRepository.GetEvalSessionRearrangedStandingProjects(evalSessionId, standingId);
            var standingType = this.evalSessionsRepository.GetEvalSessionStandingType(standingId);

            projects.DeterminateMoveableProjects(standingType);

            var currentProject = projects.Where(x => x.ProjectId == projectId).Single();

            if (!currentProject.CanMoveDown)
            {
                throw new DomainException("Operation is not supported");
            }

            var nextProject = projects.Where(x => x.ManualOrderNum == currentProject.ManualOrderNum + 1).Single();

            if (!nextProject.CanMoveUp)
            {
                throw new DomainException("Operation is not supported");
            }

            var procedureBudgetAmount = this.GetProcedureBudgetLeft(evalSession.ProcedureId, evalSessionId, standing.PreliminaryBudgetPercentage);
            var projectsAmounts = this.GetProjectsAmounts(projects.Select(x => x.ProjectId).Distinct().ToArray());

            evalSession.SwitchEvalSessionStandingProjectsOrder(standingId, currentProject.ProjectId, nextProject.ProjectId, procedureBudgetAmount, projectsAmounts);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{standingId:int}/rearrange/canApply")]
        public ErrorsDO CanApplyEvalSessionStandingRearrange(int evalSessionId, int standingId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);
            var standing = evalSession.FindEvalSessionStanding(standingId);
            var projects = this.evalSessionsRepository.GetEvalSessionRearrangedStandingProjects(evalSessionId, standingId);

            var procedureBudgetLeftAmounts = this.GetProcedureBudgetLeft(evalSession.ProcedureId, evalSession.EvalSessionId, standing.PreliminaryBudgetPercentage);
            var projectsBudgetsAmounts = this.GetProjectsAmounts(projects.Select(x => x.ProjectId).Distinct().ToArray());

            var errors = evalSession.CanApplyArrangedEvalSessionStanding(standingId, procedureBudgetLeftAmounts, projectsBudgetsAmounts);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{standingId:int}/rearrange/apply")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Standings.Rearrange.Applied), IdParam = "evalSessionId", ChildIdParam = "standingId")]
        public void ApplyEvalSessionStandingRearrange(int evalSessionId, int standingId, string version)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            var projects = evalSession.ApplyArrangedEvalSessionStanding(standingId);

            this.unitOfWork.BulkUpdate<EvalSessionProjectStanding>(projects, t => t.OrderNum, t => t.Status);

            this.unitOfWork.Save();
        }

        private Dictionary<string, decimal> GetProcedureBudgetLeft(int procedureId, int evalSessionId, int? preliminaryBudgetCoef)
        {
            var procedureOldProjects = this.evalSessionsRepository.GetProcedurePreviousEvalSessionStandingProjects(procedureId, evalSessionId);
            var procedureSpentAmounts = this.projectVersionXmlsRepository.GetProgrammeBudgetSpentAmount(procedureOldProjects);

            decimal budgetCoef = (preliminaryBudgetCoef ?? 100m) / 100;
            var procedureAmounts = this.proceduresRepository.GetProcedureShares(procedureId)
                .GroupBy(g => g.ProgrammePriorityCode)
                .ToDictionary(
                    g => g.Key,
                    g =>
                        decimal.Round(
                            g.Select(p => p.EuAmount + p.BgAmount).Aggregate(0M, (a, b) => a + b) * budgetCoef,
                            2));

            Func<Dictionary<string, decimal>,
                 IList<ProcedureGrandAmountsVO>,
                 Dictionary<string, decimal>> reduceProcedureAmounts = (pa, sa) =>
                 {
                     foreach (var spentAmount in sa)
                     {
                         var key = spentAmount.ProgrammePriorityCode;
                         pa[key] = pa[key] - spentAmount.GrandAmount;
                     }

                     return pa;
                 };

            return reduceProcedureAmounts(procedureAmounts, procedureSpentAmounts);
        }

        private Dictionary<int, Dictionary<string, decimal>> GetProjectsAmounts(int[] projectsIds)
        {
            Dictionary<int, Dictionary<string, decimal>> projectsAmounts = new Dictionary<int, Dictionary<string, decimal>>();

            var programmeBudgetGrandAmounts = this.projectVersionXmlsRepository.GetProgrammeBudgetGrandAmountForActualProjectVersions(projectsIds);

            foreach (var projectId in projectsIds)
            {
                var programmeBudgetGrandAmount = programmeBudgetGrandAmounts.Where(t => t.ProjectId == projectId);

                var grandAmounts = programmeBudgetGrandAmount
                    .GroupBy(g => g.ProgrammePriorityCode)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(t => t.GrandAmount).Aggregate(0M, (a, b) => a + b));

                projectsAmounts.Add(projectId, grandAmounts);
            }

            return projectsAmounts;
        }
    }
}
