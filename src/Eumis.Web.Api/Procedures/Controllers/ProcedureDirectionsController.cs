using ClosedXML.Excel;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.OperationalMap.MapNodes;
using Eumis.Web.Api.OperationalMap.Programmes.DataObjects;
using Eumis.Web.Api.Procedures.DataObjects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/directions")]
    public class ProcedureDirectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProceduresRepository proceduresRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;

        public ProcedureDirectionsController(
            IUnitOfWork unitOfWork,
            IProceduresRepository proceduresRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
        }

        [Route("")]
        public IList<ProcedureDirectionVO> GetProcedureDirections(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureDirections(procedureId);
        }

        [Route("{procedureDirectionId:int}")]
        public ProcedureDirectionDO GetProcedureDirection(int procedureId, int procedureDirectionId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureDirection = procedure.FindProcedureDirection(procedureDirectionId);

            return new ProcedureDirectionDO(procedureDirection, procedure.Version);
        }

        [HttpGet]
        [Route("unattachedDirections")]
        public IList<MapNodeDirectionVO> GetUnattachedDirections(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);
            var directions = this.proceduresRepository.GetProgrammePriorityDirections(procedureId);

            return directions;
        }

        [HttpPost]
        [Transaction]
        [Route("")]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Directions.Create), IdParam = "procedureId")]
        public void AddProcedureDirections(int procedureId, int[] mapNodeDirectionsIds, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.FindForUpdate(procedureId, version);
            var procedureData = this.proceduresRepository.GetProcedureParentData(procedureId);

            var pPriority = this.programmePrioritiesRepository.Find(procedureData.ProgrammePriorityId);

            pPriority.MapNodeDirections
                .Where(x => mapNodeDirectionsIds.Contains(x.MapNodeDirectionId))
                .ForEach(x => procedure.AddProcedureDirection(x.MapNodeId, x.DirectionId, x.SubDirectionId));

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureDirectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Directions.Edit), IdParam = "procedureId", ChildIdParam = "procedureDirectionId")]
        public void UpdateProcedureDirection(int procedureId, int procedureDirectionId, ProcedureDirectionDO directionDO)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.FindForUpdate(procedureId, directionDO.Version);

            procedure.UpdateProcedureDirection(procedureDirectionId, directionDO.Amount);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{procedureDirectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Directions.Delete), IdParam = "procedureId", ChildIdParam = "procedureDirectionId")]
        public void DeleteProcedureDirection(int procedureId, int procedureDirectionId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.FindForUpdate(procedureId, version);

            procedure.RemoveProcedureDirection(procedureDirectionId);

            this.unitOfWork.Save();
        }
    }
}
