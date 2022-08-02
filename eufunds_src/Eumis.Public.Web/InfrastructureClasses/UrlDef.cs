namespace Eumis.Public.Web.InfrastructureClasses
{
    public class UrlDef
    {
        public UrlDef(string controller, string action, object urlParams = null)
        {
            this.Controller = controller;
            this.Action = action;
            this.UrlParams = urlParams;
        }

        public string Controller { get; set; }

        public string Action { get; set; }

        public object UrlParams { get; set; }
    }
}