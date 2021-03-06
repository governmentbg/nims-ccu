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
    public partial class MicroDataController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected MicroDataController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Type1()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type1);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Type2()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type2);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Type3()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type3);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Type4()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type4);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public MicroDataController Actions { get { return MVC.Private.MicroData; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Private";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "MicroData";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "MicroData";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Type1 = "Type1";
            public readonly string Type2 = "Type2";
            public readonly string Type3 = "Type3";
            public readonly string Type4 = "Type4";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Type1 = "Type1";
            public const string Type2 = "Type2";
            public const string Type3 = "Type3";
            public const string Type4 = "Type4";
        }


        static readonly ActionParamsClass_Type1 s_params_Type1 = new ActionParamsClass_Type1();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Type1 Type1Params { get { return s_params_Type1; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Type1
        {
            public readonly string gid = "gid";
            public readonly string access_token = "access_token";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Type2 s_params_Type2 = new ActionParamsClass_Type2();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Type2 Type2Params { get { return s_params_Type2; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Type2
        {
            public readonly string gid = "gid";
            public readonly string access_token = "access_token";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Type3 s_params_Type3 = new ActionParamsClass_Type3();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Type3 Type3Params { get { return s_params_Type3; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Type3
        {
            public readonly string gid = "gid";
            public readonly string access_token = "access_token";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Type4 s_params_Type4 = new ActionParamsClass_Type4();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Type4 Type4Params { get { return s_params_Type4; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Type4
        {
            public readonly string gid = "gid";
            public readonly string access_token = "access_token";
            public readonly string page = "page";
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
                public readonly string Type1 = "Type1";
                public readonly string Type2 = "Type2";
                public readonly string Type3 = "Type3";
                public readonly string Type4 = "Type4";
            }
            public readonly string Type1 = "~/Areas/Private/Views/MicroData/Type1.cshtml";
            public readonly string Type2 = "~/Areas/Private/Views/MicroData/Type2.cshtml";
            public readonly string Type3 = "~/Areas/Private/Views/MicroData/Type3.cshtml";
            public readonly string Type4 = "~/Areas/Private/Views/MicroData/Type4.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_MicroDataController : Eumis.Portal.Web.Areas.Private.Controllers.MicroDataController
    {
        public T4MVC_MicroDataController() : base(Dummy.Instance) { }

        [NonAction]
        partial void Type1Override(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, string access_token, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Type1(System.Guid gid, string access_token, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type1);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "access_token", access_token);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            Type1Override(callInfo, gid, access_token, page);
            return callInfo;
        }

        [NonAction]
        partial void Type2Override(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, string access_token, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Type2(System.Guid gid, string access_token, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type2);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "access_token", access_token);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            Type2Override(callInfo, gid, access_token, page);
            return callInfo;
        }

        [NonAction]
        partial void Type3Override(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, string access_token, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Type3(System.Guid gid, string access_token, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type3);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "access_token", access_token);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            Type3Override(callInfo, gid, access_token, page);
            return callInfo;
        }

        [NonAction]
        partial void Type4Override(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, string access_token, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Type4(System.Guid gid, string access_token, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type4);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "access_token", access_token);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            Type4Override(callInfo, gid, access_token, page);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
