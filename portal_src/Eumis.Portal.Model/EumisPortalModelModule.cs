using Autofac;
using Eumis.Common.Data;
using Eumis.Portal.Model.Repositories;

namespace Eumis.Portal.Model
{
    public class EumisPortalModelModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<EumisDbConfiguration>().As<IDbConfiguration>().SingleInstance();

            moduleBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AddressRepository>().As<IAddressRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<LoginRepository>().As<ILoginRepository>().InstancePerLifetimeScope();
        }
    }
}