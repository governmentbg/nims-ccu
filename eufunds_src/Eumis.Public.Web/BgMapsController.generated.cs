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
    public partial class BgMapsController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected BgMapsController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Single()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Single);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DataSingle()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DataSingle);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DataAll()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DataAll);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BgMapsController Actions { get { return MVC.BgMaps; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "BgMaps";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "BgMaps";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Single = "Single";
            public readonly string All = "All";
            public readonly string DataSingle = "DataSingle";
            public readonly string DataAll = "DataAll";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Single = "Single";
            public const string All = "All";
            public const string DataSingle = "DataSingle";
            public const string DataAll = "DataAll";
        }


        static readonly ActionParamsClass_Single s_params_Single = new ActionParamsClass_Single();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Single SingleParams { get { return s_params_Single; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Single
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_DataSingle s_params_DataSingle = new ActionParamsClass_DataSingle();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DataSingle DataSingleParams { get { return s_params_DataSingle; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DataSingle
        {
            public readonly string id = "id";
            public readonly string dataType = "dataType";
        }
        static readonly ActionParamsClass_DataAll s_params_DataAll = new ActionParamsClass_DataAll();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DataAll DataAllParams { get { return s_params_DataAll; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DataAll
        {
            public readonly string dataType = "dataType";
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
    public partial class T4MVC_BgMapsController : Eumis.Public.Web.Controllers.BgMapsController
    {
        public T4MVC_BgMapsController() : base(Dummy.Instance) { }

        [NonAction]
        partial void SingleOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Single(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Single);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            SingleOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void AllOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult All()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.All);
            AllOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DataSingleOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id, Eumis.Public.Web.InfrastructureClasses.Maps.BgMapDataType dataType);

        [NonAction]
        public override System.Web.Mvc.ActionResult DataSingle(string id, Eumis.Public.Web.InfrastructureClasses.Maps.BgMapDataType dataType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DataSingle);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dataType", dataType);
            DataSingleOverride(callInfo, id, dataType);
            return callInfo;
        }

        [NonAction]
        partial void DataAllOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Eumis.Public.Web.InfrastructureClasses.Maps.BgMapDataType dataType);

        [NonAction]
        public override System.Web.Mvc.ActionResult DataAll(Eumis.Public.Web.InfrastructureClasses.Maps.BgMapDataType dataType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DataAll);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dataType", dataType);
            DataAllOverride(callInfo, dataType);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
