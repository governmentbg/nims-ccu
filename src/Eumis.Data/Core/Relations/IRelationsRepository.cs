namespace Eumis.Data.Core.Relations
{
    public interface IRelationsRepository
    {
        bool ProjectHasContract(int projectId, int contractId);

        void AssertProjectHasContract(int projectId, int contractId);

        bool ContractHasSpendingPlan(int contractId, int spendingPlanId);

        void AssertContractHasSpendingPlan(int contractId, int spendingPlanId);

        bool ContractHasAccessCode(int contractId, int accessCodeId);

        void AssertContractHasAccessCode(int contractId, int accessCodeId);

        bool ContractHasProcurement(int contractId, int procurementId);

        void AssertContractHasProcurement(int contractId, int procurementId);

        bool ProjectHasProjectCommunication(int projectId, int communicationId);

        void AssertProjectHasProjectCommunication(int projectId, int communicationId);

        bool ProjectHasManagingAuthorityCommunication(int projectId, int communicationId);

        void AssertProjectHasManagingAuthorityCommunication(int projectId, int communicationId);

        bool ProjectCommunicationHasProjectCommunicationFile(int communicationId, int projectCommunicationFileId);

        void AssertProjectCommunicationHasProjectCommunicationFile(int communicationId, int projectCommunicationFileId);

        bool ProjectHasProjectFile(int projectId, int projectFileId);

        void AssertProjectHasProjectFile(int projectId, int projectFileId);

        bool ContractHasCommunication(int contractId, int communicationId);

        void AssertContractHasCommunication(int contractId, int comunicationId);

        bool CertReportHasContractReportCertCorrection(int certReportId, int contractReportCertCorrectionId);

        void AssertCertReportHasContractReportCertCorrection(int certReportId, int contractReportCertCorrectionId);

        bool CertReportHasContractReportCorrection(int certReportId, int contractReportCorrectionId);

        void AssertCertReportHasContractReportCorrection(int certReportId, int contractReportCorrectionId);

        bool ContractHasContractReportFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId);

        void AssertContractHasContractReportFinancialCorrection(int contractReportId, int contractReportFinancialCorrectionId);

        bool ProcedureHasContractReportDocument(int procedureId, int contractReportDocumentId);

        void AssertProcedureHasContractReportDocument(int procedureId, int contractReportDocumentId);

        bool ContractReportHasContractReportFinancialRevalidation(int contractReportId, int contractReportFinancialRevalidationId);

        void AssertContractReportHasContractReportFinancialRevalidation(int contractReportId, int contractReportFinancialRevalidationId);

        bool ContractReportHasContractReportPaymentCheck(int contractReportId, int contractReportPaymentCheckId);

        void AssertContractReportHasContractReportPaymentCheck(int contractReportId, int contractReportPaymentCheckId);

        bool CertReportHasAdvancePaymentAmount(int certReportId, int? contractReportId = null, int? contractReportAdvancePaymentAmountId = null);

        void AssertCertReportHasAdvancePaymentAmount(int certReportId, int? contractReportId = null, int? contractReportAdvancePaymentAmountId = null);

        bool CertReportHasFinancialCorrectionCSD(int certReportId, int? contractReportFinancialCorrectionId = null, int? contractReportFinancialCorrectionCSDId = null);

        void AssertCertReportHasFinancialCorrectionCSD(int certReportId, int? contractReportFinancialCorrectionId = null, int? contractReportFinancialCorrectionCSDId = null);

        bool CertReportHasFinancialCertCorrectionCSD(int certReportId, int? contractReportFinancialCertCorrectionId = null, int? contractReportFinancialCertCorrectionCSDId = null);

        void AssertCertReportHasFinancialCertCorrectionCSD(int certReportId, int? contractReportFinancialCertCorrectionId = null, int? contractReportFinancialCertCorrectionCSDId = null);

        bool CertReportHasFinancialCSDBudgetItem(int certReportId, int? contractReportId = null, int? contractReportFinancialCSDBudgetItemId = null);

        void AssertCertReportHasFinancialCSDBudgetItem(int certReportId, int? contractReportId = null, int? contractReportFinancialCSDBudgetItemId = null);

        bool CertReportHasContractReportRevalidation(int certReportId, int contractReportRevalidationId);

        void AssertCertReportHasContractReportRevalidation(int certReportId, int contractReportRevalidationId);

        bool CertReportHasFinancialRevalidationCSD(int certReportId, int? contractReportFinancialRevalidationId = null, int? contractReportFinancialRevalidationCSDId = null);

        void AssertCertReportHasFinancialRevalidationCSD(int certReportId, int? contractReportFinancialRevalidationId = null, int? contractReportFinancialRevalidationCSDId = null);

        bool AnnualAccountReportHasContractReportCorrection(int annualAccountReportId, int contractReportCorrectionId);

        void AssertAnnualAccountReportHasContractReportCorrection(int annualAccountReportId, int contractReportCorrectionId);

        bool AnnualAccountReportHasContractReportFinancialCorrection(int annualAccountReportId, int contractReportFinancialCorrectionId);

        void AssertAnnualAccountReportHasContractReportFinancialCorrection(int annualAccountReportId, int contractReportFinancialCorrectionId);

        bool AnnualAccountReportHasContractReportFinancialCorrectionCSD(int annualAccountReportId, int? contractReportFinancialCorrectionId = null, int? contractReportFinancialCorrectionCSDId = null);

        void AssertAnnualAccountReportHasContractReportFinancialCorrectionCSD(int annualAccountReportId, int? contractReportFinancialCorrectionId = null, int? contractReportFinancialCorrectionCSDId = null);

        bool AnnualAccountReportHasCertifiedFinancialCorrectionCSD(int annualAccountReportId, int? contractReportCertAuthorityFinancialCorrectionId = null, int? contractReportCertAuthorityFinancialCorrectionCSDId = null);

        void AssertAnnualAccountReportHasCertifiedFinancialCorrectionCSD(int annualAccountReportId, int? contractReportCertAuthorityFinancialCorrectionId = null, int? contractReportCertAuthorityFinancialCorrectionCSDId = null);

        bool AnnualAccountReportHasCertifiedCorrection(int annualAccountReportId, int contractReportCertifiedCorrectionId);

        void AssertAnnualAccountReportHasCertifiedCorrection(int annualAccountReportId, int contractReportCertifiedCorrectionId);

        bool AnnualAccountReportHasCertifiedFinancialCorrection(int annualAccountReportId, int certifiedFinancialCorrectionId);

        void AssertAnnualAccountReportHasCertifiedFinancialCorrection(int annualAccountReportId, int certifiedFinancialCorrectionId);

        bool AnnualAccountReportHasCertifiedRevalidationCorrection(int annualAccountReportId, int contractReportCertifiedRevalidationCorrectionId);

        void AssertAnnualAccountReportHasCertifiedRevalidationCorrection(int annualAccountReportId, int contractReportCertifiedRevalidationCorrectionId);

        bool AnnualAccountReportHasAttachedCertReport(int annualAccountReportId, int attachedCertReportId);

        void AssertAnnualAccountReportHasAttachedCertReport(int annualAccountReportId, int attachedCertReportId);

        bool AnnualAccountReportHasAuditDocument(int annualAccountReportId, int documentId);

        void AssertAnnualAccountReportHasAuditDocument(int annualAccountReportId, int documentId);

        bool AnnualAccountReportHasCertificationDocument(int annualAccountReportId, int documentId);

        void AssertAnnualAccountReportHasCertificationDocument(int annualAccountReportId, int documentId);

        bool AnnualAccountReportHasAppendix(int annualAccountReportId, int appendixId);

        void AssertAnnualAccountReportHasAppendix(int annualAccountReportId, int appendixId);

        bool ContractReportFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId);

        void AssertContractReportFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId);

        bool ContractReportFinancialRevalidationHasFinancialCSDBudgetItem(int contractReportFinancialRevalidationId, int contractReportFinancialCSDBudgetItemId);

        void AssertContractReportFinancialRevalidationHasFinancialCSDBudgetItem(int contractReportFinancialRevalidationId, int contractReportFinancialCSDBudgetItemId);

        bool ContractReportFinancialCertCorrectionHasFinancialCSDBudgetItem(int contractReportFinancialCertCorrectionId, int contractReportFinancialCSDBudgetItemId);

        void AssertContractReportFinancialCertCorrectionHasFinancialCSDBudgetItem(int contractReportFinancialCertCorrectionId, int contractReportFinancialCSDBudgetItemId);

        bool ContractReportCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId);

        void AssertContractReportCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId);

        bool ContractReportRevalidationCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId);

        void AssertContractReportRevalidationCertAuthorityFinancialCorrectionHasFinancialCSDBudgetItem(int contractReportRevalidationCertAuthorityFinancialCorrectionId, int contractReportFinancialCSDBudgetItemId);

        bool ContractReportTechnicalCorrectionHasContractReportIndicator(int contractReportTechnicalCorrectionId, int contractReportIndicatorId);

        void AssertContractReportTechnicalCorrectionHasContractReportIndicator(int contractReportTechnicalCorrectionId, int contractReportIndicatorId);

        bool ContractHasVersion(int contractId, int versionId);

        void AssertContractHasVersion(int contractId, int versionId);

        bool ЕvalSessionHasЕvaluation(int evalSessionId, int evaluationId);

        void AssertЕvalSessionHasЕvaluation(int evalSessionId, int evaluationId);

        bool ЕvalSessionHasStanding(int evalSessionId, int standingId);

        void AssertЕvalSessionHasStanding(int evalSessionId, int standingId);

        bool EvalSessionHasResult(int evalSessionId, int resultId);

        void AssertEvalSessionHasResult(int evalSessionId, int resultId);

        bool ProgrammeHasProcedureManual(int programmeId, int programmeProcedureManualId);

        void AssertProgrammeHasProcedureManual(int programmeId, int programmeProcedureManualId);

        bool AnnualAccountReportHasCertifiedRevalidationFinancialCorrectionCSD(int annualAccountReportId, int? contractReportRevalidationCertAuthorityFinancialCorrectionId = null, int? contractReportRevalidationCertAuthorityFinancialCorrectionCSDId = null);

        void AssertAnnualAccountReportHasCertifiedRevalidationFinancialCorrectionCSD(int annualAccountReportId, int? contractReportRevalidationCertAuthorityFinancialCorrectionId = null, int? contractReportRevalidationCertAuthorityFinancialCorrectionCSDId = null);

        void AssertProcedureHasDeclaration(int procedureId, int declarationId);

        bool ProcedureHasDeclaration(int procedureId, int declarationId);
    }
}
