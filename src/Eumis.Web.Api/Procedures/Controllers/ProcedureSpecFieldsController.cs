using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procedures.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/specFields")]
    public class ProcedureSpecFieldsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;

        public ProcedureSpecFieldsController(IUnitOfWork unitOfWork, IProceduresRepository proceduresRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProcedureSpecFieldVO> GetProcedureSpecFields(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureSpecFields(procedureId);
        }

        [Route("{procedureSpecFieldId:int}")]
        public ProcedureSpecFieldDO GetProcedureSpecField(int procedureId, int procedureSpecFieldId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureSpecField = procedure.FindProcedureSpecField(procedureSpecFieldId);

            return new ProcedureSpecFieldDO(procedureSpecField, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureSpecFieldDO NewProcedureSpecField(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureSpecFieldDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.SpecFields.Create), IdParam = "procedureId")]
        public void AddProcedureSpecField(int procedureId, ProcedureSpecFieldDO procedureSpecField)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureSpecField.Version);

            procedure.AddProcedureSpecField(
                procedureSpecField.Title,
                procedureSpecField.TitleAlt,
                procedureSpecField.Description,
                procedureSpecField.DescriptionAlt,
                procedureSpecField.IsRequired.Value,
                procedureSpecField.MaxLength.Value);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureSpecFieldId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.SpecFields.Edit), IdParam = "procedureId", ChildIdParam = "procedureSpecFieldId")]
        public void UpdateProcedureSpecField(int procedureId, int procedureSpecFieldId, ProcedureSpecFieldDO procedureSpecField)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procedureSpecField.Version);

            procedure.UpdateProcedureSpecField(
                procedureSpecFieldId,
                procedureSpecField.Title,
                procedureSpecField.TitleAlt,
                procedureSpecField.Description,
                procedureSpecField.DescriptionAlt,
                procedureSpecField.IsRequired.Value,
                procedureSpecField.MaxLength.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{procedureSpecFieldId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.SpecFields.Delete), IdParam = "procedureId", ChildIdParam = "procedureSpecFieldId")]
        public void DeleteProcedureSpecField(int procedureId, int procedureSpecFieldId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureSpecField(procedureSpecFieldId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureSpecFieldId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.SpecFields.Deactivate), IdParam = "procedureId", ChildIdParam = "procedureSpecFieldId")]
        public void DeactivateProcedureSpecField(int procedureId, int procedureSpecFieldId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureSpecField(procedureSpecFieldId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procedureSpecFieldId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.SpecFields.Activate), IdParam = "procedureId", ChildIdParam = "procedureSpecFieldId")]
        public void ActivateProcedureSpecField(int procedureId, int procedureSpecFieldId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureSpecField(procedureSpecFieldId);

            this.unitOfWork.Save();
        }
    }
}
