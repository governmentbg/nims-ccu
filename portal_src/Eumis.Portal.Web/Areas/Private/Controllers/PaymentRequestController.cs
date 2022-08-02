using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Components.Web;
using Eumis.Documents;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Models;
using Eumis.Portal.Web.Models.PaymentRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using R_10045;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class PaymentRequestController : WorkflowController<EditVM>
    {
        private IBFPContractCommunicator _bfpContractCommunicator;
        private IPaymentRequestCommunicator _paymentRequestCommunicator;
        private IFinanceReportCommunicator _financeReportCommunicator;

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        public PaymentRequestController(
            IBFPContractCommunicator bfpContractCommunicator,
            IPaymentRequestCommunicator paymentRequestCommunicator,
            IFinanceReportCommunicator financeReportCommunicator,
            IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _bfpContractCommunicator = bfpContractCommunicator;
            _paymentRequestCommunicator = paymentRequestCommunicator;
            _financeReportCommunicator = financeReportCommunicator;
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutPaymentRequest();

            return base.Prepare(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Edit(Guid gid, string access_token)
        {
            this.InitAppContext(gid, access_token, true);

            return RedirectToAction("Prepare");
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Preview(Guid gid, string access_token)
        {
            return View(this.InitAppContext(gid, access_token, false));
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            PutPaymentRequest();
            _paymentRequestCommunicator.PrivateSubmitPaymentRequest
                (
                    AppContext.Current.WorkingDocument.gid,
                    AppContext.Current.WorkingDocument.token,
                    AppContext.Current.WorkingDocument.version
                );

            return View(MVC.Private.Shared.Views.Success, MVC.Private.Shared.Views._Layout, (object)Global.SuccessSave);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            PutPaymentRequest();

            base.SaveDraft();
        }

        private void PutPaymentRequest()
        {
            AppContext.Current.WorkingDocument.version =
                _paymentRequestCommunicator.PrivatePutPaymentRequest
                    (
                        AppContext.Current.WorkingDocument.gid,
                        AppContext.Current.WorkingDocument.token,
                        AppContext.Current.Xml,
                        AppContext.Current.WorkingDocument.version
                    ).version;
        }

        private R_10045.PaymentRequest InitAppContext(Guid gid, string access_token, bool isEdit)
        {
            SignInManager.SignIn(PrivateAreaConfiguration.CreatePrivateUser(access_token), false, false);

            AppContext.Current = new AppContext(DocumentMetadata.PaymentRequestMetadata.Code);

            var contractPaymentRequest = _paymentRequestCommunicator.PrivateGetPaymentRequest(gid, access_token);

            var paymentRequest = _documentSerializer.XmlDeserializeFromString<R_10045.PaymentRequest>(contractPaymentRequest.xml);
            paymentRequest.CanEnterErrors = contractPaymentRequest.CanEnterErrors;

            if (isEdit)
            {
                R_10045.PaymentRequest.LoadNomenclatures(ref paymentRequest, contractPaymentRequest.ProcedureContractReportPaymentDocuments);
                R_10045.PaymentRequest.SetAdvanceVerificationPaymentPresence(ref paymentRequest, contractPaymentRequest.contractReportHasAdvanceVerificationPayment);
            }

            AppContext.Current.Document = paymentRequest;
            AppContext.Current.Xml = contractPaymentRequest.xml;
            AppContext.Current.WorkingDocument = new WorkingDocumentData
            {
                gid = gid,
                token = access_token,
                version = contractPaymentRequest.version
            };

            return paymentRequest;
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual JsonResult GetFinanceAmount(ContractLoadFinance vm)
        {
            var version = _financeReportCommunicator.GetFinanceReport(new Guid(vm.contractGid), new Guid(vm.packageGid), CurrentUser.AccessToken);

            R_10043.FinanceReport report = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(version.xml);

            var amount = report.CostSupportingDocuments?.CostSupportingDocumentCollection.Sum(t => t.TotalAmount);
            return Json(new { amount = amount ?? 0 });
        }
    }
}