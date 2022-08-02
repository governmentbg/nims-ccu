using System;
using System.Linq;
using System.Web.Http;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.BfpCalculator.DataObjects;

namespace Eumis.Web.Api.BfpCalculator.Controllers
{
    [RoutePrefix("api/bfpCalculator")]
    public class BfpCalculatorController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IContractsRepository contractsRepository;
        private IProceduresRepository proceduresRepository;
        private IProgrammesRepository programmesRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IContractVersionsRepository contractVersionsRepository;

        public BfpCalculatorController(
            IUnitOfWork unitOfWork,
            IContractsRepository contractsRepository,
            IProceduresRepository proceduresRepository,
            IProgrammesRepository programmesRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IContractVersionsRepository contractVersionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contractsRepository = contractsRepository;
            this.proceduresRepository = proceduresRepository;
            this.programmesRepository = programmesRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.contractVersionsRepository = contractVersionsRepository;
        }

        [HttpPost]
        [Route("calculate")]
        public BfpCalculationResultDO CalculateBfpByContractBudgetLevel3Amount(int contractId, int contractBudgetLevel3AmountId, BfpCalculationInputDO input)
        {
            var contract = this.contractsRepository.Find(contractId);
            var budgetLevel3Amount = contract.ContractBudgetLevel3Amounts
                .Single(t => t.ContractBudgetLevel3AmountId == contractBudgetLevel3AmountId);

            return this.GetCalculatedBfp(budgetLevel3Amount.CurrentEuAmount, budgetLevel3Amount.CurrentBgAmount, input.BfpTotalAmount);
        }

        [HttpPost]
        [Route("calculate")]
        public BfpCalculationResultDO CalculateBfpByContract(int contractId, BfpCalculationInputDO input)
        {
            var procedureId = this.contractsRepository.GetProcedureId(contractId);
            var primaryProcedureShare = this.proceduresRepository.GetProcedureShares(procedureId).Where(t => t.IsPrimary).Single();

            return this.GetCalculatedBfp(primaryProcedureShare.EuAmount, primaryProcedureShare.BgAmount, input.BfpTotalAmount);
        }

        [HttpPost]
        [Route("calculate")]
        public BfpCalculationResultDO CalculateBfpByProgramme(int programmeId, BfpCalculationInputDO input)
        {
            var programmeBudgets = this.programmesRepository.GetProgrammeBudgets(programmeId);

            decimal currentEuAmount = 0;
            decimal currentBgAmount = 0;

            foreach (var pb in programmeBudgets)
            {
                currentEuAmount += pb.Budgets.EuAmount ?? 0;
                currentBgAmount += pb.Budgets.BgAmount ?? 0;
            }

            return this.GetCalculatedBfp(currentEuAmount, currentBgAmount, input.BfpTotalAmount);
        }

        [HttpPost]
        [Route("calculate")]
        public BfpCalculationResultDO CalculateBfpByProgrammePriority(int programmePriorityId, BfpCalculationInputDO input)
        {
            var programmePriorityBudgets = this.programmePrioritiesRepository.GetProgrammePriorityBudgets(programmePriorityId);

            decimal currentEuAmount = 0;
            decimal currentBgAmount = 0;

            foreach (var pb in programmePriorityBudgets)
            {
                currentEuAmount += pb.Budgets.EuAmount ?? 0;
                currentBgAmount += pb.Budgets.BgAmount ?? 0;
            }

            return this.GetCalculatedBfp(currentEuAmount, currentBgAmount, input.BfpTotalAmount);
        }

        [HttpPost]
        [Route("calculate")]
        public BfpCalculationResultDO CalculateBfpByProcedure(int procedureId, BfpCalculationInputDO input)
        {
            var procedureShares = this.proceduresRepository.GetProcedureShares(procedureId);

            decimal currentEuAmount = 0;
            decimal currentBgAmount = 0;

            foreach (var ps in procedureShares)
            {
                currentEuAmount += ps.EuAmount;
                currentBgAmount += ps.BgAmount;
            }

            return this.GetCalculatedBfp(currentEuAmount, currentBgAmount, input.BfpTotalAmount);
        }

        [HttpPost]
        [Route("calculate")]
        public BfpCalculationResultDO CalculateBfpByProcedure(int procedureId, int programmePriorityId, BfpCalculationInputDO input)
        {
            var procedureShares = this.proceduresRepository.GetProcedureShares(procedureId);
            var procedureShare = new ProcedureSharesVO();

            procedureShare = procedureShares.Where(t => t.ProgrammePriorityId == programmePriorityId).SingleOrDefault();

            if (procedureShare == null)
            {
                return new BfpCalculationResultDO();
            }

            return this.GetCalculatedBfp(procedureShare.EuAmount, procedureShare.BgAmount, input.BfpTotalAmount);
        }

        [HttpPost]
        [Route("calculate")]
        public BfpCalculationResultDO CalculateBfpByContractProcedure(int contractId, int programmePriorityId, BfpCalculationInputDO input)
        {
            var contract = this.contractsRepository.FindWithoutIncludes(contractId);
            var procedureShare = this.proceduresRepository.GetProcedureShares(contract.ProcedureId)
                .Where(t => t.ProgrammePriorityId == programmePriorityId)
                .Single();

            return this.GetCalculatedBfp(procedureShare.EuAmount, procedureShare.BgAmount, input.BfpTotalAmount);
        }

        private BfpCalculationResultDO GetCalculatedBfp(decimal currentEuAmount, decimal currentBgAmount, decimal bfpTotalAmount)
        {
            decimal bgAmount = Eumis.Domain.Core.BfpCalculator.GetBgAmount(bfpTotalAmount, currentEuAmount, currentBgAmount);
            decimal euAmount = Eumis.Domain.Core.BfpCalculator.GetEuAmount(bfpTotalAmount, currentEuAmount, currentBgAmount);

            return new BfpCalculationResultDO(euAmount, bgAmount);
        }
    }
}
