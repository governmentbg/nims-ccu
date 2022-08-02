using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportFinancialCertCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
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

namespace Eumis.Web.Api.ContractReportFinancialCertCorrections.Controllers
{
    [RoutePrefix("api/contractReportFinancialCertCorrections/{contractReportFinancialCertCorrectionId:int}/financialCertCorrectionCSDs")]
    public class ContractReportFinancialCertCorrectionCSDsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IUsersRepository usersRepository;
        private IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;
        private IContractReportFinancialCertCorrectionCSDsRepository contractReportFinancialCertCorrectionCSDsRepository;
        private IContractReportFinancialCertCorrectionService contractReportFinancialCertCorrectionService;
        private ICertReportCheckService certReportCheckService;

        public ContractReportFinancialCertCorrectionCSDsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IContractReportFinancialCSDsRepository contractReportFinancialCSDsRepository,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository,
            IContractReportFinancialCertCorrectionCSDsRepository contractReportFinancialCertCorrectionCSDsRepository,
            IContractReportFinancialCertCorrectionService contractReportFinancialCertCorrectionService,
            ICertReportCheckService certReportCheckService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.usersRepository = usersRepository;
            this.contractReportFinancialCSDsRepository = contractReportFinancialCSDsRepository;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
            this.contractReportFinancialCertCorrectionCSDsRepository = contractReportFinancialCertCorrectionCSDsRepository;
            this.contractReportFinancialCertCorrectionService = contractReportFinancialCertCorrectionService;
            this.certReportCheckService = certReportCheckService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportFinancialCertCorrectionCSDsVO> GetContractReportFinancialCertCorrectionCSDs(int contractReportFinancialCertCorrectionId, string csd = null, string company = null)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.View, contractReportFinancialCertCorrectionId);

            return this.contractReportFinancialCertCorrectionCSDsRepository.GetContractReportFinancialCertCorrectionCSDs(contractReportFinancialCertCorrectionId, csd, company);
        }

        [Route("~/api/certReports/{certReportId:int}/financialCertCorrections/{contractReportFinancialCertCorrectionId:int}/attachedFinancialCertCorrectionCSDs")]
        public IList<ContractReportFinancialCertCorrectionCSDsVO> GetCertReportCertCorrectionsAttachedFinancialCertCorrectionCSDs(int certReportId, int contractReportFinancialCertCorrectionId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasFinancialCertCorrectionCSD(certReportId, contractReportFinancialCertCorrectionId);

            return this.contractReportFinancialCertCorrectionCSDsRepository.GetContractReportFinancialCertCorrectionCSDs(contractReportFinancialCertCorrectionId, isAttachedToCertReport: true, certReportId: certReportId);
        }

        [Route("~/api/certReports/{certReportId:int}/financialCertCorrections/{contractReportFinancialCertCorrectionId:int}/unattachedFinancialCertCorrectionCSDs")]
        public IList<ContractReportFinancialCertCorrectionCSDsVO> GetCertReportCertCorrectionsUnattachedFinancialCertCorrectionCSDs(int certReportId, int contractReportFinancialCertCorrectionId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasFinancialCertCorrectionCSD(certReportId, contractReportFinancialCertCorrectionId);

            return this.contractReportFinancialCertCorrectionCSDsRepository.GetContractReportFinancialCertCorrectionCSDs(contractReportFinancialCertCorrectionId, isAttachedToCertReport: false);
        }

        [Route("{contractReportFinancialCertCorrectionCSDId:int}")]
        public ContractReportFinancialCertCorrectionCSDDO GetContractReportFinancialCSD(int contractReportFinancialCertCorrectionId, int contractReportFinancialCertCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.View, contractReportFinancialCertCorrectionId);

            var financialCertCorrectionCSD = this.contractReportFinancialCertCorrectionCSDsRepository.Find(contractReportFinancialCertCorrectionCSDId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialCertCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;

            if (financialCertCorrectionCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCertCorrectionCSD.CheckedByUserId.Value);
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

            return new ContractReportFinancialCertCorrectionCSDDO(
                financialCertCorrectionCSD,
                checkedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        [Route("~/api/certReports/{certReportId:int}/financialCertCorrections/{contractReportFinancialCertCorrectionId:int}/financialCertCorrectionCSDs/{contractReportFinancialCertCorrectionCSDId:int}")]
        public ContractReportFinancialCertCorrectionCSDDO GetCertReportContractReportFinancialCSD(int certReportId, int contractReportFinancialCertCorrectionId, int contractReportFinancialCertCorrectionCSDId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasFinancialCertCorrectionCSD(certReportId, contractReportFinancialCertCorrectionId, contractReportFinancialCertCorrectionCSDId);

            var financialCertCorrectionCSD = this.contractReportFinancialCertCorrectionCSDsRepository.Find(contractReportFinancialCertCorrectionCSDId);

            var financialCSDBudgetItem = this.contractReportFinancialCSDBudgetItemsRepository.Find(financialCertCorrectionCSD.ContractReportFinancialCSDBudgetItemId);

            var financialCSD = this.contractReportFinancialCSDsRepository.Find(financialCSDBudgetItem.ContractReportFinancialCSDId);

            string checkedByUser = string.Empty;
            string budgetItemCheckedByUser = string.Empty;
            string budgetItemTechCheckedByUser = string.Empty;
            string certCheckedByUser = string.Empty;

            if (financialCertCorrectionCSD.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(financialCertCorrectionCSD.CheckedByUserId.Value);
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

            return new ContractReportFinancialCertCorrectionCSDDO(
                financialCertCorrectionCSD,
                checkedByUser,
                financialCSDBudgetItem,
                financialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.Edit.CSD.Create), IdParam = "contractReportFinancialCertCorrectionId")]
        public void CreateContractReportFinancialCertCorrectionCSDStatus(int contractReportFinancialCertCorrectionId, int contractReportFinancialCSDBudgetItemId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            this.contractReportFinancialCertCorrectionService.CreateContractReportFinancialCertCorrectionCSD(contractReportFinancialCertCorrectionId, contractReportFinancialCSDBudgetItemId);
        }

        [HttpDelete]
        [Route("{contractReportFinancialCertCorrectionCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.Edit.CSD.Delete), IdParam = "contractReportFinancialCertCorrectionCSDId")]
        public void DeleteContractReportFinancialCertCorrectionCSDStatus(int contractReportFinancialCertCorrectionId, int contractReportFinancialCertCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCertCorrectionService.DeleteContractReportFinancialCertCorrectionCSD(contractReportFinancialCertCorrectionCSDId, vers);
        }

        [HttpPut]
        [Route("{contractReportFinancialCertCorrectionCSDId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.Edit.CSD.Update), IdParam = "contractReportFinancialCertCorrectionCSDId")]
        public void UpdateContractReportFinancialCSD(int contractReportFinancialCertCorrectionId, int contractReportFinancialCertCorrectionCSDId, ContractReportFinancialCertCorrectionCSDDO contractReportFinancialCertCorrectionCSD)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            this.contractReportFinancialCertCorrectionService.UpdateContractReportFinancialCertCorrectionCSD(
                contractReportFinancialCertCorrectionCSDId,
                contractReportFinancialCertCorrectionCSD.Version,
                contractReportFinancialCertCorrectionCSD.Sign,
                contractReportFinancialCertCorrectionCSD.Notes,
                contractReportFinancialCertCorrectionCSD.CertifiedEuAmount,
                contractReportFinancialCertCorrectionCSD.CertifiedBgAmount,
                contractReportFinancialCertCorrectionCSD.CertifiedBfpTotalAmount,
                contractReportFinancialCertCorrectionCSD.CertifiedSelfAmount,
                contractReportFinancialCertCorrectionCSD.CertifiedTotalAmount);
        }

        [HttpPost]
        [Route("{contractReportFinancialCertCorrectionCSDId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.Edit.CSD.ChangeStatusToEnded), IdParam = "contractReportFinancialCertCorrectionCSDId")]
        public void ChangeContractReportFinancialCertCorrectionCSDStatusToEnded(int contractReportFinancialCertCorrectionId, int contractReportFinancialCertCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCertCorrectionService.ChangeContractReportFinancialCertCorrectionCSDStatus(contractReportFinancialCertCorrectionCSDId, vers, ContractReportFinancialCertCorrectionCSDStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportFinancialCertCorrectionCSDId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportFinancialCertCorrectionCSDStatusToEnded(int contractReportFinancialCertCorrectionId, int contractReportFinancialCertCorrectionCSDId)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            var errors = this.contractReportFinancialCertCorrectionService.CanChangeContractReportFinancialCertCorrectionCSDStatusToEnded(contractReportFinancialCertCorrectionCSDId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportFinancialCertCorrectionCSDId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportFinancialCertCorrection.Edit.CSD.ChangeStatusToDraft), IdParam = "contractReportFinancialCertCorrectionCSDId")]
        public void ChangeContractReportFinancialCertCorrectionCSDStatusToDraft(int contractReportFinancialCertCorrectionId, int contractReportFinancialCertCorrectionCSDId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.Edit, contractReportFinancialCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportFinancialCertCorrectionService.ChangeContractReportFinancialCertCorrectionCSDStatus(contractReportFinancialCertCorrectionCSDId, vers, ContractReportFinancialCertCorrectionCSDStatus.Draft);
        }
    }
}
