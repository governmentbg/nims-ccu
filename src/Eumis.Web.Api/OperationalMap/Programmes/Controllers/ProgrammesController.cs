using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.OperationalMap.Programmes.DataObjects;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/programmes")]
    public class ProgrammesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProgrammesRepository programmesRepository;
        private IMapNodesRepository mapNodesRepository;
        private IProgrammeCacheManager programmeCacheManager;
        private IAuthorizer authorizer;

        public ProgrammesController(
            IUnitOfWork unitOfWork,
            IProgrammesRepository programmesRepository,
            IMapNodesRepository mapNodesRepository,
            IProgrammeCacheManager programmeCacheManager,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.programmesRepository = programmesRepository;
            this.mapNodesRepository = mapNodesRepository;
            this.programmeCacheManager = programmeCacheManager;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProgrammeTreeVO> GetProgrammesTree()
        {
            return this.programmesRepository.GetProgrammesTree();
        }

        [Route("{programmeId:int}")]
        public ProgrammeDO GetProgramme(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            var programme = this.programmesRepository.Find(programmeId);

            return new ProgrammeDO(programme);
        }

        [HttpGet]
        [Route("new")]
        public ProgrammeDO NewProgramme()
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            return new ProgrammeDO
            {
                Status = MapNodeStatus.Draft,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Create))]
        public object CreateProgramme(ProgrammeDO programme)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            if (this.programmesRepository.CanModifyProgramme(
                null,
                programme.Code,
                programme.Name,
                programme.ShortName).Any())
            {
                throw new DomainValidationException("Cannot create new programme.");
            }

            var newProgramme = new Programme(
                programme.Code,
                programme.Name,
                programme.NameAlt,
                programme.Description,
                programme.DescriptionAlt,
                programme.CompanyId);

            this.programmesRepository.Add(newProgramme);
            this.unitOfWork.Save();

            newProgramme.AddProgrammeRelation();
            this.unitOfWork.Save();

            this.programmeCacheManager.ClearCache();

            return new { ProgrammeId = newProgramme.MapNodeId };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateProgramme(ProgrammeDO programme)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var errorList = this.programmesRepository.CanModifyProgramme(
                null,
                programme.Code,
                programme.Name,
                programme.ShortName);

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{programmeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.BasicData), IdParam = "programmeId")]
        public void UpdateProgramme(int programmeId, ProgrammeDO programme)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            if (this.programmesRepository.CanModifyProgramme(
                programmeId,
                programme.Code,
                programme.Name,
                programme.ShortName).Any())
            {
                throw new DomainValidationException("Cannot edit programme.");
            }

            Programme oldProgramme = this.programmesRepository.FindForUpdate(programmeId, programme.Version);

            oldProgramme.UpdateProgramme(
                programme.Name,
                programme.NameAlt,
                programme.Description,
                programme.DescriptionAlt,
                programme.CompanyId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeId:int}/canUpdate")]
        public ErrorsDO CanUpdateProgramme(int programmeId, ProgrammeDO programme)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errorList = this.programmesRepository.CanModifyProgramme(
                programmeId,
                programme.Code,
                programme.Name,
                programme.ShortName);

            return new ErrorsDO(errorList);
        }

        [Route("name")]
        public string GetProgrammeName(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            var programme = this.programmesRepository.Find(programmeId);

            return programme.Name;
        }

        [Route("{programmeId:int}/info")]
        public MapNodeInfoVO GetMapNodeInfo(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            return this.mapNodesRepository.GetMapNodePosition(programmeId);
        }

        [HttpPost]
        [Route("{programmeId:int}/changeStatusToDraft")]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.ChangeStatusToDraft), IdParam = "programmeId")]
        public void ChangeStatusToDraft(int programmeId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);
            var programme = this.programmesRepository.FindForUpdate(programmeId, vers);
            programme.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeId:int}/changeStatusToEntered")]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.ChangeStatusToEntered), IdParam = "programmeId")]
        public void ChangeStatusToEntered(int programmeId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            if (this.programmesRepository.CanEnterProgramme(programmeId).Any())
            {
                throw new DomainValidationException("Cannot enter programme.");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            var programme = this.programmesRepository.FindForUpdate(programmeId, vers);
            programme.ChangeStatusToEntered();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeId:int}/canEnter")]
        public ErrorsDO CanEnterProgramme(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errorList = this.programmesRepository.CanEnterProgramme(programmeId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{programmeId:int}/canDelete")]
        public ErrorsDO CanDeleteProgramme(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Delete, programmeId);

            var errorList = this.programmesRepository.CanDeleteProgramme(programmeId);

            return new ErrorsDO(errorList);
        }

        [HttpDelete]
        [Route("{programmeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Delete), IdParam = "programmeId")]
        public void DeleteProgramme(int programmeId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Delete, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);
            var programme = this.programmesRepository.FindForUpdate(programmeId, vers);

            this.programmesRepository.Remove(programme);

            this.unitOfWork.Save();
        }
    }
}
