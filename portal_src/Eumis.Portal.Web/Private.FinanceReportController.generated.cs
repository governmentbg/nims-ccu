// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public partial class FinanceReportController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected FinanceReportController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Preview()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Preview);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ContentResult Save()
        {
            return new T4MVC_System_Web_Mvc_ContentResult(Area, Name, ActionNames.Save);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FinanceReportController Actions { get { return MVC.Private.FinanceReport; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Private";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "FinanceReport";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "FinanceReport";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Prepare = "Prepare";
            public readonly string Edit = "Edit";
            public readonly string Preview = "Preview";
            public readonly string Submit = "Submit";
            public readonly string Display = "Display";
            public readonly string Save = "Save";
            public readonly string Print = "Print";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Prepare = "Prepare";
            public const string Edit = "Edit";
            public const string Preview = "Preview";
            public const string Submit = "Submit";
            public const string Display = "Display";
            public const string Save = "Save";
            public const string Print = "Print";
        }


        static readonly ActionParamsClass_Prepare s_params_Prepare = new ActionParamsClass_Prepare();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Prepare PrepareParams { get { return s_params_Prepare; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Prepare
        {
            public readonly string vm = "vm";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string gid = "gid";
            public readonly string access_token = "access_token";
        }
        static readonly ActionParamsClass_Preview s_params_Preview = new ActionParamsClass_Preview();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Preview PreviewParams { get { return s_params_Preview; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Preview
        {
            public readonly string gid = "gid";
            public readonly string access_token = "access_token";
        }
        static readonly ActionParamsClass_Save s_params_Save = new ActionParamsClass_Save();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Save SaveParams { get { return s_params_Save; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Save
        {
            public readonly string model = "model";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Display = "Display";
                public readonly string Prepare = "Prepare";
                public readonly string Preview = "Preview";
                public readonly string Print = "Print";
            }
            public readonly string Display = "~/Areas/Private/Views/FinanceReport/Display.cshtml";
            public readonly string Prepare = "~/Areas/Private/Views/FinanceReport/Prepare.cshtml";
            public readonly string Preview = "~/Areas/Private/Views/FinanceReport/Preview.cshtml";
            public readonly string Print = "~/Areas/Private/Views/FinanceReport/Print.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_FinanceReportController : Eumis.Portal.Web.Areas.Private.Controllers.FinanceReportController
    {
        public T4MVC_FinanceReportController() : base(Dummy.Instance) { }

        [NonAction]
        partial void PrepareOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Eumis.Portal.Web.Models.FinanceReport.EditVM vm);

        [NonAction]
        public override System.Web.Mvc.ActionResult Prepare(Eumis.Portal.Web.Models.FinanceReport.EditVM vm)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Prepare);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "vm", vm);
            PrepareOverride(callInfo, vm);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, string access_token);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(System.Guid gid, string access_token)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "access_token", access_token);
            EditOverride(callInfo, gid, access_token);
            return callInfo;
        }

        [NonAction]
        partial void PreviewOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, string access_token);

        [NonAction]
        public override System.Web.Mvc.ActionResult Preview(System.Guid gid, string access_token)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Preview);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "access_token", access_token);
            PreviewOverride(callInfo, gid, access_token);
            return callInfo;
        }

        [NonAction]
        partial void SubmitOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Submit()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Submit);
            SubmitOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PrepareOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Prepare()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Prepare);
            PrepareOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DisplayOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Display()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Display);
            DisplayOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SaveOverride(T4MVC_System_Web_Mvc_ContentResult callInfo, Eumis.Portal.Web.Models.FinanceReport.EditVM model);

        [NonAction]
        public override System.Web.Mvc.ContentResult Save(Eumis.Portal.Web.Models.FinanceReport.EditVM model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ContentResult(Area, Name, ActionNames.Save);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SaveOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void PrintOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Print()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Print);
            PrintOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
