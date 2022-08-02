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
using Eumis.Portal.Web.Models.FinanceReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Eumis.Common.Crypto;
using Eumis.Portal.Web.Helplers;
using Eumis.Common.Extensions;
using NLog;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class FinanceReportController : WorkflowController<EditVM>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Guid _contractGid;
        private IFinanceReportCommunicator _financeReportCommunicator;
        private IBFPContractCommunicator _bfpContractCommunicator;

        public FinanceReportController(IFinanceReportCommunicator financeReportCommunicator,
            IBFPContractCommunicator bfpContractCommunicator,
            IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _financeReportCommunicator = financeReportCommunicator;
            _bfpContractCommunicator = bfpContractCommunicator;
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
        }

        [HttpGet]
        public virtual ActionResult New(Guid packageGid)
        {
            var canCreateInfo = _financeReportCommunicator.CanCreateFinanceReport(_contractGid, packageGid, CurrentUser.AccessToken);
            if (canCreateInfo != null && canCreateInfo.errors != null && canCreateInfo.errors.Count > 0)
            { 
                TempData["package_errors"] = canCreateInfo.errors;
                return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name, new { area = MVC.Report.Name });
            }

            var newReport = _financeReportCommunicator.CreateFinanceReport(_contractGid, packageGid, CurrentUser.AccessToken);

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
            var version = _financeReportCommunicator.GetFinanceReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, true);

            var financeReport = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(version.xml);
            financeReport.CanEnterErrors = version.CanEnterErrors;

            R_10043.FinanceReport.LoadNomenclatures(ref financeReport, version.ProcedureContractReportFinancialDocuments);

            AppContext.Current = new AppContext(DocumentMetadata.FinanceReportMetadata.Code);
            AppContext.Current.Document = financeReport;
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
            var version = _financeReportCommunicator.GetFinanceReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, false);

            var financeReport = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(version.xml);

            AppContext.Current = new AppContext(DocumentMetadata.FinanceReportMetadata.Code);
            AppContext.Current.Document = financeReport;

            return View(financeReport);
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid gid, Guid packageGid, string version)
        {
            _financeReportCommunicator.DeleteFinanceReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, version);

            TempData["SuccessAction"] = "Финансовият отчет е изтрит успешно.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        public virtual ActionResult MakeDraft(Guid gid, Guid packageGid, string version)
        {
            _financeReportCommunicator.MakeDraftFinanceReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));

            TempData["SuccessAction"] = "Финансовият отчет е върнат в статус Чернова.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        public virtual ActionResult MakeActual(Guid gid, Guid packageGid, string version)
        {
            _financeReportCommunicator.MakeActualFinanceReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));

            TempData["SuccessAction"] = "Финансовият отчет успешно премина в статус Актуален.";

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

            var updateDraft = _financeReportCommunicator.PutFinanceReport
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
            var financeReport = (R_10043.FinanceReport)AppContext.Current.Document;

            AppContext.Current.Document = financeReport;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10043.FinanceReport>(financeReport);

            PutDraft();

            _financeReportCommunicator.SubmitFinanceReport
                (
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    AppContext.Current.WorkingDocument.version
                );

            TempData["SuccessAction"] = "Финансовият отчет е приключен успешно.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual FileResult Download()
        {
            var financeReport = (R_10043.FinanceReport)AppContext.Current.Document;
            var xml = _documentSerializer.XmlSerializeToString<R_10043.FinanceReport>(financeReport);

            var hash = CryptoUtils.GetSha256XMLHash(xml).Truncate(10);

            var isunFile = IsunFileManager.CreateFinanceReportFile(xml, hash);

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
            HttpPostedFileBase hpfb = Request.Files["applied_fisun_file"];

            string xml = null;
            R_10043.FinanceReport financeReport = null;

            if (hpfb != null && hpfb.ContentLength != 0)
            {
                try
                {
                    xml = ZipManager.UnzipProject(hpfb.InputStream);

                    financeReport = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(xml);

                }
                catch (Exception ex)
                {
                    Logger.Error("Invalid file format", ex);

                    TempData["file_error"] = Global.AttachedFileIsCorrupted;

                    return RedirectToAction(MVC.Report.FinanceReport.ActionNames.Upload);
                }
            }
            else
            {
                TempData["file_error"] = Global.PleaseAttachFinanceReport;

                return RedirectToAction(MVC.Report.FinanceReport.ActionNames.Upload);
            }

            var currentFinanceReport = (R_10043.FinanceReport)AppContext.Current.Document;
            if (financeReport != null)
            {
                if (financeReport.contractGid != currentFinanceReport.contractGid)
                {
                    TempData["file_error"] = Global.FinanceReportMissmatchContract;
                    return RedirectToAction(MVC.Report.FinanceReport.ActionNames.Upload);
                }

                currentFinanceReport.Copy(financeReport);

                var versionfinanceReport = _financeReportCommunicator.GetFinanceReport(
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    true);

                var updateDraft = _financeReportCommunicator.PutFinanceReport
                (
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    _documentSerializer.XmlSerializeToString<R_10043.FinanceReport>(currentFinanceReport),
                    versionfinanceReport.version
                );

                return RedirectToAction(MVC.Report.FinanceReport.ActionNames.Edit, MVC.Report.FinanceReport.Name, new { gid = AppContext.Current.WorkingDocument.gid, packageGid = financeReport.packageGid });
            }

            TempData["file_error"] = String.Format(Global.NotSameFormat, hpfb.FileName);

            return RedirectToAction(MVC.Report.FinanceReport.ActionNames.Upload);
        }
    }
}
