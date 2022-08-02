using Autofac;
using Eumis.Common;
using Eumis.Common.Db;
using Eumis.Data.ActionLogs.Repositories;
using Eumis.Data.ActuallyPaidAmounts.Repositories;
using Eumis.Data.Allowances.Repositories;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.Arachne.Repositories;
using Eumis.Data.Audits.Repositories;
using Eumis.Data.BasicInterestRates.Repositories;
using Eumis.Data.CertAuthorityChecks.Repositories;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.CheckBlankTopics.Repositories;
using Eumis.Data.Companies.Repositories;
using Eumis.Data.CompensationDocuments.Repositories;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.ContractReportAdvanceNVPaymentAmounts.Repositories;
using Eumis.Data.ContractReportAdvancePaymentAmounts.Repositories;
using Eumis.Data.ContractReportCertAuthorityCorrections.Repositories;
using Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReportCertCorrections.Repositories;
using Eumis.Data.ContractReportCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCertCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
using Eumis.Data.ContractReportIndicators.Repositories;
using Eumis.Data.ContractReportRevalidationCertAuthorityCorrections.Repositories;
using Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReportRevalidations.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReportTechnicalCorrections.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Interception;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Counters;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.Declarations.Repositories;
using Eumis.Data.DomainServices;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.EuReimbursedAmounts.Repositories;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.ExpenseTypes.Repositories;
using Eumis.Data.FinancialCorrections.Repositories;
using Eumis.Data.FIReimbursedAmounts.Repositories;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Data.Guidances.Repositories;
using Eumis.Data.HistoricContract.Repositories;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.InterestSchemes.Repositories;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Measures.Repositories;
using Eumis.Data.Messages.Repositories;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitorstat.Repositories;
using Eumis.Data.News.Repositories;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.Repositories.Repos;
using Eumis.Data.NonAggregates.Repositories.Suggestions;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.OperationalMap.Directions.Repositories;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.PermissionTemplates.Repositories;
using Eumis.Data.PermissionTemplates.Repositories.Noms;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procurements.Repositories;
using Eumis.Data.Prognoses.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Data.ReimbursedAmounts.Repositories;
using Eumis.Data.SapInterfaces.Repositories;
using Eumis.Data.Sebra.Repositories;
using Eumis.Data.SpotChecks.Repositories;
using Eumis.Data.UserOrganizations.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Data.UserTypes.Repositories;
using Eumis.Domain.ActionLogs;
using Eumis.Domain.Allowances;
using Eumis.Domain.BasicInterestRates;
using Eumis.Domain.CertReports;
using Eumis.Domain.Companies;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Indicators;
using Eumis.Domain.InterestSchemes;
using Eumis.Domain.Irregularities;
using Eumis.Domain.Measures;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Directions;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.PermissionTemplates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using Eumis.Domain.Services;

