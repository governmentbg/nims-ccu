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
using Eumis.Common.Helpers;
using Eumis.Common.ReCaptcha;
using Eumis.Portal.Web.Helplers.Attributes;

namespace Eumis.Portal.Web.Controllers
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

        private PublicSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<PublicSignInManager>();
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
        private IProjectCommunicationCommunicator _projectCommunicationCommunicator;
        private IRegistrationCommunicator _registrationCommunicator;
        private ILoginRepository _loginRepository;

        public AccountController(
            IUnitOfWork unitOfWork,
            IMessageCommunicator messageCommunicator,
            IRegistrationCommunicator registrationCommunicator,
            IProjectCommunicationCommunicator projectCommunicationCommunicator,
            ILoginRepository loginRepository)
        {
            _messageCommunicator = messageCommunicator;
            _registrationCommunicator = registrationCommunicator;
            _projectCommunicationCommunicator = projectCommunicationCommunicator;
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
                var accessToken = _registrationCommunicator.Login(vm.Email, vm.Password);
                var info = _registrationCommunicator.GetRegistrationInfo(accessToken);

                if (SplashContext.Current != null && SplashContext.Current.State != null)
                {
                    SplashContext.Current.State[SplashType.MessageNotification] = false;
                }

                var hasMessages = false;
                var hasNewMessages = false;

                try
                {
                    var messages = _messageCommunicator.GetMessagesCount(accessToken);
                    hasMessages = messages.AllMessages > 0;
                    hasNewMessages = messages.NewMessages > 0;
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(Helper.GetDetailedExceptionInfo(ex));
                }

                var hasNewCommunications = false;
                try
                {
                    hasNewCommunications = _projectCommunicationCommunicator.UserHasNewCommunnications(accessToken);
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(Helper.GetDetailedExceptionInfo(ex));
                }

                SignInManager.SignIn(new EumisUser()
                {
                    Email = info.email,
                    FirstName = info.firstName,
                    LastName = info.lastName,
                    Phone = info.phone,
                    HasMessages = hasMessages,
                    HasNewMessages = hasNewMessages,
                    HasNewCommunications = hasNewCommunications,
                    AccessToken = accessToken
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

                return View(MVC.Shared.Views.Failure, (object)Global.ErrorTryAgain);
            }

            return RedirectToAction(MVC.Draft.ActionNames.Index, MVC.Draft.Name);
        }

        public virtual ActionResult Logout()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction(ActionNames.Login);
        }

        #endregion

        #region Register

        [AllowAnonymous]
        [IsAuthenticated]
        public virtual ActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        [AllowAnonymous]
        [IsAuthenticated]
        [ReCaptchaValidation]
        public virtual ActionResult Register(RegisterVM vm, bool? captchaValid)
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
                _registrationCommunicator.CreateRegistration(vm.Email, vm.FirstName, vm.LastName, vm.Phone);
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    if (ApiErrorHandling.GetError((WebException)ex) == ApiError.registrationEmailExists)
                    {
                        ModelState.AddModelError("Email", Global.ValidationEmailExists);

                        return View(vm);
                    }
                }

                return View(MVC.Shared.Views.Failure, (object)Global.ErrorTryAgain);
            }

            return View(MVC.Shared.Views.Success, (object)(String.Format(Resources.Account.RegisterSuccess, vm.Email)));
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
                _registrationCommunicator.StartPasswordRecovery(vm.Email);
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

                return View(MVC.Shared.Views.Failure, (object)Global.ErrorTryAgain);
            }

            return View(MVC.Shared.Views.Success, (object)string.Format(Resources.Account.ForgotPasswordSuccess, vm.Email));
        }

        [AllowAnonymous]
        public virtual ActionResult PasswordRecovery(string ac)
        {
            if (_registrationCommunicator.CanRecoverPassword(ac))
            {
                return View(new ActivateVM() { ActivationCode = ac });
            }
            else
            {
                return View(MVC.Shared.Views.Failure, (object)Global.ReRecoveringMessage);
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
                _registrationCommunicator.RecoverPassword(vm.ActivationCode, vm.Password);
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

            return View(MVC.Shared.Views.Success, (object)Resources.Account.RecoveryPasswordSuccess);
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

            if (_registrationCommunicator.CanActivateRegistration(ac))
            {
                return View(new ActivateVM() { ActivationCode = ac });
            }
            else
            {
                return View(MVC.Shared.Views.Failure, (object)Global.ReactivatingMessage);
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

            var accessToken = _registrationCommunicator.ActivateRegistration(vm.ActivationCode, vm.Password);

            var info = _registrationCommunicator.GetRegistrationInfo(accessToken);

            SignInManager.SignIn(new EumisUser()
            {
                Email = info.email,
                FirstName = info.firstName,
                LastName = info.lastName,
                Phone = info.phone,
                AccessToken = accessToken
            }, false, false);

            return RedirectToAction(MVC.Procedure.ActionNames.Active, MVC.Procedure.Name);
        }

        #endregion

        #region Profile

        [Authorize]
        public virtual ActionResult ProfileEdit()
        {
            var info = _registrationCommunicator.GetRegistrationInfo(CurrentUser.AccessToken);

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
                var info = _registrationCommunicator
                    .UpdateRegistrationInfo(CurrentUser.AccessToken, vm.FirstName, vm.LastName, vm.Phone, vm.Version);
                
                SignInManager.SignIn(new EumisUser()
                {
                    Email = CurrentUser.Email,
                    FirstName = info.firstName,
                    LastName = info.lastName,
                    Phone = info.phone,
                    AccessToken = CurrentUser.AccessToken
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
                _registrationCommunicator.ChangeCurrentUserPassword(CurrentUser.AccessToken, vm.OldPassword, vm.Password);
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

                return View(MVC.Shared.Views.Failure, (object)Global.ErrorTryAgain);
            }

            TempData["SuccessPasswordUpdate"] = Global.SuccessPasswordUpdate;

            return RedirectToAction(ActionNames.ChangePassword);
        }

        #endregion
    }
}