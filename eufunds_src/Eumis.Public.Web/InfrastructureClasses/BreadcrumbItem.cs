namespace Eumis.Public.Web.InfrastructureClasses
{
    public class BreadcrumbItem
    {
        public BreadcrumbItem()
        {
        }

        public BreadcrumbItem(string action, string label)
        {
            this.Action = action;
            this.Label = label;
        }

        public string Action { get; set; }

        public string Label { get; set; }
    }
}