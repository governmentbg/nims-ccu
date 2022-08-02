using Eumis.Common.Extensions;
using System;

namespace Eumis.Portal.Web.Models.ProcedureQuestion
{
    public class ProcedureDiscussionQuestionVM
    {
        public Guid Id { get; set; }

        public string ProcedureName { get; set; }

        public string SenderName { get; set; }

        public string Email { get; set; }

        [RequiredLocalized(ErrorMessageResourceName = "MessageRequired", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        [StringLengthLocalized(4000, ErrorMessageResourceName = "MessageLength", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        public string Question { get; set; }

        public string Captcha { get; set; }
    }
}
