using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Components.Communicators;
using Eumis.Components.Caches;

namespace Eumis.Components
{
    public class EumisCacheModule : Module
    {
        public bool IsFake { get; set; }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<EumisCache>().As<IEumisCache>().SingleInstance();
        }
    }
}
