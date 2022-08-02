using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportFinancialCSD;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.ContractReports.Repositories;
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

namespace Eumis.Web.Api.ContractReportFinancialCSDs.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/financialCSDBudgetItems")]
    public class ContractReportFinancialCSDBudgetItemsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IUsersRepository usersRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportFinancialCSDService contractReportFinancialCSDService;
        private ICertReportCheckService certReportCheckService;

        public ContractReportFinancialCSDBudgetItemsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportFinancialCSDService contractReportFinancialCSDService,
            ICertReportCheckService certReportCheckService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.usersRepository = usersRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportFinancialCSDsRepository = contractReportFinancialCSDsRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.contractReportFinancialCSDService = contractReportFinancialCSDService;
            this.certReportCheckService = certReportCheckService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItems(int contractReportId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItems(contractReportId, csd, company);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportFinancialCSDs")]
        public IList<ContractReportFinancialCSDsVO> GetContractReportFinancialCSDsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDsForProjectDossier(contractId);
        }

        [Route("~/api/certReports/{certReportId:int}/payments/{contractReportId:int}/attachedFinancialCSDBudgetItems")]
        public IList<ContractReportFinancialCSDBudgetItemsVO> GetCertReportContractReportAttachedFinancialCSDBudgetItems(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItems(contractReportId, isAttachedToCertReport: true, certReportId: certReportId);
        }

        [Route("~/api/certReports/{certReportId:int}/payments/{contractReportId:int}/unattachedFinancialCSDBudgetItems")]
        public IList<ContractReportFinancialCSDBudgetItemsVO> GetCertReportContractReportUnattachedFinancialCSDBudgetItems(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCSDBudgetItem(certReportId, contractReportId);

            return this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItems(contractReportId, isAttachedToCertReport: false);
        }

        [Route("~/api/contractReportFinancialCorrections/{contractReportFinancialCorrectionId:int}/financialCSDBudgetItems")]
        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportFinancialCorrection(int contractReportFinancialCorrectionId, int contractReportId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.View, contractReportFinancialCorrectionId);

            return this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItemsForContractReportFinancialCorrection(contractReportId, contractReportFinancialCorrectionId, csd, company);
        }

        [Route("~/api/contractReportFinancialRevalidations/{contractReportFinancialRevalidationId:int}/financialCSDBudgetItems")]
        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportFinancialRevalidation(int contractReportFinancialRevalidationId, int contractReportId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.View, contractReportFinancialRevalidationId);

            return this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItemsForContractReportFinancialRevalidation(contractReportId, contractReportFinancialRevalidationId, csd, company);
        }

        [Route("~/api/contractReportFinancialCertCorrections/{contractReportFinancialCertCorrectionId:int}/financialCSDBudgetItems")]
        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportFinancialCertCorrection(int contractReportFinancialCertCorrectionId, int contractReportId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.View, contractReportFinancialCertCorrectionId);

            return this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItemsForContractReportFinancialCertCorrection(contractReportId, contractReportFinancialCertCorrectionId, csd, company);
        }

        [Route("~/api/contractReportCertAuthorityFinancialCorrections/{contractReportCertAuthorityFinancialCorrectionId:int}/financialCSDBudgetItems")]
        public IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportCertAuthorityFinancialCorrection(int contractReportCertAuthorityFinancialCorrectionId, int contractReportId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.View, contractReportCertAuthorityFinancialCorrectionId);

            return this.contractReportFinancialCSDBudgetItemsRepository.GetContractReportFinancialCSDBudgetItemsForContractReportCertAuthorityFinancialCorrection(contractReportId, contractReportCertAuthorityFinancialCorrectionId, csd, company);
        }

        [Route("{contractReportFinancialCSDBudgetItemId:int}")]
        public ContractReportFinancialCSDBudgetItemDO GetContractReportFinancialCSD(int contractReportId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string techCheckedByUser = string.Empty;

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                techCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            var contractReportFnancialStatus = this.contractReportFinancialsRepository.FindWithoutIncludes(financialCSD.ContractReportFinancialId).Status;

            return new ContractReportFinancialCSDBudgetItemDO(financialCSDBudgetItem, financialCSD, checkedByUser, techCheckedByUser, contractReportFnancialStatus);
        }

        [Route("~/api/contractReportFinancialCorrections/{contractReportFinancialCorrectionId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}")]
        public ContractReportFinancialCSDBudgetItemDO GetContractReportFinancialCorrectionContractReportFinancialCSDBudgetItem(int contractReportFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.View, contractReportFinancialCorrectionId);
            this.relationsRepository.AssertContractReportFinancialCorrectionHasFinancialCSDBudgetItem(contractReportFinancialCorrectionId, contractReportFinancialCSDBudgetItemId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string techCheckedByUser = string.Empty;

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                techCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCSDBudgetItemDO(financialCSDBudgetItem, financialCSD, checkedByUser, techCheckedByUser);
        }

        [Route("~/api/contractReportFinancialRevalidations/{contractReportFinancialRevalidationId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}")]
        public ContractReportFinancialCSDBudgetItemDO GetContractReportFinancialRevalidationContractReportFinancialCSDBudgetItem(int contractReportFinancialRevalidationId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.View, contractReportFinancialRevalidationId);
            this.relationsRepository.AssertContractReportFinancialRevalidationHasFinancialCSDBudgetItem(contractReportFinancialRevalidationId, contractReportFinancialCSDBudgetItemId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string techCheckedByUser = string.Empty;

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                techCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCSDBudgetItemDO(financialCSDBudgetItem, financialCSD, checkedByUser, techCheckedByUser);
        }

        [Route("~/api/contractReportFinancialCertCorrections/{contractReportFinancialCertCorrectionId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}")]
        public ContractReportFinancialCSDBudgetItemDO GetContractReportFinancialCertCorrectionContractReportFinancialCSDBudgetItem(int contractReportFinancialCertCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.View, contractReportFinancialCertCorrectionId);
            this.relationsRepository.AssertContractReportFinancialCertCorrectionHasFinancialCSDBudgetItem(contractReportFinancialCertCorrectionId, contractReportFinancialCSDBudgetItemId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string techCheckedByUser = string.Empty;

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                techCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCSDBudgetItemDO(financialCSDBudgetItem, financialCSD, checkedByUser, techCheckedByUser);
        }

        [Route("~/api/contractReportCertAuthorityFinancialCorrections/{contractReportCertAuthorityFinancialCorrectionId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}")]
        public ContractReportFinancialCSDBudgetItemDO GetContractReportCertAuthorityFinancialCorrectionContractReportFinancialCSDBudgetItem(int contractReportCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.View, contractReportCertAuthorityFinancialCorrectionId);
            this.relationsRepository.AssertContractReportCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(contractReportCertAuthorityFinancialCorrectionId, contractReportFinancialCSDBudgetItemId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string techCheckedByUser = string.Empty;

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                techCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCSDBudgetItemDO(financialCSDBudgetItem, financialCSD, checkedByUser, techCheckedByUser);
        }

        [Route("~/api/contractReportRevalidationCertAuthorityFinancialCorrections/{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}")]
        public ContractReportFinancialCSDBudgetItemDO GetContractReportRevalidationCertAuthorityFinancialCorrectionContractReportFinancialCSDBudgetItem(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.View, contractReportRevalidationCertAuthorityFinancialCorrectionId);
            this.relationsRepository.AssertContractReportRevalidationCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(contractReportRevalidationCertAuthorityFinancialCorrectionId, contractReportFinancialCSDBudgetItemId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string techCheckedByUser = string.Empty;

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                techCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCSDBudgetItemDO(financialCSDBudgetItem, financialCSD, checkedByUser, techCheckedByUser);
        }

        [Route("~/api/certReports/{certReportId:int}/payments/{contractReportId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}")]
        public ContractReportFinancialCSDBudgetItemDO GetCertReportContractReportFinancialCSD(int certReportId, int contractReportId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.View, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCSDBudgetItem(certReportId, contractReportId, contractReportFinancialCSDBudgetItemId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(contractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string techCheckedByUser = string.Empty;
            string certCheckedByUser = string.Empty;

            if (financialCSDBudgetItem.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.TechCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.TechCheckedByUserId.Value);
                techCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (financialCSDBudgetItem.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCSDBudgetItem.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCSDBudgetItemDO(financialCSDBudgetItem, financialCSD, checkedByUser, techCheckedByUser, certCheckedByUser);
        }

        [HttpPut]
        [Route("{contractReportFinancialCSDBudgetItemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCSDBudgetItem.Update), IdParam = "contractReportFinancialCSDBudgetItemId", DisablePostData = true)]
        public void UpdateContractReportFinancialCSD(int contractReportId, int contractReportFinancialCSDBudgetItemId, ContractReportFinancialCSDBudgetItemDO contractReportFinancialCSDBudgetItem)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            this.contractReportFinancialCSDService.UpdateContractReportFinancialCSDBudgetItem(
                contractReportFinancialCSDBudgetItemId,
                contractReportFinancialCSDBudgetItem.Version,
                contractReportFinancialCSDBudgetItem.CostSupportingDocumentApproved,
                contractReportFinancialCSDBudgetItem.Notes,
                contractReportFinancialCSDBudgetItem.EuAmount,
                contractReportFinancialCSDBudgetItem.BgAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedEuAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedTotalAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedTotalAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedSelfAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedTotalAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionEuAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionTotalAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionTotalAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionSelfAmount,
                contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionTotalAmount,
                contractReportFinancialCSDBudgetItem.ApprovedEuAmount,
                contractReportFinancialCSDBudgetItem.ApprovedTotalAmount,
                contractReportFinancialCSDBudgetItem.ApprovedTotalAmount,
                contractReportFinancialCSDBudgetItem.ApprovedSelfAmount,
                contractReportFinancialCSDBudgetItem.ApprovedTotalAmount,
                contractReportFinancialCSDBudgetItem.CorrectionType,
                contractReportFinancialCSDBudgetItem.FinancialCorrectionId,
                contractReportFinancialCSDBudgetItem.IrregularityId);
        }

        [HttpPut]
        [Route("~/api/certReports/{certReportId:int}/payments/{contractReportId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}/certUpdate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCSDBudgetItem.CertUpdate), IdParam = "contractReportFinancialCSDBudgetItemId")]
        public void CertUpdateContractReportFinancialCSD(int certReportId, int contractReportId, int contractReportFinancialCSDBudgetItemId, ContractReportFinancialCSDBudgetItemDO contractReportFinancialCSDBudgetItem)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCSDBudgetItem(certReportId, contractReportId, contractReportFinancialCSDBudgetItemId);

            this.certReportCheckService.UpdateContractReportFinancialCSDBudgetItem(
                certReportId,
                contractReportFinancialCSDBudgetItemId,
                contractReportFinancialCSDBudgetItem.Version,
                contractReportFinancialCSDBudgetItem.UncertifiedApprovedEuAmount,
                contractReportFinancialCSDBudgetItem.UncertifiedApprovedBgAmount,
                contractReportFinancialCSDBudgetItem.UncertifiedApprovedBfpTotalAmount,
                contractReportFinancialCSDBudgetItem.UncertifiedApprovedSelfAmount,
                contractReportFinancialCSDBudgetItem.UncertifiedApprovedTotalAmount,
                contractReportFinancialCSDBudgetItem.CertifiedApprovedEuAmount,
                contractReportFinancialCSDBudgetItem.CertifiedApprovedBgAmount,
                contractReportFinancialCSDBudgetItem.CertifiedApprovedBfpTotalAmount,
                contractReportFinancialCSDBudgetItem.CertifiedApprovedSelfAmount,
                contractReportFinancialCSDBudgetItem.CertifiedApprovedTotalAmount);
        }

        [HttpPost]
        [Route("{contractReportFinancialCSDBudgetItemId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCSDBudgetItem.ChangeStatusToEnded), IdParam = "contractReportFinancialCSDBudgetItemId")]
        public void ChangeContractReportFinancialCSDBudgetItemStatusToEnded(int contractReportId, int contractReportFinancialCSDBudgetItemId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCSDService.ChangeContractReportFinancialCSDBudgetItemStatus(contractReportFinancialCSDBudgetItemId, vers, Domain.Contracts.ContractReportFinancialCSDBudgetItemStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportFinancialCSDBudgetItemId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialCSDBudgetItemStatusToEnded(int contractReportId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportFinancialCSDService.CanChangeContractReportFinancialCSDBudgetItemStatusToEnded(contractReportFinancialCSDBudgetItemId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/payments/{contractReportId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}/changeCertStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCSDBudgetItem.ChangeCertStatusToEnded), IdParam = "contractReportFinancialCSDBudgetItemId")]
        public void ChangeContractReportFinancialCSDBudgetItemCertStatusToEnded(int certReportId, int contractReportId, int contractReportFinancialCSDBudgetItemId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCSDBudgetItem(certReportId, contractReportId, contractReportFinancialCSDBudgetItemId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportFinancialCSDBudgetItemCertStatus(contractReportFinancialCSDBudgetItemId, vers, ContractReportFinancialCSDBudgetItemCertStatus.Ended);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/payments/{contractReportId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}/changeCertStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCSDBudgetItem.ChangeCertStatusToDraft), IdParam = "contractReportFinancialCSDBudgetItemId")]
        public void ChangeContractReportFinancialCSDBudgetItemCertStatusToDraft(int certReportId, int contractReportId, int contractReportFinancialCSDBudgetItemId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCSDBudgetItem(certReportId, contractReportId, contractReportFinancialCSDBudgetItemId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportFinancialCSDBudgetItemCertStatus(contractReportFinancialCSDBudgetItemId, vers, ContractReportFinancialCSDBudgetItemCertStatus.Draft);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/payments/{contractReportId:int}/financialCSDBudgetItems/{contractReportFinancialCSDBudgetItemId:int}/canChangeCertStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialCSDBudgetItemCertStatusToEnded(int certReportId, int contractReportId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCSDBudgetItem(certReportId, contractReportId, contractReportFinancialCSDBudgetItemId);

            var errors = this.certReportCheckService.CanChangeContractReportFinancialCSDBudgetItemCertStatus(contractReportFinancialCSDBudgetItemId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCSDBudgetItemId:int}/techCheck")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCSDBudgetItem.TechCheck), IdParam = "contractReportFinancialCSDBudgetItemId")]
        public void TechCheckContractReportFinancialCSDBudgetItem(int contractReportId, int contractReportFinancialCSDBudgetItemId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCSDService.TechCheckContractReportFinancialCSDBudgetItem(contractReportFinancialCSDBudgetItemId, vers);
        }

        [HttpPost]
        [Route("{contractReportFinancialCSDBudgetItemId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportFinancialCSDBudgetItem.ChangeStatusToDraft), IdParam = "contractReportFinancialCSDBudgetItemId")]
        public void ChangeContractReportFinancialCSDBudgetItemStatusToDraft(int contractReportId, int contractReportFinancialCSDBudgetItemId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCSDService.ChangeContractReportFinancialCSDBudgetItemStatus(contractReportFinancialCSDBudgetItemId, vers, Domain.Contracts.ContractReportFinancialCSDBudgetItemStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportFinancialCSDBudgetItemId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeContractReportFinancialCSDBudgetItemStatusToDraft(int contractReportId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportFinancialCSDService.CanChangeContractReportFinancialCSDBudgetItemStatusToDraft(contractReportFinancialCSDBudgetItemId);

            return new ErrorsDO(errors);
        }
    }
}