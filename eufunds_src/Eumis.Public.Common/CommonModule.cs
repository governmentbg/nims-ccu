using Autofac;

namespace Eumis.Public.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<DocumentSerializer>().As<IDocumentSerializer>().InstancePerLifetimeScope();
        }
    }
}
