using System;

namespace Eumis.Public.Web.InfrastructureClasses.Maps
{
    public class BgMapRendererModel : MapRendererModel
    {
        public BgMapRendererModel(RendererType type, BgMapDataType infoset, int rootId, UrlDef redirectUrl)
            : base()
        {
            this.BaseUrl = new Uri(string.Format("/0/0/{0}/", MVC.BgMaps.Name), UriKind.Relative);
            this.Type = type.ToString();
            this.Infoset = infoset.ToString();
            this.RedirectUrl = redirectUrl;
            this.RootId = rootId;

            switch (infoset)
            {
                case BgMapDataType.NameOnly:
                    this.TooltipView = MVC.Infrastructure.Views.Maps.NameOnlyTooltip;
                    break;
            }
        }
    }
}