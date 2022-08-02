using System;
using System.Collections.Generic;
using Eumis.Authentication.Authorization.ClaimsContexts.ActuallyPaidAmount;
using Eumis.Authentication.Authorization.ClaimsContexts.AnnualAccountReport;
using Eumis.Authentication.Authorization.ClaimsContexts.Audit;
using Eumis.Authentication.Authorization.ClaimsContexts.AuditAuthorityCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.CertAuthorityCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.CertReport;
using Eumis.Authentication.Authorization.ClaimsContexts.CertReportCheck;
using Eumis.Authentication.Authorization.ClaimsContexts.CompensationDocument;
using Eumis.Authentication.Authorization.ClaimsContexts.Contract;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractDebt;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractOffer;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractProcurement;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReport;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertAuthorityCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertAuthorityFinancialCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCheck;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialCertCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialRevalidation;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidation;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidationCACorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidationCAFinancialCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractReportTechnicalCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractSpendingPlan;
using Eumis.Authentication.Authorization.ClaimsContexts.ContractVersion;
using Eumis.Authentication.Authorization.ClaimsContexts.CorrectionDebt;
using Eumis.Authentication.Authorization.ClaimsContexts.EuReimbursedAmount;
using Eumis.Authentication.Authorization.ClaimsContexts.EvalSession;
using Eumis.Authentication.Authorization.ClaimsContexts.EvalSessionSheet;
using Eumis.Authentication.Authorization.ClaimsContexts.EvalSessionStandpoint;
using Eumis.Authentication.Authorization.ClaimsContexts.FinancialCorrection;
using Eumis.Authentication.Authorization.ClaimsContexts.FIReimbursedAmount;
using Eumis.Authentication.Authorization.ClaimsContexts.Irregularity;
using Eumis.Authentication.Authorization.ClaimsContexts.IrregularitySignal;
using Eumis.Authentication.Authorization.ClaimsContexts.MapNodeIndicator;
using Eumis.Authentication.Authorization.ClaimsContexts.Procedure;
using Eumis.Authentication.Authorization.ClaimsContexts.ProcedureMassCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.Prognosis;
using Eumis.Authentication.Authorization.ClaimsContexts.Programme;
using Eumis.Authentication.Authorization.ClaimsContexts.Project;
using Eumis.Authentication.Authorization.ClaimsContexts.ProjectCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.ProjectDossier;
using Eumis.Authentication.Authorization.ClaimsContexts.ProjectManagingAuthorityCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.ProjectMassManagingAuthorityCommunication;
using Eumis.Authentication.Authorization.ClaimsContexts.ProjectVersion;
using Eumis.Authentication.Authorization.ClaimsContexts.ReimbursedAmount;
using Eumis.Authentication.Authorization.ClaimsContexts.RequestPackage;
using Eumis.Authentication.Authorization.ClaimsContexts.SpotCheck;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Authentication.Authorization.ClaimsContexts.UserOrganization;
using Eumis.Common.Auth;

namespace Eumis.Authentication.Authorization
{
    internal class Authorizer : IAuthorizer
    {
        private IAccessContext accessContext;
        private IUserClaimsContextInternal currentUserClaimsContext;
        private Dictionary<Type, object> factories = new Dictionary<Type, object>();

