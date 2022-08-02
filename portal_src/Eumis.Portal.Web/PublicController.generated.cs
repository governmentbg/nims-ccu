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
namespace Eumis.Portal.Web.Controllers
{
    public partial class PublicController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected PublicController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Preview()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Preview);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PreviewDownload()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PreviewDownload);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public PublicController Actions { get { return MVC.Public; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Public";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Public";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string CurrentNews = "CurrentNews";
            public readonly string AllNews = "AllNews";
            public readonly string Preview = "Preview";
            public readonly string PreviewDownload = "PreviewDownload";
            public readonly string AccessibilityPolicy = "AccessibilityPolicy";
            public readonly string Glossary = "Glossary";
            public readonly string About = "About";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string CurrentNews = "CurrentNews";
            public const string AllNews = "AllNews";
            public const string Preview = "Preview";
            public const string PreviewDownload = "PreviewDownload";
            public const string AccessibilityPolicy = "AccessibilityPolicy";
            public const string Glossary = "Glossary";
            public const string About = "About";
        }


        static readonly ActionParamsClass_CurrentNews s_params_CurrentNews = new ActionParamsClass_CurrentNews();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CurrentNews CurrentNewsParams { get { return s_params_CurrentNews; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CurrentNews
        {
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_AllNews s_params_AllNews = new ActionParamsClass_AllNews();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AllNews AllNewsParams { get { return s_params_AllNews; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AllNews
        {
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Preview s_params_Preview = new ActionParamsClass_Preview();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Preview PreviewParams { get { return s_params_Preview; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Preview
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_PreviewDownload s_params_PreviewDownload = new ActionParamsClass_PreviewDownload();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PreviewDownload PreviewDownloadParams { get { return s_params_PreviewDownload; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PreviewDownload
        {
            public readonly string id = "id";
            public readonly string fileKey = "fileKey";
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
                public readonly string About = "About";
                public readonly string AccessibilityPolicy = "AccessibilityPolicy";
                public readonly string AllNews = "AllNews";
                public readonly string CurrentNews = "CurrentNews";
                public readonly string Glossary = "Glossary";
                public readonly string Preview = "Preview";
            }
            public readonly string About = "~/Views/Public/About.cshtml";
            public readonly string AccessibilityPolicy = "~/Views/Public/AccessibilityPolicy.cshtml";
            public readonly string AllNews = "~/Views/Public/AllNews.cshtml";
            public readonly string CurrentNews = "~/Views/Public/CurrentNews.cshtml";
            public readonly string Glossary = "~/Views/Public/Glossary.cshtml";
            public readonly string Preview = "~/Views/Public/Preview.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_PublicController : Eumis.Portal.Web.Controllers.PublicController
    {
        public T4MVC_PublicController() : base(Dummy.Instance) { }

        [NonAction]
        partial void CurrentNewsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult CurrentNews(int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CurrentNews);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            CurrentNewsOverride(callInfo, page);
            return callInfo;
        }

        [NonAction]
        partial void AllNewsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult AllNews(int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AllNews);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            AllNewsOverride(callInfo, page);
            return callInfo;
        }

        [NonAction]
        partial void PreviewOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Preview(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Preview);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PreviewOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void PreviewDownloadOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id, System.Guid fileKey);

        [NonAction]
        public override System.Web.Mvc.ActionResult PreviewDownload(int id, System.Guid fileKey)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PreviewDownload);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "fileKey", fileKey);
            PreviewDownloadOverride(callInfo, id, fileKey);
            return callInfo;
        }

        [NonAction]
        partial void AccessibilityPolicyOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult AccessibilityPolicy()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessibilityPolicy);
            AccessibilityPolicyOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GlossaryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Glossary()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Glossary);
            GlossaryOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AboutOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult About()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.About);
            AboutOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114