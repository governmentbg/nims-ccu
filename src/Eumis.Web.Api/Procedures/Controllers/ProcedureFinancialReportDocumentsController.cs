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
    [RoutePrefix("api/procedures/{procedureId}/financialReportDocs")]
    public class ProcedureFinancialReportDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProceduresRepository proceduresRepository;
        private IRelationsRepository relationsRepository;

        public ProcedureFinancialReportDocumentsController(
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
        public IList<ProcedureContractReportDocumentVO> GetProcedureFinancialReportDocuments(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureContractReportDocuments(procedureId, ProcedureContractReportDocumentType.FinancialReportDocument);
        }

        [Route("{financialReportDocumentId:int}")]
        public ProcedureContractReportDocumentDO GetProcedureFinancialReportDocument(int procedureId, int financialReportDocumentId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, financialReportDocumentId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureFinancialReportDocument = procedure.FindProcedureContractReportDocument(financialReportDocumentId);

            return new ProcedureContractReportDocumentDO(procedureFinancialReportDocument, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureContractReportDocumentDO NewProcedureFinancialReportDocument(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureContractReportDocumentDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Create), IdParam = "procedureId")]
        public void AddProcedureFinancialReportDocument(int procedureId, ProcedureContractReportDocumentDO financialReportDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, financialReportDocument.Version);

            procedure.AddProcedureContractReportDocument(
                financialReportDocument.Name,
                financialReportDocument.Extension,
                financialReportDocument.IsRequired.Value,
                ProcedureContractReportDocumentType.FinancialReportDocument);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{financialReportDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Edit), IdParam = "procedureId", ChildIdParam = "financialReportDocumentId")]
        public void UpdateProcedureFinancialReportDocument(int procedureId, int financialReportDocumentId, ProcedureContractReportDocumentDO financialReportDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, financialReportDocumentId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, financialReportDocument.Version);

            procedure.UpdateProcedureContractReportDocument(
                financialReportDocumentId,
                financialReportDocument.Name,
                financialReportDocument.Extension,
                financialReportDocument.IsRequired.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{financialReportDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Delete), IdParam = "procedureId", ChildIdParam = "financialReportDocumentId")]
        public void DeleteProcedureFinancialReportDocument(int procedureId, int financialReportDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, financialReportDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureContractReportDocument(financialReportDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{financialReportDocumentId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Deactivate), IdParam = "procedureId", ChildIdParam = "financialReportDocumentId")]
        public void DeactivateProcedureFinancialReportDocument(int procedureId, int financialReportDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, financialReportDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureContractReportDocument(financialReportDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{financialReportDocumentId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Activate), IdParam = "procedureId", ChildIdParam = "financialReportDocumentId")]
        public void ActivateProcedureFinancialReportDocument(int procedureId, int financialReportDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, financialReportDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureContractReportDocument(financialReportDocumentId);

            this.unitOfWork.Save();
        }
    }
}
