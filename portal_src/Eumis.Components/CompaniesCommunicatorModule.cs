using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Components.Communicators;

namespace Eumis.Components
{
    public class CompaniesCommunicatorModule : Module
    {
        public bool IsFake { get; set; }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            if (IsFake)
            {
                moduleBuilder.RegisterType<FakeCompaniesCommunicator>().As<ICompaniesCommunicator>().InstancePerLifetimeScope();
            }
            else
            {
                moduleBuilder.RegisterType<CompaniesCommunicator>().As<ICompaniesCommunicator>().InstancePerLifetimeScope();
            }
        }
    }
}
