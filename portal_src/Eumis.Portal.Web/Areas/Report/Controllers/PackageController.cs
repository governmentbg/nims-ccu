using System;
using System.Web.Mvc;

using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Areas.Report.Models.Package;
using Eumis.Components.Communicators;
using Newtonsoft.Json.Converters;
using PagedList;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Helpers.Attributes;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    [Authenticated]
    public partial class PackageController : BaseController
    {
        private Guid _contractGid;
        private IPackageCommunicator _packageCommunicator;
        private IBFPContractCommunicator _bFPContractCommunicator;
        
        public PackageController(
            IPackageCommunicator packageCommunicator,
            IBFPContractCommunicator bFPContractCommunicator)
        {
            _packageCommunicator = packageCommunicator;
            _bFPContractCommunicator = bFPContractCommunicator;
            _contractGid = new Guid(System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData.Values["cgid"].ToString());
        }

        public virtual ActionResult Index(int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var packages = _packageCommunicator.GetPackages(_contractGid, CurrentUser.AccessToken, Constants.PAGE_ITEMS_COUNT, offset);
            var procedureApplicationSections = _bFPContractCommunicator.GetProcedureApplciationSections(_contractGid, CurrentUser.AccessToken);
            
            var model = new IndexVM
            {
                Packages = new StaticPagedList<ContractReportPVO>(packages.results, page, Constants.PAGE_ITEMS_COUNT, packages.count),
                CanCreate = packages.canCreate,
                CanEditSent = packages.canEditSent,
                ProcedureApplicationSections = procedureApplicationSections,
            };

            return View(model);
        }

        #region Create

        [HttpGet]
        public virtual ActionResult Create()
        {
            var procedureApplicationSections = _bFPContractCommunicator.GetProcedureApplciationSections(_contractGid, CurrentUser.AccessToken);
            PackageEditVM vm = new PackageEditVM(procedureApplicationSections);

            return View(Views.CreateEdit, vm);
        }

        [HttpPost]
        public virtual ActionResult Create(PackageEditVM vm)
        {
            if (vm.ContractReportType == null || String.IsNullOrWhiteSpace(vm.ContractReportType.Value) || String.IsNullOrWhiteSpace(vm.ContractReportType.Description))
            {
                ModelState.AddModelError("ContractReportType.Value", "Полето \"Тип\" е задължително.");
            }

            if (!ModelState.IsValid)
            {
                return View(Views.CreateEdit, vm);
            }

            var result = _packageCommunicator.CanCreatePackage(_contractGid, CurrentUser.AccessToken);

            if (result != null && result.errors != null && result.errors.Count > 0)
            {
                for (int i = 0; i < result.errors.Count; i++)
                {
                    ModelState.AddModelError("error" + i, result.errors[i]);
                }
                return View(Views.CreateEdit, vm);
            }

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(vm, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss" });

            _packageCommunicator.CreatePackage(_contractGid, CurrentUser.AccessToken, body);

            return RedirectToAction(ActionNames.Index);
        }

        #endregion

        #region Copy

        [HttpGet]
        public virtual ActionResult Copy(Guid gid)
        {
            var result = _packageCommunicator.CanCopyPackage(_contractGid, gid, CurrentUser.AccessToken);
            if (result != null && result.errors != null && result.errors.Count > 0)
            {
                TempData["package_errors"] = result.errors;

                return RedirectToAction(ActionNames.Index);
            }

            var package = _packageCommunicator.CopyPackage(_contractGid, gid, CurrentUser.AccessToken);

            return RedirectToAction(ActionNames.Edit, new { gid = package.gid });
        }

        #endregion

        #region Edit

        [HttpGet]
        public virtual ActionResult Edit(Guid gid)
        {
            var package = _packageCommunicator.GetPackage(_contractGid, gid, CurrentUser.AccessToken);
            var procedureApplicationSections = _bFPContractCommunicator.GetProcedureApplciationSections(_contractGid, CurrentUser.AccessToken);

            PackageEditVM vm = new PackageEditVM(package, procedureApplicationSections);

            return View(Views.CreateEdit, vm);
        }

        [HttpPost]
        public virtual ActionResult Edit(PackageEditVM vm)
        {
            if (vm.ContractReportType == null || String.IsNullOrWhiteSpace(vm.ContractReportType.Value) || String.IsNullOrWhiteSpace(vm.ContractReportType.Description))
            {
                ModelState.AddModelError("ContractReportType.Value", "Полето \"Тип\" е задължително.");
            }

            if (!ModelState.IsValid)
            {
                vm.IsEdit = true;
                return View(Views.CreateEdit, vm);
            }

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(vm, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss" });

            var result = _packageCommunicator.CanUpdatePackage(_contractGid, vm.Gid, CurrentUser.AccessToken, body);

            if (result != null && result.errors != null && result.errors.Count > 0)
            {
                for (int i = 0; i < result.errors.Count; i++)
                {
                    ModelState.AddModelError("error" + i, result.errors[i]);
                }

                vm.IsEdit = true;
                return View(Views.CreateEdit, vm);
            }

            _packageCommunicator.UpdatePackage(_contractGid, vm.Gid, CurrentUser.AccessToken, body);

            return RedirectToAction(ActionNames.Index);
        }

        #endregion

        public virtual ActionResult MakeDraft(Guid gid, string version)
        {
            var canMakeDraftInfo = _packageCommunicator.CanMakeDraftPackage(_contractGid, gid, CurrentUser.AccessToken);
            if (canMakeDraftInfo != null && canMakeDraftInfo.errors != null && canMakeDraftInfo.errors.Count > 0)
                TempData["package_errors"] = canMakeDraftInfo.errors;
            else
            {
                _packageCommunicator.MakeDraftPackage(_contractGid, gid, CurrentUser.AccessToken, version);
                TempData["SuccessAction"] = "Пакетът е върнат в статус чернова.";
            }

            return RedirectToAction(ActionNames.Index);
        }

        public virtual ActionResult Submit(Guid gid, string version)
        {
            var canSubmitInfo = _packageCommunicator.CanSubmitPackage(_contractGid, gid, CurrentUser.AccessToken);
            if (canSubmitInfo != null && canSubmitInfo.errors != null && canSubmitInfo.errors.Count > 0)
                TempData["package_errors"] = canSubmitInfo.errors;
            else
            {
                _packageCommunicator.SubmitPackage(_contractGid, gid, CurrentUser.AccessToken, version);
                TempData["SuccessAction"] = "Пакетът е изпратен успешно.";
            }

            return RedirectToAction(ActionNames.Index);
        }

        public virtual ActionResult Delete(Guid gid, string version)
        {
            var canDeleteInfo = _packageCommunicator.CanDeletePackage(_contractGid, gid, CurrentUser.AccessToken);
            if (canDeleteInfo != null && canDeleteInfo.errors != null && canDeleteInfo.errors.Count > 0)
                TempData["package_errors"] = canDeleteInfo.errors;
            else
            {
                _packageCommunicator.DeletePackage(_contractGid, gid, CurrentUser.AccessToken, version);
                TempData["SuccessAction"] = "Пакетът е изтрит успешно.";
            }

            return RedirectToAction(ActionNames.Index);
        }

        public virtual ActionResult Preview(Guid gid)
        {
            var package = _packageCommunicator.GetPackage(_contractGid, gid, CurrentUser.AccessToken);

            return View(new PackageEditVM(package));
        }
    }
}