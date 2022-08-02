using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Directions.Repositories;
using Eumis.Data.OperationalMap.Directions.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.OperationalMap.Directions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.OperationalMap.Directions.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Directions.Controllers
{
    [RoutePrefix("api/directions")]
    public class DirectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IDirectionsRepository directionsRepository;

        public DirectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IDirectionsRepository directionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.directionsRepository = directionsRepository;
        }

        [Route("")]
        public IList<DirectionVO> GetDirections()
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var directionItems = this.directionsRepository.GetDirectionItems();
            return directionItems;
        }

        [Route("{directionId:int}")]
        public DirectionDO GetDirection(int directionId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var direction = this.directionsRepository.Find(directionId);

            return new DirectionDO(direction);
        }

        [HttpGet]
        [Route("new")]
        public DirectionDO NewDirection()
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            return new DirectionDO
            {
                Status = DirectionStatus.Draft,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Direction.Create))]
        public object CreateDirection(DirectionDO direction)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var newDirection = new Direction(
                direction.Name,
                direction.NameAlt);

            this.directionsRepository.Add(newDirection);
            this.unitOfWork.Save();

            return new { DirectionId = newDirection.DirectionId };
        }

        [HttpPut]
        [Route("{directionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Direction.Edit), IdParam = "directionId")]
        public void UpdateDirection(int directionId, DirectionDO direction)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            Direction oldDirection = this.directionsRepository.FindForUpdate(directionId, direction.Version);

            oldDirection.UpdateAttributes(
                direction.Name,
                direction.NameAlt);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{directionId:int}/changeStatusToDraft")]
        [ActionLog(Action = typeof(ActionLogGroups.Direction.ChangeStatusToDraft), IdParam = "directionId")]
        public void ChangeStatusToDraft(int directionId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            byte[] vers = System.Convert.FromBase64String(version);
            var direction = this.directionsRepository.FindForUpdate(directionId, vers);
            direction.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [Route("{directionId:int}/info")]
        public DirectionInfoVO GetMapNodeInfo(int directionId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            return this.directionsRepository.GetDirectionInfo(directionId);
        }

        [HttpPost]
        [Route("{directionId:int}/changeStatusToEntered")]
        [ActionLog(Action = typeof(ActionLogGroups.Direction.ChangeStatusToEntered), IdParam = "directionId")]
        public void ChangeStatusToEntered(int directionId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            byte[] vers = System.Convert.FromBase64String(version);
            var direction = this.directionsRepository.FindForUpdate(directionId, vers);
            direction.ChangeStatusToEntered();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{directionId:int}/canDelete")]
        public IList<string> CanDeleteDirection(int directionId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            return this.directionsRepository.CanDeleteDirection(directionId);
        }

        [HttpDelete]
        [Route("{directionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Direction.Delete), IdParam = "directionId")]
        public void DeleteDirection(int directionId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var direction = this.directionsRepository.FindForUpdate(directionId, version);
            this.directionsRepository.Remove(direction);

            this.unitOfWork.Save();
        }
    }
}
