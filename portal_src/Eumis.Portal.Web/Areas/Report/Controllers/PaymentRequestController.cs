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
using Eumis.Common.Crypto;
using Eumis.Common.Extensions;
using Eumis.Portal.Web.Helplers;
using Eumis.Portal.Web.Views.Shared.App_LocalResources;
using NLog;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class PaymentRequestController : WorkflowController<EditVM>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Guid _contractGid;
        private IPaymentRequestCommunicator _paymentRequestCommunicator;
        private IBFPContractCommunicator _bfpContractCommunicator;
        private IFinanceReportCommunicator _financeReportCommunicator;

        public PaymentRequestController(IPaymentRequestCommunicator paymentRequestCommunicator,
            IBFPContractCommunicator bfpContractCommunicator,
            IFinanceReportCommunicator financeReportCommunicator,
            IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _paymentRequestCommunicator = paymentRequestCommunicator;
            _bfpContractCommunicator = bfpContractCommunicator;
            _financeReportCommunicator = financeReportCommunicator;
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
        }

        [HttpGet]
        public virtual ActionResult New(Guid packageGid, string type)
        {
            var canCreateInfo = _paymentRequestCommunicator.CanCreatePaymentRequest(_contractGid, packageGid, type, CurrentUser.AccessToken);
            if (canCreateInfo != null && canCreateInfo.errors != null && canCreateInfo.errors.Count > 0)
            {
                TempData["package_errors"] = canCreateInfo.errors;
                return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name, new { area = MVC.Report.Name });
            }

            var newReport = _paymentRequestCommunicator.CreatePaymentRequest(_contractGid, packageGid, type, CurrentUser.AccessToken);

            return RedirectToAction(ActionNames.Edit, new { gid = newReport.gid.Value, packageGid = packageGid });
        }

        [HttpPost]
        [RequiresAppContext]
        public override ActionResult Prepare(EditVM vm)
        {
            base.Save(vm);

            this.PutDraft();

            return base.Prepare(vm);
        }

        [HttpGet]
        public virtual ActionResult Edit(Guid gid, Guid packageGid)
        {
            var version = _paymentRequestCommunicator.GetPaymentRequest(_contractGid, packageGid, gid, CurrentUser.AccessToken);

            var paymentRequest = _documentSerializer.XmlDeserializeFromString<R_10045.PaymentRequest>(version.xml);
            paymentRequest.CanEnterErrors = version.CanEnterErrors;

            R_10045.PaymentRequest.LoadNomenclatures(ref paymentRequest, version.ProcedureContractReportPaymentDocuments);
            R_10045.PaymentRequest.SetAdvanceVerificationPaymentPresence(ref paymentRequest, version.contractReportHasAdvanceVerificationPayment);

            AppContext.Current = new AppContext(DocumentMetadata.PaymentRequestMetadata.Code);
            AppContext.Current.Document = paymentRequest;
            AppContext.Current.WorkingDocument = new WorkingDocumentData()
            {
                gid = gid,
                packageGid = packageGid,
                version = version.version
            };

            return RedirectToAction(ActionNames.Prepare);
        }

        [HttpGet]
        public virtual ActionResult Preview(Guid gid, Guid packageGid)
        {
            var version = _paymentRequestCommunicator.GetPaymentRequest(_contractGid, packageGid, gid, CurrentUser.AccessToken);

            var paymentRequest = _documentSerializer.XmlDeserializeFromString<R_10045.PaymentRequest>(version.xml);

            AppContext.Current = new AppContext(DocumentMetadata.PaymentRequestMetadata.Code);
            AppContext.Current.Document = paymentRequest;

            return View(paymentRequest);
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid gid, Guid packageGid, string version)
        {
            _paymentRequestCommunicator.DeletePaymentRequest(_contractGid, packageGid, gid, CurrentUser.AccessToken, version);

            TempData["SuccessAction"] = "Искането за плащане е изтрито успешно.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        public virtual ActionResult MakeDraft(Guid gid, Guid packageGid, string version)
        {
            _paymentRequestCommunicator.MakeDraftPaymentRequest(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));

            TempData["SuccessAction"] = "Искането за плащане е върнато в статус Чернова.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        public virtual ActionResult MakeActual(Guid gid, Guid packageGid, string version)
        {
            _paymentRequestCommunicator.MakeActualPaymentRequest(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));

            TempData["SuccessAction"] = "Искането за плащане успешно премина в статус Актуален.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpPost]
        public override void SaveDraft()
        {
            var modifyDate = PutDraft();

            if (!modifyDate.HasValue)
            {
                base.SaveDraft();
            }
            else
            {
                AppContext.Current.LastSaveDate = modifyDate;
            }
        }

        private DateTime? PutDraft()
        {
            byte[] version;
            DateTime? modifyDate = null;

            if (string.IsNullOrEmpty(AppContext.Current.Xml))
            {
                AppContext.Current.Xml = _documentSerializer.XmlSerializeObjectToString(AppContext.Current.Document);
            }

            var updateDraft = _paymentRequestCommunicator.PutPaymentRequest
                (
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    AppContext.Current.Xml,
                    AppContext.Current.WorkingDocument.version
                );

            version = updateDraft.version;
            modifyDate = updateDraft.modifyDate;

            AppContext.Current.WorkingDocument.version = version;
            return modifyDate;
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Submit()
        {
            var paymentRequest = (R_10045.PaymentRequest)AppContext.Current.Document;

            AppContext.Current.Document = paymentRequest;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10045.PaymentRequest>(paymentRequest);

            PutDraft();

            _paymentRequestCommunicator.SubmitPaymentRequest
                (
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    AppContext.Current.WorkingDocument.version
                );

            TempData["SuccessAction"] = "Искането за плащане е приключено успешно.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual JsonResult GetFinanceAmount(ContractLoadFinance vm)
        {
            var version = _financeReportCommunicator.GetFinanceReport(new Guid(vm.contractGid), new Guid(vm.packageGid), CurrentUser.AccessToken);

            R_10043.FinanceReport report = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(version.xml);

            var amount = report.CostSupportingDocuments?.CostSupportingDocumentCollection.Sum(t => t.TotalAmount);

            return Json(new { amount = amount  ?? 0 });
        }


        [HttpGet]
        [RequiresAppContext]
        public virtual FileResult Download()
        {
            var paymentRequest = (R_10045.PaymentRequest)AppContext.Current.Document;
            var xml = _documentSerializer.XmlSerializeToString<R_10045.PaymentRequest>(paymentRequest);

            var hash = CryptoUtils.GetSha256XMLHash(xml).Truncate(10);

            var isunFile = IsunFileManager.CreatePaymentRequestFile(xml, hash);

            return File(isunFile.Content, isunFile.MimeType, isunFile.Filename);
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Upload(FormCollection form)
        {
            HttpPostedFileBase hpfb = Request.Files["applied_pisun_file"];

            string xml = null;
            R_10045.PaymentRequest paymentRequest = null;

            if (hpfb != null && hpfb.ContentLength != 0)
            {
                try
                {
                    xml = ZipManager.UnzipProject(hpfb.InputStream);

                    paymentRequest = _documentSerializer.XmlDeserializeFromString<R_10045.PaymentRequest>(xml);

                }
                catch (Exception ex)
                {
                    Logger.Error("Invalid file format", ex);

                    TempData["file_error"] = Global.AttachedFileIsCorrupted;

                    return RedirectToAction(MVC.Report.PaymentRequest.ActionNames.Upload);
                }
            }
            else
            {
                TempData["file_error"] = Global.PleaseAttachPaymentRequest;

                return RedirectToAction(MVC.Report.PaymentRequest.ActionNames.Upload);
            }

            var currentPaymentRequest = (R_10045.PaymentRequest)AppContext.Current.Document;
            if (paymentRequest != null)
            {
                if (paymentRequest.contractGid != currentPaymentRequest.contractGid)
                {
                    TempData["file_error"] = Global.PaymentRequestMissmatchContract;
                    return RedirectToAction(MVC.Report.PaymentRequest.ActionNames.Upload);
                }

                if (currentPaymentRequest.BasicData.IsAdvanceType != paymentRequest.BasicData.IsAdvanceType)
                {
                    TempData["file_error"] = string.Format(PaymentRequest.PaymentTypeMissmatch, currentPaymentRequest.BasicData.Type.text, paymentRequest.BasicData.Type.text);
                    return RedirectToAction(MVC.Report.PaymentRequest.ActionNames.Upload);
                }

                currentPaymentRequest.Copy(paymentRequest);

                var versionPaymentRequest = _paymentRequestCommunicator.GetPaymentRequest(
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken);

                var updateDraft = _paymentRequestCommunicator.PutPaymentRequest
                (
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    _documentSerializer.XmlSerializeToString<R_10045.PaymentRequest>(paymentRequest),
                    versionPaymentRequest.version
                );

                return RedirectToAction(MVC.Report.PaymentRequest.ActionNames.Edit, MVC.Report.PaymentRequest.Name, new { gid = AppContext.Current.WorkingDocument.gid, packageGid = paymentRequest.packageGid });
            }

            TempData["file_error"] = String.Format(Global.NotSameFormat, hpfb.FileName);

            return RedirectToAction(MVC.Report.PaymentRequest.ActionNames.Upload);
        }
    }
}
