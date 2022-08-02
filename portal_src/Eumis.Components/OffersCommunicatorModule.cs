using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Components.Communicators;

namespace Eumis.Components
{
    public class OffersCommunicatorModule : Module
    {
        public bool IsFake { get; set; }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            if (IsFake)
            {
                moduleBuilder.RegisterType<FakeOffersCommunicator>().As<IOffersCommunicator>().InstancePerLifetimeScope();
            }
            else
            {
                moduleBuilder.RegisterType<OffersCommunicator>().As<IOffersCommunicator>().InstancePerLifetimeScope();
            }
        }
    }
}
