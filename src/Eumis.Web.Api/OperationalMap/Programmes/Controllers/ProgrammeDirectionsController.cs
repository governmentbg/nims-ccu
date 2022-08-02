using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.OperationalMap.MapNodes;
using Eumis.Web.Api.OperationalMap.Programmes.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.Controllers
{
    [RoutePrefix("api/programmes/{mapNodeId}/directions")]
    public class ProgrammeDirectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProgrammesRepository programmesRepository;

        public ProgrammeDirectionsController(
            IUnitOfWork unitOfWork,
            IProgrammesRepository programmesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.programmesRepository = programmesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<MapNodeDirectionVO> GetProgrammeDirections(int mapNodeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, mapNodeId);

            return this.programmesRepository.GetProgrammeDirections(mapNodeId);
        }

        [Route("{programmeDirectionId:int}")]
        public MapNodeDirectionDO GetProgrammeDirection(int mapNodeId, int programmeDirectionId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, mapNodeId);

            var programme = this.programmesRepository.Find(mapNodeId);

            var programmeDirection = programme.FindMapNodeDirection(programmeDirectionId);

            return new MapNodeDirectionDO(programmeDirection, programme.Version);
        }

        [HttpGet]
        [Route("new")]
        public MapNodeDirectionDO NewProgrammeDirection(int mapNodeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programme = this.programmesRepository.FindWithoutIncludes(mapNodeId);

            return new MapNodeDirectionDO(mapNodeId, programme.Version);
        }

        [HttpPut]
        [Route("{programmeDirectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.Directions.Edit), IdParam = "mapNodeId", ChildIdParam = "programmeDirectionId")]
        public void UpdateProgrammeDirection(int mapNodeId, int programmeDirectionId, MapNodeDirectionDO directionDO)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programme = this.programmesRepository.FindForUpdate(mapNodeId, directionDO.Version);

            programme.UpdateMapNodeDirection(programmeDirectionId, directionDO.DirectionId.Value, directionDO.SubDirectionId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeDirectionId:int}/canUpdate")]
        public ErrorsDO CanUpdateProgrammeDirection(int mapNodeId, int programmeDirectionId, MapNodeDirectionDO directionDO)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programme = this.programmesRepository.Find(mapNodeId);

            var errors = programme.CanUpdateDirection(programmeDirectionId, directionDO.DirectionId.Value, directionDO.SubDirectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.Directions.Create), IdParam = "mapNodeId")]
        public object AddProgrammeDirection(int mapNodeId, MapNodeDirectionDO directionDO)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programme = this.programmesRepository.FindForUpdate(mapNodeId, directionDO.Version);

            var newProgrammeDirection = programme.CreateProgrammeDirection(
                directionDO.DirectionId.Value,
                directionDO.SubDirectionId);

            this.unitOfWork.Save();

            return new { newProgrammeDirection.MapNodeDirectionId };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateProgrammeDirection(int mapNodeId, MapNodeDirectionDO directionDO)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programme = this.programmesRepository.Find(mapNodeId);

            var errors = programme.CanCreateDirection(directionDO.DirectionId.Value, directionDO.SubDirectionId);

            return new ErrorsDO(errors);
        }

        [HttpDelete]
        [Route("{programmeDirectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.Directions.Delete), IdParam = "mapNodeId", ChildIdParam = "programmeDirectionId")]
        public void DeleteProgrammeDirection(int mapNodeId, int programmeDirectionId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programme = this.programmesRepository.FindForUpdate(mapNodeId, version);

            programme.RemoveMapNodeDirection(programmeDirectionId);

            this.unitOfWork.Save();
        }
    }
}
