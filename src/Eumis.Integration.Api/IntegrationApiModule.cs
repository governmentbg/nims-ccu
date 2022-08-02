using Autofac;

namespace Eumis.Integration.Api
{
    public class IntegrationApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            // Monitorstat
            moduleBuilder.RegisterType<Monitorstat.MonitorstatFileController>().InstancePerLifetimeScope();
        }
    }
}
