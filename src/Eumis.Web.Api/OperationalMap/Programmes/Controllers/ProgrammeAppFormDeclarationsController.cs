using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.OperationalMap.Programmes.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/programmes/{programmeId}/declarations")]
    public class ProgrammeAppFormDeclarationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProgrammeAppFormDeclarationsRepository programmeAppFormDeclarationsRepository;
        private IProcedureAppFormDeclarationsRepository procedureAppFormDeclarationsRepository;
        private IAuthorizer authorizer;

        public ProgrammeAppFormDeclarationsController(
            IUnitOfWork unitOfWork,
            IProgrammeAppFormDeclarationsRepository programmeAppFormDeclarationsRepository,
            IProcedureAppFormDeclarationsRepository procedureAppFormDeclarationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.programmeAppFormDeclarationsRepository = programmeAppFormDeclarationsRepository;
            this.procedureAppFormDeclarationsRepository = procedureAppFormDeclarationsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProgrammeDeclarationVO> GetProgrammeDeclarations(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            return this.programmeAppFormDeclarationsRepository.GetProgrammeDeclarations(programmeId);
        }

        [Route("{programmeDeclarationId:int}")]
        public ProgrammeAppFormDeclarationDO GetProgrammeDeclaration(int programmeId, int programmeDeclarationId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            var programmeDeclaration = this.programmeAppFormDeclarationsRepository.Find(programmeDeclarationId);

            var isDeclarationReadonly = this.procedureAppFormDeclarationsRepository.IsDeclarationReadonly(programmeDeclaration.ProgrammeDeclarationId);

            return new ProgrammeAppFormDeclarationDO(programmeDeclaration, isDeclarationReadonly);
        }

        [HttpGet]
        [Route("new")]
        public ProgrammeAppFormDeclarationDO NewProgrammeDeclaration(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var orderNum = this.programmeAppFormDeclarationsRepository.GetNextProgrammeDeclarationOrderNum(programmeId);

            return new ProgrammeAppFormDeclarationDO(programmeId, orderNum);
        }

        [HttpPost]
        [Route("canAdd")]
        public ErrorsDO CanAddProgrammeDeclaration(int programmeId, ProgrammeAppFormDeclarationDO declaration)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            IList<string> errorList = this.programmeAppFormDeclarationsRepository.CanCreateProgrammeDeclaration(
                declaration.ProgrammeId,
                declaration.Name,
                declaration.NameAlt);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Create), IdParam = "programmeId")]
        public object AddProgrammeDeclaration(int programmeId, ProgrammeAppFormDeclarationDO declaration)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            IList<string> errorList = this.programmeAppFormDeclarationsRepository.CanCreateProgrammeDeclaration(
                declaration.ProgrammeId,
                declaration.Name,
                declaration.NameAlt);
            if (errorList.Count > 0)
            {
                throw new InvalidOperationException("Cannot create ProgrammeDeclaration.");
            }

            var orderNum = this.programmeAppFormDeclarationsRepository.GetNextProgrammeDeclarationOrderNum(programmeId);

            var programmeDeclartion = new ProgrammeAppFormDeclaration(
                declaration.ProgrammeId,
                declaration.Name,
                declaration.NameAlt,
                declaration.Content,
                declaration.ContentAlt,
                declaration.FieldType,
                orderNum,
                declaration.IsConsentForNSIDataProviding);

            this.programmeAppFormDeclarationsRepository.Add(programmeDeclartion);
            this.unitOfWork.Save();

            return new { programmeDeclartion.ProgrammeDeclarationId };
        }

        [HttpPut]
        [Route("{programmeDeclarationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Edit), IdParam = "programmeId", ChildIdParam = "programmeDeclarationId")]
        public void UpdateProgrammeDeclaration(int programmeId, int programmeDeclarationId, ProgrammeAppFormDeclarationDO declaration)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var isDeclarationReadonly = this.procedureAppFormDeclarationsRepository.IsDeclarationReadonly(programmeDeclarationId);
            if (isDeclarationReadonly)
            {
                throw new InvalidOperationException("Cannot edit ProgrammeDeclaration.");
            }

            ProgrammeAppFormDeclaration programmeAppFormDeclaration = this.programmeAppFormDeclarationsRepository.FindForUpdate(programmeDeclarationId, declaration.Version);

            programmeAppFormDeclaration.UpdateAttributes(
                declaration.Content,
                declaration.ContentAlt,
                declaration.FieldType,
                declaration.IsConsentForNSIDataProviding);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{programmeDeclarationId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Activate), IdParam = "programmeId", ChildIdParam = "programmeDeclarationId")]
        public void ActivateProgrammeDeclaration(int programmeId, int programmeDeclarationId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);
            ProgrammeAppFormDeclaration declaration = this.programmeAppFormDeclarationsRepository.FindForUpdate(programmeDeclarationId, vers);

            declaration.Activate();
            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{programmeDeclarationId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Deactivate), IdParam = "programmeId", ChildIdParam = "programmeDeclarationId")]
        public void DeactivateProgrammeDeclaration(int programmeId, int programmeDeclarationId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);
            ProgrammeAppFormDeclaration declaration = this.programmeAppFormDeclarationsRepository.FindForUpdate(programmeDeclarationId, vers);

            declaration.Deactivate();
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeDeclarationId:int}/canDelete")]
        public ErrorsDO CanDeleteProgrammeDeclaration(int programmeId, int programmeDeclarationId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errorList = this.programmeAppFormDeclarationsRepository.CanDeleteProgrammeDeclaration(programmeDeclarationId);

            return new ErrorsDO(errorList);
        }

        [HttpDelete]
        [Route("{programmeDeclarationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Delete), IdParam = "programmeId", ChildIdParam = "programmeDeclarationId")]
        public void DeleteProgrammeDeclaration(int programmeId, int programmeDeclarationId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errorList = this.programmeAppFormDeclarationsRepository.CanDeleteProgrammeDeclaration(programmeDeclarationId);
            if (errorList.Count > 0)
            {
                throw new InvalidOperationException("Cannot delete ProgrammeDeclaration.");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            ProgrammeAppFormDeclaration declaration = this.programmeAppFormDeclarationsRepository.FindForUpdate(programmeDeclarationId, vers);

            this.programmeAppFormDeclarationsRepository.Remove(declaration);
            this.unitOfWork.Save();
        }

        [Route("{programmeDeclarationId:int}/relatedProcedures")]
        public IList<ProgrammeProcedureVO> GetProgrammeDeclarationRelatedProcedures(int programmeId, int programmeDeclarationId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            return this.programmeAppFormDeclarationsRepository.GetDeclarationRelatedProceduresData(programmeDeclarationId);
        }
    }
}
