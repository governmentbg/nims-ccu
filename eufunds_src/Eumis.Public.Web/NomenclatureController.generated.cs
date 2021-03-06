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
namespace Eumis.Public.Web.Controllers
{
    public partial class NomenclatureController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected NomenclatureController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult GetCompanyType()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetCompanyType);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetCompanyTypes()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetCompanyTypes);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetCompanyLegalType()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetCompanyLegalType);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetCompanyLegalTypes()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetCompanyLegalTypes);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public NomenclatureController Actions { get { return MVC.Nomenclature; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Nomenclature";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Nomenclature";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string GetCompanyType = "GetCompanyType";
            public readonly string GetCompanyTypes = "GetCompanyTypes";
            public readonly string GetCompanyLegalType = "GetCompanyLegalType";
            public readonly string GetCompanyLegalTypes = "GetCompanyLegalTypes";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string GetCompanyType = "GetCompanyType";
            public const string GetCompanyTypes = "GetCompanyTypes";
            public const string GetCompanyLegalType = "GetCompanyLegalType";
            public const string GetCompanyLegalTypes = "GetCompanyLegalTypes";
        }


        static readonly ActionParamsClass_GetCompanyType s_params_GetCompanyType = new ActionParamsClass_GetCompanyType();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetCompanyType GetCompanyTypeParams { get { return s_params_GetCompanyType; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetCompanyType
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetCompanyTypes s_params_GetCompanyTypes = new ActionParamsClass_GetCompanyTypes();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetCompanyTypes GetCompanyTypesParams { get { return s_params_GetCompanyTypes; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetCompanyTypes
        {
            public readonly string term = "term";
        }
        static readonly ActionParamsClass_GetCompanyLegalType s_params_GetCompanyLegalType = new ActionParamsClass_GetCompanyLegalType();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetCompanyLegalType GetCompanyLegalTypeParams { get { return s_params_GetCompanyLegalType; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetCompanyLegalType
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetCompanyLegalTypes s_params_GetCompanyLegalTypes = new ActionParamsClass_GetCompanyLegalTypes();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetCompanyLegalTypes GetCompanyLegalTypesParams { get { return s_params_GetCompanyLegalTypes; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetCompanyLegalTypes
        {
            public readonly string term = "term";
            public readonly string parentId = "parentId";
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
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_NomenclatureController : Eumis.Public.Web.Controllers.NomenclatureController
    {
        public T4MVC_NomenclatureController() : base(Dummy.Instance) { }

        [NonAction]
        partial void GetCompanyTypeOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetCompanyType(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetCompanyType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetCompanyTypeOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetCompanyTypesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string term);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetCompanyTypes(string term)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetCompanyTypes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            GetCompanyTypesOverride(callInfo, term);
            return callInfo;
        }

        [NonAction]
        partial void GetCompanyLegalTypeOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetCompanyLegalType(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetCompanyLegalType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetCompanyLegalTypeOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetCompanyLegalTypesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string term, string parentId);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetCompanyLegalTypes(string term, string parentId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetCompanyLegalTypes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "parentId", parentId);
            GetCompanyLegalTypesOverride(callInfo, term, parentId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
