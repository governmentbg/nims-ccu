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
    public partial class ProcedureController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ProcedureController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Info()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Info);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InfoQA()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoQA);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Question()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Question);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InfoEnded()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoEnded);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InfoEndedQA()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoEndedQA);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InfoPublicDiscussion()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoPublicDiscussion);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Comment()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Comment);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InfoArchivedPublicDiscussion()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoArchivedPublicDiscussion);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InfoDownload()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoDownload);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InfoPublicDiscussionDownload()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoPublicDiscussionDownload);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProcedureController Actions { get { return MVC.Procedure; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Procedure";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Procedure";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Active = "Active";
            public readonly string Info = "Info";
            public readonly string InfoQA = "InfoQA";
            public readonly string Question = "Question";
            public readonly string Ended = "Ended";
            public readonly string InfoEnded = "InfoEnded";
            public readonly string InfoEndedQA = "InfoEndedQA";
            public readonly string PublicDiscussion = "PublicDiscussion";
            public readonly string InfoPublicDiscussion = "InfoPublicDiscussion";
            public readonly string Comment = "Comment";
            public readonly string ArchivedPublicDiscussion = "ArchivedPublicDiscussion";
            public readonly string InfoArchivedPublicDiscussion = "InfoArchivedPublicDiscussion";
            public readonly string InfoDownload = "InfoDownload";
            public readonly string InfoPublicDiscussionDownload = "InfoPublicDiscussionDownload";
            public readonly string PublicPreview = "PublicPreview";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Active = "Active";
            public const string Info = "Info";
            public const string InfoQA = "InfoQA";
            public const string Question = "Question";
            public const string Ended = "Ended";
            public const string InfoEnded = "InfoEnded";
            public const string InfoEndedQA = "InfoEndedQA";
            public const string PublicDiscussion = "PublicDiscussion";
            public const string InfoPublicDiscussion = "InfoPublicDiscussion";
            public const string Comment = "Comment";
            public const string ArchivedPublicDiscussion = "ArchivedPublicDiscussion";
            public const string InfoArchivedPublicDiscussion = "InfoArchivedPublicDiscussion";
            public const string InfoDownload = "InfoDownload";
            public const string InfoPublicDiscussionDownload = "InfoPublicDiscussionDownload";
            public const string PublicPreview = "PublicPreview";
        }


        static readonly ActionParamsClass_Info s_params_Info = new ActionParamsClass_Info();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Info InfoParams { get { return s_params_Info; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Info
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_InfoQA s_params_InfoQA = new ActionParamsClass_InfoQA();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InfoQA InfoQAParams { get { return s_params_InfoQA; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InfoQA
        {
            public readonly string id = "id";
            public readonly string searchTerm = "searchTerm";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Question s_params_Question = new ActionParamsClass_Question();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Question QuestionParams { get { return s_params_Question; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Question
        {
            public readonly string id = "id";
            public readonly string vm = "vm";
            public readonly string captchaValid = "captchaValid";
        }
        static readonly ActionParamsClass_InfoEnded s_params_InfoEnded = new ActionParamsClass_InfoEnded();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InfoEnded InfoEndedParams { get { return s_params_InfoEnded; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InfoEnded
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_InfoEndedQA s_params_InfoEndedQA = new ActionParamsClass_InfoEndedQA();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InfoEndedQA InfoEndedQAParams { get { return s_params_InfoEndedQA; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InfoEndedQA
        {
            public readonly string id = "id";
            public readonly string searchTerm = "searchTerm";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_InfoPublicDiscussion s_params_InfoPublicDiscussion = new ActionParamsClass_InfoPublicDiscussion();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InfoPublicDiscussion InfoPublicDiscussionParams { get { return s_params_InfoPublicDiscussion; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InfoPublicDiscussion
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Comment s_params_Comment = new ActionParamsClass_Comment();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Comment CommentParams { get { return s_params_Comment; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Comment
        {
            public readonly string id = "id";
            public readonly string vm = "vm";
            public readonly string captchaValid = "captchaValid";
        }
        static readonly ActionParamsClass_InfoArchivedPublicDiscussion s_params_InfoArchivedPublicDiscussion = new ActionParamsClass_InfoArchivedPublicDiscussion();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InfoArchivedPublicDiscussion InfoArchivedPublicDiscussionParams { get { return s_params_InfoArchivedPublicDiscussion; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InfoArchivedPublicDiscussion
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_InfoDownload s_params_InfoDownload = new ActionParamsClass_InfoDownload();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InfoDownload InfoDownloadParams { get { return s_params_InfoDownload; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InfoDownload
        {
            public readonly string id = "id";
            public readonly string fileKey = "fileKey";
        }
        static readonly ActionParamsClass_InfoPublicDiscussionDownload s_params_InfoPublicDiscussionDownload = new ActionParamsClass_InfoPublicDiscussionDownload();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InfoPublicDiscussionDownload InfoPublicDiscussionDownloadParams { get { return s_params_InfoPublicDiscussionDownload; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InfoPublicDiscussionDownload
        {
            public readonly string id = "id";
            public readonly string fileKey = "fileKey";
        }
        static readonly ActionParamsClass_PublicPreview s_params_PublicPreview = new ActionParamsClass_PublicPreview();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PublicPreview PublicPreviewParams { get { return s_params_PublicPreview; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PublicPreview
        {
            public readonly string form = "form";
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
                public readonly string Active = "Active";
                public readonly string ArchivedPublicDiscussion = "ArchivedPublicDiscussion";
                public readonly string Comment = "Comment";
                public readonly string Ended = "Ended";
                public readonly string Info = "Info";
                public readonly string InfoArchivedPublicDiscussion = "InfoArchivedPublicDiscussion";
                public readonly string InfoEnded = "InfoEnded";
                public readonly string InfoEndedQA = "InfoEndedQA";
                public readonly string InfoPublicDiscussion = "InfoPublicDiscussion";
                public readonly string InfoQA = "InfoQA";
                public readonly string PublicDiscussion = "PublicDiscussion";
                public readonly string PublicPreview = "PublicPreview";
                public readonly string Question = "Question";
            }
            public readonly string Active = "~/Views/Procedure/Active.cshtml";
            public readonly string ArchivedPublicDiscussion = "~/Views/Procedure/ArchivedPublicDiscussion.cshtml";
            public readonly string Comment = "~/Views/Procedure/Comment.cshtml";
            public readonly string Ended = "~/Views/Procedure/Ended.cshtml";
            public readonly string Info = "~/Views/Procedure/Info.cshtml";
            public readonly string InfoArchivedPublicDiscussion = "~/Views/Procedure/InfoArchivedPublicDiscussion.cshtml";
            public readonly string InfoEnded = "~/Views/Procedure/InfoEnded.cshtml";
            public readonly string InfoEndedQA = "~/Views/Procedure/InfoEndedQA.cshtml";
            public readonly string InfoPublicDiscussion = "~/Views/Procedure/InfoPublicDiscussion.cshtml";
            public readonly string InfoQA = "~/Views/Procedure/InfoQA.cshtml";
            public readonly string PublicDiscussion = "~/Views/Procedure/PublicDiscussion.cshtml";
            public readonly string PublicPreview = "~/Views/Procedure/PublicPreview.cshtml";
            public readonly string Question = "~/Views/Procedure/Question.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProcedureController : Eumis.Portal.Web.Controllers.ProcedureController
    {
        public T4MVC_ProcedureController() : base(Dummy.Instance) { }

        [NonAction]
        partial void ActiveOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Active()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Active);
            ActiveOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InfoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Info(System.Guid id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Info);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InfoOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void InfoQAOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id, string searchTerm, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult InfoQA(System.Guid id, string searchTerm, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoQA);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchTerm", searchTerm);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            InfoQAOverride(callInfo, id, searchTerm, page);
            return callInfo;
        }

        [NonAction]
        partial void QuestionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Question(System.Guid id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Question);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            QuestionOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void QuestionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Eumis.Portal.Web.Models.ProcedureQuestion.ProcedureDiscussionQuestionVM vm, bool? captchaValid);

        [NonAction]
        public override System.Web.Mvc.ActionResult Question(Eumis.Portal.Web.Models.ProcedureQuestion.ProcedureDiscussionQuestionVM vm, bool? captchaValid)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Question);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "vm", vm);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "captchaValid", captchaValid);
            QuestionOverride(callInfo, vm, captchaValid);
            return callInfo;
        }

        [NonAction]
        partial void EndedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Ended()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Ended);
            EndedOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InfoEndedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id);

        [NonAction]
        public override System.Web.Mvc.ActionResult InfoEnded(System.Guid id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoEnded);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InfoEndedOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void InfoEndedQAOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id, string searchTerm, int page);

        [NonAction]
        public override System.Web.Mvc.ActionResult InfoEndedQA(System.Guid id, string searchTerm, int page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoEndedQA);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchTerm", searchTerm);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            InfoEndedQAOverride(callInfo, id, searchTerm, page);
            return callInfo;
        }

        [NonAction]
        partial void PublicDiscussionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult PublicDiscussion()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PublicDiscussion);
            PublicDiscussionOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InfoPublicDiscussionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id);

        [NonAction]
        public override System.Web.Mvc.ActionResult InfoPublicDiscussion(System.Guid id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoPublicDiscussion);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InfoPublicDiscussionOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void CommentOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Comment(System.Guid id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Comment);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CommentOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void CommentOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Eumis.Portal.Web.Models.ProcedureComment.ProcedureCommentVM vm, bool? captchaValid);

        [NonAction]
        public override System.Web.Mvc.ActionResult Comment(Eumis.Portal.Web.Models.ProcedureComment.ProcedureCommentVM vm, bool? captchaValid)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Comment);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "vm", vm);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "captchaValid", captchaValid);
            CommentOverride(callInfo, vm, captchaValid);
            return callInfo;
        }

        [NonAction]
        partial void ArchivedPublicDiscussionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ArchivedPublicDiscussion()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ArchivedPublicDiscussion);
            ArchivedPublicDiscussionOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InfoArchivedPublicDiscussionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id);

        [NonAction]
        public override System.Web.Mvc.ActionResult InfoArchivedPublicDiscussion(System.Guid id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoArchivedPublicDiscussion);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InfoArchivedPublicDiscussionOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void InfoDownloadOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id, System.Guid fileKey);

        [NonAction]
        public override System.Web.Mvc.ActionResult InfoDownload(System.Guid id, System.Guid fileKey)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoDownload);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "fileKey", fileKey);
            InfoDownloadOverride(callInfo, id, fileKey);
            return callInfo;
        }

        [NonAction]
        partial void InfoPublicDiscussionDownloadOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Guid id, System.Guid fileKey);

        [NonAction]
        public override System.Web.Mvc.ActionResult InfoPublicDiscussionDownload(System.Guid id, System.Guid fileKey)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InfoPublicDiscussionDownload);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "fileKey", fileKey);
            InfoPublicDiscussionDownloadOverride(callInfo, id, fileKey);
            return callInfo;
        }

        [NonAction]
        partial void PublicPreviewOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult PublicPreview()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PublicPreview);
            PublicPreviewOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PublicPreviewOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Web.Mvc.FormCollection form);

        [NonAction]
        public override System.Web.Mvc.ActionResult PublicPreview(System.Web.Mvc.FormCollection form)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PublicPreview);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "form", form);
            PublicPreviewOverride(callInfo, form);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
