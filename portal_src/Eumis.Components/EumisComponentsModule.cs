using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Eumis.Components.ValidationEngine;

namespace Eumis.Components
{
    public class EumisComponentsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<UesFactoryImpl>().As<IUesFactory>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DocumentSigner>().As<IDocumentSigner>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DocumentSerializer>().As<IDocumentSerializer>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<XmlSchemaValidator>().As<IXmlSchemaValidator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CSValidationEngine>().As<IValidationEngine>().InstancePerLifetimeScope();
        }
    }
}
