using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Eumis.Web.Host
{
    public class WebHostModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            HttpConfiguration config = new HttpConfiguration();

            builder.RegisterHttpRequestMessage(config);
            base.Load(builder);
        }
    }
}
