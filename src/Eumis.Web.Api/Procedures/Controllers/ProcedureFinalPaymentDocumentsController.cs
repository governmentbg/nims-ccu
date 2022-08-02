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
    [RoutePrefix("api/procedures/{procedureId}/finalPaymentDocs")]
    public class ProcedureFinalPaymentDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProceduresRepository proceduresRepository;
        private IRelationsRepository relationsRepository;

        public ProcedureFinalPaymentDocumentsController(
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
        public IList<ProcedureContractReportDocumentVO> GetProcedureFinalPaymentDocuments(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureContractReportDocuments(procedureId, ProcedureContractReportDocumentType.FinalPaymentDocument);
        }

        [Route("{finalPaymentDocumentId:int}")]
        public ProcedureContractReportDocumentDO GetProcedureFinalPaymentDocument(int procedureId, int finalPaymentDocumentId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, finalPaymentDocumentId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureFinalPaymentDocument = procedure.FindProcedureContractReportDocument(finalPaymentDocumentId);

            return new ProcedureContractReportDocumentDO(procedureFinalPaymentDocument, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureContractReportDocumentDO NewProcedureFinalPaymentDocument(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureContractReportDocumentDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Create), IdParam = "procedureId")]
        public void AddProcedureFinalPaymentDocument(int procedureId, ProcedureContractReportDocumentDO finalPaymentDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, finalPaymentDocument.Version);

            procedure.AddProcedureContractReportDocument(
                finalPaymentDocument.Name,
                finalPaymentDocument.Extension,
                finalPaymentDocument.IsRequired.Value,
                ProcedureContractReportDocumentType.FinalPaymentDocument);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{finalPaymentDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Edit), IdParam = "procedureId", ChildIdParam = "finalPaymentDocumentId")]
        public void UpdateProcedureFinalPaymentDocument(int procedureId, int finalPaymentDocumentId, ProcedureContractReportDocumentDO finalPaymentDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, finalPaymentDocumentId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, finalPaymentDocument.Version);

            procedure.UpdateProcedureContractReportDocument(
                finalPaymentDocumentId,
                finalPaymentDocument.Name,
                finalPaymentDocument.Extension,
                finalPaymentDocument.IsRequired.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{finalPaymentDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Delete), IdParam = "procedureId", ChildIdParam = "finalPaymentDocumentId")]
        public void DeleteProcedureFinalPaymentDocument(int procedureId, int finalPaymentDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, finalPaymentDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureContractReportDocument(finalPaymentDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{finalPaymentDocumentId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Deactivate), IdParam = "procedureId", ChildIdParam = "finalPaymentDocumentId")]
        public void DeactivateProcedureFinalPaymentDocument(int procedureId, int finalPaymentDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, finalPaymentDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureContractReportDocument(finalPaymentDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{finalPaymentDocumentId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Activate), IdParam = "procedureId", ChildIdParam = "finalPaymentDocumentId")]
        public void ActivateProcedureFinalPaymentDocument(int procedureId, int finalPaymentDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, finalPaymentDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureContractReportDocument(finalPaymentDocumentId);

            this.unitOfWork.Save();
        }
    }
}
