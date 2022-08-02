using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Irregularities.DataObjects;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/irregularitySignals/{signalId:int}/documents")]
    public class IrregularitySignalDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IIrregularitySignalsRepository irregularitySignalsRepository;

        public IrregularitySignalDocumentsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IIrregularitySignalsRepository irregularitySignalsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.irregularitySignalsRepository = irregularitySignalsRepository;
        }

        [Route("")]
        public IList<IrregularityDocVO> GetSignalDocuments(int signalId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.View, signalId);

            return this.irregularitySignalsRepository.GetDocuments(signalId);
        }

        [Route("{documentId:int}")]
        public IrregularitySignalDocDO GetSignalDocument(int signalId, int documentId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.View, signalId);

            var signal = this.irregularitySignalsRepository.Find(signalId);

            var document = signal.GetDocument(documentId);

            return new IrregularitySignalDocDO(document, signal.Version);
        }

        [HttpGet]
        [Route("new")]
        public IrregularitySignalDocDO NewSignalDocument(int signalId)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var signal = this.irregularitySignalsRepository.Find(signalId);

            return new IrregularitySignalDocDO(signalId, signal.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Edit.Documents.Create), IdParam = "signalId")]
        public void AddSignalDocument(int signalId, IrregularitySignalDocDO document)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, document.Version);

            signal.AddDocument(
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Edit.Documents.Edit), IdParam = "signalId", ChildIdParam = "documentId")]
        public void UpdateSignalDocument(int signalId, int documentId, IrregularitySignalDocDO document)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, document.Version);

            signal.UpdateDocument(
                documentId,
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.IrregularitySignals.Edit.Documents.Delete), IdParam = "signalId", ChildIdParam = "documentId")]
        public void DeleteSignalDocument(int signalId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(IrregularitySignalActions.Edit, signalId);

            byte[] vers = System.Convert.FromBase64String(version);
            var signal = this.irregularitySignalsRepository.FindForUpdate(signalId, vers);

            signal.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
