using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
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
    [RoutePrefix("api/programmePriorities/{mapNodeId}/directions")]
    public class ProgrammePriorityDirectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;

        public ProgrammePriorityDirectionsController(
            IUnitOfWork unitOfWork,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<MapNodeDirectionVO> GetProgrammePriorityDirections(int mapNodeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, mapNodeId);

            return this.programmePrioritiesRepository.GetProgrammePriorityDirections(mapNodeId);
        }

        [Route("{programmeDirectionId:int}")]
        public MapNodeDirectionDO GetProgrammePriorityDirection(int mapNodeId, int programmeDirectionId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, mapNodeId);

            var programmePriority = this.programmePrioritiesRepository.Find(mapNodeId);

            var direction = programmePriority.FindMapNodeDirection(programmeDirectionId);

            return new MapNodeDirectionDO(direction, programmePriority.Version);
        }

        [HttpGet]
        [Route("new")]
        public MapNodeDirectionDO NewProgrammePriorityDirection(int mapNodeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programme = this.programmePrioritiesRepository.FindWithoutIncludes(mapNodeId);

            return new MapNodeDirectionDO(mapNodeId, programme.Version);
        }

        [HttpPut]
        [Route("{programmePriorityDirectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.Edit.Directions.Edit), IdParam = "mapNodeId", ChildIdParam = "programmePriorityDirectionId")]
        public void UpdateProgrammePriorityDirection(int mapNodeId, int programmePriorityDirectionId, MapNodeDirectionDO directionDO)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programme = this.programmePrioritiesRepository.FindForUpdate(mapNodeId, directionDO.Version);

            programme.UpdateMapNodeDirection(programmePriorityDirectionId, directionDO.DirectionId.Value, directionDO.SubDirectionId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{mapNodeDirectionId:int}/canUpdate")]
        public ErrorsDO CanUpdateProgrammePriorityDirection(int mapNodeId, int mapNodeDirectionId, MapNodeDirectionDO directionDO)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programmePriority = this.programmePrioritiesRepository.Find(mapNodeId);

            var errors = programmePriority.CanUpdateDirection(mapNodeDirectionId, directionDO.DirectionId.Value, directionDO.SubDirectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.Edit.Directions.Create), IdParam = "mapNodeId")]
        public object AddProgrammePriorityDirection(int mapNodeId, MapNodeDirectionDO directionDO)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programme = this.programmePrioritiesRepository.FindForUpdate(mapNodeId, directionDO.Version);

            var newMapNodeDirection = programme.CreateProgrammeDirection(
                directionDO.DirectionId.Value,
                directionDO.SubDirectionId);

            this.unitOfWork.Save();

            return new { newMapNodeDirection.MapNodeDirectionId };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateProgrammePriorityDirection(int mapNodeId, MapNodeDirectionDO directionDO)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programmePriority = this.programmePrioritiesRepository.Find(mapNodeId);

            var errors = programmePriority.CanCreateDirection(directionDO.DirectionId.Value, directionDO.SubDirectionId);

            return new ErrorsDO(errors);
        }

        [HttpDelete]
        [Route("{programmeDirectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.Edit.Directions.Delete), IdParam = "mapNodeId", ChildIdParam = "programmeDirectionId")]
        public void DeleteProgrammePriorityDirection(int mapNodeId, int programmeDirectionId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, mapNodeId);

            var programmePriority = this.programmePrioritiesRepository.FindForUpdate(mapNodeId, version);

            programmePriority.RemoveMapNodeDirection(programmeDirectionId);

            this.unitOfWork.Save();
        }
    }
}
