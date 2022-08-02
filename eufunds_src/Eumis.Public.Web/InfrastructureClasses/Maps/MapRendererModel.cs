using System;

namespace Eumis.Public.Web.InfrastructureClasses.Maps
{
    public class MapRendererModel
    {
        public string Type { get; set; }

        public int RootId { get; set; }

        public Uri BaseUrl { get; set; }

        public string Infoset { get; set; }

        public UrlDef RedirectUrl { get; set; }

        public int? ShowId { get; set; }

        public string TooltipView { get; set; }
    }
}