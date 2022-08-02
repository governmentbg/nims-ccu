using Eumis.Components.Communicators;
using Eumis.Documents.Contracts;
using Eumis.Portal.Web.Areas.Report.Models.AccessCode;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Helpers.Attributes;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    [Authenticated]
    public partial class AccessCodeController : BaseController
    {
        private IContractRegistrationAccessCodesCommunicator _contractRegistrationAccessCodesCommunicator;
        public AccessCodeController(IContractRegistrationAccessCodesCommunicator contractRegistrationAccessCodesCommunicator)
        {
            _contractRegistrationAccessCodesCommunicator = contractRegistrationAccessCodesCommunicator;
        }

        public virtual ActionResult Index(int page = 1)
        {
            int offset = (page - 1) * Constants.PAGE_ITEMS_COUNT;
            var users = _contractRegistrationAccessCodesCommunicator.GetContractRegistrationAccessCodes(
                    CurrentUser.AccessToken, new Guid(RouteData.Values["cgid"].ToString()), offset, Constants.PAGE_ITEMS_COUNT);

            var model = new StaticPagedList<ContractRegistrationAccessCodePVO>(users.results, page, Constants.PAGE_ITEMS_COUNT, users.count);

            return View(model);
        }

        #region New

        [HttpGet]
        public virtual ActionResult New()
        {
            AccessCodeRegisterVM vm = new AccessCodeRegisterVM() { };

            return View(Views.NewEdit, vm);
        }

        [HttpPost]
        public virtual ActionResult New(AccessCodeRegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(Views.NewEdit, vm);

            vm.FixPermissions();

            Guid userGid;
            try
            {
                var accessCodeUser = _contractRegistrationAccessCodesCommunicator
                .CreateContractRegistrationAccessCode(CurrentUser.AccessToken, new Guid(RouteData.Values["cgid"].ToString()),
                    new ContractRegistrationAccessCodePVO()
                    {
                        firstName = vm.FirstName,
                        lastName = vm.LastName,
                        identifier = vm.Identifier,
                        position = vm.Position,
                        email = vm.Email,
                        isActive = true,
                        permissions = vm.Permissions
                    });

                userGid = accessCodeUser.gid;
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var error = ApiErrorHandling.GetError((WebException)ex);
                    if (error == ApiError.accessCodeEmailNotUnique)
                    {
                        ModelState.AddModelError("Email", Eumis.Common.Resources.Global.ValidationEmailExists);

                        return View(Views.NewEdit, vm);
                    }
                }

                return View(MVC.Shared.Views.Failure, MVC.Report.Name, (object)Eumis.Common.Resources.Global.ErrorTryAgain);
            }

            return RedirectToAction(ActionNames.Display, new { id = userGid, isSuccess = true });
        }

        #endregion

        #region Edit

        [HttpGet]
        public virtual ActionResult Edit(Guid id)
        {
            var user = _contractRegistrationAccessCodesCommunicator
                .GetContractRegistrationAccessCode(CurrentUser.AccessToken,
                    new Guid(RouteData.Values["cgid"].ToString()), id);

            AccessCodeRegisterVM vm = new AccessCodeRegisterVM(user);

            return View(Views.NewEdit, vm);
        }

        [HttpPost]
        public virtual ActionResult Edit(AccessCodeRegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(Views.NewEdit, vm);
            }

            vm.FixPermissions();

            Guid userGid;

            var accessCodeUser = _contractRegistrationAccessCodesCommunicator
            .UpdateContractRegistrationAccessCode(CurrentUser.AccessToken, new Guid(RouteData.Values["cgid"].ToString()), vm.id.Value,
                new ContractRegistrationAccessCodePVO()
                {
                    firstName = vm.FirstName,
                    lastName = vm.LastName,
                    identifier = vm.Identifier,
                    position = vm.Position,
                    isActive = vm.IsActive,
                    permissions = vm.Permissions,
                    version = vm.Version
                });

            userGid = accessCodeUser.gid;

            return RedirectToAction(ActionNames.Display, new { id = userGid });
        }

        #endregion

        [HttpGet]
        public virtual ActionResult Display(Guid id, bool isSuccess = false)
        {
            var user = _contractRegistrationAccessCodesCommunicator
                .GetContractRegistrationAccessCode(CurrentUser.AccessToken,
                    new Guid(RouteData.Values["cgid"].ToString()), id);

            AccessCodeDisplayVM model = new AccessCodeDisplayVM();
            model.User = user;
            model.IsSuccess = isSuccess;

            return View(model);
        }
    }
}