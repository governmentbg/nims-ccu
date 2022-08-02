using Eumis.ApplicationServices.Services.ProcedureEvalTableXml;
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

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/evalTables")]
    public class ProcedureEvalTablesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;
        private IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository;
        private IProcedureEvalTableXmlService procedureEvalTableXmlService;

        public ProcedureEvalTablesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProceduresRepository proceduresRepository,
            IProcedureEvalTableXmlsRepository procedureEvalTableXmlsRepository,
            IProcedureEvalTableXmlService procedureEvalTableXmlService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.proceduresRepository = proceduresRepository;
            this.procedureEvalTableXmlsRepository = procedureEvalTableXmlsRepository;
            this.procedureEvalTableXmlService = procedureEvalTableXmlService;
        }

        [Route("")]
        public IList<ProcedureEvalTablesVO> GetProcedureEvalTables(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureEvalTables(procedureId);
        }

        [Route("{evalTableId:int}")]
        public ProcedureEvalTableDO GetProcedureEvalTable(int procedureId, int evalTableId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureEvalTable = procedure.FindProcedureEvalTable(evalTableId);

            var procedureEvalTableXml = this.procedureEvalTableXmlsRepository.FindByProcedureEvalTableId(evalTableId);

            return new ProcedureEvalTableDO(procedureEvalTable, procedureEvalTableXml.Gid, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureEvalTableDO NewProcedureEvalTable(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureEvalTableDO(procedureId, procedure.Version);
        }

        [HttpPut]
        [Route("{evalTableId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.EvalTables.Edit), IdParam = "procedureId", ChildIdParam = "evalTableId")]
        public void UpdateProcedureEvalTable(int procedureId, int evalTableId, ProcedureEvalTableDO evalTable)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, evalTable.Version);

            procedure.UpdateProcedureEvalTable(
                evalTableId,
                evalTable.Name,
                evalTable.Type.Value);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.EvalTables.Create), IdParam = "procedureId")]
        public ProcedureEvalTableDO AddProcedureEvalTable(int procedureId, ProcedureEvalTableDO evalTable)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, evalTable.Version);

            var procedureEvalTable = procedure.AddProcedureEvalTable(
                evalTable.Name,
                evalTable.Type.Value,
                evalTable.EvalType.Value);

            this.unitOfWork.Save();

            var procedureEvalTableXml = this.procedureEvalTableXmlService.CreateEvalTable(procedureEvalTable);
            this.procedureEvalTableXmlsRepository.Add(procedureEvalTableXml);

            this.unitOfWork.Save();
            evalTable.ProcedureEvalTableId = procedureEvalTable.ProcedureEvalTableId;

            return evalTable;
        }

        [HttpDelete]
        [Route("{evalTableId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.EvalTables.Delete), IdParam = "procedureId", ChildIdParam = "evalTableId")]
        public void DeleteProcedureEvalTable(int procedureId, int evalTableId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            this.procedureEvalTableXmlsRepository.RemoveByEvalTableId(evalTableId);
            this.unitOfWork.Save();

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureEvalTable(evalTableId);
            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{evalTableId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.EvalTables.Deactivate), IdParam = "procedureId", ChildIdParam = "evalTableId")]
        public void DeactivateProcedureEvalTable(int procedureId, int evalTableId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureEvalTable(evalTableId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{evalTableId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.EvalTables.Activate), IdParam = "procedureId", ChildIdParam = "evalTableId")]
        public void ActivateProcedureEvalTable(int procedureId, int evalTableId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureEvalTable(evalTableId);

            this.unitOfWork.Save();
        }
    }
}
