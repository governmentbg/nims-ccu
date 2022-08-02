using Eumis.Common.Extensions;
using System;

namespace Eumis.Portal.Web.Models.ProcedureComment
{
    public class ProcedureCommentVM
    {
        public Guid Id { get; set; }

        public string ProcedureName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        [RequiredLocalized(ErrorMessageResourceName = "MessageRequired", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        [StringLengthLocalized(4000, ErrorMessageResourceName = "MessageLength", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        public string Message { get; set; }

        public string Captcha { get; set; }
    }
}
