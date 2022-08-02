using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.OperationalMap.ProgrammePriorities.DataObjects;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.Controllers
{
    [RoutePrefix("api/programmePriorities")]
    public class ProgrammePrioritiesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IMapNodesRepository mapNodesRepository;
        private IAuthorizer authorizer;

        public ProgrammePrioritiesController(IUnitOfWork unitOfWork, IProgrammePrioritiesRepository programmePrioritiesRepository, IMapNodesRepository mapNodesRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.mapNodesRepository = mapNodesRepository;
            this.authorizer = authorizer;
        }

        [Route("~/api/programmes/{programmeId:int}/programmePriorities")]
        public IList<ProgrammePriorityItemVO> GetProgrammePriorities(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            return this.programmePrioritiesRepository.GetProgrammePriorityItems(programmeId, System.Array.Empty<int>());
        }

        [Route("{programmePriorityId:int}")]
        public ProgrammePriorityDO GetProgrammePriority(int programmePriorityId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmePriorityId);

            var programmePriority = this.programmePrioritiesRepository.Find(programmePriorityId);

            return new ProgrammePriorityDO(programmePriority);
        }

        [HttpGet]
        [Route("new")]
        public ProgrammePriorityDO NewProgrammePriority(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.CreateProgrammePriority, programmeId);

            return new ProgrammePriorityDO
            {
                ProgrammeId = programmeId,
                Status = MapNodeStatus.Draft,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.Create))]
        public object CreateProgrammePriority(ProgrammePriorityDO programmePriority)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.CreateProgrammePriority, programmePriority.ProgrammeId);

            if (this.programmePrioritiesRepository.CanCreateProgrammePriority(programmePriority.ProgrammeId).Any())
            {
                throw new DomainValidationException("Cannot create new programme priority.");
            }

            if (this.programmePrioritiesRepository.CanModifyProgrammePriority(
                programmePriority.ProgrammeId,
                null,
                programmePriority.Code,
                programmePriority.Name).Any())
            {
                throw new DomainValidationException("Cannot create new programme priority.");
            }

            var newProgrammePriority = new ProgrammePriority(
                programmePriority.Code,
                programmePriority.Name,
                programmePriority.NameAlt,
                programmePriority.Description,
                programmePriority.DescriptionAlt,
                programmePriority.CompanyData.ProgrammePriorityType.Value,
                programmePriority.CompanyData.CompanyId.Value,
                programmePriority.CompanyData.HigherOrderCompanyId);

            this.programmePrioritiesRepository.Add(newProgrammePriority);
            this.unitOfWork.Save();

            newProgrammePriority.AddProgrammeRelation(programmePriority.ProgrammeId);
            this.unitOfWork.Save();

            return new { ProgrammePriorityId = newProgrammePriority.MapNodeId };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateProgrammePriority(ProgrammePriorityDO programmePriority)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.CreateProgrammePriority, programmePriority.ProgrammeId);

            var errorsCreate = this.programmePrioritiesRepository.CanCreateProgrammePriority(programmePriority.ProgrammeId);

            var errorList = this.programmePrioritiesRepository.CanModifyProgrammePriority(
                programmePriority.ProgrammeId,
                null,
                programmePriority.Code,
                programmePriority.Name);

            return new ErrorsDO(new List<string>(errorsCreate.Concat(errorList)));
        }

        [HttpPut]
        [Route("{programmePriorityId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.Edit.BasicData), IdParam = "programmePriorityId")]
        public void UpdateProgrammePriority(int programmePriorityId, ProgrammePriorityDO programmePriority)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            if (this.programmePrioritiesRepository.CanModifyProgrammePriority(
                programmePriority.ProgrammeId,
                programmePriorityId,
                programmePriority.Code,
                programmePriority.Name).Any())
            {
                throw new DomainValidationException("Cannot edit programme priority.");
            }

            ProgrammePriority oldProgrammePriority = this.programmePrioritiesRepository.FindForUpdate(programmePriorityId, programmePriority.Version);

            oldProgrammePriority.UpdateProgrammePriority(
                programmePriority.Name,
                programmePriority.NameAlt,
                programmePriority.Description,
                programmePriority.DescriptionAlt,
                programmePriority.CompanyData.ProgrammePriorityType.Value,
                programmePriority.CompanyData.CompanyId.Value,
                programmePriority.CompanyData.HigherOrderCompanyId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmePriorityId:int}/canUpdate")]
        public ErrorsDO CanUpdateProgrammePriority(int programmePriorityId, ProgrammePriorityDO programmePriority)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            var errorList = this.programmePrioritiesRepository.CanModifyProgrammePriority(
                programmePriority.ProgrammeId,
                programmePriorityId,
                programmePriority.Code,
                programmePriority.Name);

            return new ErrorsDO(errorList);
        }

        [Route("{programmePriorityId:int}/info")]
        public MapNodeInfoVO GetMapNodeInfo(int programmePriorityId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmePriorityId);

            return this.mapNodesRepository.GetMapNodePosition(programmePriorityId);
        }

        [HttpPost]
        [Route("{programmePriorityId:int}/changeStatusToDraft")]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.ChangeStatusToDraft), IdParam = "programmePriorityId")]
        public void ChangeStatusToDraft(int programmePriorityId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            byte[] vers = System.Convert.FromBase64String(version);
            var programmePriority = this.programmePrioritiesRepository.FindForUpdate(programmePriorityId, vers);
            programmePriority.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmePriorityId:int}/changeStatusToEntered")]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.ChangeStatusToEntered), IdParam = "programmePriorityId")]
        public void ChangeStatusToEntered(int programmePriorityId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            byte[] vers = System.Convert.FromBase64String(version);
            var programmePriority = this.programmePrioritiesRepository.FindForUpdate(programmePriorityId, vers);
            programmePriority.ChangeStatusToEntered();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmePriorityId:int}/canEnter")]
        public ErrorsDO CanEnterProgrammePriority(int programmePriorityId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            var errorList = new List<string>();

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{programmePriorityId:int}/canDelete")]
        public ErrorsDO CanDeleteProgrammePriority(int programmePriorityId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            var errorList = this.programmePrioritiesRepository.CanDeleteProgrammePriority(programmePriorityId);

            return new ErrorsDO(errorList);
        }

        [HttpDelete]
        [Route("{programmePriorityId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProgrammePriority.Delete), IdParam = "programmePriorityId")]
        public void DeleteProgrammePriority(int programmePriorityId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmePriorityId);

            byte[] vers = System.Convert.FromBase64String(version);
            var programmePriority = this.programmePrioritiesRepository.FindForUpdate(programmePriorityId, vers);

            this.programmePrioritiesRepository.Remove(programmePriority);

            this.unitOfWork.Save();
        }
    }
}
