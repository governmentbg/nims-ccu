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
    public partial class OffersController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected OffersController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Details()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Details);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DetailsDownload()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DetailsDownload);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AddtitionalDocumentDownload()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddtitionalDocumentDownload);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Drafts()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Drafts);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SubmittedDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SubmittedDetails);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult WithdrawOffer()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WithdrawOffer);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public OffersController Actions { get { return MVC.Offers; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Offers";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Offers";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string IndexExport = "IndexExport";
            public readonly string Details = "Details";
            public readonly string DetailsDownload = "DetailsDownload";
            public readonly string AddtitionalDocumentDownload = "AddtitionalDocumentDownload";
            public readonly string Submitted = "Submitted";
            public readonly string SubmittedExport = "SubmittedExport";
            public readonly string Drafts = "Drafts";
            public readonly string SubmittedDetails = "SubmittedDetails";
            public readonly string Archived = "Archived";
            public readonly string ArchivedExport = "ArchivedExport";
            public readonly string WithdrawOffer = "WithdrawOffer";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string IndexExport = "IndexExport";
            public const string Details = "Details";
            public const string DetailsDownload = "DetailsDownload";
            public const string AddtitionalDocumentDownload = "AddtitionalDocumentDownload";
            public const string Submitted = "Submitted";
            public const string SubmittedExport = "SubmittedExport";
            public const string Drafts = "Drafts";
            public const string SubmittedDetails = "SubmittedDetails";
            public const string Archived = "Archived";
            public const string ArchivedExport = "ArchivedExport";
            public const string WithdrawOffer = "WithdrawOffer";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string dpName = "dpName";
            public readonly string name = "name";
            public readonly string companyName = "companyName";
            public readonly string offersDeadlineDate = "offersDeadlineDate";
            public readonly string noticeDate = "noticeDate";
            public readonly string page = "page";
            public readonly string sortBy = "sortBy";
            public readonly string sortOrder = "sortOrder";
        }
        static readonly ActionParamsClass_IndexExport s_params_IndexExport = new ActionParamsClass_IndexExport();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IndexExport IndexExportParams { get { return s_params_IndexExport; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IndexExport
        {
            public readonly string dpName = "dpName";
            public readonly string name = "name";
            public readonly string companyName = "companyName";
            public readonly string offersDeadlineDate = "offersDeadlineDate";
            public readonly string noticeDate = "noticeDate";
        }
        static readonly ActionParamsClass_Details s_params_Details = new ActionParamsClass_Details();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Details DetailsParams { get { return s_params_Details; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Details
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_DetailsDownload s_params_DetailsDownload = new ActionParamsClass_DetailsDownload();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DetailsDownload DetailsDownloadParams { get { return s_params_DetailsDownload; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DetailsDownload
        {
            public readonly string id = "id";
            public readonly string fileKey = "fileKey";
        }
        static readonly ActionParamsClass_AddtitionalDocumentDownload s_params_AddtitionalDocumentDownload = new ActionParamsClass_AddtitionalDocumentDownload();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddtitionalDocumentDownload AddtitionalDocumentDownloadParams { get { return s_params_AddtitionalDocumentDownload; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddtitionalDocumentDownload
        {
            public readonly string id = "id";
            public readonly string fileKey = "fileKey";
        }
        static readonly ActionParamsClass_Submitted s_params_Submitted = new ActionParamsClass_Submitted();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Submitted SubmittedParams { get { return s_params_Submitted; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Submitted
        {
            public readonly string dpName = "dpName";
            public readonly string name = "name";
            public readonly string companyName = "companyName";
            public readonly string offerSubmitDate = "offerSubmitDate";
            public readonly string page = "page";
            public readonly string sortBy = "sortBy";
            public readonly string sortOrder = "sortOrder";
        }
        static readonly ActionParamsClass_SubmittedExport s_params_SubmittedExport = new ActionParamsClass_SubmittedExport();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SubmittedExport SubmittedExportParams { get { return s_params_SubmittedExport; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SubmittedExport
        {
            public readonly string dpName = "dpName";
            public readonly string name = "name";
            public readonly string companyName = "companyName";
            public readonly string offerSubmitDate = "offerSubmitDate";
        }
        static readonly ActionParamsClass_Drafts s_params_Drafts = new ActionParamsClass_Drafts();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Drafts DraftsParams { get { return s_params_Drafts; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Drafts
        {
            public readonly string dpName = "dpName";
            public readonly string name = "name";
            public readonly string companyName = "companyName";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_SubmittedDetails s_params_SubmittedDetails = new ActionParamsClass_SubmittedDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SubmittedDetails SubmittedDetailsParams { get { return s_params_SubmittedDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SubmittedDetails
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Archived s_params_Archived = new ActionParamsClass_Archived();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Archived ArchivedParams { get { return s_params_Archived; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Archived
        {
            public readonly string dpName = "dpName";
            public readonly string name = "name";
            public readonly string companyName = "companyName";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_ArchivedExport s_params_ArchivedExport = new ActionParamsClass_ArchivedExport();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ArchivedExport ArchivedExportParams { get { return s_params_ArchivedExport; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ArchivedExport
        {
            public readonly string dpName = "dpName";
            public readonly string name = "name";
            public readonly string companyName = "companyName";
        }
        static readonly ActionParamsClass_WithdrawOffer s_params_WithdrawOffer = new ActionParamsClass_WithdrawOffer();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_WithdrawOffer WithdrawOfferParams { get { return s_params_WithdrawOffer; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_WithdrawOffer
        {
            public readonly string id = "id";
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
                public readonly string _NavTabs = "_NavTabs";
                public readonly string Archived = "Archived";
                public readonly string Details = "Details";
                public readonly string Drafts = "Drafts";
                public readonly string Index = "Index";
                public readonly string Submitted = "Submitted";
                public readonly string SubmittedDetails = "SubmittedDetails";
            }
            public readonly string _NavTabs = "~/Views/Offers/_NavTabs.cshtml";
            public readonly string Archived = "~/Views/Offers/Archived.cshtml";
            public readonly string Details = "~/Views/Offers/Details.cshtml";
            public readonly string Drafts = "~/Views/Offers/Drafts.cshtml";
            public readonly string Index = "~/Views/Offers/Index.cshtml";
            public readonly string Submitted = "~/Views/Offers/Submitted.cshtml";
            public readonly string SubmittedDetails = "~/Views/Offers/SubmittedDetails.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_OffersController : Eumis.Portal.Web.Controllers.OffersController
    {
        public T4MVC_OffersController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string dpName, string name, string companyName, System.DateTime? offersDeadlineDate, System.DateTime? noticeDate, int page, string sortBy, Eumis.Common.Helpers.SortOrder sortOrder);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(string dpName, string name, string companyName, System.DateTime? offersDeadlineDate, System.DateTime? noticeDate, int page, string sortBy, Eumis.Common.Helpers.SortOrder sortOrder)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dpName", dpName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "companyName", companyName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "offersDeadlineDate", offersDeadlineDate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "noticeDate", noticeDate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sortBy", sortBy);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sortOrder", sortOrder);
            IndexOverride(callInfo, dpName, name, companyName, offersDeadlineDate, noticeDate, page, sortBy, sortOrder);
            return callInfo;
        }

        [NonAction]
        partial void IndexExportOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string dpName, string name, string companyName, System.DateTime? offersDeadlineDate, System.DateTime? noticeDate);

        [NonAction]
        public override System.Web.Mvc.ActionResult IndexExport(string dpName, string name, string companyName, System.DateTime? offersDeadlineDate, System.DateTime? noticeDate)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IndexExport);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dpName", dpName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "companyName", companyName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "offersDeadlineDate", offersDeadlineDate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "noticeDate", noticeDate);
            IndexExportOverride(callInfo, dpName, name, companyName, offersDeadlineDate, noticeDate);
            return callInfo;
        }

        [NonAction]
        partial void DetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Details(System.Guid id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Details);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DetailsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void DetailsDownloadOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id, System.Guid fileKey);

        [NonAction]
        public override System.Web.Mvc.ActionResult DetailsDownload(System.Guid id, System.Guid fileKey)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DetailsDownload);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "fileKey", fileKey);
            DetailsDownloadOverride(callInfo, id, fileKey);
            return callInfo;
        }

        [NonAction]
        partial void AddtitionalDocumentDownloadOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id, System.Guid fileKey);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddtitionalDocumentDownload(System.Guid id, System.Guid fileKey)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddtitionalDocumentDownload);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "fileKey", fileKey);
            AddtitionalDocumentDownloadOverride(callInfo, id, fileKey);
            return callInfo;
        }

        [NonAction]
        partial void SubmittedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string dpName, string name, string companyName, System.DateTime? offerSubmitDate, int page, string sortBy, Eumis.Common.Helpers.SortOrder? sortOrder);

        [NonAction]
        public override System.Web.Mvc.ActionResult Submitted(string dpName, string name, string companyName, System.DateTime? offerSubmitDate, int page, string sortBy, Eumis.Common.Helpers.SortOrder? sortOrder)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Submitted);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dpName", dpName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "companyName", companyName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "offerSubmitDate", offerSubmitDate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sortBy", sortBy);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sortOrder", sortOrder);
            SubmittedOverride(callInfo, dpName, name, companyName, offerSubmitDate, page, sortBy, sortOrder);
            return callInfo;
        }

        [NonAction]
        partial void SubmittedExportOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string dpName, string name, string companyName, System.DateTime? offerSubmitDate);

        [NonAction]
        public override System.Web.Mvc.ActionResult SubmittedExport(string dpName, string name, string companyName, System.DateTime? offerSubmitDate)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SubmittedExport);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dpName", dpName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "companyName", companyName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "offerSubmitDate", offerSubmitDate);
            SubmittedExportOverride(callInfo, dpName, name, companyName, offerSubmitDate);
            return callInfo;
        }

        [NonAction]
        partial void DraftsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string dpName, string name, string companyName, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Drafts(string dpName, string name, string companyName, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Drafts);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dpName", dpName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "companyName", companyName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            DraftsOverride(callInfo, dpName, name, companyName, page);
            return callInfo;
        }

        [NonAction]
        partial void SubmittedDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id);

        [NonAction]
        public override System.Web.Mvc.ActionResult SubmittedDetails(System.Guid id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SubmittedDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            SubmittedDetailsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void ArchivedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string dpName, string name, string companyName, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult Archived(string dpName, string name, string companyName, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Archived);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dpName", dpName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "companyName", companyName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ArchivedOverride(callInfo, dpName, name, companyName, page);
            return callInfo;
        }

        [NonAction]
        partial void ArchivedExportOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string dpName, string name, string companyName);

        [NonAction]
        public override System.Web.Mvc.ActionResult ArchivedExport(string dpName, string name, string companyName)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ArchivedExport);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dpName", dpName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "companyName", companyName);
            ArchivedExportOverride(callInfo, dpName, name, companyName);
            return callInfo;
        }

        [NonAction]
        partial void WithdrawOfferOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id, string version);

        [NonAction]
        public override System.Web.Mvc.ActionResult WithdrawOffer(System.Guid id, string version)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WithdrawOffer);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "version", version);
            WithdrawOfferOverride(callInfo, id, version);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
