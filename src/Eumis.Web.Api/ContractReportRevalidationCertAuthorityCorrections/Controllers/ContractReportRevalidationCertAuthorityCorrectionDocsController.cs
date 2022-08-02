using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ContractReportRevalidationCertAuthorityCorrections.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/contractReportRevalidationCertAuthorityCorrections/{contractReportRevalidationCertAuthorityCorrectionId:int}/documents")]
    public class ContractReportRevalidationCertAuthorityCorrectionDocsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IContractReportRevalidationCertAuthorityCorrectionsRepository contractReportRevalidationCertAuthorityCorrectionsRepository;

        public ContractReportRevalidationCertAuthorityCorrectionDocsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IContractReportRevalidationCertAuthorityCorrectionsRepository contractReportRevalidationCertAuthorityCorrectionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.contractReportRevalidationCertAuthorityCorrectionsRepository = contractReportRevalidationCertAuthorityCorrectionsRepository;
        }

        [Route("")]
        public IList<ContractReportRevalidationCertAuthorityCorrectionDocumentVO> GetSignalDocuments(int contractReportRevalidationCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.View, contractReportRevalidationCertAuthorityCorrectionId);

            return this.contractReportRevalidationCertAuthorityCorrectionsRepository.GetDocuments(contractReportRevalidationCertAuthorityCorrectionId);
        }

        [Route("{documentId:int}")]
        public ContractReportRevalidationCertAuthorityCorrectionDocumentDO GetSignalDocument(int contractReportRevalidationCertAuthorityCorrectionId, int documentId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.View, contractReportRevalidationCertAuthorityCorrectionId);

            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.Find(contractReportRevalidationCertAuthorityCorrectionId);

            var document = contractReportRevalidationCertAuthorityCorrection.GetDocument(documentId);

            return new ContractReportRevalidationCertAuthorityCorrectionDocumentDO(document, contractReportRevalidationCertAuthorityCorrection.Version);
        }

        [HttpGet]
        [Route("new")]
        public ContractReportRevalidationCertAuthorityCorrectionDocumentDO NewSignalDocument(int contractReportRevalidationCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.Find(contractReportRevalidationCertAuthorityCorrectionId);

            return new ContractReportRevalidationCertAuthorityCorrectionDocumentDO(contractReportRevalidationCertAuthorityCorrectionId, contractReportRevalidationCertAuthorityCorrection.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityCorrections.Edit.Documents.Create), IdParam = "contractReportRevalidationCertAuthorityCorrectionId")]
        public void AddSignalDocument(int contractReportRevalidationCertAuthorityCorrectionId, ContractReportRevalidationCertAuthorityCorrectionDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityCorrectionId, document.Version);

            contractReportRevalidationCertAuthorityCorrection.AddDocument(
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityCorrections.Edit.Documents.Edit), IdParam = "contractReportRevalidationCertAuthorityCorrectionId", ChildIdParam = "documentId")]
        public void UpdateSignalDocument(int contractReportRevalidationCertAuthorityCorrectionId, int documentId, ContractReportRevalidationCertAuthorityCorrectionDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityCorrectionId, document.Version);

            contractReportRevalidationCertAuthorityCorrection.UpdateDocument(
                documentId,
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportRevalidationCertAuthorityCorrections.Edit.Documents.Delete), IdParam = "contractReportRevalidationCertAuthorityCorrectionId", ChildIdParam = "documentId")]
        public void DeleteSignalDocument(int contractReportRevalidationCertAuthorityCorrectionId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityCorrectionActions.Edit, contractReportRevalidationCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportRevalidationCertAuthorityCorrection = this.contractReportRevalidationCertAuthorityCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityCorrectionId, vers);

            contractReportRevalidationCertAuthorityCorrection.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
