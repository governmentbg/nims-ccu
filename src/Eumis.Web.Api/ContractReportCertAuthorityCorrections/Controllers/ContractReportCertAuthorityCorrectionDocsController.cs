using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ContractReportCertAuthorityCorrections.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/contractReportCertAuthorityCorrections/{contractReportCertAuthorityCorrectionId:int}/documents")]
    public class ContractReportCertAuthorityCorrectionDocsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository;

        public ContractReportCertAuthorityCorrectionDocsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IContractReportCertAuthorityCorrectionsRepository contractReportCertAuthorityCorrectionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.contractReportCertAuthorityCorrectionsRepository = contractReportCertAuthorityCorrectionsRepository;
        }

        [Route("")]
        public IList<ContractReportCertAuthorityCorrectionDocumentVO> GetSignalDocuments(int contractReportCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.View, contractReportCertAuthorityCorrectionId);

            return this.contractReportCertAuthorityCorrectionsRepository.GetDocuments(contractReportCertAuthorityCorrectionId);
        }

        [Route("{documentId:int}")]
        public ContractReportCertAuthorityCorrectionDocumentDO GetSignalDocument(int contractReportCertAuthorityCorrectionId, int documentId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.View, contractReportCertAuthorityCorrectionId);

            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.Find(contractReportCertAuthorityCorrectionId);

            var document = contractReportCertAuthorityCorrection.GetDocument(documentId);

            return new ContractReportCertAuthorityCorrectionDocumentDO(document, contractReportCertAuthorityCorrection.Version);
        }

        [HttpGet]
        [Route("new")]
        public ContractReportCertAuthorityCorrectionDocumentDO NewSignalDocument(int contractReportCertAuthorityCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.Find(contractReportCertAuthorityCorrectionId);

            return new ContractReportCertAuthorityCorrectionDocumentDO(contractReportCertAuthorityCorrectionId, contractReportCertAuthorityCorrection.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityCorrections.Edit.Documents.Create), IdParam = "contractReportCertAuthorityCorrectionId")]
        public void AddSignalDocument(int contractReportCertAuthorityCorrectionId, ContractReportCertAuthorityCorrectionDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.FindForUpdate(contractReportCertAuthorityCorrectionId, document.Version);

            contractReportCertAuthorityCorrection.AddDocument(
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityCorrections.Edit.Documents.Edit), IdParam = "contractReportCertAuthorityCorrectionId", ChildIdParam = "documentId")]
        public void UpdateSignalDocument(int contractReportCertAuthorityCorrectionId, int documentId, ContractReportCertAuthorityCorrectionDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.FindForUpdate(contractReportCertAuthorityCorrectionId, document.Version);

            contractReportCertAuthorityCorrection.UpdateDocument(
                documentId,
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertAuthorityCorrections.Edit.Documents.Delete), IdParam = "contractReportCertAuthorityCorrectionId", ChildIdParam = "documentId")]
        public void DeleteSignalDocument(int contractReportCertAuthorityCorrectionId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityCorrectionActions.Edit, contractReportCertAuthorityCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertAuthorityCorrection = this.contractReportCertAuthorityCorrectionsRepository.FindForUpdate(contractReportCertAuthorityCorrectionId, vers);

            contractReportCertAuthorityCorrection.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
