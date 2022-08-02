namespace Eumis.Public.Web.InfrastructureClasses.Pies
{
    public class PieRendererModel
    {
        public PieRendererModel(string title, UrlDef dataUrl, string tooltipView, bool percentageLabelEnabled = true)
        {
            this.Title = title;
            this.DataUrl = dataUrl;
            this.TooltipView = tooltipView;
            this.PercentageLabelEnabled = percentageLabelEnabled;
        }

        public string Title { get; set; }

        public UrlDef DataUrl { get; set; }

        public string TooltipView { get; set; }

        public bool PercentageLabelEnabled { get; set; }
    }
}