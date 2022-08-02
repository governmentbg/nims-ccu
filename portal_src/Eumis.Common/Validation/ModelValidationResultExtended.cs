
using System;
using System.Web.Mvc;
namespace Eumis.Common.Validation
{
    [Serializable]
    public class ModelValidationResultExtended //: ModelValidationResult
    {
        public string MemberName { get; set; }
        public string Message { get; set; }

        public bool IsStopError { get; set; }
        public bool IsAngularValidation { get; set; }
        public ValidationMessageType ErrorMessageType { get; set; }
        public string ErrorComplexMessage { get; set; }
        public string ErrorSimpleMessage { get; set; }
        public string ErrorRowIdentifier { get; set; }
        public string DisplayErrorMessage
        {
            get
            {
                if (string.IsNullOrEmpty(this.ErrorRowIdentifier))
                {
                    return this.ErrorComplexMessage;
                }

                return $"{this.ErrorRowIdentifier} {this.ErrorComplexMessage}";
            }
        }
    }
}
