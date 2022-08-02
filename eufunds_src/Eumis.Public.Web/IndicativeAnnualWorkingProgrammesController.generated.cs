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
    public partial class IndicativeAnnualWorkingProgrammesController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected IndicativeAnnualWorkingProgrammesController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult GetProgramme()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetProgramme);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetProgrammes()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetProgrammes);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetYear()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetYear);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetIawpType()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetIawpType);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public IndicativeAnnualWorkingProgrammesController Actions { get { return MVC.IndicativeAnnualWorkingProgrammes; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "IndicativeAnnualWorkingProgrammes";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "IndicativeAnnualWorkingProgrammes";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Show = "Show";
            public readonly string GetProgramme = "GetProgramme";
            public readonly string GetProgrammes = "GetProgrammes";
            public readonly string GetYear = "GetYear";
            public readonly string GetYears = "GetYears";
            public readonly string GetIawpType = "GetIawpType";
            public readonly string GetIawpTypes = "GetIawpTypes";
            public readonly string Print = "Print";
            public readonly string ExportToExcel = "ExportToExcel";
            public readonly string ExportToHtml = "ExportToHtml";
            public readonly string ExportToXml = "ExportToXml";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Show = "Show";
            public const string GetProgramme = "GetProgramme";
            public const string GetProgrammes = "GetProgrammes";
            public const string GetYear = "GetYear";
            public const string GetYears = "GetYears";
            public const string GetIawpType = "GetIawpType";
            public const string GetIawpTypes = "GetIawpTypes";
            public const string Print = "Print";
            public const string ExportToExcel = "ExportToExcel";
            public const string ExportToHtml = "ExportToHtml";
            public const string ExportToXml = "ExportToXml";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string programmeId = "programmeId";
            public readonly string year = "year";
            public readonly string iawpType = "iawpType";
            public readonly string showRes = "showRes";
            public readonly string page = "page";
            public readonly string vm = "vm";
        }
        static readonly ActionParamsClass_Show s_params_Show = new ActionParamsClass_Show();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Show ShowParams { get { return s_params_Show; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Show
        {
            public readonly string iawpId = "iawpId";
            public readonly string iawpType = "iawpType";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_GetProgramme s_params_GetProgramme = new ActionParamsClass_GetProgramme();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetProgramme GetProgrammeParams { get { return s_params_GetProgramme; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetProgramme
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetProgrammes s_params_GetProgrammes = new ActionParamsClass_GetProgrammes();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetProgrammes GetProgrammesParams { get { return s_params_GetProgrammes; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetProgrammes
        {
            public readonly string term = "term";
        }
        static readonly ActionParamsClass_GetYear s_params_GetYear = new ActionParamsClass_GetYear();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetYear GetYearParams { get { return s_params_GetYear; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetYear
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetIawpType s_params_GetIawpType = new ActionParamsClass_GetIawpType();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetIawpType GetIawpTypeParams { get { return s_params_GetIawpType; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetIawpType
        {
            public readonly string id = "id";
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
                public readonly string IawpTable = "IawpTable";
                public readonly string IawpTableForIntegratedProcedures = "IawpTableForIntegratedProcedures";
                public readonly string Index = "Index";
            }
            public readonly string IawpTable = "~/Views/IndicativeAnnualWorkingProgrammes/IawpTable.cshtml";
            public readonly string IawpTableForIntegratedProcedures = "~/Views/IndicativeAnnualWorkingProgrammes/IawpTableForIntegratedProcedures.cshtml";
            public readonly string Index = "~/Views/IndicativeAnnualWorkingProgrammes/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_IndicativeAnnualWorkingProgrammesController : Eumis.Public.Web.Controllers.IndicativeAnnualWorkingProgrammesController
    {
        public T4MVC_IndicativeAnnualWorkingProgrammesController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string programmeId, string year, string iawpType, bool showRes, string page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(string programmeId, string year, string iawpType, bool showRes, string page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "programmeId", programmeId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "year", year);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "iawpType", iawpType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "showRes", showRes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            IndexOverride(callInfo, programmeId, year, iawpType, showRes, page);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Eumis.Public.Web.Models.IndicativeAnnualWorkingProgrammes.IndicativeAnnualWorkingProgrammesSearchVM vm);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(Eumis.Public.Web.Models.IndicativeAnnualWorkingProgrammes.IndicativeAnnualWorkingProgrammesSearchVM vm)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "vm", vm);
            IndexOverride(callInfo, vm);
            return callInfo;
        }

        [NonAction]
        partial void ShowOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string iawpId, string iawpType, string page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Show(string iawpId, string iawpType, string page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Show);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "iawpId", iawpId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "iawpType", iawpType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ShowOverride(callInfo, iawpId, iawpType, page);
            return callInfo;
        }

        [NonAction]
        partial void GetProgrammeOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetProgramme(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetProgramme);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetProgrammeOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetProgrammesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string term);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetProgrammes(string term)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetProgrammes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            GetProgrammesOverride(callInfo, term);
            return callInfo;
        }

        [NonAction]
        partial void GetYearOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes.IndicativeAnnualWorkingProgrammeYear id);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetYear(Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes.IndicativeAnnualWorkingProgrammeYear id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetYear);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetYearOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetYearsOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetYears()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetYears);
            GetYearsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetIawpTypeOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes.IndicativeAnnualWorkingProgrammeType id);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetIawpType(Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes.IndicativeAnnualWorkingProgrammeType id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetIawpType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetIawpTypeOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetIawpTypesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetIawpTypes()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetIawpTypes);
            GetIawpTypesOverride(callInfo);
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

        [NonAction]
        partial void ExportToExcelOverride(T4MVC_System_Web_Mvc_FileResult callInfo);

        [NonAction]
        public override System.Web.Mvc.FileResult ExportToExcel()
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.ExportToExcel);
            ExportToExcelOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ExportToHtmlOverride(T4MVC_System_Web_Mvc_FileResult callInfo);

        [NonAction]
        public override System.Web.Mvc.FileResult ExportToHtml()
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.ExportToHtml);
            ExportToHtmlOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ExportToXmlOverride(T4MVC_System_Web_Mvc_FileResult callInfo);

        [NonAction]
        public override System.Web.Mvc.FileResult ExportToXml()
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.ExportToXml);
            ExportToXmlOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114