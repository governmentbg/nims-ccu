using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eumis.Common.Data;
using Eumis.Components.Communicators;
using Eumis.Portal.Model.Repositories;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Eumis.Portal.Web.Models;
using Eumis.Portal.Web.Models.Account;
using Eumis.Common.Resources;
using Eumis.Portal.Web.Controllers.Base;
using System.Web.Security;
using Eumis.Components.Web;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Eumis.Portal.Model.Entities;
using Eumis.Common.Localization;
using Eumis.Portal.Web.Helplers;
using Eumis.Common.ReCaptcha;
using Eumis.Portal.Web.Helplers.Attributes;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    [Authenticated]
    public partial class AccountController : BaseController
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ReportSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ReportSignInManager>();
            }
        }

        private EumisUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<EumisUserManager>();
            }
        }

        private IUnitOfWork _unitOfWork;
        private IMessageCommunicator _messageCommunicator;
        private IContractRegistrationCommunicator _contractRegistrationCommunicator;
        private IContractRegistrationAccessCodesCommunicator _contractRegistrationAccessCodesCommunicator;
        private ILoginRepository _loginRepository;

        public AccountController(IUnitOfWork unitOfWork, IMessageCommunicator messageCommunicator,
            IContractRegistrationCommunicator contractRegistrationCommunicator, 
            IContractRegistrationAccessCodesCommunicator contractRegistrationAccessCodesCommunicator,
            ILoginRepository loginRepository)
        {
            _messageCommunicator = messageCommunicator;
            _contractRegistrationCommunicator = contractRegistrationCommunicator;
            _contractRegistrationAccessCodesCommunicator = contractRegistrationAccessCodesCommunicator;
            _loginRepository = loginRepository;
            _unitOfWork = unitOfWork;
        }

        #region Login/Logoff

        [AllowAnonymous]
        public virtual ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction(ActionNames.ProfileEdit);
            }

            return View(new LoginVM());
        }

        [HttpPost]
        [AllowAnonymous]
        [ReCaptchaValidation]
        public virtual ActionResult Login(LoginVM vm, bool? captchaValid)
        {
            if (captchaValid.HasValue && !captchaValid.Value)
            {
                ModelState.AddModelError(Constants.CaptchaModelName, Global.ValidationCaptcha);
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var accessToken = _contractRegistrationCommunicator.Login(vm.Email, vm.Password);
                var info = _contractRegistrationCommunicator.GetRegistrationInfo(accessToken);

                SignInManager.SignIn(new EumisUser()
                {
                    Email = info.email,
                    FirstName = info.firstName,
                    LastName = info.lastName,
                    Phone = info.phone,
                    HasMessages = false,
                    HasNewMessages = false,
                    HasNewCommunications = false,
                    AccessToken = accessToken,
                    UserType = ReportUserType.Parent,
                    Permissions = ReportUserPermissions.GrandFullPermissions()
                }, false, false);
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var webError = ApiErrorHandling.GetError((WebException)ex);
                    if (webError == ApiError.unauthorized)
                    {
                        ModelState.AddModelError("__form", Global.LoginUnauthorized);

                        return View(vm);
                    }
                    else if (webError == ApiError.registrationNotActivated)
                    {
                        ModelState.AddModelError("__form", Global.LoginUnauthorized);

                        return View(vm);
                    }
                }

                return View(MVC.Shared.Views.Failure, MVC.Report.Shared.Views._Layout, (object)Global.ErrorTryAgain);
            }

            return RedirectToAction(MVC.Report.List.ActionNames.Index, MVC.Report.List.Name);
        }

        [AllowAnonymous]
        public virtual ActionResult AccessCodeLogin()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction(MVC.Report.List.ActionNames.Index, MVC.Report.List.Name);
            }

            return View(new AccessCodeLoginVM());
        }

        [HttpPost]
        [AllowAnonymous]
        [ReCaptchaValidation]
        public virtual ActionResult AccessCodeLogin(AccessCodeLoginVM vm, bool? captchaValid)
        {
            if (captchaValid.HasValue && !captchaValid.Value)
            {
                ModelState.AddModelError(Constants.CaptchaModelName, Global.ValidationCaptcha);
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var accessToken = _contractRegistrationAccessCodesCommunicator.Login(vm.Email, vm.Code, vm.ContractNumber);
                var info = _contractRegistrationAccessCodesCommunicator.GetRegistrationInfo(accessToken);

                SignInManager.SignIn(new EumisUser()
                {
                    Email = info.email,
                    FirstName = info.firstName,
                    LastName = info.lastName,
                    Phone = string.Empty,
                    HasMessages = false,
                    HasNewMessages = false,
                    HasNewCommunications = false,
                    AccessToken = accessToken,
                    UserType = ReportUserType.Child,
                    Permissions = ReportUserPermissions.LoadPermissions(info.permissions)
                }, false, false);
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var unauthorizeMessage = "Грешно потребителско име, № на договор или Код за достъп.";
                    var webError = ApiErrorHandling.GetError((WebException)ex);
                    if (webError == ApiError.unauthorized)
                    {
                        ModelState.AddModelError("__form", unauthorizeMessage);

                        return View(vm);
                    }
                    else if (webError == ApiError.accessCodeNotActive)
                    {
                        ModelState.AddModelError("__form", "Профилът Ви е деактивиран.");

                        return View(vm);
                    }
                }

                return View(MVC.Shared.Views.Failure, MVC.Report.Shared.Views._Layout, (object)Global.ErrorTryAgain);
            }

            return RedirectToAction(MVC.Report.List.ActionNames.Index, MVC.Report.List.Name);
        }

        public virtual ActionResult Logout()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction(ActionNames.Login);
        }

        #endregion

        #region ForgotPassword

        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordVM());
        }

        [HttpPost]
        [AllowAnonymous]
        [ReCaptchaValidation]
        public virtual ActionResult ForgotPassword(ForgotPasswordVM vm, bool? captchaValid)
        {
            if (captchaValid.HasValue && !captchaValid.Value)
            {
                ModelState.AddModelError(Constants.CaptchaModelName, Global.ValidationCaptcha);
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                _contractRegistrationCommunicator.StartPasswordRecovery(vm.Email);
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    if (ApiErrorHandling.GetError((WebException)ex) == ApiError.registrationEmailDoesNotExist)
                    {
                        ModelState.AddModelError("Email", Global.ValidationEmailDoesNotExist);

                        return View(vm);
                    }
                }

                return View(MVC.Shared.Views.Failure, MVC.Report.Shared.Views._Layout, (object)Global.ErrorTryAgain);
            }

            return View(MVC.Shared.Views.Success, MVC.Report.Shared.Views._Layout, (object)string.Format(Resources.Account.ForgotPasswordSuccess, vm.Email));
        }

        [AllowAnonymous]
        public virtual ActionResult PasswordRecovery(string ac)
        {
            if (_contractRegistrationCommunicator.CanRecoverPassword(ac))
            {
                return View(new ActivateVM() { ActivationCode = ac });
            }
            else
            {
                return View(MVC.Shared.Views.Failure, MVC.Report.Shared.Views._Layout, (object)Global.ReRecoveringMessage);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult PasswordRecovery(ActivateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                _contractRegistrationCommunicator.RecoverPassword(vm.ActivationCode, vm.Password);
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    if (ApiErrorHandling.GetError((WebException)ex) == ApiError.registrationNotActivated)
                    {
                        ModelState.AddModelError("__form", Global.LoginUnauthorized);

                        return View(vm);
                    }
                }
            }

            return View(MVC.Shared.Views.Success, MVC.Report.Shared.Views._Layout, (object)Resources.Account.RecoveryPasswordSuccess);
        }


        #endregion

        #region Activate

        /// <summary>
        /// Email activation
        /// </summary>
        /// <param name="ac">activationCode</param>
        /// <param name="hc">hasCertificate</param>
        /// <returns></returns>
        [AllowAnonymous]
        public virtual ActionResult Activate(string ac)
        {
            if (string.IsNullOrEmpty(ac))
            {
                throw new ArgumentNullException("Missing Activation Code.");
            }

            if (_contractRegistrationCommunicator.CanActivateRegistration(ac))
            {
                return View(new ActivateVM() { ActivationCode = ac });
            }
            else
            {
                return View(MVC.Shared.Views.Failure, MVC.Report.Shared.Views._Layout, (object)Global.ReactivatingMessage);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Activate(ActivateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var accessToken = _contractRegistrationCommunicator.ActivateRegistration(vm.ActivationCode, vm.Password);

            var info = _contractRegistrationCommunicator.GetRegistrationInfo(accessToken);

            SignInManager.SignIn(new EumisUser()
            {
                Email = info.email,
                FirstName = info.firstName,
                LastName = info.lastName,
                Phone = info.phone,
                AccessToken = accessToken,
                UserType = ReportUserType.Parent,
                Permissions = ReportUserPermissions.GrandFullPermissions()
            }, false, false);

            return RedirectToAction(MVC.Report.List.ActionNames.Index, MVC.Report.List.Name, new { area = MVC.Report.Name });
        }

        #endregion

        #region Profile

        [Authorize]
        public virtual ActionResult ProfileEdit()
        {
            var info = _contractRegistrationCommunicator.GetRegistrationInfo(CurrentUser.AccessToken);

            var vm = new ProfileVM()
            {
                FirstName = info.firstName,
                LastName = info.lastName,
                Phone = info.phone,
                Email = info.email,
                Version = info.version
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult ProfileEdit(ProfileVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                var info = _contractRegistrationCommunicator
                    .UpdateRegistrationInfo(CurrentUser.AccessToken, vm.Phone, vm.Version);
                
                SignInManager.SignIn(new EumisUser()
                {
                    Email = CurrentUser.Email,
                    FirstName = info.firstName,
                    LastName = info.lastName,
                    Phone = info.phone,
                    AccessToken = CurrentUser.AccessToken,
                    UserType = ReportUserType.Parent,
                    Permissions = ReportUserPermissions.GrandFullPermissions()
                }, false, false);
            }
            catch 
            {
                return View(MVC.Shared.Views.Failure, (object)Global.ErrorTryAgain);
            }

            TempData["SuccessProfileUpdate"] = Global.SuccessProfileUpdate;

            return RedirectToAction(ActionNames.ProfileEdit);
        }

        [Authorize]
        public virtual ActionResult ChangePassword()
        {
            return View(new ChangePasswordVM());
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult ChangePassword(ChangePasswordVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                _contractRegistrationCommunicator.ChangeCurrentUserPassword(CurrentUser.AccessToken, vm.OldPassword, vm.Password);
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    if (ApiErrorHandling.GetError((WebException)ex) == ApiError.wrongOldPassword)
                    {
                        ModelState.AddModelError("__form", Global.WrongOldPassword);

                        return View(vm);
                    }
                }

                return View(MVC.Shared.Views.Failure, MVC.Report.Shared.Views._Layout, (object)Global.ErrorTryAgain);
            }

            TempData["SuccessPasswordUpdate"] = Global.SuccessPasswordUpdate;

            return RedirectToAction(ActionNames.ChangePassword);
        }

        #endregion
    }
}