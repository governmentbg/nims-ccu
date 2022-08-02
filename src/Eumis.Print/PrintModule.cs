using Autofac;

namespace Eumis.Print
{
    public class PrintModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<PrintManager>().As<IPrintManager>().InstancePerLifetimeScope();
        }
    }
}
