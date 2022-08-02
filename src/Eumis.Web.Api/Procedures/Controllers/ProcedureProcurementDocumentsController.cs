using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procedures.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/procurementDocs")]
    public class ProcedureProcurementDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProceduresRepository proceduresRepository;
        private IRelationsRepository relationsRepository;

        public ProcedureProcurementDocumentsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProceduresRepository proceduresRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.proceduresRepository = proceduresRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ProcedureContractReportDocumentVO> GetProcedureProcurementDocuments(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureContractReportDocuments(procedureId, ProcedureContractReportDocumentType.ProcurementDocument);
        }

        [Route("{procurementDocumentId:int}")]
        public ProcedureContractReportDocumentDO GetProcedureProcurementDocument(int procedureId, int procurementDocumentId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, procurementDocumentId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureProcurementDocument = procedure.FindProcedureContractReportDocument(procurementDocumentId);

            return new ProcedureContractReportDocumentDO(procedureProcurementDocument, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureContractReportDocumentDO NewProcedureProcurementDocument(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureContractReportDocumentDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Create), IdParam = "procedureId")]
        public void AddProcedureProcurementDocument(int procedureId, ProcedureContractReportDocumentDO procurementDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procurementDocument.Version);

            procedure.AddProcedureContractReportDocument(
                procurementDocument.Name,
                procurementDocument.Extension,
                procurementDocument.IsRequired.Value,
                ProcedureContractReportDocumentType.ProcurementDocument);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procurementDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Edit), IdParam = "procedureId", ChildIdParam = "procurementDocumentId")]
        public void UpdateProcedureProcurementDocument(int procedureId, int procurementDocumentId, ProcedureContractReportDocumentDO procurementDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, procurementDocumentId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, procurementDocument.Version);

            procedure.UpdateProcedureContractReportDocument(
                procurementDocumentId,
                procurementDocument.Name,
                procurementDocument.Extension,
                procurementDocument.IsRequired.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{procurementDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Delete), IdParam = "procedureId", ChildIdParam = "procurementDocumentId")]
        public void DeleteProcedureProcurementDocument(int procedureId, int procurementDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, procurementDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureContractReportDocument(procurementDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procurementDocumentId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Deactivate), IdParam = "procedureId", ChildIdParam = "procurementDocumentId")]
        public void DeactivateProcedureProcurementDocument(int procedureId, int procurementDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, procurementDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureContractReportDocument(procurementDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{procurementDocumentId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Activate), IdParam = "procedureId", ChildIdParam = "procurementDocumentId")]
        public void ActivateProcedureProcurementDocument(int procedureId, int procurementDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, procurementDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureContractReportDocument(procurementDocumentId);

            this.unitOfWork.Save();
        }
    }
}
