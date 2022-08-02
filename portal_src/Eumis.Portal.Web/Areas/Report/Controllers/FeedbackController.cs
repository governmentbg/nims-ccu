using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Components;
using Eumis.Portal.Web.Controllers.Base;
using Eumis.Portal.Web.Models.Feedback;
using Eumis.Common.Resources;
using System.Net;
using Eumis.Components.Communicators;
using Eumis.Common.Helpers;
using System.Configuration;
using Eumis.Common.Config;
using Eumis.Common.ReCaptcha;
using Eumis.Portal.Web.Helplers.Attributes;

namespace Eumis.Portal.Web.Areas.Report.Controllers
{
    public partial class FeedbackController : BaseController
    {
        private IMailCommunicator _mailCommunicator;

        public FeedbackController(IMailCommunicator mailCommunicator)
        {
            _mailCommunicator = mailCommunicator;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ReCaptchaValidation]
        public virtual ActionResult Index(FeedbackVM vm, bool? captchaValid)
        {
            if (captchaValid.HasValue && !captchaValid.Value)
            {
                ModelState.AddModelError(Constants.CaptchaModelName, Global.ValidationCaptcha);
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            string subjectText = String.Empty;
            switch (vm.Type)
            {
                case "1":
                    subjectText = Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback.SubjectOptionQuestion;
                    break;
                case "2":
                    subjectText = Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback.SubjectOptionSuggestion;
                    break;
                case "3":
                    subjectText = Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback.SubjectOptionTechnicalProblem;
                    break;
                case "4":
                    subjectText = Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback.AbuseReport;
                    break;
                default:
                    break;
            }

            subjectText += " (обратна връзка)";

            vm.Name += (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.Email)) ? string.Format(" {0}: {1}", Global.User.ToLower(), CurrentUser.Email) : string.Empty;

            foreach (var recipient in ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:FeedbackEmail").Split(','))
            {
                _mailCommunicator.Send(recipient, subjectText, vm.Name ?? string.Empty, vm.Email ?? string.Empty, vm.Message);
            }

            TempData["SuccessFeedbackSend"] = true;

            return RedirectToAction(ActionNames.Index);
        }
    }
}