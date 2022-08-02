using Autofac;
using Autofac.Integration.Mvc;
using Eumis.Public.Common;
using Eumis.Public.Common.NLog;
using Eumis.Public.Data;
using Eumis.Public.Domain;
using Eumis.Public.Web.InfrastructureClasses.Maps;

namespace Eumis.Public.Web.App_Start
{
    public static class IoCConfig
    {
        public static IContainer Init()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new DomainModule());

            builder.RegisterModule(new DataModule());

            builder.RegisterModule(new MapsModule());

            builder.RegisterModule(new LogModule());

            builder.RegisterModule(new CommonModule());

            return builder.Build();
        }
    }
}