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
    [RoutePrefix("api/procedures/{procedureId}/intermediatePaymentDocs")]
    public class ProcedureIntermediatePaymentDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProceduresRepository proceduresRepository;
        private IRelationsRepository relationsRepository;

        public ProcedureIntermediatePaymentDocumentsController(
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
        public IList<ProcedureContractReportDocumentVO> GetProcedureIntermediatePaymentDocuments(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureContractReportDocuments(procedureId, ProcedureContractReportDocumentType.IntermediatePaymentDocument);
        }

        [Route("{intermediatePaymentDocumentId:int}")]
        public ProcedureContractReportDocumentDO GetProcedureIntermediatePaymentDocument(int procedureId, int intermediatePaymentDocumentId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, intermediatePaymentDocumentId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureIntermediatePaymentDocument = procedure.FindProcedureContractReportDocument(intermediatePaymentDocumentId);

            return new ProcedureContractReportDocumentDO(procedureIntermediatePaymentDocument, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureContractReportDocumentDO NewProcedureIntermediatePaymentDocument(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureContractReportDocumentDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Create), IdParam = "procedureId")]
        public void AddProcedureIntermediatePaymentDocument(int procedureId, ProcedureContractReportDocumentDO intermediatePaymentDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, intermediatePaymentDocument.Version);

            procedure.AddProcedureContractReportDocument(
                intermediatePaymentDocument.Name,
                intermediatePaymentDocument.Extension,
                intermediatePaymentDocument.IsRequired.Value,
                ProcedureContractReportDocumentType.IntermediatePaymentDocument);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{intermediatePaymentDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Edit), IdParam = "procedureId", ChildIdParam = "intermediatePaymentDocumentId")]
        public void UpdateProcedureIntermediatePaymentDocument(int procedureId, int intermediatePaymentDocumentId, ProcedureContractReportDocumentDO intermediatePaymentDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, intermediatePaymentDocumentId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, intermediatePaymentDocument.Version);

            procedure.UpdateProcedureContractReportDocument(
                intermediatePaymentDocumentId,
                intermediatePaymentDocument.Name,
                intermediatePaymentDocument.Extension,
                intermediatePaymentDocument.IsRequired.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{intermediatePaymentDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Delete), IdParam = "procedureId", ChildIdParam = "intermediatePaymentDocumentId")]
        public void DeleteProcedureIntermediatePaymentDocument(int procedureId, int intermediatePaymentDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, intermediatePaymentDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureContractReportDocument(intermediatePaymentDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{intermediatePaymentDocumentId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Deactivate), IdParam = "procedureId", ChildIdParam = "intermediatePaymentDocumentId")]
        public void DeactivateProcedureIntermediatePaymentDocument(int procedureId, int intermediatePaymentDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, intermediatePaymentDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureContractReportDocument(intermediatePaymentDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{intermediatePaymentDocumentId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Activate), IdParam = "procedureId", ChildIdParam = "intermediatePaymentDocumentId")]
        public void ActivateProcedureIntermediatePaymentDocument(int procedureId, int intermediatePaymentDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, intermediatePaymentDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureContractReportDocument(intermediatePaymentDocumentId);

            this.unitOfWork.Save();
        }
    }
}
