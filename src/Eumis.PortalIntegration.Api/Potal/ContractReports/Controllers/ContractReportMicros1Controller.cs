using System;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractReportMicro;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractReports.DataObjects;

namespace Eumis.PortalIntegration.Api.Portal.ContractReports.Controllers
{
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/packages/{contractReportGid:guid}/micros")]
    public class ContractReportMicros1Controller : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportMicrosRepository contractReportMicrosRepository;
        private IContractReportMicroService contractReportMicroService;

        public ContractReportMicros1Controller(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportsRepository contractReportsRepository,
            IContractReportMicrosRepository contractReportMicrosRepository,
            IContractReportMicroService contractReportMicroService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportMicrosRepository = contractReportMicrosRepository;
            this.contractReportMicroService = contractReportMicroService;
        }

        [Route("{contractReportMicroGid:guid}/items")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public IHttpActionResult GetContractReportMicroItems(Guid contractGid, Guid contractReportGid, Guid contractReportMicroGid, int offset = 0, int? limit = null)
        {
            var microType = this.contractReportMicrosRepository.GetMicroType(contractReportMicroGid);

            switch (microType)
            {
                case ContractReportMicroType.Type1:
                    return this.Ok(this.contractReportMicrosRepository.GetPortalType1Items(contractReportMicroGid, offset, limit));
                case ContractReportMicroType.Type2:
                    return this.Ok(this.contractReportMicrosRepository.GetPortalType2Items(contractReportMicroGid, offset, limit));
                case ContractReportMicroType.Type3:
                    return this.Ok(this.contractReportMicrosRepository.GetPortalType3Items(contractReportMicroGid, offset, limit));
                case ContractReportMicroType.Type4:
                    return this.Ok(this.contractReportMicrosRepository.GetPortalType4Items(contractReportMicroGid, offset, limit));
                default:
                    throw new DomainException("Invalid micto type");
            }
        }

        [HttpGet]
        [Route("{contractReportMicroGid:guid}/hasFile/{fileKey:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public bool CheckMicroHasFile(Guid contractGid, Guid contractReportGid, Guid contractReportMicroGid, Guid fileKey)
        {
            return this.contractReportMicrosRepository.CheckMicroHasFile(contractReportMicroGid, fileKey);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void CreateContractReportMicro(Guid contractGid, Guid contractReportGid, ContractReportMicroType type)
        {
            var contractReport = this.contractReportsRepository.Find(contractReportGid);
            this.contractReportMicroService.CreateContractReportMicro(contractReport, type, Eumis.Domain.Contracts.Source.Beneficiary);

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportMicro.Create),
                contractReport.ContractReportId,
                null,
                null,
                null);
        }

        [HttpPut]
        [Route("{contractReportMicroGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO UpdateContractReportMicro(Guid contractGid, Guid contractReportGid, Guid contractReportMicroGid, ContractReportMicroDO microDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroGid, microDO.Version);
                var errors = this.contractReportMicroService.UpdateContractReportMicro(micro, microDO.ExcelBlobKey.Value, microDO.ExcelName, out var warnings);

                var response = new ErrorsDO(errors, warnings);

                this.actionLogger.LogAction(
                    typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportMicro.UpdateExcel),
                    micro.ContractReportId,
                    micro.ContractReportMicroId,
                    null,
                    null);

                if (errors.Count == 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }

                return response;
            }
        }

        [HttpPut]
        [Route("{contractReportMicroGid:guid}/withSimevCode")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO UpdateContractReportMicroWithSimevCode(Guid contractGid, Guid contractReportGid, Guid contractReportMicroGid, ContractReportMicroSimevDO microDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroGid, microDO.Version);
                var errors = this.contractReportMicroService.UpdateContractReportMicroWithSimevCode(micro, microDO.SimevCode, out var warnings);

                var response = new ErrorsDO(errors, warnings);

                this.actionLogger.LogAction(
                    typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportMicro.UpdateExcelWithSimevCode),
                    micro.ContractReportId,
                    micro.ContractReportMicroId,
                    microDO,
                    response);

                if (errors.Count == 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }

                return response;
            }
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportMicroGid:guid}/enter")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void EnterContractReportMicro(Guid contractGid, Guid contractReportGid, Guid contractReportMicroGid, VersionDO version)
        {
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroGid, version.Version);
            this.contractReportMicroService.ChangeContractReportMicroStatus(micro, ContractReportMicroStatus.Entered);

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportMicro.ChangeStatusToEntered),
                micro.ContractReportId,
                micro.ContractReportMicroId,
                version,
                null);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportMicroGid:guid}/canEnter")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO CanEnterContractReportMicro(Guid contractGid, Guid contractReportGid, Guid contractReportMicroGid)
        {
            var micro = this.contractReportMicrosRepository.Find(contractReportMicroGid);

            var errors = this.contractReportMicroService.CanChangeContractReportMicroStatusToEntered(micro);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportMicroGid:guid}/makeDraft")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DraftContractReportMicro(Guid contractGid, Guid contractReportGid, Guid contractReportMicroGid, VersionDO version)
        {
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroGid, version.Version);
            this.contractReportMicroService.ChangeContractReportMicroStatus(micro, ContractReportMicroStatus.Draft);

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportMicro.ChangeStatusToDraft),
                micro.ContractReportId,
                micro.ContractReportMicroId,
                version,
                null);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportMicroGid:guid}/makeActual")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void ActualContractReportMicro(Guid contractGid, Guid contractReportGid, Guid contractReportMicroGid, VersionDO version)
        {
            var regId = this.accessContext.ContractRegistrationId;
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroGid, version.Version);
            this.contractReportMicroService.ChangeContractReportMicroStatus(micro, ContractReportMicroStatus.Actual, regId);

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportMicro.ChangeStatusToActual),
                micro.ContractReportId,
                micro.ContractReportMicroId,
                version,
                null);
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{contractReportMicroGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteContractReportMicro(Guid contractGid, Guid contractReportGid, Guid contractReportMicroGid, VersionDO version)
        {
            var micro = this.contractReportMicrosRepository.FindForUpdate(contractReportMicroGid, version.Version);
            this.contractReportMicroService.DeleteContractReportMicro(micro);

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractReports.Edit.ContractReportMicro.Delete),
                micro.ContractReportId,
                micro.ContractReportMicroId,
                null,
                null);
        }

        [HttpPost]
        [Route("canCreate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO CanCreateContractReportMicro(Guid contractGid, Guid contractReportGid, ContractReportMicroType type)
        {
            var contractReportId = this.contractReportsRepository.GetContractReportId(contractReportGid);

            var errorList = this.contractReportMicroService.CanCreateContractReportMicro(contractReportId, type);

            return new ErrorsDO(errorList);
        }
    }
}
