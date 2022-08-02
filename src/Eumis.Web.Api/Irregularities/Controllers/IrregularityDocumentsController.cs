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
    [RoutePrefix("api/irregularities/{irregularityId:int}/documents")]
    public class IrregularityDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IIrregularitiesRepository irregularitiesRepository;

        public IrregularityDocumentsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IIrregularitiesRepository irregularitiesRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.irregularitiesRepository = irregularitiesRepository;
        }

        [Route("")]
        public IList<IrregularityDocVO> GetDocuments(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.View, irregularityId);

            return this.irregularitiesRepository.GetDocuments(irregularityId);
        }

        [Route("{documentId:int}")]
        public IrregularityDocDO GetDocument(int irregularityId, int documentId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.View, irregularityId);

            var irregularity = this.irregularitiesRepository.Find(irregularityId);

            var document = irregularity.GetDocument(documentId);

            return new IrregularityDocDO(document, irregularity.Version);
        }

        [HttpGet]
        [Route("new")]
        public IrregularityDocDO NewDocument(int irregularityId)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            var irregularity = this.irregularitiesRepository.Find(irregularityId);

            return new IrregularityDocDO(irregularityId, irregularity.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Documents.Create), IdParam = "irregularityId")]
        public void AddDocument(int irregularityId, IrregularityDocDO document)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            var irregularity = this.irregularitiesRepository.FindForUpdate(irregularityId, document.Version);

            irregularity.AddDocument(
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Documents.Edit), IdParam = "irregularityId", ChildIdParam = "documentId")]
        public void UpdateDocument(int irregularityId, int documentId, IrregularityDocDO document)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            var irregularity = this.irregularitiesRepository.FindForUpdate(irregularityId, document.Version);

            irregularity.UpdateDocument(
                documentId,
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Irregularities.Edit.Documents.Delete), IdParam = "irregularityId", ChildIdParam = "documentId")]
        public void DeleteDocument(int irregularityId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(IrregularityActions.Edit, irregularityId);

            byte[] vers = System.Convert.FromBase64String(version);
            var irregularity = this.irregularitiesRepository.FindForUpdate(irregularityId, vers);

            irregularity.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
