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
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/documents")]
    public class ProcedureDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IAuthorizer authorizer;

        public ProcedureDocumentsController(IUnitOfWork unitOfWork, IProceduresRepository proceduresRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProcedureDocumentsVO> GetProcedureDocuments(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureDocuments(procedureId);
        }

        [Route("{documentId:int}")]
        public ProcedureDocumentDO GetProcedureDocument(int procedureId, int documentId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureDocument = procedure.FindProcedureDocument(documentId);

            return new ProcedureDocumentDO(procedureDocument, procedure.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureDocumentDO NewProcedureDocument(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureDocumentDO(procedureId, procedure.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Documents.Edit), IdParam = "procedureId", ChildIdParam = "documentId")]
        public void UpdateProcedureDocument(int procedureId, int documentId, ProcedureDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, document.Version);

            procedure.UpdateProcedureDocument(
                documentId,
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Documents.Create), IdParam = "procedureId")]
        public void AddProcedureDocument(int procedureId, ProcedureDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, document.Version);

            procedure.AddProcedureDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Documents.Delete), IdParam = "procedureId", ChildIdParam = "documentId")]
        public void DeleteProcedureDocument(int procedureId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
