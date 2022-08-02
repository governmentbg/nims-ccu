namespace Eumis.Common.Validation
{
    public class ValidationOption
    {
        public string ModelPath { get; set; }
        public string ErrorSimpleMessage { get; set; }
        public string ErrorRowIdentifier { get; set; }
        public string ErrorComplexMessage { get; set; }
        public bool IsAngularValidation { get; set; }
        public bool IsStopError { get; set; }

        public static ValidationOption Create(string modelPath, string errorSimpleMessage, string errorComplexMessage, bool isAngularValidation, bool isStopError, string errorRowIdentifier = null)
        {
            ValidationOption vo = new ValidationOption()
            {
                ModelPath = modelPath,
                ErrorSimpleMessage = errorSimpleMessage,
                ErrorComplexMessage = errorComplexMessage,
                IsAngularValidation = isAngularValidation,
                IsStopError = isStopError,
                ErrorRowIdentifier = errorRowIdentifier
            };

            return vo;
        }
    }
}
