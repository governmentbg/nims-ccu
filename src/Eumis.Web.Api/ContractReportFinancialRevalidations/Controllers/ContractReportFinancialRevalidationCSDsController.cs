using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportFinancialRevalidation;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
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

namespace Eumis.Web.Api.ContractReportFinancialRevalidations.Controllers
{
    [RoutePrefix("api/contractReportFinancialRevalidations/{contractReportFinancialRevalidationId:int}/financialRevalidationCSDs")]
    public class ContractReportFinancialRevalidationCSDsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IUsersRepository usersRepository;
        private IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository;
        private IContractReportFinancialRevalidationService contractReportFinancialRevalidationService;
        private ICertReportCheckService certReportCheckService;

        public ContractReportFinancialRevalidationCSDsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository,
            IContractReportFinancialRevalidationService contractReportFinancialRevalidationService,
            ICertReportCheckService certReportCheckService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.usersRepository = usersRepository;
            this.contractReportFinancialCSDsRepository = contractReportFinancialCSDsRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.contractReportFinancialRevalidationCSDsRepository = contractReportFinancialRevalidationCSDsRepository;
            this.contractReportFinancialRevalidationService = contractReportFinancialRevalidationService;
            this.certReportCheckService = certReportCheckService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportFinancialRevalidationCSDsVO> GetContractReportFinancialRevalidationCSDs(int contractReportFinancialRevalidationId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.View, contractReportFinancialRevalidationId);

