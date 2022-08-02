using Autofac;
using Eumis.Common.Db;
using Eumis.Domain.ActionLogs;
using Eumis.Domain.Allowances;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.Audits;
using Eumis.Domain.BasicInterestRates;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Domain.CertReports;
using Eumis.Domain.Companies;
using Eumis.Domain.Contracts;
using Eumis.Domain.Debts;
using Eumis.Domain.Emails;
using Eumis.Domain.EuReimbursedAmounts;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.ExpenseTypes;
using Eumis.Domain.FIReimbursedAmounts;
using Eumis.Domain.Guidances;
using Eumis.Domain.HistoricContracts;
using Eumis.Domain.Indicators;
using Eumis.Domain.InterestSchemes;
using Eumis.Domain.Irregularities;
using Eumis.Domain.Messages;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Notifications;
using Eumis.Domain.OperationalMap.Directions;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProcedureManuals;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procurements;
using Eumis.Domain.Projects;
using Eumis.Domain.Registrations;
using Eumis.Domain.RequestPackages;
using Eumis.Domain.SapInterfaces;
using Eumis.Domain.SpotChecks;
using Eumis.Domain.Users;

namespace Eumis.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<MapNodesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProgrammePrioritiesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProgrammesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<UsersModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<NonAggregatesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<DirectionsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<IndicatorsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ExpenseTypesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<DeclarationModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProceduresModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<CompaniesModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<NewsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<MessageModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<GuidanceModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
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
            moduleBuilder.RegisterType<AuditsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<MonitoringFinancialControlModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<IrregularityModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<DebtsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<CertReportModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<AnnualAccountReportModelDBConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<CertAuthorityChecksModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<EuReimbursedAmountsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<SapImportModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<FIReimbursedAmountModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ActionLogsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<HistoricContractsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<NotificationsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProcedureManualModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<MonitorstatModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProcurementsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<ProgrammeDeclarationsModelDbConfiguration>().As<IDbConfiguration>().SingleInstance();
        }
    }
}
