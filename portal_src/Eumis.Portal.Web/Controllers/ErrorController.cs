using Eumis.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eumis.Portal.Model.Repositories;
using Eumis.Portal.Model.Entities;

namespace Eumis.Portal.Web.Controllers
{
    public class CustomHandleErrorInfo : HandleErrorInfo
    {
        public string ErrorCode { get; set; }

        public CustomHandleErrorInfo(
                Exception exception,
                string controllerName = "Error",
                string actionName = "Show")
            : base(exception, controllerName, actionName)
        { }
    }

    public partial class ErrorController : Controller
    {
        [HttpGet]
        public virtual ActionResult Show(string code, bool? isIisError)
        {
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction(MVC.Default.ActionNames.Index, MVC.Default.Name);
            }

            CustomHandleErrorInfo errorInfo = null;

            if (ViewData.Model != null && ViewData.Model is CustomHandleErrorInfo)
                errorInfo = (CustomHandleErrorInfo)ViewData.Model;
            else
                errorInfo = new CustomHandleErrorInfo(new Exception());

            errorInfo.ErrorCode = code;

            if (isIisError.HasValue && isIisError.Value)
            {
                IUnitOfWork unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();
                ILoginRepository loginRepository = DependencyResolver.Current.GetService<ILoginRepository>();

                LoginCertificate login = loginRepository.CreateLoginCertificate();
                login.IP = Request.UserHostAddress.Length > 50
                    ? Request.UserHostAddress.Substring(0, 50)
                    : Request.UserHostAddress;
                login.ErrorCode = errorInfo.ErrorCode;
                login.IsIisErrorOccurred = true;
                login.IsLoginSuccessful = false;

                unitOfWork.Save();
            }

            return View(MVC.Shared.Views.Error, errorInfo);
        }
    }
}