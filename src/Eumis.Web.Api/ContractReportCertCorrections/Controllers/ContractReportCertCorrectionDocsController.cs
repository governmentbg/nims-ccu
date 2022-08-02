using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ContractReportCertCorrections.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertCorrections.Controllers
{
    [RoutePrefix("api/contractReportCertCorrections/{contractReportCertCorrectionId:int}/documents")]
    public class ContractReportCertCorrectionDocsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository;

        public ContractReportCertCorrectionDocsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IContractReportCertCorrectionsRepository contractReportCertCorrectionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.contractReportCertCorrectionsRepository = contractReportCertCorrectionsRepository;
        }

        [Route("")]
        public IList<ContractReportCertCorrectionDocumentVO> GetSignalDocuments(int contractReportCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.View, contractReportCertCorrectionId);

            return this.contractReportCertCorrectionsRepository.GetDocuments(contractReportCertCorrectionId);
        }

        [Route("{documentId:int}")]
        public ContractReportCertCorrectionDocumentDO GetSignalDocument(int contractReportCertCorrectionId, int documentId)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.View, contractReportCertCorrectionId);

            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.Find(contractReportCertCorrectionId);

            var document = contractReportCertCorrection.GetDocument(documentId);

            return new ContractReportCertCorrectionDocumentDO(document, contractReportCertCorrection.Version);
        }

        [HttpGet]
        [Route("new")]
        public ContractReportCertCorrectionDocumentDO NewSignalDocument(int contractReportCertCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.Find(contractReportCertCorrectionId);

            return new ContractReportCertCorrectionDocumentDO(contractReportCertCorrectionId, contractReportCertCorrection.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertCorrections.Edit.Documents.Create), IdParam = "contractReportCertCorrectionId")]
        public void AddSignalDocument(int contractReportCertCorrectionId, ContractReportCertCorrectionDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.FindForUpdate(contractReportCertCorrectionId, document.Version);

            contractReportCertCorrection.AddDocument(
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertCorrections.Edit.Documents.Edit), IdParam = "contractReportCertCorrectionId", ChildIdParam = "documentId")]
        public void UpdateSignalDocument(int contractReportCertCorrectionId, int documentId, ContractReportCertCorrectionDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.FindForUpdate(contractReportCertCorrectionId, document.Version);

            contractReportCertCorrection.UpdateDocument(
                documentId,
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCertCorrections.Edit.Documents.Delete), IdParam = "contractReportCertCorrectionId", ChildIdParam = "documentId")]
        public void DeleteSignalDocument(int contractReportCertCorrectionId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCertCorrectionActions.Edit, contractReportCertCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCertCorrection = this.contractReportCertCorrectionsRepository.FindForUpdate(contractReportCertCorrectionId, vers);

            contractReportCertCorrection.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
