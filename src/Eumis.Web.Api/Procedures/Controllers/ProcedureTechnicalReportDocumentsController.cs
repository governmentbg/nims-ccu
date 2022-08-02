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
    [RoutePrefix("api/procedures/{procedureId}/technicalReportDocs")]
    public class ProcedureTechnicalReportDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProceduresRepository proceduresRepository;
        private IRelationsRepository relationsRepository;

        public ProcedureTechnicalReportDocumentsController(
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
        public IList<ProcedureContractReportDocumentVO> GetProcedureTechnicalReportDocuments(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureContractReportDocuments(procedureId, ProcedureContractReportDocumentType.TechnicalReportDocument);
        }

        [Route("{technicalReportDocumentId:int}")]
        public ProcedureContractReportDocumentDO GetProcedureTechnicalReportDocument(int procedureId, int technicalReportDocumentId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, technicalReportDocumentId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureTechnicalReportDocument = procedure.FindProcedureContractReportDocument(technicalReportDocumentId);

            return new ProcedureContractReportDocumentDO(procedureTechnicalReportDocument, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureContractReportDocumentDO NewProcedureTechnicalReportDocument(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureContractReportDocumentDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Create), IdParam = "procedureId")]
        public void AddProcedureTechnicalReportDocument(int procedureId, ProcedureContractReportDocumentDO technicalReportDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, technicalReportDocument.Version);

            procedure.AddProcedureContractReportDocument(
                technicalReportDocument.Name,
                technicalReportDocument.Extension,
                technicalReportDocument.IsRequired.Value,
                ProcedureContractReportDocumentType.TechnicalReportDocument);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{technicalReportDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Edit), IdParam = "procedureId", ChildIdParam = "technicalReportDocumentId")]
        public void UpdateProcedureTechnicalReportDocument(int procedureId, int technicalReportDocumentId, ProcedureContractReportDocumentDO technicalReportDocument)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, technicalReportDocumentId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, technicalReportDocument.Version);

            procedure.UpdateProcedureContractReportDocument(
                technicalReportDocumentId,
                technicalReportDocument.Name,
                technicalReportDocument.Extension,
                technicalReportDocument.IsRequired.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{technicalReportDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Delete), IdParam = "procedureId", ChildIdParam = "technicalReportDocumentId")]
        public void DeleteProcedureTechnicalReportDocument(int procedureId, int technicalReportDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, technicalReportDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureContractReportDocument(technicalReportDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{technicalReportDocumentId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Deactivate), IdParam = "procedureId", ChildIdParam = "technicalReportDocumentId")]
        public void DeactivateProcedureTechnicalReportDocument(int procedureId, int technicalReportDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, technicalReportDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureContractReportDocument(technicalReportDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{technicalReportDocumentId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.ContractReportDocuments.Activate), IdParam = "procedureId", ChildIdParam = "technicalReportDocumentId")]
        public void ActivateProcedureTechnicalReportDocument(int procedureId, int technicalReportDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);
            this.relationsRepository.AssertProcedureHasContractReportDocument(procedureId, technicalReportDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureContractReportDocument(technicalReportDocumentId);

            this.unitOfWork.Save();
        }
    }
}
