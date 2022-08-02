using Autofac;
using Eumis.Integration.Monitorstat.Api;
using Eumis.Integration.Monitorstat.Communicators;

namespace Eumis.Integration.Monitorstat
{
    public class MonitorstatModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // services
            builder.RegisterType<MonitorstatCommunicator>().As<IMonitorstatCommunicator>().InstancePerLifetimeScope();

            // controllers
            builder.RegisterType<NomenclatureController>().InstancePerLifetimeScope();
            builder.RegisterType<SurveyController>().InstancePerLifetimeScope();
            builder.RegisterType<MapNodeController>().InstancePerLifetimeScope();
            builder.RegisterType<ProcedureRequestController>().InstancePerLifetimeScope();
            builder.RegisterType<SubjectRequestController>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
