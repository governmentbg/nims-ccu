using Eumis.ApplicationServices.Services.ContractReportMicro;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/microNewVersion")]
    public class ContractReportMicroNewVersionController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportMicrosRepository contractReportMicrosRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportMicroService contractReportMicroService;

        public ContractReportMicroNewVersionController(
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

        [HttpGet]
        [Route("{contractReportMicroId:int}/canCreate")]
        public ErrorsDO CanCreateContractReportMicroVersion(int contractReportId, int contractReportMicroId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.Edit, contractReportId));

            var micro = this.contractReportMicrosRepository.Find(contractReportMicroId);
            var errors = this.contractReportMicroService.CanCreateContractReportMicroNewVersion(micro);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportMicroId:int}/Create")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.NewVersion.Create), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        public void CreateContractReportMicroVersion(int contractReportId, int contractReportMicroId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            var micro = this.contractReportMicrosRepository.Find(contractReportMicroId);

            this.contractReportMicroService.CreateContractReportMicroNewVersion(micro);
        }

        [HttpPut]
        [Route("")]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.NewVersion.Update), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        public ErrorsDO UpdateContractReportMicro(int contractReportId, ContractReportMicroDO microDO)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.Edit, contractReportId));

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                IList<string> errors = new List<string>();
                var micro = this.contractReportMicrosRepository.FindForUpdate(microDO.ContractReportMicroId, microDO.Version);

                if (micro.Type != ContractReportMicroType.Type2)
                {
                    errors.Add("Нова версия се създава само за микорданни ЕСФ");
                }

                if (micro.Status != ContractReportMicroStatus.Draft)
                {
                    errors.Add("Статуса на микроданните трябва да бъде \"Чернова\"");
                }

                if (errors.Count == 0)
                {
                    errors = this.contractReportMicroService.UpdateContractReportMicroNewVersion(micro, microDO.File.Key, microDO.File.Name, out var dummy);
                }

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

        [HttpPost]
        [Route("{contractReportMicroId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.NewVersion.ChangeStatusToActual), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        public void ChangeContractReportMicroStatusToActual(int contractReportId, int contractReportMicroId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.Edit, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroId, vers);

            var actualMicro = this.contractReportMicrosRepository.GetActualContractReportMicro(contractReportId, micro.Type);

            this.contractReportMicroService.ChangeContractReportNewVersionMicroStatusToActual(micro, actualMicro, confirm.Note);
        }

        [HttpGet]
        [Route("{contractReportMicroId:int}/canDelete")]
        public ErrorsDO CanDeleteContractReportMicro(int contractReportId, int contractReportMicroId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.Edit, contractReportId));

            var micro = this.contractReportMicrosRepository.Find(contractReportMicroId);
            var errors = this.contractReportMicroService.CanDeleteContractReportMicroNewVersion(micro);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportMicroId:int}/delete")]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportMicro.NewVersion.Delete), IdParam = "contractReportId", ChildIdParam = "contractReportMicroId")]
        [Transaction]
        public void DeleteContractReportMicro(int contractReportId, int contractReportMicroId, string version)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.Edit, contractReportId));

            byte[] vers = System.Convert.FromBase64String(version);
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroId, vers);

            this.contractReportMicroService.DeleteContractReportMicroNewVersion(micro);
        }
    }
}
