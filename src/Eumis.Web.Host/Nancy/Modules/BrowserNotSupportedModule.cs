using Eumis.Web.Host.Nancy.Models;
using Nancy;

namespace Eumis.Web.Host.Nancy.Modules
{
    public class BrowserNotSupportedModule : NancyModule
    {
        public BrowserNotSupportedModule()
        {
            this.Get("/browserNotSupported", ctx =>
            {
                this.Context.NegotiationContext.WithNoCacheHeaders();
                return this.View["browserNotSupported", new LayoutModel()];
            });
        }
    }
}