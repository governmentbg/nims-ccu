using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.ApplicationServices.Services.FinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.FinancialCorrections.Repositories;
using Eumis.Data.FinancialCorrections.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.FinancialCorrections.DataObjects;

namespace Eumis.Web.Api.FinancialCorrections.Controllers
{
    [RoutePrefix("api/financialCorrections/{financialCorrectionId:int}/versions")]
    public class FinancialCorrectionVersionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IFinancialCorrectionService financialCorrectionService;
        private IFinancialCorrectionVersionsRepository financialCorrectionVersionsRepository;
        private IFinancialCorrectionsRepository financialCorrectionsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IContractsRepository contractsRepository;
        private IUsersRepository usersRepository;
        private IProceduresRepository proceduresRepository;

        public FinancialCorrectionVersionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IFinancialCorrectionService financialCorrectionService,
            IFinancialCorrectionVersionsRepository financialCorrectionVersionsRepository,
            IFinancialCorrectionsRepository financialCorrectionsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IContractsRepository contractsRepository,
            IUsersRepository usersRepository,
            IProceduresRepository proceduresRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.financialCorrectionService = financialCorrectionService;
            this.financialCorrectionVersionsRepository = financialCorrectionVersionsRepository;
            this.financialCorrectionsRepository = financialCorrectionsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.contractsRepository = contractsRepository;
            this.usersRepository = usersRepository;
            this.proceduresRepository = proceduresRepository;
        }

        [Route("")]
        public IList<FinancialCorrectionVersionVO> GetFinancialCorrectionVersions(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, financialCorrectionId);

            return this.financialCorrectionVersionsRepository.GetFinancialCorrectionVersions(financialCorrectionId);
        }

        [Route("{financialCorrectionVersionId:int}")]
        public FinancialCorrectionVersionDO GetFinancialCorrectionVersion(int financialCorrectionId, int financialCorrectionVersionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, financialCorrectionId);

            var financialCorrectionVersion = this.financialCorrectionVersionsRepository.Find(financialCorrectionVersionId);

            var financialCorrection = this.financialCorrectionsRepository.Find(financialCorrectionVersion.FinancialCorrectionId);

            return new FinancialCorrectionVersionDO(financialCorrectionVersion, financialCorrection);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FinancialCorrections.Edit.Versions.Create))]
        public object CreateFinancialCorrectionVersion(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            var financialCorrectionVersion = this.financialCorrectionService.CreateFinancialCorrectionVersion(financialCorrectionId);

            return new { FinancialCorrectionVersionId = financialCorrectionVersion.FinancialCorrectionVersionId };
        }

        [HttpPut]
        [Route("{financialCorrectionVersionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FinancialCorrections.Edit.Versions.Edit), IdParam = "financialCorrectionVersionId")]
        public void UpdateFinancialCorrectionVersion(int financialCorrectionId, int financialCorrectionVersionId, FinancialCorrectionVersionDO financialCorrectionVersionDO)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            if (!this.financialCorrectionService.CanModifyFinancialCorrectionVersion(financialCorrectionVersionId))
            {
                throw new DomainValidationException("Cannot delete Financial Correction Version");
            }

            var financialCorrectionVersion = this.financialCorrectionVersionsRepository.FindForUpdate(financialCorrectionVersionId, financialCorrectionVersionDO.Version);

            financialCorrectionVersion.UpdateAttributes(
                financialCorrectionVersionDO.Percent,
                financialCorrectionVersionDO.EuAmount,
                financialCorrectionVersionDO.BgAmount,
                financialCorrectionVersionDO.BfpAmount,
                financialCorrectionVersionDO.SelfAmount,
                financialCorrectionVersionDO.TotalAmount,
                financialCorrectionVersionDO.FinancialCorrectionImposingReasonId,
                financialCorrectionVersionDO.Description,
                financialCorrectionVersionDO.ViolationFoundBy,
                financialCorrectionVersionDO.AmendmentReason,
                financialCorrectionVersionDO.CorrectionBearer,
                financialCorrectionVersionDO.File != null ? financialCorrectionVersionDO.File.Key : (Guid?)null,
                financialCorrectionVersionDO.ViolationIds);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{financialCorrectionVersionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FinancialCorrections.Edit.Versions.Delete), IdParam = "financialCorrectionVersionId")]
        public void DeleteFinancialCorrectionVersion(int financialCorrectionId, int financialCorrectionVersionId, string version)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            if (!this.financialCorrectionService.CanModifyFinancialCorrectionVersion(financialCorrectionVersionId))
            {
                throw new DomainValidationException("Cannot delete Financial Correction Version");
            }

            var financialCorrectionVersion = this.financialCorrectionVersionsRepository.FindForUpdate(financialCorrectionVersionId, vers);
            this.financialCorrectionVersionsRepository.Remove(financialCorrectionVersion);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{financialCorrectionVersionId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FinancialCorrections.Edit.Versions.ChangeStatusToActual), IdParam = "financialCorrectionVersionId")]
        public void ChangeFinancialCorrectionVersionStatusToActual(int financialCorrectionId, int financialCorrectionVersionId, string version)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.financialCorrectionService.ChangeFinancialCorrectionVersionStatusToActual(financialCorrectionVersionId, vers);
        }

        [HttpPost]
        [Route("{financialCorrectionVersionId:int}/canChangeStatusToActual")]
        public ErrorsDO CanChangeFinancialCorrectionVersionStatusToActual(int financialCorrectionId, int financialCorrectionVersionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            var version = this.financialCorrectionVersionsRepository.Find(financialCorrectionVersionId);

            return new ErrorsDO(version.CanChangeStatusToActual());
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateFinancialCorrectionVersion(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            var errors = this.financialCorrectionService.CanCreateFinancialCorrectionVersion(financialCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{financialCorrectionVersionId:int}/calculate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public FinancialCorrectionVersionDO CalculateFinancialCorrectionVersionAmounts(int financialCorrectionId, int financialCorrectionVersionId, FinancialCorrectionVersionDO financialCorrectionVersion)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            var financialCorrection = this.financialCorrectionsRepository.Find(financialCorrectionId);

            decimal euAmount = 0;
            decimal bgAmount = 0;
            decimal bfpAmount = 0;
            decimal selfAmount = 0;

            if (financialCorrection.ContractContractId.HasValue && financialCorrection.ContractBudgetLevel3AmountId.HasValue)
            {
                var contract = this.contractsRepository.Find(financialCorrection.ContractId);
                var contractContract = contract
                    .ContractContracts
                    .Where(t => t.ContractContractId == financialCorrection.ContractContractId.Value).Single();

                var budgetItemsLevel3 = contract.ContractBudgetLevel3Amounts
                    .Where(t => t.ContractBudgetLevel3AmountId == financialCorrection.ContractBudgetLevel3AmountId.Value)
                    .Single();

                euAmount = Eumis.Domain.Core.BfpCalculator.GetEuAmount(contractContract.TotalFundedValue, budgetItemsLevel3.CurrentEuAmount, budgetItemsLevel3.CurrentBgAmount);
                bgAmount = Eumis.Domain.Core.BfpCalculator.GetBgAmount(contractContract.TotalFundedValue, budgetItemsLevel3.CurrentEuAmount, budgetItemsLevel3.CurrentBgAmount);
                bfpAmount = contractContract.TotalFundedValue;
                selfAmount = 0;
            }
            else
            {
                if (financialCorrection.ContractContractId.HasValue)
                {
                    var contract = this.contractsRepository.Find(financialCorrection.ContractId);
                    var contractContract = contract
                        .ContractContracts
                        .Where(t => t.ContractContractId == financialCorrection.ContractContractId.Value).Single();

                    var procedureId = this.contractsRepository.GetProcedureId(financialCorrection.ContractId);
                    var primaryProcedureShare = this.proceduresRepository.GetProcedureShares(procedureId).Where(t => t.IsPrimary).Single();

                    euAmount = Eumis.Domain.Core.BfpCalculator.GetEuAmount(contractContract.TotalFundedValue, primaryProcedureShare.EuAmount, primaryProcedureShare.BgAmount);
                    bgAmount = Eumis.Domain.Core.BfpCalculator.GetBgAmount(contractContract.TotalFundedValue, primaryProcedureShare.EuAmount, primaryProcedureShare.BgAmount);
                    bfpAmount = contractContract.TotalFundedValue;
                    selfAmount = 0;
                }
                else
                {
                    var contractBudget = this.contractVersionsRepository.GetLastVersion(financialCorrection.ContractId)
                        .GetDocument()
                        .BFPContractDirectionsBudgetContract
                        .BFPContractBudget
                        .BFPContractProgrammeBudgetCollection;
                    foreach (var cb in contractBudget)
                    {
                        euAmount += cb.EUAmount;
                        bgAmount += cb.NationalAmount;
                        bfpAmount += cb.GrandAmount;
                        selfAmount += cb.SelfAmount;
                    }
                }
            }

            var correctedEuAmount = Eumis.Domain.Core.Calculator.RoundBy2((euAmount * financialCorrectionVersion.Percent.Value) / 100);
            var correctedBgAmount = Eumis.Domain.Core.Calculator.RoundBy2((bgAmount * financialCorrectionVersion.Percent.Value) / 100);
            var correctedBfpAmount = Eumis.Domain.Core.Calculator.RoundBy2((bfpAmount * financialCorrectionVersion.Percent.Value) / 100);
            var correctedSelfAmount = Eumis.Domain.Core.Calculator.RoundBy2((selfAmount * financialCorrectionVersion.Percent.Value) / 100);

            return new FinancialCorrectionVersionDO()
            {
                EuAmount = correctedEuAmount,
                BgAmount = correctedBgAmount,
                BfpAmount = correctedBfpAmount,
                SelfAmount = correctedSelfAmount,
                TotalAmount = correctedBfpAmount + correctedSelfAmount,
            };
        }
    }
}
