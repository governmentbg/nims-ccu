using Eumis.ApplicationServices.Services.Monitorstat;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Procedures.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/monitorstatRequests")]
    public class ProcedureMonitorstatRequestsController : ApiController
    {
        private IAuthorizer authorizer;
        private IProcedureMonitorstatRequestsRepository procedureMonitorstatRequestsRepository;
        private IUnitOfWork unitOfWork;
        private IRelationsRepository relationsRepository;
        private IMonitorstatService monitorstatService;

        public ProcedureMonitorstatRequestsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProcedureMonitorstatRequestsRepository procedureMonitorstatRequestsRepository,
            IRelationsRepository relationsRepository,
            IMonitorstatService monitorstatService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.procedureMonitorstatRequestsRepository = procedureMonitorstatRequestsRepository;
            this.relationsRepository = relationsRepository;
            this.monitorstatService = monitorstatService;
        }

        [Route("")]
        public IList<ProcedureMonitorstatRequestsVO> GetProcedureMonitorstatRequests(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.procedureMonitorstatRequestsRepository.GetProcedureRequests(procedureId);
        }

        [Route("{procedureMonitorstatRequestId:int}")]
        public ProcedureMonitorstatRequestDO GetProcedureMonitorstatRequest(int procedureMonitorstatRequestId, int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var request = this.procedureMonitorstatRequestsRepository.Find(procedureMonitorstatRequestId);

            return new ProcedureMonitorstatRequestDO(request);
        }

        [Route("new")]
        public ProcedureMonitorstatRequestDO GetProcedureMonitorstatRequest(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return new ProcedureMonitorstatRequestDO() { Status = ProcedureMonitorstatRequestStatus.Draft };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Monitorstat.CreateRequest), IdParam = "procedureId")]
        public object AttachProcedureMonitorstatDocuments(int procedureId, [FromBody] ProcedureMonitorstatRequestDO request)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedureRequest = new ProcedureMonitorstatRequest(procedureId, request.ExecutionStartDate, request.ExecutionEndDate);
            procedureRequest.UpdateAttributes(request.ExecutionStartDate, request.ExecutionEndDate);

            this.procedureMonitorstatRequestsRepository.Add(procedureRequest);

            this.unitOfWork.Save();

            return new { procedureRequest.ProcedureMonitorstatRequestId };
        }

        [HttpPut]
        [Route("{procedureMonitorstatRequestId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Monitorstat.EditRequest), IdParam = "procedureId", ChildIdParam = "procedureMonitorstatRequestId")]
        public void UpdateProcedureMonitorstatDocuments(int procedureId, int procedureMonitorstatRequestId, [FromBody] ProcedureMonitorstatRequestDO request)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedureRequest = this.procedureMonitorstatRequestsRepository.FindForUpdate(procedureMonitorstatRequestId, request.Version);
            procedureRequest.UpdateAttributes(request.ExecutionStartDate, request.ExecutionEndDate);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{procedureMonitorstatRequestId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Monitorstat.DeleteRequest), IdParam = "procedureId", ChildIdParam = "procedureMonitorstatRequestId")]
        public void DeleteProcedureMonitorstatDocuments(int procedureId, string version, int procedureMonitorstatRequestId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = Convert.FromBase64String(version);

            var request = this.procedureMonitorstatRequestsRepository.FindForUpdate(procedureMonitorstatRequestId, vers);

            request.AssertIsDraft();

            this.procedureMonitorstatRequestsRepository.Remove(request);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{procedureMonitorstatRequestId:int}/canSendRequest")]
        public ErrorsDO CanSendMonitorstatRequest(int procedureId, int procedureMonitorstatRequestId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            IList<string> errors = this.monitorstatService.CanSendProcedureMonitorstatRequest(procedureMonitorstatRequestId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{procedureMonitorstatRequestId:int}/sendRequest")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Monitorstat.SendRequest), IdParam = "procedureId", ChildIdParam = "procedureMonitorstatRequestId")]
        public void SendMonitorstatRequest(int procedureId, int procedureMonitorstatRequestId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = Convert.FromBase64String(version);

            this.monitorstatService.SendProcedureMonitorstatRequest(procedureId, procedureMonitorstatRequestId, vers);
        }
    }
}