            return this.contractReportFinancialRevalidationCSDsRepository.GetContractReportFinancialRevalidationCSDs(contractReportFinancialRevalidationId, csd, company);
        }

        [Route("~/api/contractReportRevalidationCertAuthorityFinancialCorrections/{contractReportRevalidationCertAuthorityFinancialCorrectionId:int}/financialCSDBudgetItems")]
        public IList<ContractReportFinancialRevalidationCSDsVO> GetContractReportFinancialCSDBudgetItemsForContractReportRevalidationCertAuthorityFinancialCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.View, contractReportRevalidationCertAuthorityFinancialCorrectionId);

            return this.contractReportFinancialRevalidationCSDsRepository.GetContractReportFinancialRevalidationCSDsForContractReportRevalidationCertAuthorityFinancialCorrection(contractReportId, contractReportRevalidationCertAuthorityFinancialCorrectionId, csd, company);
        }

        [Route("~/api/certReports/{certReportId:int}/financialRevalidations/{contractReportFinancialRevalidationId:int}/attachedFinancialRevalidationCSDs")]
        public IList<ContractReportFinancialRevalidationCSDsVO> GetCertReportRevalidationsAttachedFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.contractReportFinancialRevalidationCSDsRepository.GetContractReportFinancialRevalidationCSDs(contractReportFinancialRevalidationId, isAttachedToCertReport: true, certReportId: certReportId);
        }

        [Route("~/api/certReports/{certReportId:int}/financialRevalidations/{contractReportFinancialRevalidationId:int}/unattachedFinancialRevalidationCSDs")]
        public IList<ContractReportFinancialRevalidationCSDsVO> GetCertReportRevalidationsUnattachedFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialRevalidationCSD(certReportId, contractReportFinancialRevalidationId);

            return this.contractReportFinancialRevalidationCSDsRepository.GetContractReportFinancialRevalidationCSDs(contractReportFinancialRevalidationId, isAttachedToCertReport: false);
        }

        [Route("{contractReportFinancialRevalidationCSDId:int}")]
        public ContractReportFinancialRevalidationCSDDO GetContractReportFinancialCSD(int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.View, contractReportFinancialRevalidationId);

            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.Find(contractReportFinancialRevalidationCSDId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialRevalidationCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;

            if (financialRevalidationCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialRevalidationCSD.CheckedByUserId.Value);
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

            return new ContractReportFinancialRevalidationCSDDO(
                financialRevalidationCSD,
                checkedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        [Route("~/api/certReports/{certReportId:int}/financialRevalidations/{contractReportFinancialRevalidationId:int}/financialRevalidationCSDs/{contractReportFinancialRevalidationCSDId:int}")]
        public ContractReportFinancialRevalidationCSDDO GetCertReportContractReportFinancialCSD(int certReportId, int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasFinancialRevalidationCSD(certReportId, contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDId);

            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.Find(contractReportFinancialRevalidationCSDId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialRevalidationCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;
            string certCheckedByUser = string.Empty;

            if (financialRevalidationCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialRevalidationCSD.CheckedByUserId.Value);
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

            if (financialRevalidationCSD.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialRevalidationCSD.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialRevalidationCSDDO(
                financialRevalidationCSD,
                checkedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser,
                certCheckedByUser);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Edit.CSD.Create), IdParam = "contractReportFinancialRevalidationId")]
        public void CreateContractReportFinancialRevalidationCSDStatus(int contractReportFinancialRevalidationId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            this.contractReportFinancialRevalidationService.CreateContractReportFinancialRevalidationCSD(contractReportFinancialRevalidationId, contractReportFinancialCSDBudgetItemId);
        }

        [HttpDelete]
        [Route("{contractReportFinancialRevalidationCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Edit.CSD.Delete), IdParam = "contractReportFinancialRevalidationCSDId")]
        public void DeleteContractReportFinancialRevalidationCSDStatus(int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialRevalidationService.DeleteContractReportFinancialRevalidationCSD(contractReportFinancialRevalidationCSDId, vers);
        }

        [HttpPut]
        [Route("{contractReportFinancialRevalidationCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Edit.CSD.Update), IdParam = "contractReportFinancialRevalidationCSDId")]
        public void UpdateContractReportFinancialCSD(int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId, ContractReportFinancialRevalidationCSDDO contractReportFinancialRevalidationCSD)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            this.contractReportFinancialRevalidationService.UpdateContractReportFinancialRevalidationCSD(
                contractReportFinancialRevalidationCSDId,
                contractReportFinancialRevalidationCSD.Version,
                contractReportFinancialRevalidationCSD.Notes,
                contractReportFinancialRevalidationCSD.RevalidatedEuAmount,
                contractReportFinancialRevalidationCSD.RevalidatedBgAmount,
                contractReportFinancialRevalidationCSD.RevalidatedBfpTotalAmount,
                contractReportFinancialRevalidationCSD.RevalidatedSelfAmount,
                contractReportFinancialRevalidationCSD.RevalidatedTotalAmount);
        }

        [HttpPut]
        [Route("~/api/certReports/{certReportId:int}/financialRevalidations/{contractReportFinancialRevalidationId:int}/financialRevalidationCSDs/{contractReportFinancialRevalidationCSDId:int}/certUpdate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Edit.CSD.CertUpdate), IdParam = "contractReportFinancialRevalidationCSDId")]
        public void CertUpdateContractReportFinancialCSD(int certReportId, int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId, ContractReportFinancialRevalidationCSDDO contractReportFinancialRevalidationCSD)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialRevalidationCSD(certReportId, contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDId);

            this.certReportCheckService.UpdateContractReportFinancialRevalidationCSD(
                certReportId,
                contractReportFinancialRevalidationCSDId,
                contractReportFinancialRevalidationCSD.Version,
                contractReportFinancialRevalidationCSD.UncertifiedRevalidatedEuAmount,
                contractReportFinancialRevalidationCSD.UncertifiedRevalidatedBgAmount,
                contractReportFinancialRevalidationCSD.UncertifiedRevalidatedBfpTotalAmount,
                contractReportFinancialRevalidationCSD.UncertifiedRevalidatedSelfAmount,
                contractReportFinancialRevalidationCSD.UncertifiedRevalidatedTotalAmount,
                contractReportFinancialRevalidationCSD.CertifiedRevalidatedEuAmount,
                contractReportFinancialRevalidationCSD.CertifiedRevalidatedBgAmount,
                contractReportFinancialRevalidationCSD.CertifiedRevalidatedBfpTotalAmount,
                contractReportFinancialRevalidationCSD.CertifiedRevalidatedSelfAmount,
                contractReportFinancialRevalidationCSD.CertifiedRevalidatedTotalAmount);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationCSDId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Edit.CSD.ChangeStatusToEnded), IdParam = "contractReportFinancialRevalidationCSDId")]
        public void ChangeContractReportFinancialRevalidationCSDStatusToEnded(int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialRevalidationService.ChangeContractReportFinancialRevalidationCSDStatus(contractReportFinancialRevalidationCSDId, vers, ContractReportFinancialRevalidationCSDStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationCSDId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialRevalidationCSDStatusToEnded(int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            var errors = this.contractReportFinancialRevalidationService.CanChangeContractReportFinancialRevalidationCSDStatusToEnded(contractReportFinancialRevalidationCSDId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/financialRevalidations/{contractReportFinancialRevalidationId:int}/financialRevalidationCSDs/{contractReportFinancialRevalidationCSDId:int}/changeCertStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Edit.CSD.ChangeCertStatusToEnded), IdParam = "contractReportFinancialRevalidationCSDId")]
        public void ChangeContractReportFinancialRevalidationCSDCertStatusToEnded(int certReportId, int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialRevalidationCSD(certReportId, contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportFinancialRevalidationCSDCertStatus(contractReportFinancialRevalidationCSDId, vers, ContractReportFinancialRevalidationCSDCertStatus.Ended);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/financialRevalidations/{contractReportFinancialRevalidationId:int}/financialRevalidationCSDs/{contractReportFinancialRevalidationCSDId:int}/changeCertStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Edit.CSD.ChangeCertStatusToDraft), IdParam = "contractReportFinancialRevalidationCSDId")]
        public void ChangeContractReportFinancialRevalidationCSDCertStatusToDraft(int certReportId, int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialRevalidationCSD(certReportId, contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportFinancialRevalidationCSDCertStatus(contractReportFinancialRevalidationCSDId, vers, ContractReportFinancialRevalidationCSDCertStatus.Draft);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/financialRevalidations/{contractReportFinancialRevalidationId:int}/financialRevalidationCSDs/{contractReportFinancialRevalidationCSDId:int}/canChangeCertStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialRevalidationCSDCertStatusToEnded(int certReportId, int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialRevalidationCSD(certReportId, contractReportFinancialRevalidationId, contractReportFinancialRevalidationCSDId);

            var errors = this.certReportCheckService.CanChangeContractReportFinancialRevalidationCSDCertStatusToEnded(contractReportFinancialRevalidationCSDId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialRevalidationCSDId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialRevalidation.Edit.CSD.ChangeStatusToDraft), IdParam = "contractReportFinancialRevalidationCSDId")]
        public void ChangeContractReportFinancialRevalidationCSDStatusToDraft(int contractReportFinancialRevalidationId, int contractReportFinancialRevalidationCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.Edit, contractReportFinancialRevalidationId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialRevalidationService.ChangeContractReportFinancialRevalidationCSDStatus(contractReportFinancialRevalidationCSDId, vers, ContractReportFinancialRevalidationCSDStatus.Draft);
        }
    }
}
