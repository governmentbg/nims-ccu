namespace Eumis.Public.Web.InfrastructureClasses.Charts
{
    public class ChartRendererModel
    {
        public ChartRendererModel(string title, string yTitle, UrlDef dataUrl, string tooltipView, bool isStacked)
        {
            this.Title = title;
            this.YTitle = yTitle;
            this.DataUrl = dataUrl;
            this.TooltipView = tooltipView;
            this.IsStacked = isStacked;
        }

        public string Title { get; set; }

        public string YTitle { get; set; }

        public UrlDef DataUrl { get; set; }

        public string TooltipView { get; set; }

        public bool IsStacked { get; set; }

        public bool HasStackLabels { get; set; }
    }
}