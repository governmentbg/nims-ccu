using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procedures.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/timeLimits")]
    public class ProcedureTimeLimitsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;

        public ProcedureTimeLimitsController(IUnitOfWork unitOfWork, IProceduresRepository proceduresRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProcedureTimeLimitsVO> GetProcedureTimeLimits(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureTimeLimits(procedureId);
        }

        [Route("{procedureTimeLimitId:int}")]
        public ProcedureTimeLimitDO GetProcedureShare(int procedureId, int procedureTimeLimitId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureTimeLimit = procedure.FindProcedureTimeLimit(procedureTimeLimitId);

            return new ProcedureTimeLimitDO(procedureTimeLimit, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureTimeLimitDO NewProcedureTimeLimit(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var version = this.proceduresRepository.GetVersion(procedureId);

            return new ProcedureTimeLimitDO(procedureId, version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.TimeLimits.Create), IdParam = "procedureId")]
        public void AddProcedureTimeLimit(int procedureId, ProcedureTimeLimitDO procedureTimeLimit)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureTimeLimit.Version);

            procedure.AddProcedureTimeLimit(
                procedureTimeLimit.EndDate.Value.AddMilliseconds(procedureTimeLimit.EndTime.Value),
                procedureTimeLimit.Notes);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureTimeLimitId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.TimeLimits.Edit), IdParam = "procedureId", ChildIdParam = "procedureTimeLimitId")]
        public void UpdateProcedureTimeLimit(int procedureId, int procedureTimeLimitId, ProcedureTimeLimitDO procedureTimeLimit)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureTimeLimit.Version);

            procedure.UpdateProcedureTimeLimit(
                procedureTimeLimitId,
                procedureTimeLimit.EndDate.Value.AddMilliseconds(procedureTimeLimit.EndTime.Value),
                procedureTimeLimit.Notes);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{procedureTimeLimitId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.TimeLimits.Delete), IdParam = "procedureId", ChildIdParam = "procedureTimeLimitId")]
        public void DeleteProcedureTimeLimit(int procedureId, int procedureTimeLimitId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureTimeLimit(procedureTimeLimitId);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("isValidEndTime")]
        public bool IsValidEndTime(int procedureId, DateTime endDate, int endTime, int? procedureTimeLimitId = null)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var endDateTime = endDate.AddMilliseconds(endTime);

            return this.proceduresRepository.IsValidProcedureTimeLimitEndTime(procedureId, endDateTime, procedureTimeLimitId);
        }
    }
}
