using Eumis.ApplicationServices.Services.ContractReportMicro;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/micros")]
    public class ContractReportMicrosController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportMicrosRepository contractReportMicrosRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportMicroService contractReportMicroService;

        public ContractReportMicrosController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportMicrosRepository contractReportMicrosRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportMicroService contractReportMicroService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportMicrosRepository = contractReportMicrosRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportMicroService = contractReportMicroService;
        }

        [Route("")]
        public IList<ContractReportMicroVO> GetContractReportMicros(int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsNotDraftFromBeneficiary();

            return this.contractReportMicrosRepository.GetContractReportMicros(contractReportId);
        }

        [Route("{contractReportMicroId:int}")]
        public ContractReportMicroDO GetContractReportMicro(int contractReportId, int contractReportMicroId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var micro = this.contractReportMicrosRepository.Find(contractReportMicroId);

            if (micro.Source == Source.Beneficiary && (micro.Status == ContractReportMicroStatus.Draft || micro.Status == ContractReportMicroStatus.Entered))
            {
                throw new UnauthorizedAccessException("Cannot get ContractReportMicro with status 'Draft' or 'Entered' when the microdata has source 'Beneficiary'");
            }

            return new ContractReportMicroDO(micro);
        }

        [HttpPost]
        [Transaction]
        [Route("")]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.Create), IdParam = "contractReportId")]
        public object CreateContractReportMicro(int contractReportId, ContractReportMicroType type)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            var micro = this.contractReportMicroService.CreateContractReportMicro(contractReport, type, Source.AdministrativeAuthority);

            return new { ContractReportMicroId = micro.ContractReportMicroId };
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateContractReportMicro(int contractReportId, ContractReportMicroType type)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var errorList = this.contractReportMicroService.CanCreateContractReportMicro(contractReportId, type);

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{contractReportMicroId:int}")]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.UpdateExcel), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        public ErrorsDO UpdateContractReportMicro(int contractReportId, int contractReportMicroId, ContractReportMicroDO microDO)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.Edit, contractReportId));

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroId, microDO.Version);
                var errors = this.contractReportMicroService.UpdateContractReportMicro(micro, microDO.File.Key, microDO.File.Name, out var dummy);

                var response = new ErrorsDO(errors);

                if (errors.Count == 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }

                return new ErrorsDO(errors);
            }
        }

        [HttpDelete]
        [Route("{contractReportMicroId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.Delete), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        public void DeleteContractReportMicro(int contractReportId, int contractReportMicroId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroId, vers);

            this.contractReportMicroService.DeleteContractReportMicro(micro);
        }

        [HttpPost]
        [Route("{contractReportMicroId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.ChangeStatusToDraft), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        public void ChangeContractReportMicroStatusToDraft(int contractReportId, int contractReportMicroId, string version)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.Edit, contractReportId));

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroId, vers);

            this.contractReportMicroService.ChangeContractReportMicroStatus(micro, ContractReportMicroStatus.Draft);
        }

        [HttpPost]
        [Transaction]
        [Route("{contractReportMicroId:int}/changeStatusToEntered")]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.ChangeStatusToEntered), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        public void EnterContractReportMicro(int contractReportId, int contractReportMicroId, string version)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.Edit, contractReportId));

            byte[] vers = System.Convert.FromBase64String(version);
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroId, vers);

            this.contractReportMicroService.ChangeContractReportMicroStatus(micro, ContractReportMicroStatus.Entered);
        }

        [Route("{contractReportMicroId:int}/canChangeStatusToEntered")]
        public ErrorsDO CanChangeContractReportMicroStatusToEntered(int contractReportId, int contractReportMicroId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.Edit, contractReportId));

            var micro = this.contractReportMicrosRepository.Find(contractReportMicroId);
            var errors = this.contractReportMicroService.CanChangeContractReportMicroStatusToEntered(micro);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportMicroId:int}/changeStatusToReturned")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.ChangeStatusToReturned), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        public void ChangeContractReportMicroStatusToReturned(int contractReportId, int contractReportMicroId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroId, vers);

            this.contractReportMicroService.ChangeContractReportMicroStatusToReturned(micro, confirm.Note);
        }

        [HttpPost]
        [Route("{contractReportMicroId:int}/canChangeStatusToReturned")]
        public ErrorsDO CanChangeContractReportMicroStatusToReturned(int contractReportId, int contractReportMicroId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            var type = this.contractReportMicrosRepository.GetMicroType(contractReportMicroId);
            var errors = this.contractReportMicroService.CanChangeContractReportMicroStatusToReturned(contractReportId, type);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportMicroId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.ChangeStatusToActual), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        public void ChangeContractReportMicroStatusToActual(int contractReportId, int contractReportMicroId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroId, vers);

            this.contractReportMicroService.ChangeContractReportMicroStatus(micro, ContractReportMicroStatus.Actual);
        }
    }
}