namespace Eumis.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CountersRepository>().As<ICountersRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RelationsRepository>().As<IRelationsRepository>().InstancePerLifetimeScope();

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

            // AggregateRoot Repositories
            moduleBuilder.RegisterType<MapNodesRepository>().As<IMapNodesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UsersRepository>().As<IUsersRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserNomsRepository>().As<IUserNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PermissionTemplatesRepository>().As<IPermissionTemplatesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammesRepository>().As<IProgrammesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IndicatorsRepository>().As<IIndicatorsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IndicatorItemTypesRepository>().As<IIndicatorItemTypesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MeasuresRepository>().As<IMeasuresRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CheckBlankTopicsRepository>().As<ICheckBlankTopicsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammePrioritiesRepository>().As<IProgrammePrioritiesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DirectionsRepository>().As<IDirectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DirectionNomsRepository>().As<IEntityNomsRepository<Direction, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SubDirectionNomsRepository>().As<ISubDirectionNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProceduresRepository>().As<IProceduresRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureVersionsRepository>().As<IProcedureVersionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureEvalTableXmlsRepository>().As<IProcedureEvalTableXmlsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcurementsRepository>().As<IProcurementsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CompaniesRepository>().As<ICompaniesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RegistrationsRepository>().As<IRegistrationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PermissionsRepository>().As<IPermissionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectVersionXmlsRepository>().As<IProjectVersionXmlsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectVersionXmlFileNomsRepository>().As<IProjectVersionXmlFileNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectsRepository>().As<IProjectsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectCommunicationsRepository>().As<IProjectCommunicationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectManagingAuthorityCommunicationsRepository>().As<IProjectManagingAuthorityCommunicationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectMassManagingAuthorityCommunicationsRepository>().As<IProjectMassManagingAuthorityCommunicationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RegProjectXmlsRepository>().As<IRegProjectXmlsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserTypesRepository>().As<IUserTypesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RequestPackagesRepository>().As<IRequestPackagesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EmailsRepository>().As<IEmailsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserOrganizationsRepository>().As<IUserOrganizationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionsRepository>().As<IEvalSessionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionReportsRepository>().As<IEvalSessionReportsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionStandpointXmlsRepository>().As<IEvalSessionStandpointXmlsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ExpenseTypesRepository>().As<IExpenseTypesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AllowancesRepository>().As<IAllowancesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<BasicInterestRatesRepository>().As<IBasicInterestRatesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<InterestSchemesRepository>().As<IInterestSchemesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DeclarationsRepository>().As<IDeclarationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectFilesRepository>().As<IProjectFilesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectCommunicationFilesRepository>().As<IProjectCommunicationFilesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NewsRepository>().As<INewsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MessagesRepository>().As<IMessagesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GuidancesRepository>().As<IGuidancesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractsRepository>().As<IContractsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractVersionsRepository>().As<IContractVersionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractProcurementsRepository>().As<IContractProcurementsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractCommunicationXmlsRepository>().As<IContractCommunicationXmlsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractRegistrationsRepository>().As<IContractRegistrationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractAccessCodesRepository>().As<IContractAccessCodesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractSpendingPlansRepository>().As<IContractSpendingPlansRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractUserNomsRepository>().As<IContractUserNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportsRepository>().As<IContractReportsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialsRepository>().As<IContractReportFinancialsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportPaymentsRepository>().As<IContractReportPaymentsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportTechnicalsRepository>().As<IContractReportTechnicalsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialChecksRepository>().As<IContractReportFinancialChecksRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportPaymentChecksRepository>().As<IContractReportPaymentChecksRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportTechnicalChecksRepository>().As<IContractReportTechnicalChecksRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCorrectionsRepository>().As<IContractReportFinancialCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCSDsRepository>().As<IContractReportFinancialCSDsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCSDBudgetItemsRepository>().As<IContractReportFinancialCSDBudgetItemsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicrosRepository>().As<IContractReportMicrosRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicrosDistrictNomsRepository>().As<IContractReportMicrosDistrictNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicrosMunicipalityNomsRepository>().As<IContractReportMicrosMunicipalityNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicrosSettlementNomsRepository>().As<IContractReportMicrosSettlementNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicroChecksRepository>().As<IContractReportMicroChecksRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractNomsRepository>().As<IContractNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractContractNomsRepository>().As<IContractContractNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CertAuthorityChecksRepository>().As<ICertAuthorityChecksRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCorrectionCSDsRepository>().As<IContractReportFinancialCorrectionCSDsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FlatFinancialCorrectionsRepository>().As<IFlatFinancialCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractBudgetLevel3NomsRepository>().As<IContractBudgetLevel3NomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractDebtsRepository>().As<IContractDebtsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractDebtVersionsRepository>().As<IContractDebtVersionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractDebtNomsRepository>().As<IContractDebtNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CertReportsRepository>().As<ICertReportsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AnnualAccountReportsRepository>().As<IAnnualAccountReportsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EuReimbursedAmountsRepository>().As<IEuReimbursedAmountsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ActuallyPaidAmountsRepository>().As<IActuallyPaidAmountsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CompensationDocumentsRepository>().As<ICompensationDocumentsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportIndicatorsRepository>().As<IContractReportIndicatorsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CorrectionDebtsRepository>().As<ICorrectionDebtsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CorrectionDebtVersionsRepository>().As<ICorrectionDebtVersionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RegOfferXmlsRepository>().As<IRegOfferXmlsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportAdvancePaymentAmountsRepository>().As<IContractReportAdvancePaymentAmountsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportCorrectionsRepository>().As<IContractReportCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PrognosesRepository>().As<IPrognosesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FIReimbursedAmountsRepository>().As<IFIReimbursedAmountsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportAdvanceNVPaymentAmountsRepository>().As<IContractReportAdvanceNVPaymentAmountsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NotificationEntriesRepository>().As<INotificationEntriesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NotificationSettingsRepository>().As<INotificationSettingsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NotificationEventsNomsRepository>().As<INotificationEventsNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserNotificationsRepository>().As<IUserNotificationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NotificationEventsPermissionsRepository>().As<INotificationEventsPermissionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CertReportSnapshotsRepository>().As<ICertReportSnapshotsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureMonitorstatDocumentsRepository>().As<IProcedureMonitorstatDocumentsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureMonitorstatEconomicActivitiesRepository>().As<IProcedureMonitorstatEconomicActivitiesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureMonitorstatRequestsRepository>().As<IProcedureMonitorstatRequestsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureMonitorstatRequestNomsRepository>().As<IProcedureMonitorstatRequestNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureMassCommunicationsRespository>().As<IProcedureMassCommunicationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureAppFormDeclarationsRepository>().As<IProcedureAppFormDeclarationsRepository>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<ContractReportCertAuthorityCorrectionsRepository>().As<IContractReportCertAuthorityCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportCertAuthorityFinancialCorrectionsRepository>().As<IContractReportCertAuthorityFinancialCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportCertAuthorityFinancialCorrectionCSDsRepository>().As<IContractReportCertAuthorityFinancialCorrectionCSDsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportCertCorrectionsRepository>().As<IContractReportCertCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCertCorrectionsRepository>().As<IContractReportFinancialCertCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCertCorrectionCSDsRepository>().As<IContractReportFinancialCertCorrectionCSDsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCertCorrectionProcedureNomsRepository>().As<IContractReportFinancialCertCorrectionProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportRevalidationsRepository>().As<IContractReportRevalidationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialRevalidationsRepository>().As<IContractReportFinancialRevalidationsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialRevalidationCSDsRepository>().As<IContractReportFinancialRevalidationCSDsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialRevalidationProcedureNomsRepository>().As<IContractReportFinancialRevalidationProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportTechnicalCorrectionsRepository>().As<IContractReportTechnicalCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportTechnicalCorrectionIndicatorsRepository>().As<IContractReportTechnicalCorrectionIndicatorsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportRevalidationCertAuthorityCorrectionsRepository>().As<IContractReportRevalidationCertAuthorityCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportRevalidationCertAuthorityFinancialCorrectionsRepository>().As<IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository>().As<IContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository>().InstancePerLifetimeScope();

            // SpotChecks Repositories
            moduleBuilder.RegisterType<SpotCheckPlansRepository>().As<ISpotCheckPlansRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SpotChecksRepository>().As<ISpotChecksRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SpotCheckPlanNomsRepository>().As<ISpotCheckPlanNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SpotCheckItemNomsRepository>().As<ISpotCheckItemNomsRepository>().InstancePerLifetimeScope();

            // Reimbursed Amounts Repositories
            moduleBuilder.RegisterType<DebtReimbursedAmountsRepository>().As<IDebtReimbursedAmountsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReimbursedAmountsRepository>().As<IContractReimbursedAmountsRepository>().InstancePerLifetimeScope();

            // Audits Repositories
            moduleBuilder.RegisterType<AuditsRepository>().As<IAuditsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AuditItemNomsRepository>().As<IAuditItemNomsRepository>().InstancePerLifetimeScope();

            // Financial Corrections Repositories
            moduleBuilder.RegisterType<FinancialCorrectionsRepository>().As<IFinancialCorrectionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FinancialCorrectionVersionsRepository>().As<IFinancialCorrectionVersionsRepository>().InstancePerLifetimeScope();

            // Irregularity Repositories
            moduleBuilder.RegisterType<IrregularitySignalsRepository>().As<IIrregularitySignalsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularitiesRepository>().As<IIrregularitiesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularityVersionsRepository>().As<IIrregularityVersionsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularitySignalNomsRepository>().As<IIrregularitySignalNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularityCategoryNomsRepository>().As<IEntityCodeNomsRepository<IrregularityCategory, EntityCodeNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularityTypeNomsRepository>().As<IIrregularityTypeNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularityFinancialStatusNomsRepository>().As<IEntityCodeNomsRepository<IrregularityFinancialStatus, EntityCodeNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularitySanctionCategoryNomsRepository>().As<IEntityCodeNomsRepository<IrregularitySanctionCategory, EntityCodeNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularitySanctionTypeNomsRepository>().As<IIrregularitySanctionTypeNomsRepository>().InstancePerLifetimeScope();

            // Monitorstat Repositories
            moduleBuilder.RegisterType<MonitorstatSurveysRepository>().As<IMonitorstatSurveysRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MonitorstatSurveyNomsRepository>().As<IMonitorstatSurveyNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MonitorstatMapNodesRepository>().As<IMonitorstatMapNodesRepository>().InstancePerLifetimeScope();

            // SapInterfaces Repositories
            moduleBuilder.RegisterType<SapFilesRepository>().As<ISapFilesRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SapSchemasRepository>().As<ISapSchemasRepository>().InstancePerLifetimeScope();

            // HistoricContractInterfaces Repositories
            moduleBuilder.RegisterType<HistoricContractRequestRepository>().As<IHistoricContractRequestRepository>().InstancePerLifetimeScope();

            // Monitoring Repositories
            moduleBuilder.RegisterType<MonitoringReportsRepository>().As<IMonitoringReportsRepository>().InstancePerLifetimeScope();

            // Declaration Repositories
            moduleBuilder.RegisterType<ProgrammeAppFormDeclarationsRepository>().As<IProgrammeAppFormDeclarationsRepository>().InstancePerLifetimeScope();

            // NonAggregates Repositories
            moduleBuilder.RegisterType<RegulationInvestmentPrioritiesRepository>().As<IRegulationInvestmentPrioritiesRepository>().InstancePerLifetimeScope();

            // Nom Repositories
            moduleBuilder.RegisterType<InstitutionNomsRepository>().As<IInstitutionNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<InstitutionTypeNomsRepository>().As<IEntityNomsRepository<InstitutionType, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IndicatorNomsRepository>().As<IIndicatorNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IndicatorItemTypeNomsRepository>().As<IEntityNomsRepository<IndicatorItemType, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MeasureNomsRepository>().As<IEntityNomsRepository<Measure, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<BudgetPeriodNomsRepository>().As<IBudgetPeriodNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RegulationInvestmentPriorityNomsRepository>().As<IRegulationInvestmentPriorityNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammeNomsRepository>().As<IProgrammeNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammePriorityNomsRepository>().As<IProgrammePriorityNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammePriorityCompanyNomsRepository>().As<IProgrammePriorityCompanyNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammeApplicationDocumentNomsRepository>().As<IProgrammeApplicationDocumentNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CountryNomsRepository>().As<IEntityCodeNomsRepository<Country, EntityCodeNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Nuts1NomsRepository>().As<INuts1NomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Nuts2NomsRepository>().As<INuts2NomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DistrictNomsRepository>().As<IDistrictNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MunicipalityNomsRepository>().As<IMunicipalityNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SettlementNomsRepository>().As<ISettlementNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProtectedZoneNomsRepository>().As<IProtectedZoneNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NutsLevelEnumNomsRepository>().As<IEnumNomsRepository<NutsLevel>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterGeneric(typeof(EnumNomsRepository<>)).As(typeof(IEnumNomsRepository<>)).InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ExpenseTypeNomsRepository>().As<IExpenseTypeNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CompanyLegalTypeNomsRepository>().As<ICompanyLegalTypeNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CompanyTypeNomsRepository>().As<IEntityGidNomsRepository<CompanyType, CompanyTypeGidNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CompanySizeTypeNomsRepository>().As<ICompanySizeTypeNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ErrandLegalActGidNomsRepository>().As<IEntityGidNomsRepository<ErrandLegalAct, EntityGidNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ErrandTypeNomsCodeRepository>().As<IEntityCodeNomsRepository<ErrandType, EntityCodeNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ErrandTypeNomsRepository>().As<IDependentEntityNomsRepository<ErrandType, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ErrandAreaNomsRepository>().As<IEntityCodeNomsRepository<ErrandArea, EntityCodeNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UinTypeEnumNomsRepository>().As<IEnumNomsRepository<UinType>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<KidCodeNomsRepository>().As<IEntityCodeNomsRepository<KidCode, EntityCodeNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PermissionTemplateNomsRepository>().As<IEntityNomsRepository<PermissionTemplate, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectTypeNomsRepository>().As<IProjectTypeNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureNomsRepository>().As<IProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CompanyNomsRepository>().As<ICompanyNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserTypeNomsRepository>().As<IUserTypeNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserOrganizationNomsRepository>().As<IUserOrganizationNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectProcedureNomsRepository>().As<IProjectProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractProcedureNomsRepository>().As<IContractProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MapNodeNomsRepository>().As<IEntityNomsRepository<MapNode, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<BasicInterestRateNomsRepository>().As<IEntityNomsRepository<BasicInterestRate, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AllowanceNomsRepository>().As<IEntityNomsRepository<Allowance, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionUserTypeNomsRepository>().As<IEvalSessionUserTypeNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionProjectNomsRepository>().As<IEvalSessionProjectNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionUserNomsRepository>().As<IEvalSessionUserNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionProcedureNomsRepository>().As<IEvalSessionProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionSheetXmlsRepository>().As<IEvalSessionSheetXmlsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureEvalTableTypeEnumNomsRepository>().As<IProcedureEvalTableTypeEnumNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionDistributionNomsRepository>().As<IEvalSessionDistributionNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionSheetStatusEnumNomsRepository>().As<IEvalSessionSheetStatusEnumNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionStandpointStatusEnumNomsRepository>().As<IEvalSessionStandpointStatusEnumNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectRecieveTypeEnumNomsRepository>().As<IEnumNomsRepository<ProjectRecieveType>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectRegistrationStatusEnumNomsRepository>().As<IEnumNomsRepository<ProjectRegistrationStatus>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractRegistrationNomsRepository>().As<IContractRegistrationNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammeCompanyNomsRepository>().As<IEntityNomsRepository<Company, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportProcedureNomsRepository>().As<IContractReportProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportCertAuthorityFinancialCorrectionProcedureNomsRepository>().As<IContractReportCertAuthorityFinancialCorrectionProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportRevalidationCertAuthorityFinancialCorrectionProcedureNomsRepository>().As<IContractReportRevalidationCertAuthorityFinancialCorrectionProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCorrectionProcedureNomsRepository>().As<IContractReportFinancialCorrectionProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FlatFinancialCorrectionContractNomsRepository>().As<IFlatFinancialCorrectionContractNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OtherViolationNomsRepository>().As<IOtherViolationNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FinancialCorrectionImposingReasonNomsRepository>().As<IEntityNomsRepository<FinancialCorrectionImposingReason, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularitySignalNomsRepository>().As<IIrregularitySignalNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularityNomsRepository>().As<IIrregularityNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FinancialCorrectionNomsRepository>().As<IFinancialCorrectionNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FlatFinancialCorrectionNomsRepository>().As<IFlatFinancialCorrectionNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportPaymentNomsRepository>().As<IContractReportPaymentNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<InterestSchemeNomsRepository>().As<IEntityNomsRepository<InterestScheme, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MessageRecipientsNomsRepository>().As<IMessageRecipientsNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectDossierDocumentTypeEnumNomsRepository>().As<IProjectDossierDocumentTypeEnumNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectDossierContractNomsRepository>().As<IProjectDossierContractNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionProjectStandingRejectionReasonNomsRepository>().As<IEntityNomsRepository<EvalSessionProjectStandingRejectionReason, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CertReportsNomsRepository>().As<IEntityNomsRepository<CertReport, EntityNomVO>>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ActionLogsRepository>().As<IActionLogsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ActionLogGroupNomsRepository>().As<IActionLogGroupNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ActionLogPortalGroupNomsRepository>().As<IActionLogPortalGroupNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ActionLogsAddRepository>().As<IActionLogsAddRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<BlobsRepository>().As<IBlobsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ArachneRepository>().As<IArachneRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportTechnicalCorrectionProcedureNomsRepository>().As<IContractReportTechnicalCorrectionProcedureNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportStatusWithoutDraftNomsRepository>().As<IContractReportStatusWithoutDraftNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractProcurementPlanNomsRepository>().As<IContractProcurementPlanNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportTypeNomsRepository>().As<IContractReportTypeNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureSpecFieldMaxLengthNomsRepository>().As<IProcedureSpecFieldMaxLengthNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammeAppFormDeclarationNomsRepository>().As<IProgrammeAppFormDeclarationNomsRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SebraRepository>().As<ISebraRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureApplicationDocNomsRepository>().As<IProcedureApplicationDocNomsRepository>().InstancePerLifetimeScope();

            // Suggestion Repositories
            moduleBuilder.RegisterType<ExpenseSubTypeSuggestionsRepository>().As<IExpenseSubTypeSuggestionsRepository>().InstancePerLifetimeScope();

            // Domain services
            moduleBuilder.RegisterType<ProcedureDomainService>().As<IProcedureDomainService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NomenclatureDomainService>().As<INomenclatureDomainService>().InstancePerLifetimeScope();

            // DbInterceptors
            moduleBuilder.RegisterType<LoggingEumisDbCommandInterceptor>().As<IEumisDbCommandInterceptor>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<WhereInFixupEumisDbCommandInterceptor>().As<IEumisDbCommandInterceptor>().InstancePerLifetimeScope();
        }
    }
}
