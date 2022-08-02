using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Eumis.Integration.Host
{
    public class IntegrationHostModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            moduleBuilder.RegisterHttpRequestMessage(httpConfiguration);
        }
    }
}
