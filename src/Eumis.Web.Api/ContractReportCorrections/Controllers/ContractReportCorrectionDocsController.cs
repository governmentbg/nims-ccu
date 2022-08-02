using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ContractReportCorrections.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCorrections.Controllers
{
    [RoutePrefix("api/contractReportCorrections/{contractReportCorrectionId:int}/documents")]
    public class ContractReportCorrectionDocsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IContractReportCorrectionsRepository contractReportCorrectionsRepository;

        public ContractReportCorrectionDocsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IContractReportCorrectionsRepository contractReportCorrectionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.contractReportCorrectionsRepository = contractReportCorrectionsRepository;
        }

        [Route("")]
        public IList<ContractReportCorrectionDocumentVO> GetSignalDocuments(int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.View, contractReportCorrectionId);

            return this.contractReportCorrectionsRepository.GetDocuments(contractReportCorrectionId);
        }

        [Route("{documentId:int}")]
        public ContractReportCorrectionDocumentDO GetSignalDocument(int contractReportCorrectionId, int documentId)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.View, contractReportCorrectionId);

            var contractReportCorrection = this.contractReportCorrectionsRepository.Find(contractReportCorrectionId);

            var document = contractReportCorrection.GetDocument(documentId);

            return new ContractReportCorrectionDocumentDO(document, contractReportCorrection.Version);
        }

        [HttpGet]
        [Route("new")]
        public ContractReportCorrectionDocumentDO NewSignalDocument(int contractReportCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            var contractReportCorrection = this.contractReportCorrectionsRepository.Find(contractReportCorrectionId);

            return new ContractReportCorrectionDocumentDO(contractReportCorrectionId, contractReportCorrection.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.Edit.Documents.Create), IdParam = "contractReportCorrectionId")]
        public void AddSignalDocument(int contractReportCorrectionId, ContractReportCorrectionDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            var contractReportCorrection = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, document.Version);

            contractReportCorrection.AddDocument(
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.Edit.Documents.Edit), IdParam = "contractReportCorrectionId", ChildIdParam = "documentId")]
        public void UpdateSignalDocument(int contractReportCorrectionId, int documentId, ContractReportCorrectionDocumentDO document)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            var contractReportCorrection = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, document.Version);

            contractReportCorrection.UpdateDocument(
                documentId,
                document.Description,
                document.File.Name,
                document.File.Key);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportCorrections.Edit.Documents.Delete), IdParam = "contractReportCorrectionId", ChildIdParam = "documentId")]
        public void DeleteSignalDocument(int contractReportCorrectionId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCorrectionActions.Edit, contractReportCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractReportCorrection = this.contractReportCorrectionsRepository.FindForUpdate(contractReportCorrectionId, vers);

            contractReportCorrection.RemoveDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
