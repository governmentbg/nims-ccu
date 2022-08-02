using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.ApplicationServices.Services.Irregularity;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Irregularities;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Irregularities.DataObjects;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/irregularities/{irregularityId:int}/versions")]
    public class IrregularityVersionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IIrregularityVersionsRepository irregularityVersionsRepository;
        private IIrregularityVersionService irregularityVersionService;
        private IContractVersionsRepository contractVersionsRepository;

        public IrregularityVersionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IIrregularityVersionsRepository irregularityVersionsRepository,
            IIrregularityVersionService irregularityVersionService,
            IContractVersionsRepository contractVersionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.irregularityVersionsRepository = irregularityVersionsRepository;
            this.irregularityVersionService = irregularityVersionService;
            this.contractVersionsRepository = contractVersionsRepository;
        }

        [Route("")]
        public IList<IrregularityVersionVO> GetVersions(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.View, irregularityId);

            return this.irregularityVersionsRepository.GetVersions(irregularityId);
        }

        [Route("{versionId:int}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public IrregularityVersionDO GetVersion(int irregularityId, int versionId)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.View, versionId);

            var version = this.irregularityVersionsRepository.Find(versionId);

            return new IrregularityVersionDO(version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Create), IdParam = "irregularityId")]
        public object AddVersion(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            if (this.irregularityVersionService.CanCreateVersion(irregularityId).Any())
            {
                throw new InvalidOperationException("Cannot create new version");
            }

            var activeVersion = this.irregularityVersionsRepository.GetActiveVersion(irregularityId);
            var newVersion = new IrregularityVersion(activeVersion);
            this.irregularityVersionsRepository.Add(newVersion);

            this.unitOfWork.Save();

            return new { VersionId = newVersion.IrregularityVersionId };
        }

        [HttpPut]
        [Route("{versionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Edit.EditData), IdParam = "irregularityId", ChildIdParam = "versionId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateVersion(int irregularityId, int versionId, IrregularityVersionDO version)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            if (!this.irregularityVersionService.CanEditVersion(versionId))
            {
                throw new InvalidOperationException("Cannot update version.");
            }

            var irrVersion = this.irregularityVersionsRepository.FindForUpdate(versionId, version.Version);

            irrVersion.UpdateVersionData(
                version.IrregularityDateFrom.Value, // basic data
                version.IrregularityDateTo,
                version.IrregularityClassification,
                version.IrregularityCategoryId,
                version.IrregularityTypeId,
                version.EndingActRegNum,
                version.EndingActDate,
                version.CaseState,
                version.IrregularityEndDate,
                //// report data
                version.CreateDate,
                version.ReportYear.Value,
                version.ReportQuarter.Value,
                version.Rapporteur,
                version.RapporteurComments,
                //// impaired regulations
                version.ImpairedRegulationAct,
                version.ImpairedRegulationNum,
                version.ImpairedRegulationYear,
                version.ImpairedRegulation,
                version.ImpairedNationalRegulation,
                //// euPercent
                version.EUCoFinancingPercent,
                //// expenses
                version.ExpensesBfpEuAmountLv,
                version.ExpensesBfpBgAmountLv,
                version.ExpensesSelfAmountLv,
                version.ExpensesBfpEuAmountEuro,
                version.ExpensesBfpBgAmountEuro,
                version.ExpensesSelfAmountEuro,
                //// irregular expenses
                version.IrregularExpensesBfpEuAmountLv,
                version.IrregularExpensesBfpBgAmountLv,
                version.IrregularExpensesBfpEuAmountEuro,
                version.IrregularExpensesBfpBgAmountEuro,
                //// certified expenses
                version.CertifiedExpensesBfpEuAmountLv,
                version.CertifiedExpensesBfpBgAmountLv,
                version.CertifiedExpensesBfpEuAmountEuro,
                version.CertifiedExpensesBfpBgAmountEuro,
                //// paid
                version.PaidBfpEuAmountLv,
                version.PaidBfpBgAmountLv,
                version.PaidBfpEuAmountEuro,
                version.PaidBfpBgAmountEuro,
                //// decertification
                version.ShouldDecertifyIrregularExpenses,
                version.DecertificationComments,
                //// sanctions
                version.SanctionProcedureType.Value,
                version.SanctionProcedureKind,
                version.SanctionProcedureStartDate,
                version.SanctionProcedureExpectedEndDate,
                version.SanctionProcedureEndDate,
                version.SanctionProcedureStatus,
                version.SanctionCategoryId,
                version.SanctionTypeId,
                version.Fines,
                //// general data
                version.ContractDebtStatus,
                version.IsNewUnlawfulPractice,
                version.ShouldInformOther,
                version.ProcedureStatus,
                version.FinancialStatusId,
                version.AppliedPractices,
                version.BeneficiaryData,
                version.AdminAscertainments,
                version.IrregularityDetectedBy,
                version.AdminProcedures,
                version.PenaltyProcedures,
                version.ShouldReportToOlaf.Value,
                version.ReasonNotReportingToOlaf,
                version.CheckTime);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canCreate")]
        [Transaction]
        public ErrorsDO CanCreateVersion(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            var errors = this.irregularityVersionService.CanCreateVersion(irregularityId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{versionId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Activate), IdParam = "irregularityId", ChildIdParam = "versionId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void ActivateVersion(int irregularityId, int versionId, string version)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            this.irregularityVersionService.ActivateVersion(versionId, System.Convert.FromBase64String(version));
        }

        [HttpDelete]
        [Route("{versionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Versions.Delete), IdParam = "irregularityId", ChildIdParam = "versionId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteVersion(int irregularityId, int versionId, string version)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            if (!this.irregularityVersionService.CanEditVersion(versionId))
            {
                throw new InvalidOperationException("Cannot delete version.");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            var irrVersion = this.irregularityVersionsRepository.FindForUpdate(versionId, vers);
            this.irregularityVersionsRepository.Remove(irrVersion);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("{versionId:int}/canCalculateExpenses")]
        public ErrorsDO CanCalculateExpenses(int versionId)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);
            var contractId = this.irregularityVersionsRepository.GetContractId(versionId);
            var errors = new ErrorsDO();

            if (!contractId.HasValue)
            {
                errors.AddError("Не е възможно за нередности без договор");
            }

            return errors;
        }

        [HttpGet]
        [Route("{versionId:int}/calculateExpenses")]
        public IrregularityExpensesDO CalculateExpenses(int versionId)
        {
            this.authorizer.AssertCanDo(IrregularityVersionActions.Edit, versionId);

            if (this.CanCalculateExpenses(versionId).Errors.Any())
            {
                throw new InvalidOperationException("Cannot calculate expenses");
            }

            var contractId = this.irregularityVersionsRepository.GetContractId(versionId);
            var contractBudget = this.contractVersionsRepository.GetLastVersion(contractId.Value)
                .GetDocument()
                .BFPContractDirectionsBudgetContract
                .BFPContractBudget
                .BFPContractProgrammeBudgetCollection;

            var expenses = new IrregularityExpensesDO();
            foreach (var cb in contractBudget)
            {
                expenses.EuAmount += cb.EUAmount;
                expenses.BgAmount += cb.NationalAmount;
                expenses.SelfAmount += cb.SelfAmount;
            }

            return expenses;
        }
    }
}
