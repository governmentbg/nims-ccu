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

namespace Eumis.Web.Api.OperationalMap.SubDirections.Controllers
{
    [RoutePrefix("api/directions/{directionId}/subDirections")]
    public class SubDirectionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IDirectionsRepository directionsRepository;

        public SubDirectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IDirectionsRepository directionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.directionsRepository = directionsRepository;
        }

        [Route("")]
        public IList<SubDirectionVO> GetSubDirections(int directionId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var subDirections = this.directionsRepository.GetSubDirections(directionId);
            return subDirections;
        }

        [Route("{subDirectionId:int}")]
        public SubDirectionDO GetSubDirection(int directionId, int subDirectionId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var direction = this.directionsRepository.Find(directionId);
            var subDirection = direction.SubDirections.FirstOrDefault(t => t.SubDirectionId == subDirectionId);

            return new SubDirectionDO(subDirection, direction.Version);
        }

        [HttpGet]
        [Route("new")]
        public SubDirectionDO NewSubDirection(int directionId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var directionExists = this.directionsRepository.FindWithoutIncludes(directionId);

            return new SubDirectionDO()
            {
                DirectionId = directionExists.DirectionId,
                Version = directionExists.Version,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Direction.Edit))]
        public object CreateSubDirection(int directionId, SubDirectionDO subDirection)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var direction = this.directionsRepository.FindForUpdate(directionId, subDirection.Version);
            var subdirection = direction.AddSubDirection(subDirection.Name, subDirection.NameAlt);

            this.unitOfWork.Save();

            return new { DirectionId = subdirection.DirectionId, SubDirectionId = subdirection.SubDirectionId };
        }

        [HttpPut]
        [Route("{subDirectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Direction.Edit), IdParam = "directionId")]
        public void UpdateSubDirection(int directionId, int subDirectionId, SubDirectionDO subDirection)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var direction = this.directionsRepository.FindForUpdate(directionId, subDirection.Version);
            direction.UpdateSubDirection(subDirectionId, subDirection.Name, subDirection.NameAlt);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{subDirectionId:int}/canDelete")]
        public IList<string> DeleteSubDirection(int directionId, int subDirectionId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            return this.directionsRepository.CanDeleteSubDirection(directionId, subDirectionId);
        }

        [HttpDelete]
        [Route("{subDirectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Direction.Edit), IdParam = "directionId")]
        public void DeleteSubDirection(int directionId, int subDirectionId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var direction = this.directionsRepository.FindForUpdate(directionId, version);
            direction.RemoveSubDirection(subDirectionId);

            this.unitOfWork.Save();
        }
    }
}
