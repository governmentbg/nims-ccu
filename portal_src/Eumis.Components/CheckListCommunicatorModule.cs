using Autofac;
using Eumis.Components.Communicators;

namespace Eumis.Components
{
    public class CheckListCommunicatorModule : Module
    {
        public bool IsFake { get; set; }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            if (IsFake)
            {
                moduleBuilder.RegisterType<FakeCheckListCommunicator>().As<ICheckListCommunicator>().InstancePerLifetimeScope();
            }
            else
            {
                moduleBuilder.RegisterType<CheckListCommunicator>().As<ICheckListCommunicator>().InstancePerLifetimeScope();
            }
        }
    }
}
