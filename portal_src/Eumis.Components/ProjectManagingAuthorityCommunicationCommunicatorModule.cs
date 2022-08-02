using Autofac;
using Eumis.Components.Communicators;

namespace Eumis.Components
{
    class ProjectManagingAuthorityCommunicationCommunicatorModule : Module
    {
        public bool IsFake { get; set; }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            if (IsFake)
            {
                moduleBuilder.RegisterType<FakeProjectCommunicationCommunicator>().As<IProjectCommunicationCommunicator>().InstancePerLifetimeScope();
            }
            else
            {
                moduleBuilder.RegisterType<ProjectCommunicationCommunicator>().As<IProjectCommunicationCommunicator>().InstancePerLifetimeScope();
            }
        }
    }
}
