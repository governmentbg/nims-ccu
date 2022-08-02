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
    [RoutePrefix("api/procedures/{procedureId}/advancePaymentDocs")]
    public class ProcedureAdvancePaymentDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProceduresRepository proceduresRepository;
        private IRelationsRepository relationsRepository;

        public ProcedureAdvancePaymentDocumentsController(
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
        public IList<ProcedureContractReportDocumentVO> GetProcedureAdvancePaymentDocuments(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureContractReportDocuments(procedureId, ProcedureContractReportDocumentType.AdvancePaymentDocument);
        }

        [Route("{advancePaymentDocumentId:int}")]
        public ProcedureContractReportDocumentDO GetProcedureAdvancePaymentDocument(int procedureId, int advancePaymentDocumentId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, advancePaymentDocumentId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureAdvancePaymentDocument = procedure.FindProcedureContractReportDocument(advancePaymentDocumentId);

            return new ProcedureContractReportDocumentDO(procedureAdvancePaymentDocument, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureContractReportDocumentDO NewProcedureAdvancePaymentDocument(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureContractReportDocumentDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Create), IdParam = "procedureId")]
        public void AddProcedureAdvancePaymentDocument(int procedureId, ProcedureContractReportDocumentDO advancePaymentDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, advancePaymentDocument.Version);

            procedure.AddProcedureContractReportDocument(
                advancePaymentDocument.Name,
                advancePaymentDocument.Extension,
                advancePaymentDocument.IsRequired.Value,
                ProcedureContractReportDocumentType.AdvancePaymentDocument);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{advancePaymentDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Edit), IdParam = "procedureId", ChildIdParam = "advancePaymentDocumentId")]
        public void UpdateProcedureAdvancePaymentDocument(int procedureId, int advancePaymentDocumentId, ProcedureContractReportDocumentDO advancePaymentDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, advancePaymentDocumentId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, advancePaymentDocument.Version);

            procedure.UpdateProcedureContractReportDocument(
                advancePaymentDocumentId,
                advancePaymentDocument.Name,
                advancePaymentDocument.Extension,
                advancePaymentDocument.IsRequired.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{advancePaymentDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Delete), IdParam = "procedureId", ChildIdParam = "advancePaymentDocumentId")]
        public void DeleteProcedureAdvancePaymentDocument(int procedureId, int advancePaymentDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, advancePaymentDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureContractReportDocument(advancePaymentDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{advancePaymentDocumentId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Deactivate), IdParam = "procedureId", ChildIdParam = "advancePaymentDocumentId")]
        public void DeactivateProcedureAdvancePaymentDocument(int procedureId, int advancePaymentDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, advancePaymentDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureContractReportDocument(advancePaymentDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{advancePaymentDocumentId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Activate), IdParam = "procedureId", ChildIdParam = "advancePaymentDocumentId")]
        public void ActivateProcedureAdvancePaymentDocument(int procedureId, int advancePaymentDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, advancePaymentDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureContractReportDocument(advancePaymentDocumentId);

            this.unitOfWork.Save();
        }
    }
}
