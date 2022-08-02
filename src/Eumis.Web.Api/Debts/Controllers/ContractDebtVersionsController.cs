using Eumis.ApplicationServices.Services.ContractDebt;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain;
using Eumis.Domain.Debts.DataObjects;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/contractDebts/{contractDebtId:int}/versions")]
    public class ContractDebtVersionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractDebtService contractDebtService;
        private IContractDebtVersionsRepository contractDebtVersionsRepository;
        private IContractDebtsRepository contractDebtsRepository;
        private IUsersRepository usersRepository;

        public ContractDebtVersionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractDebtService contractDebtService,
            IContractDebtVersionsRepository contractDebtVersionsRepository,
            IContractDebtsRepository contractDebtsRepository,
            IUsersRepository usersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractDebtService = contractDebtService;
            this.contractDebtVersionsRepository = contractDebtVersionsRepository;
            this.contractDebtsRepository = contractDebtsRepository;
            this.usersRepository = usersRepository;
        }

        [Route("")]
        public IList<ContractDebtVersionVO> GetContractDebtVersions(int contractDebtId)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.View, contractDebtId);

            return this.contractDebtVersionsRepository.GetContractDebtVersions(contractDebtId);
        }

        [Route("{contractDebtVersionId:int}")]
        public ContractDebtVersionDO GetContractDebtVersion(int contractDebtId, int contractDebtVersionId)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.View, contractDebtId);

            var contractDebtVersion = this.contractDebtVersionsRepository.Find(contractDebtVersionId);
            var createdByUser = this.usersRepository.GetUserFullname(contractDebtVersion.CreatedByUserId);

            return new ContractDebtVersionDO(contractDebtVersion, createdByUser);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Versions.Create))]
        public object CreateContractDebtVersion(int contractDebtId)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            var contractDebtVersion = this.contractDebtService.CreateContractDebtVersion(contractDebtId);

            return new { ContractDebtVersionId = contractDebtVersion.ContractDebtVersionId };
        }

        [HttpPut]
        [Route("{contractDebtVersionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Versions.Edit), IdParam = "contractDebtVersionId")]
        public void UpdateContractDebtVersion(int contractDebtId, int contractDebtVersionId, ContractDebtVersionDO contractDebtVersion)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            var version = this.contractDebtVersionsRepository.FindForUpdate(contractDebtVersionId, contractDebtVersion.Version);
            if (!this.contractDebtService.CanUpdateContractDebtVersion(version))
            {
                throw new DomainValidationException("Cannot update contract version.");
            }

            version.UpdateAttributes(
                contractDebtVersion.EuAmount,
                contractDebtVersion.BgAmount,
                contractDebtVersion.TotalAmount,
                contractDebtVersion.CertStatus,
                contractDebtVersion.CertEuAmount,
                contractDebtVersion.CertBgAmount,
                contractDebtVersion.CertTotalAmount,
                contractDebtVersion.CreateNotes,
                contractDebtVersion.ExecutionStatus);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{contractDebtVersionId:int}/updatePartial")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Versions.EditPartial), IdParam = "contractDebtVersionId")]
        public void UpdatePartialContractDebtVersion(int contractDebtId, int contractDebtVersionId, ContractDebtVersionDO contractDebtVersion)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            var version = this.contractDebtVersionsRepository.FindForUpdate(contractDebtVersionId, contractDebtVersion.Version);
            if (!this.contractDebtService.CanUpdateContractDebtVersion(version))
            {
                throw new DomainValidationException("Cannot update contract version.");
            }

            version.UpdateAttributesPartial(
                contractDebtVersion.EuAmount,
                contractDebtVersion.BgAmount,
                contractDebtVersion.TotalAmount,
                contractDebtVersion.CertStatus,
                contractDebtVersion.CertEuAmount,
                contractDebtVersion.CertBgAmount,
                contractDebtVersion.CertTotalAmount,
                contractDebtVersion.CreateNotes,
                contractDebtVersion.ModifyDate);

            if (version.CanChangeStatusToActual().Any())
            {
                throw new DomainException("Requirements for updating an activated version are not met.");
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractDebtVersionId:int}/canUpdatePartial")]
        public ErrorsDO CanUpdatePartialContractDebtVersion(int contractDebtId, int contractDebtVersionId, ContractDebtVersionDO contractDebtVersion)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            var version = this.contractDebtVersionsRepository.FindForUpdate(contractDebtVersionId, contractDebtVersion.Version);
            if (!this.contractDebtService.CanUpdateContractDebtVersion(version))
            {
                throw new DomainValidationException("Cannot update contract version.");
            }

            version.UpdateAttributesPartial(
                contractDebtVersion.EuAmount,
                contractDebtVersion.BgAmount,
                contractDebtVersion.TotalAmount,
                contractDebtVersion.CertStatus,
                contractDebtVersion.CertEuAmount,
                contractDebtVersion.CertBgAmount,
                contractDebtVersion.CertTotalAmount,
                contractDebtVersion.CreateNotes,
                contractDebtVersion.ModifyDate);

            var errors = version.CanChangeStatusToActual();
            return new ErrorsDO(errors);
        }

        [HttpDelete]
        [Route("{contractDebtVersionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Versions.Delete), IdParam = "contractDebtVersionId")]
        public void DeleteContractDebtVersion(int contractDebtId, int contractDebtVersionId, string version)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            byte[] vers = System.Convert.FromBase64String(version);

            var contractDebtVersion = this.contractDebtVersionsRepository.FindForUpdate(contractDebtVersionId, vers);
            if (!this.contractDebtService.CanDeleteContractDebtVersion(contractDebtVersion))
            {
                throw new DomainValidationException("Cannot delete contract version.");
            }

            this.contractDebtVersionsRepository.Remove(contractDebtVersion);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{contractDebtVersionId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractDebts.Edit.Versions.ChangeStatusToActual), IdParam = "contractDebtVersionId")]
        public void ChangeContractDebtVersionStatusToActual(int contractDebtId, int contractDebtVersionId, string version)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractDebtService.ChangeContractDebtVersionStatusToActual(contractDebtVersionId, vers);
        }

        [HttpPost]
        [Route("{contractDebtVersionId:int}/canChangeStatusToActual")]
        public ErrorsDO CanChangeContractDebtVersionStatusToActual(int contractDebtId, int contractDebtVersionId)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            var contractDebtVersion = this.contractDebtVersionsRepository.Find(contractDebtVersionId);
            var errors = contractDebtVersion.CanChangeStatusToActual();

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractDebtVersion(int contractDebtId)
        {
            this.authorizer.AssertCanDo(ContractDebtActions.Edit, contractDebtId);

            var errors = this.contractDebtService.CanCreateContractDebtVersion(contractDebtId);

            return new ErrorsDO(errors);
        }
    }
}
