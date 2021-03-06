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
    public partial class ProjectController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ProjectController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Draft()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Draft);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult DownloadFiles()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.DownloadFiles);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ContentResult Save()
        {
            return new T4MVC_System_Web_Mvc_ContentResult(Area, Name, ActionNames.Save);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProjectController Actions { get { return MVC.Private.Project; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Private";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Project";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Project";
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
            public readonly string Draft = "Draft";
            public readonly string ValidateDraft = "ValidateDraft";
            public readonly string Submit = "Submit";
            public readonly string DownloadFiles = "DownloadFiles";
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
            public const string Draft = "Draft";
            public const string ValidateDraft = "ValidateDraft";
            public const string Submit = "Submit";
            public const string DownloadFiles = "DownloadFiles";
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
        static readonly ActionParamsClass_Draft s_params_Draft = new ActionParamsClass_Draft();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Draft DraftParams { get { return s_params_Draft; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Draft
        {
            public readonly string gid = "gid";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_DownloadFiles s_params_DownloadFiles = new ActionParamsClass_DownloadFiles();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DownloadFiles DownloadFilesParams { get { return s_params_DownloadFiles; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DownloadFiles
        {
            public readonly string projectGid = "projectGid";
            public readonly string hash = "hash";
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
                public readonly string Draft = "Draft";
                public readonly string Prepare = "Prepare";
                public readonly string Preview = "Preview";
                public readonly string Print = "Print";
            }
            public readonly string Display = "~/Areas/Private/Views/Project/Display.cshtml";
            public readonly string Draft = "~/Areas/Private/Views/Project/Draft.cshtml";
            public readonly string Prepare = "~/Areas/Private/Views/Project/Prepare.cshtml";
            public readonly string Preview = "~/Areas/Private/Views/Project/Preview.cshtml";
            public readonly string Print = "~/Areas/Private/Views/Project/Print.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProjectController : Eumis.Portal.Web.Areas.Private.Controllers.ProjectController
    {
        public T4MVC_ProjectController() : base(Dummy.Instance) { }

        [NonAction]
        partial void PrepareOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Eumis.Portal.Web.Models.Project.EditVM vm);

        [NonAction]
        public override System.Web.Mvc.ActionResult Prepare(Eumis.Portal.Web.Models.Project.EditVM vm)
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
        partial void DraftOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid);

        [NonAction]
        public override System.Web.Mvc.ActionResult Draft(System.Guid gid)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Draft);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            DraftOverride(callInfo, gid);
            return callInfo;
        }

        [NonAction]
        partial void ValidateDraftOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ValidateDraft()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ValidateDraft);
            ValidateDraftOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DraftOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Eumis.Portal.Web.Models.Project.EditVM model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Draft(Eumis.Portal.Web.Models.Project.EditVM model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Draft);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            DraftOverride(callInfo, model);
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
        partial void DownloadFilesOverride(T4MVC_System_Web_Mvc_FileResult callInfo, System.Guid projectGid, string hash);

        [NonAction]
        public override System.Web.Mvc.FileResult DownloadFiles(System.Guid projectGid, string hash)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.DownloadFiles);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "projectGid", projectGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "hash", hash);
            DownloadFilesOverride(callInfo, projectGid, hash);
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
        partial void SaveOverride(T4MVC_System_Web_Mvc_ContentResult callInfo, Eumis.Portal.Web.Models.Project.EditVM model);

        [NonAction]
        public override System.Web.Mvc.ContentResult Save(Eumis.Portal.Web.Models.Project.EditVM model)
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
