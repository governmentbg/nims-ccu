namespace Eumis.PortalIntegration.Api.Portal.Projects.DataObjects
{
    public class ProjectValidationErrorDO
    {
        public ProjectValidationErrorDO(string error, string errorAlt, bool isRequired)
        {
            this.Error = error;
            this.ErrorAlt = errorAlt;
            this.IsRequired = isRequired;
        }

        public string Error { get; set; }

        public string ErrorAlt { get; set; }

        public bool IsRequired { get; set; }
    }
}
