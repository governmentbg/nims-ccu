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
namespace Eumis.Portal.Web.Areas.Report.Controllers
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
        public virtual System.Web.Mvc.ActionResult New()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.New);
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
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Download()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Download);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Delete()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult MakeDraft()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MakeDraft);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult MakeActual()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MakeActual);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SaveDraft()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveDraft);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SaveDraftWithSimevCode()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveDraftWithSimevCode);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Submit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Submit);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public MicroDataController Actions { get { return MVC.Report.MicroData; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Report";
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
            public readonly string New = "New";
            public readonly string Type1 = "Type1";
            public readonly string Type2 = "Type2";
            public readonly string Type3 = "Type3";
            public readonly string Type4 = "Type4";
            public readonly string Download = "Download";
            public readonly string Delete = "Delete";
            public readonly string MakeDraft = "MakeDraft";
            public readonly string MakeActual = "MakeActual";
            public readonly string SaveDraft = "SaveDraft";
            public readonly string SaveDraftWithSimevCode = "SaveDraftWithSimevCode";
            public readonly string Submit = "Submit";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string New = "New";
            public const string Type1 = "Type1";
            public const string Type2 = "Type2";
            public const string Type3 = "Type3";
            public const string Type4 = "Type4";
            public const string Download = "Download";
            public const string Delete = "Delete";
            public const string MakeDraft = "MakeDraft";
            public const string MakeActual = "MakeActual";
            public const string SaveDraft = "SaveDraft";
            public const string SaveDraftWithSimevCode = "SaveDraftWithSimevCode";
            public const string Submit = "Submit";
        }


        static readonly ActionParamsClass_New s_params_New = new ActionParamsClass_New();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_New NewParams { get { return s_params_New; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_New
        {
            public readonly string packageGid = "packageGid";
            public readonly string type = "type";
        }
        static readonly ActionParamsClass_Type1 s_params_Type1 = new ActionParamsClass_Type1();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Type1 Type1Params { get { return s_params_Type1; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Type1
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Type2 s_params_Type2 = new ActionParamsClass_Type2();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Type2 Type2Params { get { return s_params_Type2; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Type2
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Type3 s_params_Type3 = new ActionParamsClass_Type3();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Type3 Type3Params { get { return s_params_Type3; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Type3
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Type4 s_params_Type4 = new ActionParamsClass_Type4();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Type4 Type4Params { get { return s_params_Type4; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Type4
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Download s_params_Download = new ActionParamsClass_Download();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Download DownloadParams { get { return s_params_Download; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Download
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string fileKey = "fileKey";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string version = "version";
        }
        static readonly ActionParamsClass_MakeDraft s_params_MakeDraft = new ActionParamsClass_MakeDraft();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_MakeDraft MakeDraftParams { get { return s_params_MakeDraft; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_MakeDraft
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string version = "version";
        }
        static readonly ActionParamsClass_MakeActual s_params_MakeActual = new ActionParamsClass_MakeActual();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_MakeActual MakeActualParams { get { return s_params_MakeActual; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_MakeActual
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string version = "version";
        }
        static readonly ActionParamsClass_SaveDraft s_params_SaveDraft = new ActionParamsClass_SaveDraft();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveDraft SaveDraftParams { get { return s_params_SaveDraft; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveDraft
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string version = "version";
            public readonly string blobKey = "blobKey";
            public readonly string fileName = "fileName";
        }
        static readonly ActionParamsClass_SaveDraftWithSimevCode s_params_SaveDraftWithSimevCode = new ActionParamsClass_SaveDraftWithSimevCode();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveDraftWithSimevCode SaveDraftWithSimevCodeParams { get { return s_params_SaveDraftWithSimevCode; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveDraftWithSimevCode
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string version = "version";
            public readonly string simevCode = "simevCode";
        }
        static readonly ActionParamsClass_Submit s_params_Submit = new ActionParamsClass_Submit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Submit SubmitParams { get { return s_params_Submit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Submit
        {
            public readonly string gid = "gid";
            public readonly string packageGid = "packageGid";
            public readonly string version = "version";
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
            public readonly string Type1 = "~/Areas/Report/Views/MicroData/Type1.cshtml";
            public readonly string Type2 = "~/Areas/Report/Views/MicroData/Type2.cshtml";
            public readonly string Type3 = "~/Areas/Report/Views/MicroData/Type3.cshtml";
            public readonly string Type4 = "~/Areas/Report/Views/MicroData/Type4.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_MicroDataController : Eumis.Portal.Web.Areas.Report.Controllers.MicroDataController
    {
        public T4MVC_MicroDataController() : base(Dummy.Instance) { }

        [NonAction]
        partial void NewOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid packageGid, Eumis.Documents.Contracts.ContractReportMicroType type);

        [NonAction]
        public override System.Web.Mvc.ActionResult New(System.Guid packageGid, Eumis.Documents.Contracts.ContractReportMicroType type)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.New);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "type", type);
            NewOverride(callInfo, packageGid, type);
            return callInfo;
        }

        [NonAction]
        partial void Type1Override(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Type1(System.Guid gid, System.Guid packageGid, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type1);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            Type1Override(callInfo, gid, packageGid, page);
            return callInfo;
        }

        [NonAction]
        partial void Type2Override(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Type2(System.Guid gid, System.Guid packageGid, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type2);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            Type2Override(callInfo, gid, packageGid, page);
            return callInfo;
        }

        [NonAction]
        partial void Type3Override(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Type3(System.Guid gid, System.Guid packageGid, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type3);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            Type3Override(callInfo, gid, packageGid, page);
            return callInfo;
        }

        [NonAction]
        partial void Type4Override(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Type4(System.Guid gid, System.Guid packageGid, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Type4);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            Type4Override(callInfo, gid, packageGid, page);
            return callInfo;
        }

        [NonAction]
        partial void DownloadOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, System.Guid fileKey);

        [NonAction]
        public override System.Web.Mvc.ActionResult Download(System.Guid gid, System.Guid packageGid, System.Guid fileKey)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Download);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "fileKey", fileKey);
            DownloadOverride(callInfo, gid, packageGid, fileKey);
            return callInfo;
        }

        [NonAction]
        partial void DeleteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, string version);

        [NonAction]
        public override System.Web.Mvc.ActionResult Delete(System.Guid gid, System.Guid packageGid, string version)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "version", version);
            DeleteOverride(callInfo, gid, packageGid, version);
            return callInfo;
        }

        [NonAction]
        partial void MakeDraftOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, string version);

        [NonAction]
        public override System.Web.Mvc.ActionResult MakeDraft(System.Guid gid, System.Guid packageGid, string version)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MakeDraft);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "version", version);
            MakeDraftOverride(callInfo, gid, packageGid, version);
            return callInfo;
        }

        [NonAction]
        partial void MakeActualOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, string version);

        [NonAction]
        public override System.Web.Mvc.ActionResult MakeActual(System.Guid gid, System.Guid packageGid, string version)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MakeActual);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "version", version);
            MakeActualOverride(callInfo, gid, packageGid, version);
            return callInfo;
        }

        [NonAction]
        partial void SaveDraftOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, string version, System.Guid blobKey, string fileName);

        [NonAction]
        public override System.Web.Mvc.ActionResult SaveDraft(System.Guid gid, System.Guid packageGid, string version, System.Guid blobKey, string fileName)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveDraft);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "version", version);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "blobKey", blobKey);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "fileName", fileName);
            SaveDraftOverride(callInfo, gid, packageGid, version, blobKey, fileName);
            return callInfo;
        }

        [NonAction]
        partial void SaveDraftWithSimevCodeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, string version, string simevCode);

        [NonAction]
        public override System.Web.Mvc.ActionResult SaveDraftWithSimevCode(System.Guid gid, System.Guid packageGid, string version, string simevCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveDraftWithSimevCode);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "version", version);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "simevCode", simevCode);
            SaveDraftWithSimevCodeOverride(callInfo, gid, packageGid, version, simevCode);
            return callInfo;
        }

        [NonAction]
        partial void SubmitOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid gid, System.Guid packageGid, string version);

        [NonAction]
        public override System.Web.Mvc.ActionResult Submit(System.Guid gid, System.Guid packageGid, string version)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Submit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "gid", gid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageGid", packageGid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "version", version);
            SubmitOverride(callInfo, gid, packageGid, version);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114