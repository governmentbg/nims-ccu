using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Components.Communicators;

namespace Eumis.Components
{
    class FinanceReportCommunicatorModule : Module
    {
        public bool IsFake { get; set; }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            if (IsFake)
            {
                moduleBuilder.RegisterType<FakeFinanceReportCommunicator>().As<IFinanceReportCommunicator>().InstancePerLifetimeScope();
            }
            else
            {
                moduleBuilder.RegisterType<FinanceReportCommunicator>().As<IFinanceReportCommunicator>().InstancePerLifetimeScope();
            }
        }
    }
}
