using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportCertAuthorityFinancialCorrectionService;
using Eumis.ApplicationServices.Services.ContractReportFinancialCertCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCertCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/contractReportCertAuthorityFinancialCorrections/{contractReportCertAuthorityFinancialCorrectionId:int}/certAuthorityFinancialCorrectionCSDs")]
    public class ContractReportCertAuthorityFinancialCorrectionCSDsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IUsersRepository usersRepository;
        private IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportCertAuthorityFinancialCorrectionCSDsRepository contractReportCertAuthorityFinancialCorrectionCSDsRepository;
        private IContractReportCertAuthorityFinancialCorrectionService contractReportCertAuthorityFinancialCorrectionService;
        private ICertReportCheckService certReportCheckService;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;

        public ContractReportCertAuthorityFinancialCorrectionCSDsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportCertAuthorityFinancialCorrectionCSDsRepository contractReportCertAuthorityFinancialCorrectionCSDsRepository,
            IContractReportCertAuthorityFinancialCorrectionService contractReportCertAuthorityFinancialCorrectionService,
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
            this.contractReportCertAuthorityFinancialCorrectionCSDsRepository = contractReportCertAuthorityFinancialCorrectionCSDsRepository;
            this.contractReportCertAuthorityFinancialCorrectionService = contractReportCertAuthorityFinancialCorrectionService;
            this.certReportCheckService = certReportCheckService;
            this.relationsRepository = relationsRepository;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
        }

        [Route("")]
        public IList<ContractReportCertAuthorityFinancialCorrectionCSDsVO> GetContractReportCertAuthorityFinancialCorrectionCSDs(int contractReportCertAuthorityFinancialCorrectionId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.View, contractReportCertAuthorityFinancialCorrectionId);

            return this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.GetContractReportCertAuthorityFinancialCorrectionCSDs(contractReportCertAuthorityFinancialCorrectionId, csd, company);
        }

        [Route("{contractReportCertAuthorityFinancialCorrectionCSDId:int}")]
        public ContractReportCertAuthorityFinancialCorrectionCSDDO GetContractReportFinancialCSD(int contractReportCertAuthorityFinancialCorrectionId, int contractReportCertAuthorityFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.View, contractReportCertAuthorityFinancialCorrectionId);

            var certAuthorityFinancialCorrectionCSD = this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.Find(contractReportCertAuthorityFinancialCorrectionCSDId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(certAuthorityFinancialCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;

            if (certAuthorityFinancialCorrectionCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(certAuthorityFinancialCorrectionCSD.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                budgetItemCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                budgetItemTechCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportCertAuthorityFinancialCorrectionCSDDO(
                certAuthorityFinancialCorrectionCSD,
                checkedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityFinancialCorrections/{contractReportCertAuthorityFinancialCorrectionId:int}/unattachedFinancialCorrectionCSDs")]
        public IList<ContractReportCertAuthorityFinancialCorrectionCSDsVO> GetFinancialCorrectionCSDsForContractReportCertAuthorityFinancialCorrectionCSDs(int annualAccountReportId, int contractReportCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetFinancialCorrectionCSDsForContractReportCertAuthorityFinancialCorrectionCSDs(contractReportCertAuthorityFinancialCorrectionId);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityFinancialCorrections/{contractReportCertAuthorityFinancialCorrectionId:int}/attachedFinancialCorrectionCSDs")]
        public IList<ContractReportCertAuthorityFinancialCorrectionCSDsVO> GetContractReportCertAuthorityFinancialCorrectionCSDs(int annualAccountReportId, int contractReportCertAuthorityFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetContractReportCertAuthorityFinancialCorrectionCSDs(annualAccountReportId, contractReportCertAuthorityFinancialCorrectionId);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/certAuthorityFinancialCorrections/{contractReportCertAuthorityFinancialCorrectionId:int}/financialCorrectionCSDs/{contractReportCertAuthorityFinancialCorrectionCSDId:int}")]
        public ContractReportCertAuthorityFinancialCorrectionCSDDO GetAnnualAccountReportContractReportFinancialCSD(int annualAccountReportId, int contractReportCertAuthorityFinancialCorrectionId, int contractReportCertAuthorityFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasCertifiedFinancialCorrectionCSD(annualAccountReportId, contractReportCertAuthorityFinancialCorrectionId, contractReportCertAuthorityFinancialCorrectionCSDId);

            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.View, contractReportCertAuthorityFinancialCorrectionId);

            var certAuthorityFinancialCorrectionCSD = this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.Find(contractReportCertAuthorityFinancialCorrectionCSDId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(certAuthorityFinancialCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;

            if (certAuthorityFinancialCorrectionCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(certAuthorityFinancialCorrectionCSD.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                budgetItemCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                budgetItemTechCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportCertAuthorityFinancialCorrectionCSDDO(
                certAuthorityFinancialCorrectionCSD,
                checkedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.Edit.CSD.Create), IdParam = "contractReportCertAuthorityFinancialCorrectionId")]
        public void CreateContractReportCertAuthorityFinancialCorrectionCSDStatus(int contractReportCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            this.contractReportCertAuthorityFinancialCorrectionService.CreateContractReportCertAuthorityFinancialCorrectionCSD(contractReportCertAuthorityFinancialCorrectionId, contractReportFinancialCSDBudgetItemId);
        }

        [HttpDelete]
        [Route("{contractReportCertAuthorityFinancialCorrectionCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.Edit.CSD.Delete), IdParam = "contractReportCertAuthorityFinancialCorrectionCSDId")]
        public void DeleteContractReportCertAuthorityFinancialCorrectionCSDStatus(int contractReportCertAuthorityFinancialCorrectionId, int contractReportCertAuthorityFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportCertAuthorityFinancialCorrectionService.DeleteContractReportCertAuthorityFinancialCorrectionCSD(contractReportCertAuthorityFinancialCorrectionCSDId, vers);
        }

        [HttpPut]
        [Route("{contractReportCertAuthorityFinancialCorrectionCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.Edit.CSD.Update), IdParam = "contractReportCertAuthorityFinancialCorrectionCSDId")]
        public void UpdateContractReportFinancialCSD(int contractReportCertAuthorityFinancialCorrectionId, int contractReportCertAuthorityFinancialCorrectionCSDId, ContractReportCertAuthorityFinancialCorrectionCSDDO contractReportCertAuthorityFinancialCorrectionCSD)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            this.contractReportCertAuthorityFinancialCorrectionService.UpdateContractReportCertAuthorityFinancialCorrectionCSD(
                contractReportCertAuthorityFinancialCorrectionCSDId,
                contractReportCertAuthorityFinancialCorrectionCSD.Version,
                contractReportCertAuthorityFinancialCorrectionCSD.Sign,
                contractReportCertAuthorityFinancialCorrectionCSD.Notes,
                contractReportCertAuthorityFinancialCorrectionCSD.CertifiedEuAmount,
                contractReportCertAuthorityFinancialCorrectionCSD.CertifiedBgAmount,
                contractReportCertAuthorityFinancialCorrectionCSD.CertifiedBfpTotalAmount,
                contractReportCertAuthorityFinancialCorrectionCSD.CertifiedSelfAmount,
                contractReportCertAuthorityFinancialCorrectionCSD.CertifiedTotalAmount);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityFinancialCorrectionCSDId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.Edit.CSD.ChangeStatusToEnded), IdParam = "contractReportCertAuthorityFinancialCorrectionCSDId")]
        public void ChangeContractReportCertAuthorityFinancialCorrectionCSDStatusToEnded(int contractReportCertAuthorityFinancialCorrectionId, int contractReportCertAuthorityFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportCertAuthorityFinancialCorrectionService.ChangeContractReportCertAuthorityFinancialCorrectionCSDStatus(contractReportCertAuthorityFinancialCorrectionCSDId, vers, ContractReportCertAuthorityFinancialCorrectionCSDStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityFinancialCorrectionCSDId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportCertAuthorityFinancialCorrectionCSDStatusToEnded(int contractReportCertAuthorityFinancialCorrectionId, int contractReportCertAuthorityFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            var errors = this.contractReportCertAuthorityFinancialCorrectionService.CanChangeContractReportCertAuthorityFinancialCorrectionCSDStatusToEnded(contractReportCertAuthorityFinancialCorrectionCSDId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportCertAuthorityFinancialCorrectionCSDId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityFinancialCorrection.Edit.CSD.ChangeStatusToDraft), IdParam = "contractReportCertAuthorityFinancialCorrectionCSDId")]
        public void ChangeContractReportCertAuthorityFinancialCorrectionCSDStatusToDraft(int contractReportCertAuthorityFinancialCorrectionId, int contractReportCertAuthorityFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.Edit, contractReportCertAuthorityFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportCertAuthorityFinancialCorrectionService.ChangeContractReportCertAuthorityFinancialCorrectionCSDStatus(contractReportCertAuthorityFinancialCorrectionCSDId, vers, ContractReportCertAuthorityFinancialCorrectionCSDStatus.Draft);
        }
    }
}
