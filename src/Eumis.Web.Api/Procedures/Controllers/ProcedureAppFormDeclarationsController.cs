using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.DataObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/declarations")]
    public class ProcedureAppFormDeclarationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;
        private IProceduresRepository proceduresRepository;
        private IProcedureAppFormDeclarationsRepository procedureDeclarationsRepository;

        public ProcedureAppFormDeclarationsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository,
            IProceduresRepository proceduresRepository,
            IProcedureAppFormDeclarationsRepository procedureDeclarationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
            this.proceduresRepository = proceduresRepository;
            this.procedureDeclarationsRepository = procedureDeclarationsRepository;
        }

        [Route("")]
        public IList<ProcedureDeclarationVO> GetProcedureDeclarations(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.procedureDeclarationsRepository.GetDeclarations(procedureId);
        }

        [Route("{declarationId:int}")]
        public ProcedureDeclarationDO GetProcedureDeclaration(int procedureId, int declarationId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            this.relationsRepository.AssertProcedureHasDeclaration(procedureId, declarationId);

            return this.procedureDeclarationsRepository.GetDeclaration(declarationId);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureDeclarationDO NewProcedureDeclaration(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var programmeId = this.proceduresRepository.GetPrimaryProcedureProgrammeId(procedureId);

            return new ProcedureDeclarationDO()
            {
                ProgrammeId = programmeId,
                ProcedureId = procedureId,
                Status = ActiveStatus.NotActivated,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Declarations.Create), IdParam = "procedureId")]
        public void AddProcedureDeclaration(int procedureId, ProcedureDeclarationDO declaration)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedureDeclaration = new ProcedureAppFormDeclaration(
                procedureId,
                declaration.ProgrammeDeclarationId.Value,
                declaration.IsRequired.Value);

            this.procedureDeclarationsRepository.Add(procedureDeclaration);
            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureDeclarationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Declarations.Edit), IdParam = "procedureId", ChildIdParam = "procedureDeclarationId")]
        public void UpdateProcedureDeclaration(int procedureId, int procedureDeclarationId, ProcedureDeclarationDO declaration)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            this.relationsRepository.AssertProcedureHasDeclaration(procedureId, procedureDeclarationId);

            var procedureDeclaration = this.procedureDeclarationsRepository.Find(procedureDeclarationId);
            procedureDeclaration.AssertIsNotActivated();

            procedureDeclaration.SetAttributes(
                declaration.ProgrammeDeclarationId.Value,
                declaration.IsRequired.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{procedureDeclarationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Declarations.Delete), IdParam = "procedureId", ChildIdParam = "procedureDeclarationId")]
        public void DeleteProcedureDeclaration(int procedureId, int procedureDeclarationId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasDeclaration(procedureId, procedureDeclarationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var procedureDeclaration = this.procedureDeclarationsRepository.FindForUpdate(procedureDeclarationId, vers);

            procedureDeclaration.AssertIsNotActivated();

            this.procedureDeclarationsRepository.Remove(procedureDeclaration);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureDeclarationId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Declarations.Deactivate), IdParam = "procedureId", ChildIdParam = "procedureDeclarationId")]
        public void DeactivateProcedureDeclaration(int procedureId, int procedureDeclarationId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasDeclaration(procedureId, procedureDeclarationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var procedureDeclaration = this.procedureDeclarationsRepository.FindForUpdate(procedureDeclarationId, vers);

            procedureDeclaration.Deactivate();

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureDeclarationId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Declarations.Activate), IdParam = "procedureId", ChildIdParam = "procedureDeclarationId")]
        public void ActivateProcedureDeclaration(int procedureId, int procedureDeclarationId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasDeclaration(procedureId, procedureDeclarationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var procedureDeclaration = this.procedureDeclarationsRepository.FindForUpdate(procedureDeclarationId, vers);

            procedureDeclaration.Activate();

            this.unitOfWork.Save();
        }
    }
}
