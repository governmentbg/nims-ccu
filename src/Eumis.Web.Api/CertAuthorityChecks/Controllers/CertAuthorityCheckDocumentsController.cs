using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.CertAuthorityChecks.Repositories;
using Eumis.Data.CertAuthorityChecks.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.CertAuthorityChecks.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/certAuthorityChecks/{certAuthorityCheckId}/documents")]
    public class CertAuthorityCheckDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ICertAuthorityChecksRepository certAuthorityChecksRepository;
        private IAuthorizer authorizer;

        public CertAuthorityCheckDocumentsController(
            IUnitOfWork unitOfWork,
            ICertAuthorityChecksRepository certAuthorityChecksRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.certAuthorityChecksRepository = certAuthorityChecksRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<CertAuthorityCheckDocumentVO> GetCertAuthorityCheckDocuments(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            return this.certAuthorityChecksRepository.GetCertAuthorityCheckDocuments(certAuthorityCheckId);
        }

        [Route("{documentId:int}")]
        public CertAuthorityCheckDocumentDO GetCertAuthorityCheckDocument(int certAuthorityCheckId, int documentId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.View, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.Find(certAuthorityCheckId);

            var certAuthorityCheckDocument = certAuthorityCheck.FindCertAuthorityCheckDocument(documentId);

            return new CertAuthorityCheckDocumentDO(certAuthorityCheckDocument, certAuthorityCheck.Version);
        }

        [HttpGet]
        [Route("new")]
        public CertAuthorityCheckDocumentDO NewCertAuthorityCheckDocument(int certAuthorityCheckId)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.Find(certAuthorityCheckId);

            return new CertAuthorityCheckDocumentDO(certAuthorityCheckId, certAuthorityCheck.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.Documents.Edit), IdParam = "certAuthorityCheckId", ChildIdParam = "documentId")]
        public void UpdateCertAuthorityCheckDocument(int certAuthorityCheckId, int documentId, CertAuthorityCheckDocumentDO document)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, document.Version);

            certAuthorityCheck.UpdateCertAuthorityCheckDocument(
                documentId,
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.Documents.Create), IdParam = "certAuthorityCheckId")]
        public object AddCertAuthorityCheckDocument(int certAuthorityCheckId, CertAuthorityCheckDocumentDO document)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, document.Version);

            var newCertAuthorityCheckDocument = certAuthorityCheck.AddCertAuthorityCheckDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();

            return new { CertAuthorityCheckDocumentId = newCertAuthorityCheckDocument.CertAuthorityCheckDocumentId };
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertAuthorityChecks.Edit.Documents.Delete), IdParam = "certAuthorityCheckId", ChildIdParam = "documentId")]
        public void DeleteCertAuthorityCheckDocument(int certAuthorityCheckId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(CertAuthorityCheckActions.Edit, certAuthorityCheckId);

            byte[] vers = System.Convert.FromBase64String(version);

            var certAuthorityCheck = this.certAuthorityChecksRepository.FindForUpdate(certAuthorityCheckId, vers);

            certAuthorityCheck.RemoveCertAuthorityCheckDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
