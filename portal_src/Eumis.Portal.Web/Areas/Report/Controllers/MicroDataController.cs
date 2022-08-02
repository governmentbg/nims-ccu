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
using Eumis.Portal.Web.Models.TechnicalReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using Eumis.Portal.Web.Areas.Report.Models.MicroData;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    [Authenticated]
    public partial class MicroDataController : BaseController
    {
        private Guid _contractGid;
        private IMicroDataCommunicator _microDataCommunicator;

        public MicroDataController(IMicroDataCommunicator microDataCommunicator)
        {
            _microDataCommunicator = microDataCommunicator;
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
        }

        [HttpGet]
        public virtual ActionResult New(Guid packageGid, ContractReportMicroType type)
        {
            var canCreateInfo = _microDataCommunicator.CanCreate(_contractGid, packageGid, type, CurrentUser.AccessToken);

            if (canCreateInfo != null && canCreateInfo.errors != null && canCreateInfo.errors.Count > 0)
            {
                TempData["package_errors"] = canCreateInfo.errors;
            }
            else
            {
                _microDataCommunicator.Create(_contractGid, packageGid, type, CurrentUser.AccessToken);
                TempData["SuccessAction"] = "Микроданните са създадени успешно.";
            }

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name, new { area = MVC.Report.Name });
        }

        [HttpGet]
        public virtual ActionResult Type1(Guid gid, Guid packageGid, int page = 1)
        {
            int offset = (page - 1) * Constants.MICRO_PAGE_ITEMS_COUNT;
            var items = _microDataCommunicator.GetType1(_contractGid, packageGid, gid, CurrentUser.AccessToken, Constants.MICRO_PAGE_ITEMS_COUNT, offset);

            var model = new MicroStaticPagedList<ContractReportMicroType1ItemPVO>(items.items.results, page, Constants.MICRO_PAGE_ITEMS_COUNT, items.items.count, items.versionNum, items.contractNumber);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Type2(Guid gid, Guid packageGid, int page = 1)
        {
            int offset = (page - 1) * Constants.MICRO_PAGE_ITEMS_COUNT;
            var items = _microDataCommunicator.GetType2(_contractGid, packageGid, gid, CurrentUser.AccessToken, Constants.MICRO_PAGE_ITEMS_COUNT, offset);

            var model = new MicroStaticPagedList<ContractReportMicroType2ItemPVO>(items.items.results, page, Constants.MICRO_PAGE_ITEMS_COUNT, items.items.count, items.versionNum, items.contractNumber);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Type3(Guid gid, Guid packageGid, int page = 1)
        {
            int offset = (page - 1) * Constants.MICRO_PAGE_ITEMS_COUNT;
            var items = _microDataCommunicator.GetType3(_contractGid, packageGid, gid, CurrentUser.AccessToken, Constants.MICRO_PAGE_ITEMS_COUNT, offset);

            var model = new MicroStaticPagedList<ContractReportMicroType3ItemPVO>(items.items.results, page, Constants.MICRO_PAGE_ITEMS_COUNT, items.items.count, items.versionNum, items.contractNumber);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Type4(Guid gid, Guid packageGid, int page = 1)
        {
            int offset = (page - 1) * Constants.MICRO_PAGE_ITEMS_COUNT;
            var items = _microDataCommunicator.GetType4(_contractGid, packageGid, gid, CurrentUser.AccessToken, Constants.MICRO_PAGE_ITEMS_COUNT, offset);

            var model = new MicroStaticPagedList<ContractReportMicroType4ItemPVO>(items.items.results, page, Constants.MICRO_PAGE_ITEMS_COUNT, items.items.count, items.versionNum, items.contractNumber);

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Download(Guid gid, Guid packageGid, Guid fileKey)
        {
            if (!_microDataCommunicator.CheckMicroHasFile(_contractGid, packageGid, gid, fileKey, CurrentUser.AccessToken))
            {
                return new HttpNotFoundResult();
            }

            return this.DownloadFile(fileKey);
        }

        [HttpGet]
        public virtual ActionResult Delete(Guid gid, Guid packageGid, string version)
        {
            _microDataCommunicator.Delete(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));

            TempData["SuccessAction"] = "Микроданните са изтрити успешно.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        public virtual ActionResult MakeDraft(Guid gid, Guid packageGid, string version)
        {
            _microDataCommunicator.MakeDraft(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));

            TempData["SuccessAction"] = "Микроданните са върнати в статус Чернова.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        public virtual ActionResult MakeActual(Guid gid, Guid packageGid, string version)
        {
            _microDataCommunicator.MakeActual(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));

            TempData["SuccessAction"] = "Микроданните успешно преминаха в статус Актуален.";

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }

        [HttpGet]
        public virtual ActionResult SaveDraft(Guid gid, Guid packageGid, string version, Guid blobKey, string fileName)
        {
            var updateDraftInfo = _microDataCommunicator.Put
                (
                    _contractGid,
                    packageGid,
                    gid,
                    CurrentUser.AccessToken,
                    blobKey,
                    fileName,
                    Convert.FromBase64String(version)
                );

            if (updateDraftInfo != null && updateDraftInfo.warnings != null && updateDraftInfo.warnings.Count > 0)
            {
                TempData["package_warnings"] = updateDraftInfo.warnings;
            }

            if (updateDraftInfo != null && updateDraftInfo.errors != null && updateDraftInfo.errors.Count > 0)
            {
                TempData["package_errors"] = updateDraftInfo.errors;
            }
            else
            {
                TempData["SuccessAction"] = "Микроданните са запазени успешно.";
            }

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name, new { area = MVC.Report.Name });
        }

        [HttpGet]
        public virtual ActionResult SaveDraftWithSimevCode(Guid gid, Guid packageGid, string version, string simevCode)
        {
            var updateDraftInfo = _microDataCommunicator.PutWithSimevCode
                (
                    _contractGid,
                    packageGid,
                    gid,
                    CurrentUser.AccessToken,
                    simevCode,
                    Convert.FromBase64String(version)
                );

            if (updateDraftInfo != null && updateDraftInfo.errors != null && updateDraftInfo.errors.Count > 0)
            {
                TempData["package_errors"] = updateDraftInfo.errors;
            }
            else
            {
                TempData["SuccessAction"] = "Микроданните са запазени успешно.";
            }

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name, new { area = MVC.Report.Name });
        }

        [HttpGet]
        public virtual ActionResult Submit(Guid gid, Guid packageGid, string version)
        {
            var canSubmitInfo = _microDataCommunicator.CanSubmit(_contractGid, packageGid, gid, CurrentUser.AccessToken);
            if (canSubmitInfo != null && canSubmitInfo.errors != null && canSubmitInfo.errors.Count > 0)
            { 
                TempData["package_errors"] = canSubmitInfo.errors;
            }
            else
            {
                _microDataCommunicator.Submit(_contractGid, packageGid, gid, CurrentUser.AccessToken, Convert.FromBase64String(version));
                TempData["SuccessAction"] = "Микроданните са изпратени успешно.";
            }

            return RedirectToAction(MVC.Report.Package.ActionNames.Index, MVC.Report.Package.Name);
        }
    }
}