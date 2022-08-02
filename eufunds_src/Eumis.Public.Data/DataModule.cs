using Autofac;
using Autofac.Extras.Attributed;
using Eumis.Public.Common;
using Eumis.Public.Data.Calendar.Repositories;
using Eumis.Public.Data.Companies.Repositories;
using Eumis.Public.Data.ContractContracts.Repositories;
using Eumis.Public.Data.Contracts.Repositories;
using Eumis.Public.Data.Core;
using Eumis.Public.Data.ExecutedContracts.Repositories;
using Eumis.Public.Data.Procedures.Repositories;
using Eumis.Public.Data.ProgrammeGroups.Repositories;
using Eumis.Public.Data.Repositories;
using Eumis.Public.Model.Repositories;

namespace Eumis.Public.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder
                .Register(c =>
                    c.ResolveKeyed<UnitOfWorkFactory>("factory")(DbKey.Umis) as IUnitOfWork)
                .Keyed<IUnitOfWork>(DbKey.Umis).InstancePerLifetimeScope();

            moduleBuilder
                .Register(c =>
                    c.ResolveKeyed<UnitOfWorkFactory>("factory")(DbKey.Main) as IUnitOfWork)
                .Keyed<IUnitOfWork>(DbKey.Main).As<IUnitOfWork>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<UnitOfWork>().Keyed<IUnitOfWork>("factory");

            // DisposableTuple
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,,,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,,,,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,,,,,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,,,,,,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,,,,,,,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,,,,,,,,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,,,,,,,,,>)).AsSelf();

            // Main
            moduleBuilder.RegisterType<MapsRepository>().As<IMapsRepository>().InstancePerLifetimeScope();

            // Umis
            moduleBuilder.RegisterType<InfrastructureRepository>().As<IInfrastructureRepository>().WithAttributeFilter();
            moduleBuilder.RegisterType<UmisRepository>().As<IUmisRepository>().WithAttributeFilter();
            moduleBuilder.RegisterType<NomenclatureRepository>().As<INomenclatureRepository>().WithAttributeFilter();

            moduleBuilder.RegisterType<CompaniesRepository>().As<ICompaniesRepository>().WithAttributeFilter();
            moduleBuilder.RegisterType<CalendarsRepository>().As<ICalendarsRepository>().WithAttributeFilter();
            moduleBuilder.RegisterType<ProgrammeGroupsRepository>().As<IProgrammeGroupsRepository>().WithAttributeFilter();
            moduleBuilder.RegisterType<ProceduresRepository>().As<IProceduresRepository>().WithAttributeFilter();
            moduleBuilder.RegisterType<ExecutedContractsRepository>().As<IExecutedContractsRepository>().WithAttributeFilter();
            moduleBuilder.RegisterType<ContractContractsRepository>().As<IContractContractsRepository>().WithAttributeFilter();
            moduleBuilder.RegisterType<ContractsRepository>().As<IContractsRepository>().WithAttributeFilter();
        }
    }
}
