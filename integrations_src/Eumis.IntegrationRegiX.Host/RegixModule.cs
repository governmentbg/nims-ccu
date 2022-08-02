using Autofac;
using Eumis.IntegrationRegiX.Api;
using Eumis.IntegrationRegiX.Host.Auth;
using Eumis.IntegrationRegiX.Host.Communicators;
using System.Net.Http;

namespace Eumis.IntegrationRegiX.Host
{
    public class RegixModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // services
            builder.RegisterType<RegixCommunicator>().As<IRegixCommunicator>().InstancePerLifetimeScope();

            // controllers
            builder.RegisterType<BulstatController>().InstancePerLifetimeScope();
            builder.RegisterType<AvtrController>().InstancePerLifetimeScope();
            builder.RegisterType<DaeuController>().InstancePerLifetimeScope();
            builder.RegisterType<GraoController>().InstancePerLifetimeScope();
            builder.RegisterType<NraController>().InstancePerLifetimeScope();
            builder.RegisterType<MvrController>().InstancePerLifetimeScope();
            builder.RegisterType<MpController>().InstancePerLifetimeScope();
            builder.RegisterType<RezmaController>().InstancePerLifetimeScope();
            builder.RegisterType<BabhzhsController>().InstancePerLifetimeScope();
            builder.RegisterType<GppController>().InstancePerLifetimeScope();

            // contexts
            builder.Register(c => c.Resolve<HttpRequestMessage>().GetRegixCallContext()).As<IRegixCallContext>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
