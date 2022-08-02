using Eumis.Common.Crypto;
using Eumis.Common.Extensions;
using Eumis.Common.Resources;
using Eumis.Components;
using Eumis.Components.Communicators;
using Eumis.Documents;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Eumis.Portal.Web.Helplers;
using Eumis.Portal.Web.Models.TechnicalReport;
using NLog;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class TechnicalReportController : WorkflowController<EditVM>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Guid _contractGid;
        private ITechnicalReportCommunicator _technicalReportCommunicator;
        private IBFPContractCommunicator _bfpContractCommunicator;

        public TechnicalReportController(ITechnicalReportCommunicator technicalReportCommunicator,
            IBFPContractCommunicator bfpContractCommunicator,
            IDocumentSerializer documentSerializer)
            : base(documentSerializer)
        {
            _technicalReportCommunicator = technicalReportCommunicator;
            _bfpContractCommunicator = bfpContractCommunicator;
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
        }

        [HttpGet]
        public virtual ActionResult New(Guid packageGid)
        {
            var canCreateInfo = _technicalReportCommunicator.CanCreateTechnicalReport(_contractGid, packageGid, CurrentUser.AccessToken);
            if (canCreateInfo != null && canCreateInfo.errors != null && canCreateInfo.errors.Count > 0)
            {
                TempData["package_errors"] = canCreateInfo.errors;
                return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name, new { area = MVC.Report.Name });
            }

            var newReport = _technicalReportCommunicator.CreateTechnicalReport(_contractGid, packageGid, CurrentUser.AccessToken);

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
            var version = _technicalReportCommunicator.GetTechnicalReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, true);

            var technicalReport = _documentSerializer.XmlDeserializeFromString<R_10044.TechnicalReport>(version.xml);
            technicalReport.CanEnterErrors = version.CanEnterErrors;

            var procedureApplicationSections = _bfpContractCommunicator.GetProcedureApplciationSections(_contractGid, CurrentUser.AccessToken);

            R_10044.TechnicalReport.LoadNomenclatures(ref technicalReport, version.ProcedureContractReportTechnicalDocuments, procedureApplicationSections);

            AppContext.Current = new AppContext(DocumentMetadata.TechnicalReportMetadata.Code);
            AppContext.Current.Document = technicalReport;
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
            var version = _technicalReportCommunicator.GetTechnicalReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, false);

            var technicalReport = _documentSerializer.XmlDeserializeFromString<R_10044.TechnicalReport>(version.xml);
            
            var procedureApplicationSections = _bfpContractCommunicator.GetProcedureApplciationSections(_contractGid, CurrentUser.AccessToken);
            R_10044.TechnicalReport.LoadNomenclatures(ref technicalReport, version.ProcedureContractReportTechnicalDocuments, procedureApplicationSections);

            AppContext.Current = new AppContext(DocumentMetadata.TechnicalReportMetadata.Code);
            AppContext.Current.Document = technicalReport;

            return View(technicalReport);
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid gid, Guid packageGid, string version)
        {
            _technicalReportCommunicator.DeleteTechnicalReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, version);

            TempData["SuccessAction"] = "Техническият отчет е изтрит успешно.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        public virtual ActionResult MakeDraft(Guid gid, Guid packageGid, string version)
        {
            _technicalReportCommunicator.MakeDraftTechnicalReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));

            TempData["SuccessAction"] = "Техническият отчет е върнат в статус Чернова.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        public virtual ActionResult MakeActual(Guid gid, Guid packageGid, string version)
        {
            _technicalReportCommunicator.MakeActualTechnicalReport(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));

            TempData["SuccessAction"] = "Техническият отчет успешно премина в статус Актуален.";

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

            var updateDraft = _technicalReportCommunicator.PutTechnicalReport
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
            var technicalReport = (R_10044.TechnicalReport)AppContext.Current.Document;

            AppContext.Current.Document = technicalReport;
            AppContext.Current.Xml = _documentSerializer.XmlSerializeToString<R_10044.TechnicalReport>(technicalReport);

            PutDraft();

            _technicalReportCommunicator.SubmitTechnicalReport
                (
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    AppContext.Current.WorkingDocument.version
                );

            TempData["SuccessAction"] = "Техническият отчет е приключен успешно.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        [RequiresAppContext]
        public virtual FileResult Download()
        {
            var technicalReport = (R_10044.TechnicalReport)AppContext.Current.Document;
            var xml = _documentSerializer.XmlSerializeToString<R_10044.TechnicalReport>(technicalReport);

            var hash = CryptoUtils.GetSha256XMLHash(xml).Truncate(10);

            var isunFile = IsunFileManager.CreateTechnicalReportFile(xml, hash);

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
            HttpPostedFileBase hpfb = Request.Files["applied_tisun_file"];

            string xml = null;
            R_10044.TechnicalReport technicalReport = null;

            if (hpfb != null && hpfb.ContentLength != 0)
            {
                try
                {
                    xml = ZipManager.UnzipProject(hpfb.InputStream);

                    technicalReport = _documentSerializer.XmlDeserializeFromString<R_10044.TechnicalReport>(xml);

                }
                catch(Exception ex)
                {
                    Logger.Error("Invalid file format", ex);

                    TempData["file_error"] = Global.AttachedFileIsCorrupted;

                    return RedirectToAction(MVC.Report.TechnicalReport.ActionNames.Upload);
                }
            }
            else
            {
                TempData["file_error"] = Global.PleaseAttachTechnicalReport;

                return RedirectToAction(MVC.Report.TechnicalReport.ActionNames.Upload);
            }

            var currentTechnicalReport = (R_10044.TechnicalReport)AppContext.Current.Document;
            if (technicalReport != null)
            {
                if (technicalReport.contractGid != currentTechnicalReport.contractGid)
                {
                    TempData["file_error"] = Global.TechnicalReportMissmatchContract;
                    return RedirectToAction(MVC.Report.TechnicalReport.ActionNames.Upload);
                }

                currentTechnicalReport.Load(technicalReport);

                var versionTechnicalReport = _technicalReportCommunicator.GetTechnicalReport(
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    true);

                var updateDraft = _technicalReportCommunicator.PutTechnicalReport
                (
                    _contractGid,
                    AppContext.Current.WorkingDocument.packageGid,
                    AppContext.Current.WorkingDocument.gid,
                    CurrentUser.AccessToken,
                    _documentSerializer.XmlSerializeToString<R_10044.TechnicalReport>(technicalReport),
                    versionTechnicalReport.version
                );

                return RedirectToAction(MVC.Report.TechnicalReport.ActionNames.Edit, MVC.Report.TechnicalReport.Name, new { gid = AppContext.Current.WorkingDocument.gid, packageGid = technicalReport.packageGid});
            }

            TempData["file_error"] = String.Format(Global.NotSameFormat, hpfb.FileName);

            return RedirectToAction(MVC.Report.TechnicalReport.ActionNames.Upload);
        }
    }
}
