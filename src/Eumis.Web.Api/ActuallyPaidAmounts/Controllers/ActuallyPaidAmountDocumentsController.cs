using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ActuallyPaidAmounts.Repositories;
using Eumis.Data.ActuallyPaidAmounts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.ActuallyPaidAmounts.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ActuallyPaidAmounts.Controllers
{
    [RoutePrefix("api/actuallyPaidAmounts/{paidAmountId}/documents")]
    [ActionLogPrefix(typeof(ActionLogGroups.ActuallyPaidAmounts.Edit.Documents))]
    public class ActuallyPaidAmountDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActuallyPaidAmountsRepository paidAmountRepository;
        private IAuthorizer authorizer;

        public ActuallyPaidAmountDocumentsController(IUnitOfWork unitOfWork, IActuallyPaidAmountsRepository paidAmountRepository, IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.paidAmountRepository = paidAmountRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ActuallyPaidAmountDocumentVO> GetActuallyPaidAmountDocuments(int paidAmountId)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.View, paidAmountId);

            return this.paidAmountRepository.GetActuallyPaidAmountDocuments(paidAmountId);
        }

        [Route("{documentId:int}")]
        public ActuallyPaidAmountDocumentDO GetActuallyPaidAmountDocument(int paidAmountId, int documentId)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.View, paidAmountId);

            var actualPayment = this.paidAmountRepository.Find(paidAmountId);
            var actualPaymentDocument = actualPayment.FindActuallyPaidAmountDocument(documentId);

            return new ActuallyPaidAmountDocumentDO(actualPaymentDocument, actualPayment.Version);
        }

        [HttpGet]
        [Route("new")]
        public ActuallyPaidAmountDocumentDO NewActuallyPaidAmountDocument(int paidAmountId)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            var actualPayment = this.paidAmountRepository.Find(paidAmountId);

            return new ActuallyPaidAmountDocumentDO(paidAmountId, actualPayment.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Edit", IdParam = "paidAmountId", ChildIdParam = "documentId")]
        public void UpdateActuallyPaidAmountDocument(int paidAmountId, int documentId, ActuallyPaidAmountDocumentDO document)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            var actualPayment = this.paidAmountRepository.FindForUpdate(paidAmountId, document.Version);

            actualPayment.UpdateActuallyPaidAmountDocument(
                documentId,
                document.Name,
                document.Description,
                document.File?.Key);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Create", IdParam = "paidAmountId")]
        public virtual void AddActuallyPaidAmountDocument(int paidAmountId, ActuallyPaidAmountDocumentDO document)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            var actualPayment = this.paidAmountRepository.FindForUpdate(paidAmountId, document.Version);

            actualPayment.AddActuallyPaidAmountDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLogSuffix(SuffixAction = "Delete", IdParam = "paidAmountId", ChildIdParam = "documentId")]
        public virtual void DeleteActuallyPaidAmountDocument(int paidAmountId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var actualPayment = this.paidAmountRepository.FindForUpdate(paidAmountId, vers);

            actualPayment.RemoveActuallyPaidAmountDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
