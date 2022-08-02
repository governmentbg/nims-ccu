using Eumis.ApplicationServices.Services.ProgrammeDeclaration;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.OperationalMap.Programmes.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/programmes/{programmeId}/declarations/{programmeDeclarationId}/items")]
    public class ProgrammeAppFormDeclarationItemsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProgrammeAppFormDeclarationsRepository programmeAppFormDeclarationsRepository;
        private IProgrammeDeclarationService programmeDeclarationService;

        public ProgrammeAppFormDeclarationItemsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProgrammeAppFormDeclarationsRepository programmeAppFormDeclarationsRepository,
            IProgrammeDeclarationService programmeDeclarationService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.programmeAppFormDeclarationsRepository = programmeAppFormDeclarationsRepository;
            this.programmeDeclarationService = programmeDeclarationService;
        }

        [Route("")]
        public IList<ProgrammeDeclarationItemVO> GetProgrammeDeclarationItems(int programmeId, int programmeDeclarationId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            return this.programmeAppFormDeclarationsRepository.GetProgrammeDeclarationItems(programmeDeclarationId);
        }

        [Route("{programmeDeclarationItemId:int}")]
        public ProgrammeDeclarationItemDO GetProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, int programmeDeclarationItemId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            var programmeDeclaration = this.programmeAppFormDeclarationsRepository.Find(programmeDeclarationId);
            var programmeDeclarationItem = programmeDeclaration.FindProgrammeDeclarationItem(programmeDeclarationItemId);

            return new ProgrammeDeclarationItemDO(programmeDeclarationItem, programmeDeclaration.ProgrammeId, programmeDeclaration.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProgrammeDeclarationItemDO NewProgrammeDeclarationItem(int programmeId, int programmeDeclarationId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var programmeDeclaration = this.programmeAppFormDeclarationsRepository.FindWithoutIncludes(programmeDeclarationId);

            return new ProgrammeDeclarationItemDO(programmeDeclaration.ProgrammeDeclarationId, programmeDeclaration.ProgrammeId, programmeDeclaration.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Edit.Items.Create), IdParam = "programmeId", ChildIdParam = "programmeDeclarationId")]
        public void AddProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, ProgrammeDeclarationItemDO programmeDeclarationItem)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            if (this.programmeAppFormDeclarationsRepository.CanAddProgrammeDeclarationItem(programmeDeclarationId, programmeDeclarationItem.OrderNum.Value).Count > 0)
            {
                throw new InvalidOperationException("Cannot add ProgrammeDeclarationItem");
            }

            var programmeDeclaration = this.programmeAppFormDeclarationsRepository.FindForUpdate(programmeDeclarationId, programmeDeclarationItem.Version);

            programmeDeclaration.AddProgrammeDeclarationItem(programmeDeclarationItem.OrderNum.Value, programmeDeclarationItem.Content);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canAdd")]
        public ErrorsDO CanAddProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, ProgrammeDeclarationItemDO programmeDeclarationItem)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            IList<string> errors = this.programmeAppFormDeclarationsRepository.CanAddProgrammeDeclarationItem(programmeDeclarationId, programmeDeclarationItem.OrderNum.Value);

            return new ErrorsDO(errors);
        }

        [HttpPut]
        [Route("{programmeDeclarationItemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Edit.Items.Edit), IdParam = "programmeId", ChildIdParam = "programmeDeclarationItemId")]
        public void UpdateProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, int programmeDeclarationItemId, ProgrammeDeclarationItemDO programmeDeclarationItem)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            if (this.programmeAppFormDeclarationsRepository.CanUpdateProgrammeDeclarationItem(
                programmeDeclarationId,
                programmeDeclarationItemId,
                programmeDeclarationItem.OrderNum.Value)
                .Count > 0)
            {
                throw new InvalidOperationException("Cannot update ProgrammeDeclarationItem");
            }

            var programmeDeclaration = this.programmeAppFormDeclarationsRepository.FindForUpdate(programmeDeclarationId, programmeDeclarationItem.Version);

            var item = programmeDeclaration.FindProgrammeDeclarationItem(programmeDeclarationItemId);

            item.SetAttributes(programmeDeclarationItem.OrderNum.Value, programmeDeclarationItem.Content);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeDeclarationItemId:int}/canUpdate")]
        public ErrorsDO CanUpdateProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, int programmeDeclarationItemId, ProgrammeDeclarationItemDO programmeDeclarationItem)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errors = this.programmeAppFormDeclarationsRepository.CanUpdateProgrammeDeclarationItem(
                programmeDeclarationId,
                programmeDeclarationItemId,
                programmeDeclarationItem.OrderNum.Value);

            return new ErrorsDO(errors);
        }

        [HttpPut]
        [Route("{programmeDeclarationItemId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Edit.Items.Deactivate), IdParam = "programmeId", ChildIdParam = "programmeDeclarationItemId")]
        public void ActivateProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, int programmeDeclarationItemId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);

            var programmeDeclaration = this.programmeAppFormDeclarationsRepository.FindForUpdate(programmeDeclarationId, vers);

            programmeDeclaration.ActivateProgrammeDeclarationItem(programmeDeclarationItemId);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeDeclarationItemId:int}/canActivate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO CanActivateProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, int programmeDeclarationItemId, int orderNum)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errors = this.programmeAppFormDeclarationsRepository.CanAddProgrammeDeclarationItem(programmeDeclarationId, orderNum);

            return new ErrorsDO(errors);
        }

        [HttpPut]
        [Route("{programmeDeclarationItemId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Edit.Items.Activate), IdParam = "programmeId", ChildIdParam = "programmeDeclarationItemId")]
        public void DeactivateProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, int programmeDeclarationItemId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);

            var programmeDeclaration = this.programmeAppFormDeclarationsRepository.FindForUpdate(programmeDeclarationId, vers);

            programmeDeclaration.DeactivateProgrammeDeclarationItem(programmeDeclarationItemId);
            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{programmeDeclarationItemId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Edit.Items.Delete), IdParam = "programmeId", ChildIdParam = "programmeDeclarationItemId")]
        public void DeleteProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, int programmeDeclarationItemId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);

            var programmeDeclaration = this.programmeAppFormDeclarationsRepository.FindForUpdate(programmeDeclarationId, vers);

            programmeDeclaration.RemoveProgrammeDeclarationItem(programmeDeclarationItemId);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeDeclarationItemId:int}/canDelete")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]

        public ErrorsDO CanDeleteProgrammeDeclarationItem(int programmeId, int programmeDeclarationId, int programmeDeclarationItemId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errors = this.programmeAppFormDeclarationsRepository.CanDeleteProgrammeDeclarationItem(programmeDeclarationId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("loadItems")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.AppFormDeclarations.Edit.Items.Load), IdParam = "programmeId", ChildIdParam = "programmeDeclarationId")]
        public ErrorsDO LoadProgrammeDeclarationItems(int programmeId, int programmeDeclarationId, FileDO file)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            return new ErrorsDO(this.programmeDeclarationService.LoadProgrammeDeclarationItems(programmeDeclarationId, file.Key));
        }
    }
}
