using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Web.Host.Owin
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            // Do not use cached views in debug mode
            #if DEBUG
            environment.Views(runtimeViewDiscovery: true, runtimeViewUpdates: true);
            #endif
            base.Configure(environment);
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            this.Conventions.ViewLocationConventions.Add((viewName, model, context) =>
            {
                return string.Concat("App/", viewName);
            });
        }
    }
}
