using Autofac;
using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.EventHandlers;
using Eumis.ApplicationServices.NotificationEventHandlers;
using Eumis.ApplicationServices.Services.ActuallyPaidAmount;
using Eumis.ApplicationServices.Services.AnnualAccountReport;
using Eumis.ApplicationServices.Services.Audit;
using Eumis.ApplicationServices.Services.CertReport;
using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.Company;
using Eumis.ApplicationServices.Services.CompensationDocument;
using Eumis.ApplicationServices.Services.Contract;
using Eumis.ApplicationServices.Services.ContractCommunication;
using Eumis.ApplicationServices.Services.ContractDebt;
using Eumis.ApplicationServices.Services.ContractProcurement;
using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.ApplicationServices.Services.ContractReportAdvanceNVPaymentAmount;
using Eumis.ApplicationServices.Services.ContractReportAdvancePaymentAmount;
using Eumis.ApplicationServices.Services.ContractReportCertAuthorityCorrection;
using Eumis.ApplicationServices.Services.ContractReportCertAuthorityFinancialCorrectionService;
using Eumis.ApplicationServices.Services.ContractReportCertCorrection;
using Eumis.ApplicationServices.Services.ContractReportCorrection;
using Eumis.ApplicationServices.Services.ContractReportFinancialCertCorrection;
using Eumis.ApplicationServices.Services.ContractReportFinancialCorrection;
using Eumis.ApplicationServices.Services.ContractReportFinancialCSD;
using Eumis.ApplicationServices.Services.ContractReportFinancialRevalidation;
using Eumis.ApplicationServices.Services.ContractReportIndicator;
using Eumis.ApplicationServices.Services.ContractReportMicro;
using Eumis.ApplicationServices.Services.ContractReportRevalidation;
using Eumis.ApplicationServices.Services.ContractReportRevalidationCertAuthorityCorrection;
using Eumis.ApplicationServices.Services.ContractReportRevalidationCertAuthorityFinancialCorrection;
using Eumis.ApplicationServices.Services.ContractReportTechnicalCorrection;
using Eumis.ApplicationServices.Services.ContractReportTechnicalMember;
using Eumis.ApplicationServices.Services.ContractSpendingPlan;
using Eumis.ApplicationServices.Services.ContractVersionXml;
using Eumis.ApplicationServices.Services.CorrectionDebt;
using Eumis.ApplicationServices.Services.EuReimbursedAmount;
using Eumis.ApplicationServices.Services.EvalSession;
using Eumis.ApplicationServices.Services.EvalSession.Parsers;
using Eumis.ApplicationServices.Services.EvalSessionAutomaticProjectEvaluation;
using Eumis.ApplicationServices.Services.EvalSessionAutomaticProjectEvaluation.Parsers;
using Eumis.ApplicationServices.Services.EvalSessionReport;
using Eumis.ApplicationServices.Services.EvalSessionSheetXml;
using Eumis.ApplicationServices.Services.FinancialCorrection;
using Eumis.ApplicationServices.Services.FIReimbursedAmount;
using Eumis.ApplicationServices.Services.FlatFinancialCorrection;
using Eumis.ApplicationServices.Services.HistoricContract;
using Eumis.ApplicationServices.Services.Irregularity;
using Eumis.ApplicationServices.Services.Monitorstat;
using Eumis.ApplicationServices.Services.Notification;
using Eumis.ApplicationServices.Services.ProcedureEvalTableXml;
using Eumis.ApplicationServices.Services.ProcedureVersion;
using Eumis.ApplicationServices.Services.ProgrammeApplicationDocuments;
using Eumis.ApplicationServices.Services.ProgrammeApplicationDocuments.Parsers;
using Eumis.ApplicationServices.Services.ProgrammeDeclaration;
using Eumis.ApplicationServices.Services.ProgrammeDeclaration.Parsers;
using Eumis.ApplicationServices.Services.ProjectCommunication;
using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication;
using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication.Parsers;
using Eumis.ApplicationServices.Services.ProjectRegistration;
using Eumis.ApplicationServices.Services.ProjectVersionXml;
using Eumis.ApplicationServices.Services.Registrations;
using Eumis.ApplicationServices.Services.Regix;
using Eumis.ApplicationServices.Services.ReimbursedAmount;
using Eumis.ApplicationServices.Services.RequestPackage;
using Eumis.ApplicationServices.Services.SapInterfaces;
using Eumis.ApplicationServices.Services.Sebra;
using Eumis.ApplicationServices.Services.Sebra.Parsers;
using Eumis.ApplicationServices.Services.SpotCheck;
using Eumis.Domain.Core;
using Eumis.Print;

namespace Eumis.ApplicationServices
{
    public class ApplicationServicesModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            // Communicators
            moduleBuilder.RegisterType<DocumentRestApiCommunicator>().As<IDocumentRestApiCommunicator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<BlobServerCommunicator>().As<IBlobServerCommunicator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MonitorstatRestApiCommunicator>().As<IMonitorstatRestApiCommunicator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RegixRestApiCommunicator>().As<IRegixRestApiCommunicator>().InstancePerLifetimeScope();

            // Domain Event Handlers
            moduleBuilder.RegisterType<RegistrationCreatedEventHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractRegistrationCreatedEventHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RegistrationPasswordRecoveredEventHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractRegistrationPasswordRecoveredEventHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserActivatedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserUpdatedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserPasswordRecoveredEventHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectRegisteredHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureVersionChangedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureCanceledHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureEndedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureSetToDraftHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureTerminatedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectEvalStatusChangedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectVersionActivatedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionSheetCanceledHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionStatusChangedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionStandpointCanceledHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<QuestionSentHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AnswerReceivedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MessageSentHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractVersionActivatedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractVersionContractDateChangedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractProcurementXmlActivatedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractContractRegistrationActivatedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractContractRegistrationDeactivatedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractAuthorityCommunicationSentEventHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportReturnedDocumentHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionPublishedHandler>().As<IEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectMACommunicationQuestionSentHandler>().As<IEventHandler>().InstancePerLifetimeScope();

