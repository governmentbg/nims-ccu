using Eumis.Common.Extensions;
namespace Eumis.Portal.Web.Models.Feedback
{
    public class FeedbackVM
    {
        [RequiredLocalized(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        public string Type { get; set; }

        [StringLengthLocalized(100, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        [RegularExpressionLocalized(@"^[а-яА-Яa-zA-Z ]*$", ErrorMessageResourceName = "NameRegularExpression", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        public string Name { get; set; }

        [StringLengthLocalized(100, ErrorMessageResourceName = "EmailMaxlength", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        [RegularExpressionLocalized(@"^[\w\-!#$%&'*+/=?^`{|}~.""]+@([\w]+[.-]?)+[\w]\.[\w]+$", ErrorMessageResourceName = "EmailRegularExpression", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        [RequiredLocalized(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        public string Email { get; set; }

        [RequiredLocalized(ErrorMessageResourceName = "MessageRequired", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        [StringLengthLocalized(4000, ErrorMessageResourceName = "MessageLength", ErrorMessageResourceType = typeof(Eumis.Portal.Web.Views.Shared.App_LocalResources.Feedback))]
        public string Message { get; set; }

        public string Captcha { get; set; }
    }
}