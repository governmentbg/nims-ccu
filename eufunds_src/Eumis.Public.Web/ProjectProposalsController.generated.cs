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
    public partial class ProjectProposalsController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ProjectProposalsController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult GetPriority()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetPriority);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetPriorities()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetPriorities);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetProcedure()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetProcedure);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetProcedures()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetProcedures);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProjectProposalsController Actions { get { return MVC.ProjectProposals; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ProjectProposals";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ProjectProposals";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string GetProgramme = "GetProgramme";
            public readonly string GetProgrammes = "GetProgrammes";
            public readonly string GetPriority = "GetPriority";
            public readonly string GetPriorities = "GetPriorities";
            public readonly string GetProcedure = "GetProcedure";
            public readonly string GetProcedures = "GetProcedures";
            public readonly string Print = "Print";
            public readonly string ExportToExcel = "ExportToExcel";
            public readonly string ExportToHtml = "ExportToHtml";
            public readonly string ExportToXml = "ExportToXml";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string GetProgramme = "GetProgramme";
            public const string GetProgrammes = "GetProgrammes";
            public const string GetPriority = "GetPriority";
            public const string GetPriorities = "GetPriorities";
            public const string GetProcedure = "GetProcedure";
            public const string GetProcedures = "GetProcedures";
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
            public readonly string priorityAxisId = "priorityAxisId";
            public readonly string procedureId = "procedureId";
            public readonly string showRes = "showRes";
            public readonly string page = "page";
            public readonly string vm = "vm";
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
        static readonly ActionParamsClass_GetPriority s_params_GetPriority = new ActionParamsClass_GetPriority();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetPriority GetPriorityParams { get { return s_params_GetPriority; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetPriority
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetPriorities s_params_GetPriorities = new ActionParamsClass_GetPriorities();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetPriorities GetPrioritiesParams { get { return s_params_GetPriorities; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetPriorities
        {
            public readonly string term = "term";
            public readonly string parentId = "parentId";
        }
        static readonly ActionParamsClass_GetProcedure s_params_GetProcedure = new ActionParamsClass_GetProcedure();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetProcedure GetProcedureParams { get { return s_params_GetProcedure; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetProcedure
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetProcedures s_params_GetProcedures = new ActionParamsClass_GetProcedures();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetProcedures GetProceduresParams { get { return s_params_GetProcedures; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetProcedures
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
                public readonly string Index = "Index";
            }
            public readonly string Index = "~/Views/ProjectProposals/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProjectProposalsController : Eumis.Public.Web.Controllers.ProjectProposalsController
    {
        public T4MVC_ProjectProposalsController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string programmeId, string priorityAxisId, string procedureId, bool showRes, string page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(string programmeId, string priorityAxisId, string procedureId, bool showRes, string page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "programmeId", programmeId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "priorityAxisId", priorityAxisId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "procedureId", procedureId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "showRes", showRes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            IndexOverride(callInfo, programmeId, priorityAxisId, procedureId, showRes, page);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Eumis.Public.Web.Models.Indicators.ProjectProposalsSearchVM vm);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(Eumis.Public.Web.Models.Indicators.ProjectProposalsSearchVM vm)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "vm", vm);
            IndexOverride(callInfo, vm);
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
        partial void GetPriorityOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetPriority(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetPriority);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetPriorityOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetPrioritiesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string term, string parentId);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetPriorities(string term, string parentId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetPriorities);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "parentId", parentId);
            GetPrioritiesOverride(callInfo, term, parentId);
            return callInfo;
        }

        [NonAction]
        partial void GetProcedureOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetProcedure(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetProcedure);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetProcedureOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetProceduresOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string term, string parentId);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetProcedures(string term, string parentId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetProcedures);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "parentId", parentId);
            GetProceduresOverride(callInfo, term, parentId);
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