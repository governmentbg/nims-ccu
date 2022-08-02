using Autofac;

namespace Eumis.Public.Common.NLog
{
    public class LogModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<NLogLogger>().As<ILogger>().SingleInstance();
        }
    }
}
