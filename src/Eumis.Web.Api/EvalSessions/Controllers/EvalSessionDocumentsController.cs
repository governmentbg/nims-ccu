using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/documents")]
    public class EvalSessionDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionsRepository evalSessionsRepository;
        private IAuthorizer authorizer;

        public EvalSessionDocumentsController(IUnitOfWork unitOfWork, IEvalSessionsRepository evalSessionsRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionsRepository = evalSessionsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<EvalSessionDocumentsVO> GetEvalSessionDocuments(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionDocuments(evalSessionId);
        }

        [Route("{documentId:int}")]
        public EvalSessionDocumentDO GetEvalSessionDocument(int evalSessionId, int documentId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionDocument = evalSession.FindEvalSessionDocument(documentId);

            return new EvalSessionDocumentDO(evalSessionDocument, evalSession.Version);
        }

        [HttpGet]
        [Route("new")]
        public EvalSessionDocumentDO NewEvalSessionDocument(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            return new EvalSessionDocumentDO(evalSessionId, evalSession.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Documents.Edit), IdParam = "evalSessionId", ChildIdParam = "documentId")]
        public void UpdateEvalSessionDocument(int evalSessionId, int documentId, EvalSessionDocumentDO evalSessionDocument)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSessionDocument.Version);

            evalSession.UpdateEvalSessionDocument(
                documentId,
                evalSessionDocument.Name,
                evalSessionDocument.File.Key,
                evalSessionDocument.Description);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Documents.Create), IdParam = "evalSessionId")]
        public void AddEvalSessionDocument(int evalSessionId, EvalSessionDocumentDO evalSessionDocument)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSessionDocument.Version);

            evalSession.AddEvalSessionDocument(
                evalSessionDocument.Name,
                evalSessionDocument.File.Key,
                evalSessionDocument.Description);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{documentId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Documents.Delete), IdParam = "evalSessionId", ChildIdParam = "documentId")]
        public void CancelEvalSessionReport(int evalSessionId, int documentId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);
            evalSession.CancelEvalSessionDocument(documentId, confirm.Note);

            this.unitOfWork.Save();
        }
    }
}
