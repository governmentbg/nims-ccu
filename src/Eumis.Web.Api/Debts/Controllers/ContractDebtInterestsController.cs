using Eumis.ApplicationServices.Services.ContractDebt;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Allowances.Repositories;
using Eumis.Data.BasicInterestRates.Repositories;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.InterestSchemes.Repositories;
using Eumis.Data.ReimbursedAmounts.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Debts.DataObjects;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/contractDebts/{contractDebtId:int}/interests")]
    public class ContractDebtInterestsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractDebtService contractDebtService;
        private IContractDebtsRepository contractDebtsRepository;
        private IContractDebtVersionsRepository contractDebtVersionsRepository;
        private IDebtReimbursedAmountsRepository reimbursedAmountsRepository;
        private IInterestSchemesRepository interestSchemesRepository;
        private IAllowancesRepository allowancesRepository;
        private IBasicInterestRatesRepository basicInterestRatesRepository;
        private IUsersRepository usersRepository;

        public ContractDebtInterestsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractDebtService contractDebtService,
            IContractDebtsRepository contractDebtsRepository,
            IContractDebtVersionsRepository contractDebtVersionsRepository,
            IDebtReimbursedAmountsRepository reimbursedAmountsRepository,
            IInterestSchemesRepository interestSchemesRepository,
            IAllowancesRepository allowancesRepository,
            IBasicInterestRatesRepository basicInterestRatesRepository,
            IUsersRepository usersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractDebtService = contractDebtService;
            this.contractDebtsRepository = contractDebtsRepository;
            this.contractDebtVersionsRepository = contractDebtVersionsRepository;
            this.reimbursedAmountsRepository = reimbursedAmountsRepository;
            this.interestSchemesRepository = interestSchemesRepository;
            this.allowancesRepository = allowancesRepository;
            this.basicInterestRatesRepository = basicInterestRatesRepository;
            this.usersRepository = usersRepository;
        }

        [Route("")]
        public IList<ContractDebtInterestVO> GetContractDebtInterests(int contractDebtId)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.View, contractDebtId);

            return this.contractDebtsRepository.GetContractDebtInterests(contractDebtId);
        }

        [Route("{contractDebtInterestId:int}")]
        public ContractDebtInterestDO GetContractDebtInterest(int contractDebtId, int contractDebtInterestId)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.View, contractDebtId);

            var contractDebt = this.contractDebtsRepository.Find(contractDebtId);
            var contractDebtInterest = contractDebt.ContractDebtInterests.Where(t => t.ContractDebtInterestId == contractDebtInterestId).Single();

            bool isLast = contractDebt.ContractDebtInterests.OrderByDescending(t => t.OrderNum).First().OrderNum == contractDebtInterest.OrderNum;

            return new ContractDebtInterestDO(contractDebtInterest, contractDebt.Version, isLast);
        }

        [HttpGet]
        [Route("new")]
        public ContractDebtInterestDO NewContractDebtInterest(int contractDebtId)
        {
            this.authorizer.AssertCanDo(ContractDebtListActions.Create);

            var contractDebt = this.contractDebtsRepository.Find(contractDebtId);

            DateTime dateFrom;
            if (!contractDebt.ContractDebtInterests.Any())
            {
                dateFrom = contractDebt.InterestStartDate;
            }
            else
            {
                dateFrom = contractDebt.ContractDebtInterests.OrderBy(t => t.OrderNum).Last().DateTo.AddDays(1);
            }

            return new ContractDebtInterestDO()
            {
                ContractDebtId = contractDebtId,
                DateFrom = dateFrom,
                Version = contractDebt.Version,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Interests.Create))]
        public object CreateContractDebtInterest(int contractDebtId, ContractDebtInterestDO contractDebtInterest)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            var newContractDebtInterest = this.contractDebtService.CreateContractDebtInterest(
                contractDebtId,
                contractDebtInterest.Version,
                contractDebtInterest.InterestSchemeId.Value,
                contractDebtInterest.DateTo.Value,
                contractDebtInterest.EuInterestAmount.Value,
                contractDebtInterest.BgInterestAmount.Value,
                contractDebtInterest.TotalInterestAmount.Value,
                contractDebtInterest.EuAmount.Value,
                contractDebtInterest.BgAmount.Value,
                contractDebtInterest.TotalAmount.Value);

            return new { ContractDebtInterestId = newContractDebtInterest.ContractDebtInterestId };
        }

        [HttpPut]
        [Route("{contractDebtInterestId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Interests.Edit), IdParam = "contractDebtId")]
        public void UpdateContractDebtInterest(int contractDebtId, int contractDebtInterestId, ContractDebtInterestDO contractDebtInterest)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            this.contractDebtService.UpdateContractDebtInterest(
                contractDebtId,
                contractDebtInterest.Version,
                contractDebtInterestId,
                contractDebtInterest.InterestSchemeId.Value,
                contractDebtInterest.DateTo.Value,
                contractDebtInterest.EuInterestAmount.Value,
                contractDebtInterest.BgInterestAmount.Value,
                contractDebtInterest.TotalInterestAmount.Value,
                contractDebtInterest.EuAmount.Value,
                contractDebtInterest.BgAmount.Value,
                contractDebtInterest.TotalAmount.Value);
        }

        [HttpDelete]
        [Route("{contractDebtInterestId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Interests.Delete), IdParam = "contractDebtId")]
        public void DeleteContractDebtInterest(int contractDebtId, int contractDebtInterestId, string version)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractDebtService.DeleteContractDebtInterest(contractDebtId, vers, contractDebtInterestId);
        }

        [HttpPost]
        [Route("calculate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Interests.Delete), IdParam = "contractDebtId")]
        public ContractDebtInterestDO CalculateContractDebtInterestRates(int contractDebtId, ContractDebtInterestDO contractDebtInterest)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);
            var result = new ContractDebtInterestDO();

            if (!contractDebtInterest.DateTo.HasValue || !contractDebtInterest.InterestSchemeId.HasValue)
            {
                result.Error = "Полетата 'Дата до' и 'Схема на олихвяване' трябва да са попълнени";
                return result;
            }

            if (contractDebtInterest.DateFrom == default(DateTime))
            {
                throw new Exception("ContractDebtInterest property DateFrom cannot be the default DateTime value");
            }

            if (contractDebtInterest.DateFrom > contractDebtInterest.DateTo)
            {
                result.Error = "Стойността на полето 'Дата до' трябва да е по-голяма или равна на стойността на полето 'Дата от'";
                return result;
            }

            var contractDebt = this.contractDebtsRepository.Find(contractDebtId);
            var actualContractDebtVersion = this.contractDebtVersionsRepository.GetActualVersion(contractDebtId);
            var reimbursedAmounts = this.reimbursedAmountsRepository.FindAllEnteredForDebt(contractDebt.ContractDebtId).Where(t => t.ReimbursementDate < contractDebtInterest.DateFrom);

            result.EuAmount = actualContractDebtVersion.EuAmount;
            result.BgAmount = actualContractDebtVersion.BgAmount;
            result.TotalAmount = actualContractDebtVersion.TotalAmount;

            foreach (var reimbursedAmount in reimbursedAmounts)
            {
                result.EuAmount = result.EuAmount - (reimbursedAmount.PrincipalBfp.EuAmount ?? 0);
                result.BgAmount = result.BgAmount - (reimbursedAmount.PrincipalBfp.BgAmount ?? 0);
                result.TotalAmount = result.TotalAmount - (reimbursedAmount.PrincipalBfp.TotalAmount ?? 0);
            }

            var interestScheme = this.interestSchemesRepository.Find(contractDebtInterest.InterestSchemeId.Value);
            var allowance = this.allowancesRepository.Find(interestScheme.AllowanceId);
            var basicInterestRate = this.basicInterestRatesRepository.Find(interestScheme.BasicInterestRateId);

            var fromAllowanceRate = allowance.Rates.Where(t => t.Date <= contractDebtInterest.DateFrom).OrderBy(t => t.Date).LastOrDefault();

            if (fromAllowanceRate == null)
            {
                result.Error = "Не съществува процент с дата по-малка от стойността на полето 'Дата от' в надбавката на избраната схема за олихвяване";
                return result;
            }

            var nextToFromAllowanceRate = allowance.Rates.OrderBy(t => t.Date).SkipWhile(t => t.AllowanceRateId != fromAllowanceRate.AllowanceRateId).Skip(1).FirstOrDefault();
            var toAllowanceRate = allowance.Rates.Where(t => t.Date > contractDebtInterest.DateTo).OrderBy(t => t.Date).FirstOrDefault();

            if ((nextToFromAllowanceRate != null) && nextToFromAllowanceRate != toAllowanceRate)
            {
                result.Error = "Не съществува процент, който може да обхване периода ['Дата от', 'Дата до'] в надбавката на избраната схема за олихвяване";
                return result;
            }

            var fromInterestRate = basicInterestRate.Rates.Where(t => t.Date <= contractDebtInterest.DateFrom).OrderBy(t => t.Date).LastOrDefault();

            if (fromInterestRate == null)
            {
                result.Error = "Не съществува процент с дата по-малка от стойността на полето 'Дата от' в основния лихвен процент на избраната схема за олихвяване";
                return result;
            }

            var nextToFromInterestRate = basicInterestRate.Rates.OrderBy(t => t.Date).SkipWhile(t => t.InterestRateId != fromInterestRate.InterestRateId).Skip(1).FirstOrDefault();
            var toInterestRate = basicInterestRate.Rates.Where(t => t.Date > contractDebtInterest.DateTo).OrderBy(t => t.Date).FirstOrDefault();

            if ((nextToFromInterestRate != null) && nextToFromInterestRate != toInterestRate)
            {
                result.Error = "Не съществува процент, който може да обхване периода ['Дата от', 'Дата до'] в основния лихвен процент на избраната схема за олихвяване";
                return result;
            }

            var rate = (fromAllowanceRate.Rate + fromInterestRate.Rate) / interestScheme.AnnualBasis;
            var period = (contractDebtInterest.DateTo.Value - contractDebtInterest.DateFrom).Days + 1;

            rate = Math.Round(rate, 5);

            result.EuInterestAmount = Eumis.Domain.Core.Calculator.RoundBy2(((result.EuAmount.Value * rate) / 100) * period);
            result.BgInterestAmount = Eumis.Domain.Core.Calculator.RoundBy2(((result.BgAmount.Value * rate) / 100) * period);
            result.TotalInterestAmount = Eumis.Domain.Core.Calculator.RoundBy2(((result.TotalAmount.Value * rate) / 100) * period);

            return result;
        }
    }
}
