namespace Eumis.Portal.Web.Models
{
    public class DownloadDraftXmlVM
    {
        public string LinkName { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string PostActionName { get; set; }
        public string SecondActionName { get; set; }
        public string ControllerName { get; set; }
        public bool HasXmlUpdate { get; set; }
        public string FORM_ID { get; set; }
        public bool ErrorMessageOnly { get; set; } = false;
    }
}