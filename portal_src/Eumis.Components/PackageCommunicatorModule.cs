using Autofac;
using Eumis.Components.Communicators;

namespace Eumis.Components
{
    public class PackageCommunicatorModule : Module
    {
        public bool IsFake { get; set; }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            if (IsFake)
            {
                moduleBuilder.RegisterType<FakePackageCommunicator>().As<IPackageCommunicator>().InstancePerLifetimeScope();
            }
            else
            {
                moduleBuilder.RegisterType<PackageCommunicator>().As<IPackageCommunicator>().InstancePerLifetimeScope();
            }
        }
    }
}