        public Authorizer(
            IAccessContext accessContext,
            UserClaimsContextInternalFactory userClaimsContextFactory,
            RequestPackageClaimsContextFactory requestPackageClaimsContextFactory,
            UserOrganizationClaimsContextFactory userOrganizationClaimsContextFactory,
            ProgrammeClaimsContextFactory programmeClaimsContextFactory,
            ProcedureClaimsContextFactory procedureClaimsContextFactory,
            ProjectClaimsContextFactory projectClaimsContextFactory,
            ProjectCommunicationClaimsContextFactory projectCommunicationClaimsContextFactory,
            ProjectVersionClaimsContextFactory projectVersionClaimsContextFactory,
            EvalSessionClaimsContextFactory evalSessionClaimsContextFactory,
            EvalSessionSheetClaimsContextFactory evalSessionSheetClaimsContextFactory,
            EvalSessionStandpointClaimsContextFactory evalSessionStandpointClaimsContextFactory,
            ContractClaimsContextFactory contractClaimsContextFactory,
            ContractVersionClaimsContextFactory contractVersionClaimsContextFactory,
            ContractProcurementClaimsContextFactory contractProcurementClaimsContextFactory,
            ContractProcurementsOfferClaimsContextFactory contractProcurementsOfferClaimsContextFactory,
            ContractCommunicationClaimsContextFactory contractCommunicationClaimsContextFactory,
            ContractSpendingPlanClaimsContextFactory contractSpendingPlanClaimsContextFactory,
            ContractReportClaimsContextFactory contractReportClaimsContextFactory,
            ContractReportCheckClaimsContextFactory contractReportCheckClaimsContextFactory,
            ContractReportFinancialCorrectionClaimsContextFactory contractReportFinancialCorrectionClaimsContextFactory,
            ContractReportTechnicalCorrectionClaimsContextFactory contractReportTechnicalCorrectionClaimsContextFactory,
            SpotCheckPlanClaimsContextFactory spotCheckPlanClaimsContextFactory,
            SpotCheckClaimsContextFactory spotCheckClaimsContextFactory,
            AuditClaimsContextFactory auditClaimsContextFactory,
            FlatFinancialCorrectionClaimsContextFactory flatFinancialCorrectionClaimsContextFactory,
            FinancialCorrectionClaimsContextFactory financialCorrectionClaimsContextFactory,
            ContractDebtClaimsContextFactory contractDebtClaimsContextFactory,
            ActuallyPaidAmountClaimsContextFactory actuallyPaidAmountClaimsContextFactory,
            DebtReimbursedAmountClaimsContextFactory debtReimbursedAmountClaimsContextFactory,
            ContractReimbursedAmountClaimsContextFactory contractReimbursedAmountClaimsContextFactory,
            CompensationDocumentClaimsContextFactory compensationDocumentClaimsContextFactory,
            CorrectionDebtClaimsContextFactory correctionDebtClaimsContextFactory,
            IrregularitySignalClaimsContextFactory irregularitySignalClaimsContextFactory,
            IrregularityClaimsContextFactory irregularityClaimsContextFactory,
            IrregularityVersionClaimsContextFactory irregularityVersionClaimsContextFactory,
            AnnualAccountReportClaimsContextFactory annualAccountReportClaimsContextFactory,
            CertReportClaimsContextFactory certReportClaimsContextFactory,
            CertReportCheckClaimsContextFactory certReportCheckClaimsContextFactory,
            EuReimbursedAmountClaimsContextFactory euReimbursedAmountClaimsContextFactory,
            CertAuthorityCommunicationClaimsContextFactory certAuthorityCommunicationClaimsContextFactory,
            AuditAuthorityCommunicationClaimsContextFactory auditAuthorityCommunicationClaimsContextFactory,
            ContractReportCorrectionClaimsContextFactory contractReportCorrectionClaimsContextFactory,
            ContractReportRevalidationClaimsContextFactory contractReportRevalidationClaimsContextFactory,
            ContractReportFinancialRevalidationClaimsContextFactory contractReportFinancialRevalidationClaimsContextFactory,
            ContractReportCertCorrectionClaimsContextFactory contractReportCertCorrectionClaimsContextFactory,
            ContractReportFinancialCertCorrectionClaimsContextFactory contractReportFinancialCertCorrectionClaimsContextFactory,
            ContractReportCertAuthorityCorrectionClaimsContextFactory contractReportCertAuthorityCorrectionClaimsContextFactory,
            ContractReportCertAuthorityFinancialCorrectionClaimsContextFactory contractReportCertAuthorityFinancialCorrectionClaimsContextFactory,
            ContractReportRevalidationCACorrectionClaimsContextFactory contractReportRevalidationCertAuthorityCorrectionClaimsContextFactory,
            ContractReportRevalidationCAFinancialCorrectionClaimsContextFactory contractReportRevalidationCertAuthorityFinancialCorrectionClaimsContextFactory,
            ProjectDossierClaimsContextFactory projectDossierClaimsContextFactory,
            ProgrammePrognosisClaimsContextFactory programmePrognosisClaimsContextFactory,
            ProgrammePriorityPrognosisClaimsContextFactory programmePriorityPrognosisClaimsContextFactory,
            ProcedurePrognosisClaimsContextFactory procedurePrognosisClaimsContextFactory,
            FIReimbursedAmountClaimsContextFactory fiReimbursedAmountClaimsContextFactory,
            MapNodeIndicatorClaimsContextFactory mapNodeIndicatorClaimsContextFactory,
            ProcedureMassCommunicationClaimsContextFactory procedureMassCommunicationClaimsContextFactory,
            ProjectManagingAuthorityCommunicationClaimsContextFactory projectManagingAuthorityCommunicationClaimsContextFactory,
            ProjectMassManagingAuthorityCommunicationClaimsContextFactory projectMassManagingAuthorityCommunicationClaimsContextFactory)
        {
            this.accessContext = accessContext;
            if (this.accessContext.IsUser)
            {
                this.currentUserClaimsContext = (IUserClaimsContextInternal)userClaimsContextFactory(this.accessContext.UserId);
            }

            this.AddFactory(userClaimsContextFactory.Invoke);
            this.AddFactory(requestPackageClaimsContextFactory.Invoke);
            this.AddFactory(userOrganizationClaimsContextFactory.Invoke);
            this.AddFactory(programmeClaimsContextFactory.Invoke);
            this.AddFactory(procedureClaimsContextFactory.Invoke);
            this.AddFactory(projectClaimsContextFactory.Invoke);
            this.AddFactory(projectCommunicationClaimsContextFactory.Invoke);
            this.AddFactory(projectVersionClaimsContextFactory.Invoke);
            this.AddFactory(evalSessionClaimsContextFactory.Invoke);
            this.AddFactory(evalSessionSheetClaimsContextFactory.Invoke);
            this.AddFactory(evalSessionStandpointClaimsContextFactory.Invoke);
            this.AddFactory(contractClaimsContextFactory.Invoke);
            this.AddFactory(contractVersionClaimsContextFactory.Invoke);
            this.AddFactory(contractProcurementClaimsContextFactory.Invoke);
            this.AddFactory(contractProcurementsOfferClaimsContextFactory.Invoke);
            this.AddFactory(contractCommunicationClaimsContextFactory.Invoke);
            this.AddFactory(contractSpendingPlanClaimsContextFactory.Invoke);
            this.AddFactory(contractReportClaimsContextFactory.Invoke);
            this.AddFactory(contractReportCheckClaimsContextFactory.Invoke);
            this.AddFactory(contractReportFinancialCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(contractReportTechnicalCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(spotCheckPlanClaimsContextFactory.Invoke);
            this.AddFactory(spotCheckClaimsContextFactory.Invoke);
            this.AddFactory(auditClaimsContextFactory.Invoke);
            this.AddFactory(flatFinancialCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(financialCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(contractDebtClaimsContextFactory.Invoke);
            this.AddFactory(actuallyPaidAmountClaimsContextFactory.Invoke);
            this.AddFactory(debtReimbursedAmountClaimsContextFactory.Invoke);
            this.AddFactory(contractReimbursedAmountClaimsContextFactory.Invoke);
            this.AddFactory(compensationDocumentClaimsContextFactory.Invoke);
            this.AddFactory(correctionDebtClaimsContextFactory.Invoke);
            this.AddFactory(irregularitySignalClaimsContextFactory.Invoke);
            this.AddFactory(irregularityClaimsContextFactory.Invoke);
            this.AddFactory(irregularityVersionClaimsContextFactory.Invoke);
            this.AddFactory(annualAccountReportClaimsContextFactory.Invoke);
            this.AddFactory(certReportClaimsContextFactory.Invoke);
            this.AddFactory(certReportCheckClaimsContextFactory.Invoke);
            this.AddFactory(euReimbursedAmountClaimsContextFactory.Invoke);
            this.AddFactory(certAuthorityCommunicationClaimsContextFactory.Invoke);
            this.AddFactory(auditAuthorityCommunicationClaimsContextFactory.Invoke);
            this.AddFactory(contractReportCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(contractReportRevalidationClaimsContextFactory.Invoke);
            this.AddFactory(contractReportFinancialRevalidationClaimsContextFactory.Invoke);
            this.AddFactory(contractReportCertCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(contractReportFinancialCertCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(contractReportCertAuthorityCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(contractReportCertAuthorityFinancialCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(contractReportRevalidationCertAuthorityCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(contractReportRevalidationCertAuthorityFinancialCorrectionClaimsContextFactory.Invoke);
            this.AddFactory(projectDossierClaimsContextFactory.Invoke);
            this.AddFactory(programmePrognosisClaimsContextFactory.Invoke);
            this.AddFactory(programmePriorityPrognosisClaimsContextFactory.Invoke);
            this.AddFactory(procedurePrognosisClaimsContextFactory.Invoke);
            this.AddFactory(fiReimbursedAmountClaimsContextFactory.Invoke);
            this.AddFactory(mapNodeIndicatorClaimsContextFactory.Invoke);
            this.AddFactory(procedureMassCommunicationClaimsContextFactory.Invoke);
            this.AddFactory(projectManagingAuthorityCommunicationClaimsContextFactory.Invoke);
            this.AddFactory(projectMassManagingAuthorityCommunicationClaimsContextFactory.Invoke);
        }

        private void AddFactory<TClaimsContext>(Func<int, TClaimsContext> factory)
        {
            this.factories.Add(typeof(TClaimsContext), factory);
        }

        public bool CanDo(Enum action)
        {
            if (!this.accessContext.IsUser)
            {
                return false;
            }

            Func<IUserClaimsContextInternal, bool> check;
            if (ActionConfiguration.ActionsMap.ContainsKey(action))
            {
                check = ActionConfiguration.ActionsMap[action];
            }
            else
            {
                // reject unknown/unmapped actions
                check = (cu) => false;
            }

            return check(this.currentUserClaimsContext);
        }

        public bool CanDo(Enum action, int id)
        {
            if (!this.accessContext.IsUser)
            {
                return false;
            }

            Func<Dictionary<Type, object>, IUserClaimsContextInternal, int, bool> check;
            if (ActionConfiguration.ObjectActionsMap.ContainsKey(action))
            {
                check = ActionConfiguration.ObjectActionsMap[action];
            }
            else
            {
                // reject unknown/unmapped actions
                check = (factories, cu, oid) => false;
            }

            return check(this.factories, this.currentUserClaimsContext, id);
        }

        public bool CanDo(string action, int? id = null)
        {
            Enum actionEnum = ActionConfiguration.StringToAction[action];

            if (id == null || id == 0)
            {
                return this.CanDo(actionEnum);
            }
            else
            {
                return this.CanDo(actionEnum, id.Value);
            }
        }
    }
}
