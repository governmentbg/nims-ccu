using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
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
    [RoutePrefix("api/contractReports/{contractReportId:int}/technicals")]
    public class ContractReportTechnicalsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportService contractReportService;

        public ContractReportTechnicalsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportService contractReportService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportService = contractReportService;
        }

        [Route("")]
        public IList<ContractReportTechnicalVO> GetContractReportTechnicals(int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var contractReport = this.contractReportsRepository.Find(contractReportId);

            contractReport.AssertIsNotDraftFromBeneficiary();

            return this.contractReportTechnicalsRepository.GetContractReportTechnicals(contractReportId);
        }

        [Route("{contractReportTechnicalId:int}")]
        public ContractReportTechnicalDO GetContractReportTechnical(int contractReportId, int contractReportTechnicalId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.View, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.View, contractReportId));

            var technical = this.contractReportTechnicalsRepository.FindWithoutIncludes(contractReportTechnicalId);

            var contractReport = this.contractReportsRepository.FindWithoutIncludes(contractReportId);

            if (contractReport.Source == Source.Beneficiary && (technical.Status == ContractReportTechnicalStatus.Draft || technical.Status == ContractReportTechnicalStatus.Entered))
            {
                throw new UnauthorizedAccessException("Cannot get ContractReportTechnical with status 'Draft' or 'Entered' when the ContractReport has source 'Beneficiary'");
            }

            return new ContractReportTechnicalDO(technical);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnical.Create), IdParam = "contractReportId")]
        public object CreateContractReportTechnical(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            var newContractReportTechnical = this.contractReportService.CreateContractReportTechnical(contractReportId);

            return new { ContractReportTechnicalId = newContractReportTechnical.ContractReportTechnicalId };
        }

        [HttpDelete]
        [Route("{contractReportTechnicalId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnical.Delete), IdParam = "contractReportId", ChildIdParam = "contractReportTechnicalId")]
        public void DeleteContractReportTechnical(int contractReportId, int contractReportTechnicalId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.DeleteContractReportTechnical(contractReportId, contractReportTechnicalId, vers);
        }

        [HttpPost]
        [Route("{contractReportTechnicalId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnical.ChangeStatusToDraft), IdParam = "contractReportId", ChildIdParam = "contractReportTechnicalId")]
        public void ChangeContractReportTechnicalStatusToDraft(int contractReportId, int contractReportTechnicalId, string version)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractReportActions.Edit, contractReportId),
                Tuple.Create<Enum, int?>(ContractReportCheckActions.EditTechnical, contractReportId));

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportTechnicalStatus(contractReportId, contractReportTechnicalId, vers, Domain.Contracts.ContractReportTechnicalStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportTechnicalId:int}/changeStatusToReturned")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnical.ChangeStatusToReturned), IdParam = "contractReportId", ChildIdParam = "contractReportTechnicalId")]
        public void ChangeContractReportTechnicalStatusToReturned(int contractReportId, int contractReportTechnicalId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportTechnicalStatusToReturned(contractReportId, contractReportTechnicalId, vers, confirm.Note);
        }

        [HttpPost]
        [Route("{contractReportTechnicalId:int}/changeStatusToActual")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportTechnical.ChangeStatusToActual), IdParam = "contractReportId", ChildIdParam = "contractReportTechnicalId")]
        public void ChangeContractReportTechnicalStatusToActual(int contractReportId, int contractReportTechnicalId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            var contractReport = this.contractReportsRepository.Find(contractReportId);
            contractReport.AssertIsNotBeneficiary();

            byte[] vers = System.Convert.FromBase64String(version);
            this.contractReportService.ChangeContractReportTechnicalStatus(contractReportId, contractReportTechnicalId, vers, Domain.Contracts.ContractReportTechnicalStatus.Actual);
        }

        [HttpPost]
        [Route("{contractReportTechnicalId:int}/canChangeStatusToReturned")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO CanChangeContractReportTechnicalStatusToReturned(int contractReportId, int contractReportTechnicalId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            var errors = this.contractReportService.CanChangeContractReportTechnicalStatusToReturned(contractReportId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateContractReportTechnical(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportActions.Edit, contractReportId);

            var errorList = this.contractReportService.CanCreateContractReportTechnical(contractReportId);

            return new ErrorsDO(errorList);
        }
    }
}
