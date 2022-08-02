using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.OperationalMap.ProgrammePriorities.DataObjects;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.Controllers
{
    [RoutePrefix("api/programmePriorities/{programmePriorityId}/budgets")]
    public class ProgrammePriorityBudgetsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IMapNodesRepository mapNodesRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IAuthorizer authorizer;

        public ProgrammePriorityBudgetsController(
            IUnitOfWork unitOfWork,
            IMapNodesRepository mapNodesRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.mapNodesRepository = mapNodesRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProgrammePriorityBudgetsWrapperVO> GetProgrammePriorityBudgets(int programmePriorityId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmePriorityId);

            return this.programmePrioritiesRepository.GetProgrammePriorityBudgets(programmePriorityId);
        }

        [Route("{budgetPeriodId:int}")]
        public ProgrammePriorityBudgetWrapperDO GetProgrammePriorityBudget(int programmePriorityId, int budgetPeriodId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmePriorityId);

            var programmePriority = this.programmePrioritiesRepository.Find(programmePriorityId);
            var budgets = programmePriority.FindProgrammePriorityBudgetPeriod(budgetPeriodId);

            return new ProgrammePriorityBudgetWrapperDO(budgets, programmePriority.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProgrammePriorityBudgetWrapperDO NewProgrammePriorityBudget(int programmePriorityId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            var programmePriority = this.programmePrioritiesRepository.Find(programmePriorityId);

            return new ProgrammePriorityBudgetWrapperDO(programmePriorityId, programmePriority.Version);
        }

        [HttpPut]
        [Route("{budgetPeriodId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.Edit.Budgets.Edit), IdParam = "programmePriorityId", ChildIdParam = "budgetPeriodId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateProgrammePriorityBudget(int programmePriorityId, int budgetPeriodId, ProgrammePriorityBudgetWrapperDO budget)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            ProgrammePriority programmePriority = this.programmePrioritiesRepository.FindForUpdate(programmePriorityId, budget.Version);

            programmePriority.UpdateProgrammePriorityBudgetPeriod(
                budget.BudgetPeriodId.Value,
                budget.Budgets.BudgetAmount.BgAmount,
                budget.Budgets.IsActive);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.Edit.Budgets.Create), IdParam = "programmePriorityId")]
        public void CreateProgrammePriorityBudget(int programmePriorityId, ProgrammePriorityBudgetWrapperDO budget)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            ProgrammePriority programmePriority = this.programmePrioritiesRepository.FindForUpdate(programmePriorityId, budget.Version);

            programmePriority.AddProgrammePriorityBudgetPeriod(
                budget.BudgetPeriodId.Value,
                new Domain.OperationalMap.MapNodes.DataObjects.ProgrammePriorityBudgetDO
                {
                    BgAmount = budget.Budgets.BudgetAmount.BgAmount,
                    BgReservedAmount = budget.Budgets.ReservedBudgetAmount.BgAmount,
                    NextThreeWithAdvances = budget.Budgets.NextThreeAmount.AmountWithAdvances,
                    NextThreeWithoutAdvances = budget.Budgets.NextThreeAmount.AmountWithoutAdvances,
                    IsActive = budget.Budgets.IsActive,
                });

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{budgetPeriodId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.Edit.Budgets.Delete), IdParam = "programmePriorityId", ChildIdParam = "budgetPeriodId")]
        public void DetachProgrammePriorityBudget(int programmePriorityId, int budgetPeriodId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            byte[] vers = System.Convert.FromBase64String(version);
            ProgrammePriority programmePriority = this.programmePrioritiesRepository.FindForUpdate(programmePriorityId, vers);

            programmePriority.RemoveProgrammePriorityBudgetPeriod(budgetPeriodId);
            this.unitOfWork.Save();
        }
    }
}
