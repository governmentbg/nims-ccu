using Autofac;
using Monitorstat.Common.MonitorstatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common
{
    public class MonitorstatCommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // services
            builder.RegisterType<MonitorstatServiceClient>().InstancePerLifetimeScope();
            
            base.Load(builder);
        }
    }
}
