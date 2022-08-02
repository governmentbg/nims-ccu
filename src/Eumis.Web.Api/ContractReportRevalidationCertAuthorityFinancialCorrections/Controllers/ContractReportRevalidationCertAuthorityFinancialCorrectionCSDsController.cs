using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportRevalidationCertAuthorityFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
using Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/contractReportRevalidationCertAuthorityFinancialCorrections/{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/certAuthorityFinancialCorrectionCSDs")]
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IUsersRepository usersRepository;
        private IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository;
        private IContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository;
        private IContractReportRevalidationCertAuthorityFinancialCorrectionService contractReportRevalidationCertAuthorityFinancialCorrectionService;
        private ICertReportCheckService certReportCheckService;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;

        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository,
            IContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository,
            IContractReportRevalidationCertAuthorityFinancialCorrectionService contractReportRevalidationCertAuthorityFinancialCorrectionService,
            ICertReportCheckService certReportCheckService,
            IRelationsRepository relationsRepository,
            IAnnualAccountReportsRepository annualAccountReportsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.usersRepository = usersRepository;
            this.contractReportFinancialCSDsRepository = contractReportFinancialCSDsRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.contractReportFinancialRevalidationCSDsRepository = contractReportFinancialRevalidationCSDsRepository;
            this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository = contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository;
            this.contractReportRevalidationCertAuthorityFinancialCorrectionService = contractReportRevalidationCertAuthorityFinancialCorrectionService;
            this.certReportCheckService = certReportCheckService;
            this.relationsRepository = relationsRepository;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
        }

        [Route("")]
        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> GetContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(int contractReportRevalidationCertAuthorityFinancialCorrectionId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.View, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            return this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.GetContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(contractReportRevalidationCertAuthorityFinancialCorrectionId, csd, company);
        }

        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionCSDId:int}")]
        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDDO GetContractReportFinancialCSD(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.View, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var revalidationCertAuthorityFinancialCorrectionCSD = this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.Find(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.GetContractReportFinancialRevalidationCSDByBudgetItem(revalidationCertAuthorityFinancialCorrectionCSD.ContractReportId, revalidationCertAuthorityFinancialCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialRevalidationCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string financialRevalidationCSDCheckedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;

            if (financialRevalidationCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.FindWithoutIncludes(financialRevalidationCSD.CheckedByUserId.Value);
                financialRevalidationCSDCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.FindWithoutIncludes(financialCSDBudgetItem.CheckedByUserId.Value);
                budgetItemCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.FindWithoutIncludes(financialCSDBudgetItem.TechCheckedByUserId.Value);
                budgetItemTechCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportRevalidationCertAuthorityFinancialCorrectionCSDDO(
                revalidationCertAuthorityFinancialCorrectionCSD,
                financialRevalidationCSD,
                financialRevalidationCSDCheckedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.Edit.CSD.Create), IdParam = "contractReportRevalidationCertAuthorityFinancialCorrectionId")]
        public void CreateContractReportRevalidationCertAuthorityFinancialCorrectionCSD(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionService.CreateContractReportRevalidationCertAuthorityFinancialCorrectionCSD(contractReportRevalidationCertAuthorityFinancialCorrectionId, contractReportFinancialCSDBudgetItemId);
        }

        [HttpPut]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.Edit.CSD.Update), IdParam = "contractReportRevalidationCertAuthorityFinancialCorrectionCSDId")]
        public void UpdateContractReportFinancialCSD(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, ContractReportRevalidationCertAuthorityFinancialCorrectionCSDDO contractReportRevalidationCertAuthorityFinancialCorrectionCSD)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionService.UpdateContractReportRevalidationCertAuthorityFinancialCorrectionCSD(
                contractReportRevalidationCertAuthorityFinancialCorrectionCSDId,
                contractReportRevalidationCertAuthorityFinancialCorrectionCSD.Version,
                contractReportRevalidationCertAuthorityFinancialCorrectionCSD.Sign,
                contractReportRevalidationCertAuthorityFinancialCorrectionCSD.Notes,
                contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedEuAmount,
                contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedBgAmount,
                contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedBfpTotalAmount,
                contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedSelfAmount,
                contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedTotalAmount);
        }

        [HttpDelete]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.Edit.CSD.Delete), IdParam = "contractReportRevalidationCertAuthorityFinancialCorrectionCSDId")]
        public void DeleteContractReportRevalidationCertAuthorityFinancialCorrectionCSD(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionService.DeleteContractReportRevalidationCertAuthorityFinancialCorrectionCSD(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, vers);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionCSDId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.Edit.CSD.ChangeStatusToEnded), IdParam = "contractReportRevalidationCertAuthorityFinancialCorrectionCSDId")]
        public void ChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatusToEnded(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionService.ChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, vers, ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionCSDId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatusToEnded(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            var errors = this.contractReportRevalidationCertAuthorityFinancialCorrectionService.CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatusToEnded(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportRevalidationCertAuthorityFinancialCorrectionCSDId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityFinancialCorrection.Edit.CSD.ChangeStatusToDraft), IdParam = "contractReportRevalidationCertAuthorityFinancialCorrectionCSDId")]
        public void ChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatusToDraft(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.Edit, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionService.ChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, vers, ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Draft);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityRevalidationFinancialCorrections/{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/attachedFinancialCorrectionCSDs")]
        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> GetContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(int annualAccountReportId, int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(annualAccountReportId, contractReportRevalidationCertAuthorityFinancialCorrectionId);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityRevalidationFinancialCorrections/{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/financialCorrectionCSDs/{contractReportRevalidationCertAuthorityFinancialCorrectionCSDId:int}")]
        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDDO GetAnnualAccountReportContractReportFinancialCSD(int annualAccountReportId, int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertifiedRevalidationFinancialCorrectionCSD(annualAccountReportId, contractReportRevalidationCertAuthorityFinancialCorrectionId, contractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

            var revalidationCertAuthorityFinancialCorrectionCSD = this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.Find(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.GetContractReportFinancialRevalidationCSDByBudgetItem(revalidationCertAuthorityFinancialCorrectionCSD.ContractReportId, revalidationCertAuthorityFinancialCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialRevalidationCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string financialRevalidationCSDCheckedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;

            if (financialRevalidationCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.FindWithoutIncludes(financialRevalidationCSD.CheckedByUserId.Value);
                financialRevalidationCSDCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.FindWithoutIncludes(financialCSDBudgetItem.CheckedByUserId.Value);
                budgetItemCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.FindWithoutIncludes(financialCSDBudgetItem.TechCheckedByUserId.Value);
                budgetItemTechCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportRevalidationCertAuthorityFinancialCorrectionCSDDO(
                revalidationCertAuthorityFinancialCorrectionCSD,
                financialRevalidationCSD,
                financialRevalidationCSDCheckedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityRevalidationFinancialCorrections/{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/unattachedFinancialCorrectionCSDs")]
        public IList<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsVO> GetFinancialCorrectionCSDsForContractReportCertAuthorityFinancialCorrectionCSDs(int annualAccountReportId, int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetFinancialCorrectionCSDsForContractReportCertAuthorityRevalidationFinancialCorrectionCSDs(contractReportRevalidationCertAuthorityFinancialCorrectionId);
        }
    }
}
