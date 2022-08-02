using Autofac;
using Eumis.Public.Common.Db;
using Eumis.Public.Domain.Entities.Umis.Allowances;
using Eumis.Public.Domain.Entities.Umis.Audits;
using Eumis.Public.Domain.Entities.Umis.BasicInterestRates;
using Eumis.Public.Domain.Entities.Umis.CertReports;
using Eumis.Public.Domain.Entities.Umis.Companies;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.Debts;
using Eumis.Public.Domain.Entities.Umis.Emails;
using Eumis.Public.Domain.Entities.Umis.EuReimbursedAmounts;
using Eumis.Public.Domain.Entities.Umis.EvalSessions;
using Eumis.Public.Domain.Entities.Umis.ExpenseTypes;
using Eumis.Public.Domain.Entities.Umis.FIReimbursedAmounts;
using Eumis.Public.Domain.Entities.Umis.HistoricContracts;
using Eumis.Public.Domain.Entities.Umis.Indicators;
using Eumis.Public.Domain.Entities.Umis.InterestSchemes;
using Eumis.Public.Domain.Entities.Umis.Irregularities;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.InvestmentPriorities;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammeGroups;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.ProgrammePriorities;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.Programmes;
using Eumis.Public.Domain.Entities.Umis.OperationalMap.SpecificTargets;
using Eumis.Public.Domain.Entities.Umis.Procedures;
using Eumis.Public.Domain.Entities.Umis.Projects;
using Eumis.Public.Domain.Entities.Umis.Registrations;
using Eumis.Public.Domain.Entities.Umis.RequestPackages;
using Eumis.Public.Domain.Entities.Umis.SapInterfaces;
using Eumis.Public.Domain.Entities.Umis.SpotChecks;
using Eumis.Public.Domain.Entities.Umis.Users;

namespace Eumis.Public.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            // Main
            moduleBuilder.RegisterType<DbConfiguration>().As<IDbConfiguration>().SingleInstance();

            // Umis
            moduleBuilder.RegisterType<MapNodesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProgrammePrioritiesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProgrammesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProgrammeGroupsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<UsersDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<NonAggregatesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<InvestmentPrioritiesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<SpecificTargetsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<IndicatorsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ExpenseTypesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProceduresModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<CompaniesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProjectsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<RegistrationsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<RequestPackagesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<EmailsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<EvalSessionsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<AllowancesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<BasicInterestRateModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<InterestSchemesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ContractModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<SpotChecksModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<EuReimbursedAmountsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<AuditsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<MonitoringFinancialControlModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<IrregularityModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<DebtsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<CertReportModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<SapImportModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<FIReimbursedAmountModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<HistoricContractsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
        }
    }
}