            // Notification events handlers
            moduleBuilder.RegisterType<NotificationProgrammeEventHandler>().As<INotificationEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NotificationProcedureEventHandler>().As<INotificationEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NotificationContractEventHandler>().As<INotificationEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NotificationIndependentEventHandler>().As<INotificationEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NotificationEvalSessionEventHandler>().As<INotificationEventHandler>().InstancePerLifetimeScope();

            // Services
            moduleBuilder.RegisterType<CompanyCreationService>().As<ICompanyCreationService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectRegistrationService>().As<IProjectRegistrationService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectCommunicationService>().As<IProjectCommunicationService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SebraService>().As<ISebraService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectManagingAuthorityCommunicationService>().As<IProjectManagingAuthorityCommunicationService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectVersionXmlService>().As<IProjectVersionXmlService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionAutomaticProjectEvaluationService>().As<IEvalSessionAutomaticProjectEvaluationService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionReportService>().As<IEvalSessionReportService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionSheetXmlService>().As<IEvalSessionSheetXmlService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionStandpointXmlService>().As<IEvalSessionStandpointXmlService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureEvalTableXmlService>().As<IProcedureEvalTableXmlService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractService>().As<IContractService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractVersionXmlService>().As<IContractVersionXmlService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractProcurementService>().As<IContractProcurementService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractCommunicationService>().As<IContractCommunicationService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProcedureVersionService>().As<IProcedureVersionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractSpendingPlanService>().As<IContractSpendingPlanService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportService>().As<IContractReportService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCSDService>().As<IContractReportFinancialCSDService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SpotCheckPlanService>().As<ISpotCheckPlanService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SpotCheckService>().As<ISpotCheckService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AuditService>().As<IAuditService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCorrectionService>().As<IContractReportFinancialCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FlatFinancialCorrectionService>().As<IFlatFinancialCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FinancialCorrectionService>().As<IFinancialCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularitySignalService>().As<IIrregularitySignalService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularityService>().As<IIrregularityService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<IrregularityVersionService>().As<IIrregularityVersionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractDebtService>().As<IContractDebtService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CertReportService>().As<ICertReportService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AnnualAccountReportService>().As<IAnnualAccountReportService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ActuallyPaidAmountService>().As<IActuallyPaidAmountService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ReimbursedAmountService>().As<IReimbursedAmountService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CompensationDocumentService>().As<ICompensationDocumentService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CertReportCheckService>().As<ICertReportCheckService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportIndicatorService>().As<IContractReportIndicatorService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EuReimbursedAmountService>().As<IEuReimbursedAmountService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CorrectionDebtService>().As<ICorrectionDebtService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportAdvancePaymentAmountService>().As<IContractReportAdvancePaymentAmountService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportCorrectionService>().As<IContractReportCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportRevalidationService>().As<IContractReportRevalidationService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialRevalidationService>().As<IContractReportFinancialRevalidationService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportCertAuthorityCorrectionService>().As<IContractReportCertAuthorityCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportCertAuthorityFinancialCorrectionService>().As<IContractReportCertAuthorityFinancialCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportRevalidationCertAuthorityCorrectionService>().As<IContractReportRevalidationCertAuthorityCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportRevalidationCertAuthorityFinancialCorrectionService>().As<IContractReportRevalidationCertAuthorityFinancialCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportCertCorrectionService>().As<IContractReportCertCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportFinancialCertCorrectionService>().As<IContractReportFinancialCertCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicroService>().As<IContractReportMicroService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SapFileService>().As<ISapFileService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FIReimbursedAmountService>().As<IFIReimbursedAmountService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportAdvanceNVPaymentAmountService>().As<IContractReportAdvanceNVPaymentAmountService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportTechnicalMemberService>().As<IContractReportTechnicalMemberService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicroType1Parser>().As<IContractReportMicroType1Parser>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicroType2Parser>().As<IContractReportMicroType2Parser>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicroType3Parser>().As<IContractReportMicroType3Parser>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportMicroType4Parser>().As<IContractReportMicroType4Parser>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammeApplicationDocumentParser>().As<IProgrammeApplicationDocumentParser>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammeApplicationDocumentService>().As<IProgrammeApplicationDocumentService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ContractReportTechnicalCorrectionService>().As<IContractReportTechnicalCorrectionService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<HistoricContractService>().As<IHistoricContractService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NotificationSettingService>().As<INotificationSettingService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RequestPackageService>().As<IRequestPackageService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RegixService>().As<IRegixService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MonitorstatService>().As<IMonitorstatService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RegOfferService>().As<IRegOfferService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammeDeclarationService>().As<IProgrammeDeclarationService>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionService>().As<IEvalSessionService>().InstancePerLifetimeScope();

            // Parsers
            moduleBuilder.RegisterType<EvalSessionsAutomaticProjectEvaluationParser>().As<IEvalSessionsAutomaticProjectEvaluationParser>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProgrammeDeclarationItemParser>().As<IProgrammeDeclarationItemParser>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SebraProjectParser>().As<ISebraProjectParser>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ProjectMassManagingAuthorityCommunicationRecipientParser>().As<IProjectMassManagingAuthorityCommunicationRecipientParser>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EvalSessionProjectParser>().As<IEvalSessionProjectParser>().InstancePerLifetimeScope();

            // External dependencies
            moduleBuilder.RegisterModule<PrintModule>();
        }
    }
}
