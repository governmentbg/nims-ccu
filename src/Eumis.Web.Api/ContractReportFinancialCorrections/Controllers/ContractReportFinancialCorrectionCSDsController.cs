using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportFinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
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

namespace Eumis.Web.Api.ContractReportFinancialCSDs.Controllers
{
    [RoutePrefix("api/contractReportFinancialCorrections/{contractReportFinancialCorrectionId:int}/financialCorrectionCSDs")]
    public class ContractReportFinancialCorrectionCSDsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IUsersRepository usersRepository;
        private IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository;
        private IContractReportFinancialCorrectionService contractReportFinancialCorrectionService;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private ICertReportCheckService certReportCheckService;

        public ContractReportFinancialCorrectionCSDsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository,
            IContractReportFinancialCorrectionService contractReportFinancialCorrectionService,
            IAnnualAccountReportsRepository annualAccountReportsRepository,
            ICertReportCheckService certReportCheckService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.usersRepository = usersRepository;
            this.contractReportFinancialCSDsRepository = contractReportFinancialCSDsRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.contractReportFinancialCorrectionCSDsRepository = contractReportFinancialCorrectionCSDsRepository;
            this.contractReportFinancialCorrectionService = contractReportFinancialCorrectionService;
            this.certReportCheckService = certReportCheckService;
            this.relationsRepository = relationsRepository;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
        }

        [Route("")]
        public IList<ContractReportFinancialCorrectionCSDsVO> GetContractReportFinancialCorrectionCSDs(int contractReportFinancialCorrectionId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.View, contractReportFinancialCorrectionId);

            return this.contractReportFinancialCorrectionCSDsRepository.GetContractReportFinancialCorrectionCSDs(contractReportFinancialCorrectionId, csd, company);
        }

        [Route("~/api/certReports/{certReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/attachedFinancialCorrectionCSDs")]
        public IList<ContractReportFinancialCorrectionCSDsVO> GetCertReportFinancialCorrectionsAttachedFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasFinancialCorrectionCSD(certReportId, contractReportFinancialCorrectionId);

            return this.contractReportFinancialCorrectionCSDsRepository.GetContractReportFinancialCorrectionCSDs(contractReportFinancialCorrectionId, isAttachedToCertReport: true, certReportId: certReportId);
        }

        [Route("~/api/certReports/{certReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/unattachedFinancialCorrectionCSDs")]
        public IList<ContractReportFinancialCorrectionCSDsVO> GetCertReportCorrectionsUnattachedFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            this.relationsRepository.AssertCertReportHasFinancialCorrectionCSD(certReportId, contractReportFinancialCorrectionId);

            return this.contractReportFinancialCorrectionCSDsRepository.GetContractReportFinancialCorrectionCSDs(contractReportFinancialCorrectionId, isAttachedToCertReport: false);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/unattachedFinancialCorrectionCSDs")]
        public IList<ContractReportFinancialCorrectionCSDsVO> GetFinancialCorrectionCSDsForContractReportFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            return this.annualAccountReportsRepository.GetFinancialCorrectionCSDsForContractReportFinancialCorrectionCSDs(annualAccountReportId, contractReportFinancialCorrectionId);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/attachedFinancialCorrectionCSDs")]
        public IList<ContractReportFinancialCorrectionCSDsVO> GetContractReportFinancialCorrectionCSDs(int annualAccountReportId, int contractReportFinancialCorrectionId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            return this.annualAccountReportsRepository.GetContractReportFinancialCorrectionCSDs(annualAccountReportId, contractReportFinancialCorrectionId);
        }

        [Route("{contractReportFinancialCorrectionCSDId:int}")]
        public ContractReportFinancialCorrectionCSDDO GetContractReportFinancialCSD(int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.View, contractReportFinancialCorrectionId);

            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.Find(contractReportFinancialCorrectionCSDId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;

            if (financialCorrectionCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCorrectionCSD.CheckedByUserId.Value);
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

            return new ContractReportFinancialCorrectionCSDDO(
                financialCorrectionCSD,
                checkedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        [Route("~/api/certReports/{certReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/financialCorrectionCSDs/{contractReportFinancialCorrectionCSDId:int}")]
        public ContractReportFinancialCorrectionCSDDO GetCertReportContractReportFinancialCSD(int certReportId, int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasFinancialCorrectionCSD(certReportId, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDId);

            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.Find(contractReportFinancialCorrectionCSDId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;
            string certCheckedByUser = string.Empty;

            if (financialCorrectionCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCorrectionCSD.CheckedByUserId.Value);
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

            if (financialCorrectionCSD.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCorrectionCSD.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCorrectionCSDDO(
                financialCorrectionCSD,
                checkedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser,
                certCheckedByUser);
        }

        [Route("~/api/annualAccountReports/{annualAccountReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/financialCorrectionCSDs/{contractReportFinancialCorrectionCSDId:int}")]
        public ContractReportFinancialCorrectionCSDDO GetAnnualAccountReportContractReportFinancialCSD(int annualAccountReportId, int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.View, annualAccountReportId);

            this.relationsRepository.AssertAnnualAccountReportHasContractReportFinancialCorrectionCSD(annualAccountReportId, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDId);

            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.Find(contractReportFinancialCorrectionCSDId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;
            string certCheckedByUser = string.Empty;

            if (financialCorrectionCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCorrectionCSD.CheckedByUserId.Value);
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

            if (financialCorrectionCSD.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCorrectionCSD.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportFinancialCorrectionCSDDO(
                financialCorrectionCSD,
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
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Edit.CSD.Create), IdParam = "contractReportFinancialCorrectionId")]
        public void CreateContractReportFinancialCorrectionCSDStatus(int contractReportFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            this.contractReportFinancialCorrectionService.CreateContractReportFinancialCorrectionCSD(contractReportFinancialCorrectionId, contractReportFinancialCSDBudgetItemId);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCorrectionCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Edit.CSD.Delete), IdParam = "contractReportFinancialCorrectionCSDId")]
        public void DeleteContractReportFinancialCorrectionCSDStatus(int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCorrectionService.DeleteContractReportFinancialCorrectionCSD(contractReportFinancialCorrectionCSDId, vers);
        }

        [HttpPut]
        [Route("{contractReportFinancialCorrectionCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Edit.CSD.Update), IdParam = "contractReportFinancialCorrectionCSDId")]
        public void UpdateContractReportFinancialCSD(int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId, ContractReportFinancialCorrectionCSDDO contractReportFinancialCorrectionCSD)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            this.contractReportFinancialCorrectionService.UpdateContractReportFinancialCorrectionCSD(
                contractReportFinancialCorrectionCSDId,
                contractReportFinancialCorrectionCSD.Version,
                contractReportFinancialCorrectionCSD.Sign,
                contractReportFinancialCorrectionCSD.Notes,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedEuAmount,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedTotalAmount,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedTotalAmount,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedSelfAmount,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedTotalAmount,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionEuAmount,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionTotalAmount,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionTotalAmount,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionSelfAmount,
                contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionTotalAmount,
                contractReportFinancialCorrectionCSD.CorrectedApprovedEuAmount,
                contractReportFinancialCorrectionCSD.CorrectedApprovedBgAmount,
                contractReportFinancialCorrectionCSD.CorrectedApprovedBfpTotalAmount,
                contractReportFinancialCorrectionCSD.CorrectedApprovedSelfAmount,
                contractReportFinancialCorrectionCSD.CorrectedApprovedTotalAmount,
                contractReportFinancialCorrectionCSD.CorrectionType,
                contractReportFinancialCorrectionCSD.FinancialCorrectionId,
                contractReportFinancialCorrectionCSD.IrregularityId);
        }

        [HttpPut]
        [Route("~/api/certReports/{certReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/financialCorrectionCSDs/{contractReportFinancialCorrectionCSDId:int}/certUpdate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Edit.CSD.CertUpdate), IdParam = "contractReportFinancialCorrectionCSDId")]
        public void CertUpdateContractReportFinancialCSD(int certReportId, int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId, ContractReportFinancialCorrectionCSDDO contractReportFinancialCorrectionCSD)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCorrectionCSD(certReportId, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDId);

            this.certReportCheckService.UpdateContractReportFinancialCorrectionCSD(
                certReportId,
                contractReportFinancialCorrectionCSDId,
                contractReportFinancialCorrectionCSD.Version,
                contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedEuAmount,
                contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedBgAmount,
                contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedBfpTotalAmount,
                contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedSelfAmount,
                contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedTotalAmount,
                contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedEuAmount,
                contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedBgAmount,
                contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedBfpTotalAmount,
                contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedSelfAmount,
                contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedTotalAmount);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionCSDId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Edit.CSD.ChangeStatusToEnded), IdParam = "contractReportFinancialCorrectionCSDId")]
        public void ChangeContractReportFinancialCorrectionCSDStatusToEnded(int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCorrectionService.ChangeContractReportFinancialCorrectionCSDStatus(contractReportFinancialCorrectionCSDId, vers, ContractReportFinancialCorrectionCSDStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionCSDId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialCorrectionCSDStatusToEnded(int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            var errors = this.contractReportFinancialCorrectionService.CanChangeContractReportFinancialCorrectionCSDStatusToEnded(contractReportFinancialCorrectionCSDId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/financialCorrectionCSDs/{contractReportFinancialCorrectionCSDId:int}/changeCertStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Edit.CSD.ChangeCertStatusToEnded), IdParam = "contractReportFinancialCorrectionCSDId")]
        public void ChangeContractReportFinancialCorrectionCSDCertStatusToEnded(int certReportId, int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCorrectionCSD(certReportId, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportFinancialCorrectionCSDCertStatus(contractReportFinancialCorrectionCSDId, vers, ContractReportFinancialCorrectionCSDCertStatus.Ended);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/financialCorrectionCSDs/{contractReportFinancialCorrectionCSDId:int}/changeCertStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Edit.CSD.ChangeCertStatusToDraft), IdParam = "contractReportFinancialCorrectionCSDId")]
        public void ChangeContractReportFinancialCorrectionCSDCertStatusToDraft(int certReportId, int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCorrectionCSD(certReportId, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportFinancialCorrectionCSDCertStatus(contractReportFinancialCorrectionCSDId, vers, ContractReportFinancialCorrectionCSDCertStatus.Draft);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/financialCorrections/{contractReportFinancialCorrectionId:int}/financialCorrectionCSDs/{contractReportFinancialCorrectionCSDId:int}/canChangeCertStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialCorrectionCSDCertStatusToEnded(int certReportId, int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCorrectionCSD(certReportId, contractReportFinancialCorrectionId, contractReportFinancialCorrectionCSDId);

            var errors = this.certReportCheckService.CanChangeContractReportFinancialCorrectionCSDCertStatusToEnded(contractReportFinancialCorrectionCSDId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCorrectionCSDId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCorrection.Edit.CSD.ChangeStatusToDraft), IdParam = "contractReportFinancialCorrectionCSDId")]
        public void ChangeContractReportFinancialCorrectionCSDStatusToDraft(int contractReportFinancialCorrectionId, int contractReportFinancialCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.Edit, contractReportFinancialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCorrectionService.ChangeContractReportFinancialCorrectionCSDStatus(contractReportFinancialCorrectionCSDId, vers, ContractReportFinancialCorrectionCSDStatus.Draft);
        }
    }
}
