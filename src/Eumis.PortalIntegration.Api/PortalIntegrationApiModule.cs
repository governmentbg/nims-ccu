using Autofac;

namespace Eumis.PortalIntegration.Api
{
    public class PortalIntegrationApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            // Portal
            moduleBuilder.RegisterType<Portal.Procedures.Controllers.ProceduresController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Projects.Controllers.ProjectsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Companies.Controllers.CompaniesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Commuincators.Controllers.RegixController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Registrations.Controllers.RegistrationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Registrations.Controllers.RegProjectXmlsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Registrations.Controllers.RegProjectMessagesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Registrations.Controllers.RegProjectCommunicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Registrations.Controllers.RegProjectCommunicationAnswersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Emails.Controllers.EmailsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Guidances.Controllers.GuidancesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractRegistrations.Controllers.ContractRegistrationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractRegistrations.Controllers.ContractAccessCodesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractRegistrations.Controllers.ContractAccessCodes1Controller>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractRegistrations.Controllers.ContractsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractVersions.Controllers.ContractVersionXmlsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractProcurements.Controllers.ContractProcurementXmlsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractCommunications.Controllers.ContractCommunications1Controller>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractSpendingPlans.Controllers.ContractSpendingPlanXmlsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractReports.Controllers.ContractReportsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractReports.Controllers.ContractReportFinancials1Controller>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractReports.Controllers.ContractReportPayments1Controller>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractReports.Controllers.ContractReportTechnicals1Controller>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractDifferentiatedPositions.Controllers.ContractDifferentiatedPositionsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.Registrations.Controllers.RegOfferXmlsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractOffers.Controllers.ContractRegOffers1Controller>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.ContractReports.Controllers.ContractReportMicros1Controller>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Portal.News.Controllers.NewsController>().InstancePerLifetimeScope();

            // Documents
            moduleBuilder.RegisterType<Documents.ProjectVersions.Controllers.ProjectVersionsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ProcedureEvalTables.Controllers.ProcedureEvalTablesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.EvalSessionSheets.Controllers.EvalSessionSheetsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.EvalSessionStandpoints.Controllers.EvalSessionStandpointsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ProjectCommunications.Controllers.ProjectCommunicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ProjectManagingAuthorityCommunications.Controllers.ProjectManagingAuthorityCommunicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ProjectManagingAuthorityCommunications.Controllers.ProjectManagingAuthorityCommunicationAnswersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ContractVersions.Controllers.ContractVersionsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ContractProcurements.Controllers.ContractProcurementsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ContractCommunications.Controllers.ContractCommunicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ContractSpendingPlans.Controllers.ContractSpendingPlansController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ContractReports.Controllers.ContractReportFinancialsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ContractReports.Controllers.ContractReportPaymentsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ContractReports.Controllers.ContractReportTechnicalsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ContractReports.Controllers.ContractReportMicrosController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Documents.ContractOffers.Controllers.ContractRegOffersController>().InstancePerLifetimeScope();
        }
    }
}
