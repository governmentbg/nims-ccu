using Autofac;

namespace Eumis.Public.Web.InfrastructureClasses.Maps
{
    public class MapsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BgMapsContainer>().As<IMapsContainer>().SingleInstance();
            builder.RegisterType<BgMapsDataExtractor>().As<IMapsDataExtractorGeneric<BgMapDataType>>().InstancePerLifetimeScope();
        }
    }
}