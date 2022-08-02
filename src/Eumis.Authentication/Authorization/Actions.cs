namespace Eumis.Authentication.Authorization
{
    public enum ProjectListActions
    {
        Search,
        ViewCreate,
    }

    public enum ProjectActions
    {
        View,
        Edit,
        Withdraw,
        SearchCommunication,
        CreateCommunication,
        SearchVersions,
        CreateVersion,
    }

    public enum ProjectCommunicationActions
    {
        View,
        Edit,
        Register,
        PrintRegistration,
        Apply,
        Reject,
        Cancel,
        Delete,
    }

    public enum ProjectManagingAuthorityCommunicationListActions
    {
        Search,
        Create,
    }

    public enum ProjectManagingAuthorityCommunicationActions
    {
        View,
        Edit,
        Delete,
        Cancel,
    }

    public enum ProjectMassManagingAuthorityCommunicationActions
    {
        View,
        Edit,
        Create,
    }

    public enum ProjectVersionActions
    {
        View,
        Edit,
        Delete,
    }

    public enum ContractListActions
    {
        Search,
        Create,
    }

    public enum ContractActions
    {
        View,
        ViewData,
        Edit,
        Delete,
        ChangeStatusToDraft,
        MarkAsChecked,
        CreateAuditAuthorityCommunication,
        SearchAuditAuthorityCommunications,
        CreateAdminAuthorityCommunication,
        SearchAdminAuthorityCommunications,
        CreateCertAuthorityCommunication,
        SearchCertAuthorityCommunications,
        MonitorCheckSheet,
    }

    public enum ContractVersionActions
    {
        View,
        Edit,
        Delete,
        ChangeStatusToDraft,
        MarkAsChecked,
    }

    public enum ContractProcurementActions
    {
        View,
        Edit,
        Delete,
        ChangeStatusToDraft,
        MarkAsChecked,
    }

    public enum ContractProcurementsOfferActions
    {
        View,
    }

    public enum ContractSpendingPlanActions
    {
        View,
        Edit,
        Delete,
        ChangeStatusToDraft,
        MarkAsChecked,
    }

    public enum ContractCommunicationListActions
    {
        Search,
    }

    public enum ContractCommunicationActions
    {
        View,
        Edit,
        Delete,
    }

    public enum ContractReportCheckListActions
    {
        Search,
    }

    public enum ContractReportCheckActions
    {
        View,
        Edit,
        EditFinancial,
        EditTechnical,
        MarkAsUnchecked,
        MarkAsAccepted,
        MarkAsRefused,
        MonitorCheckSheet,
    }

    public enum ContractReportFinancialCorrectionListActions
    {
        Search,
        Create,
    }

    public enum ContractReportFinancialCorrectionActions
    {
        View,
        Edit,
        Delete,
        MonitorCheckSheet,
    }

    public enum ContractReportTechnicalCorrectionListActions
    {
        Search,
        Create,
    }

    public enum ContractReportTechnicalCorrectionActions
    {
        View,
        Edit,
        Delete,
        ChangeStatusToDraft,
    }

    public enum ContractReportListActions
    {
        Search,
        Create,
    }

    public enum ContractReportActions
    {
        View,
        Edit,
        Delete,
        MarkAsChecked,
    }

    public enum ContractRegistrationListActions
    {
        Search,
    }

    public enum ContractRegistrationActions
    {
        View,
        Edit,
        Activate,
        Deactivate,
        CreateOrAttachRegistration,
    }

    public enum ProcedureListActions
    {
        Search,
    }

    public enum ProcedureActions
    {
        View,
        Edit,
        SetDraft,
        SetEntered,
        SetChecked,
        SetActive,
        SetEnded,
        SetTerminated,
        SetCanceled,
        CreateProject,
        MonitorCheckSheet,
    }

    public enum ProcedureMassCommunicationActions
    {
        View,
        Edit,
        Create,
    }

    public enum IndicativeAnnualWorkingProgrammeListActions
    {
        Search,
        Create,
    }

    public enum IndicativeAnnualWorkingProgrammeActions
    {
        View,
        Edit,
        SetPublished,
        SetАrchived,
        SetCanceled,
    }

    public enum RegistrationListActions
    {
        Search,
    }

    public enum RegistrationActions
    {
        View,
    }

    public enum ContractAccessCodeListActions
    {
        Search,
    }

    public enum ContractAccessCodeActions
    {
        View,
    }

    public enum MeasureListActions
    {
        Search,
        Create,
    }

    public enum MeasureActions
    {
        View,
        Edit,
        Delete,
    }

    public enum ProgrammeListActions
    {
        Create,
    }

    public enum ProgrammeActions
    {
        View,
        Edit,
        Delete,
        CreateProgrammePriority,
        CreateProcedure,
        MonitorCheckSheet,
    }

    public enum IndicatorListActions
    {
        Search,
        Create,
    }

    public enum IndicatorActions
    {
        View,
        Edit,
        Delete,
    }

    public enum MapNodeIndicatorListActions
    {
        Search,
        Create,
    }

    public enum MapNodeIndicatorActions
    {
        View,
        Edit,
    }

    public enum CheckBlankTopicListActions
    {
        Search,
        Create,
    }

    public enum CheckBlankTopicActions
    {
        View,
        Edit,
        Delete,
    }

    public enum ExpenseTypeListActions
    {
        Search,
        Create,
    }

    public enum ExpenseTypeActions
    {
        View,
        Edit,
        Delete,
    }

    public enum AllowanceListActions
    {
        Search,
        Create,
    }

    public enum AllowanceActions
    {
        View,
        Edit,
        Delete,
    }

    public enum BasicInterestRateListActions
    {
        Search,
        Create,
    }

    public enum BasicInterestRateActions
    {
        View,
        Edit,
        Delete,
    }

    public enum InterestSchemeListActions
    {
        Search,
        Create,
    }

    public enum InterestSchemeActions
    {
        View,
        Edit,
        Delete,
    }

    public enum DeclarationListActions
    {
        Search,
        Create,
    }

    public enum DeclarationActions
    {
        View,
        Edit,
    }

    public enum ProgrammeGroupListActions
    {
        Search,
        Create,
    }

    public enum ProgrammeGroupActions
    {
        View,
        Edit,
        Delete,
    }

    public enum CompanyListActions
    {
        Search,
        Create,
    }

    public enum CompanyActions
    {
        View,
        Edit,
        Delete,
    }

    public enum MonitorstatListActions
    {
        Import,
    }

    public enum NewsListActions
    {
        Search,
        Create,
    }

    public enum NewsActions
    {
        View,
        Edit,
    }

    public enum GuidanceListActions
    {
        Search,
        Create,
    }

    public enum GuidanceActions
    {
        View,
        Edit,
    }

    public enum PermissionTemplateListActions
    {
        Search,
        Create,
    }

    public enum PermissionTemplateActions
    {
        View,
        Edit,
    }

    public enum RequestPackageListActions
    {
        Search,
        Create,
        CreateDirect,
        CanControl,
    }

    public enum RequestPackageActions
    {
        View,
        Edit,
        SetDraft,
        SetEntered,
        SetChecked,
        SetEnded,
        SetCanceled,
        ChangeUserStatus,
    }

    public enum UserTypeListActions
    {
        Search,
        Create,
    }

    public enum UserTypeActions
    {
        View,
        Edit,
        Delete,
    }

    public enum UserListActions
    {
        Search,
        ViewCreate,
    }

    public enum UserActions
    {
        View,
        SetIsLocked,
        SetIsDeleted,
    }

    public enum UserOrganizationListActions
    {
        Search,
        Create,
    }

    public enum UserOrganizationActions
    {
        View,
        Edit,
        Delete,
        CreateUser,
        CreateRequestPackage,
    }

    public enum ActionLogActions
    {
        View,
    }

    public enum EvalSessionListActions
    {
        Search,
        Create,
    }

    public enum EvalSessionActions
    {
        ViewSession,
        EditSession,
        ViewSessionData,
        EditSessionData,
        SetDraft,
        SetActive,
        SetEnded,
        SetCanceled,
        SetEndedByLAG,
    }

    public enum MyEvalSession
    {
        ViewSession,
        ViewSessionSheets,
        ViewSessionStandpoints,
    }

    public enum MyEvalSessionSheetActions
    {
        Edit,
    }

    public enum MyEvalSessionStandpointActions
    {
        View,
        Edit,
    }

    public enum SpotCheckPlanListActions
    {
        Search,
        Create,
    }

    public enum SpotCheckPlanActions
    {
        View,
        Edit,
    }

    public enum SpotCheckListActions
    {
        Search,
        Create,
    }

    public enum SpotCheckActions
    {
        View,
        Edit,
        MonitorCheckSheet,
    }

    public enum AuditListActions
    {
        Search,
        Create,
    }

    public enum AuditActions
    {
        View,
        Edit,
    }

    public enum FlatFinancialCorrectionListActions
    {
        Search,
        Create,
    }

    public enum FlatFinancialCorrectionActions
    {
        View,
        Edit,
    }

    public enum FinancialCorrectionListActions
    {
        Search,
        Create,
    }

    public enum FinancialCorrectionActions
    {
        View,
        Edit,
    }

    public enum ActuallyPaidAmountListActions
    {
        Search,
        Create,
    }

    public enum ActuallyPaidAmountActions
    {
        View,
        Edit,
    }

    public enum CompensationDocumentListActions
    {
        Search,
        Create,
    }

    public enum CompensationDocumentActions
    {
        View,
        Edit,
    }

    public enum DebtReimbursedAmountListActions
    {
        Search,
        Create,
    }

    public enum DebtReimbursedAmountActions
    {
        View,
        Edit,
    }

    public enum ContractReimbursedAmountListActions
    {
        Search,
        Create,
    }

    public enum ContractReimbursedAmountActions
    {
        View,
        Edit,
    }

    public enum FIReimbursedAmountListActions
    {
        Search,
        Create,
    }

    public enum FIReimbursedAmountActions
    {
        View,
        Edit,
    }

    public enum IrregularitySignalListActions
    {
        Search,
        Create,
    }

    public enum IrregularitySignalActions
    {
        View,
        Edit,
        MonitorCheckSheet,
    }

    public enum IrregularityListActions
    {
        Search,
        Create,
    }

    public enum IrregularityActions
    {
        View,
        Edit,
    }

    public enum IrregularityVersionActions
    {
        View,
        Edit,
    }

    public enum ContractDebtListActions
    {
        Search,
        Create,
    }

    public enum ContractDebtActions
    {
        View,
        Edit,
    }

    public enum CorrectionDebtListActions
    {
        Search,
        Create,
    }

    public enum CorrectionDebtActions
    {
        View,
        Edit,
    }

    public enum CertReportListActions
    {
        Search,
        Create,
    }

    public enum CertReportActions
    {
        View,
        Edit,
        MonitorCheckSheet,
    }

    public enum CertReportCheckListActions
    {
        Search,
    }

    public enum CertReportCheckActions
    {
        View,
        Edit,
    }

    public enum AnnualAccountReportListActions
    {
        Search,
        Create,
    }

    public enum AnnualAccountReportActions
    {
        View,
        Edit,
        ViewCertificationDocument,
        ViewAuditDcument,
        EditCertificationDocument,
        EditAuditDcument,
    }

    public enum CertAuthorityCheckListActions
    {
        Search,
        Create,
    }

    public enum CertAuthorityCheckActions
    {
         View,
         Edit,
    }

    public enum EuReimbursedAmountListActions
    {
        Search,
        Create,
    }

    public enum EuReimbursedAmountActions
    {
         View,
         Edit,
    }

    public enum CertAuthorityCommunicationListActions
    {
        Search,
    }

    public enum CertAuthorityCommunicationActions
    {
        View,
        Edit,
        Delete,
    }

    public enum AuditAuthorityCommunicationListActions
    {
        Search,
    }

    public enum AuditAuthorityCommunicationActions
    {
        View,
        Edit,
        Delete,
    }

    public enum ContractReportCorrectionListActions
    {
        Search,
        Create,
    }

    public enum ContractReportCorrectionActions
    {
        View,
        Edit,
        MonitorCheckSheet,
    }

    public enum ContractReportRevalidationListActions
    {
        Search,
        Create,
    }

    public enum ContractReportRevalidationActions
    {
        View,
        Edit,
        MonitorCheckSheet,
    }

    public enum ContractReportCertCorrectionListActions
    {
        Search,
        Create,
    }

    public enum ContractReportCertCorrectionActions
    {
        View,
        Edit,
    }

    public enum ContractReportCertAuthorityCorrectionListActions
    {
        Search,
        Create,
    }

    public enum ContractReportCertAuthorityCorrectionActions
    {
        View,
        Edit,
    }

    public enum ContractReportRevalidationCertAuthorityCorrectionListActions
    {
        Search,
        Create,
    }

    public enum ContractReportRevalidationCertAuthorityCorrectionActions
    {
        View,
        Edit,
    }

    public enum ContractReportFinancialRevalidationListActions
    {
        Search,
        Create,
    }

    public enum ContractReportFinancialRevalidationActions
    {
        View,
        Edit,
        Delete,
        MonitorCheckSheet,
    }

    public enum ContractReportCertAuthorityFinancialCorrectionListActions
    {
        Search,
        Create,
    }

    public enum ContractReportCertAuthorityFinancialCorrectionActions
    {
        View,
        Edit,
        Delete,
    }

    public enum ContractReportRevalidationCertAuthorityFinancialCorrectionListActions
    {
        Search,
        Create,
    }

    public enum ContractReportRevalidationCertAuthorityFinancialCorrectionActions
    {
        View,
        Edit,
        Delete,
    }

    public enum ContractReportFinancialCertCorrectionListActions
    {
        Search,
        Create,
    }

    public enum ContractReportFinancialCertCorrectionActions
    {
        View,
        Edit,
        Delete,
    }

    public enum PrognosisListActions
    {
        Search,
        Create,
        ViewYearlyReport,
        ViewMonthlyReport,
        ViewProgrammePriorityReport,
        ViewProgrammeReport,
    }

    public enum ProgrammePrognosisActions
    {
        View,
        Edit,
    }

    public enum ProgrammePriorityPrognosisActions
    {
        View,
        Edit,
    }

    public enum ProcedurePrognosisActions
    {
        View,
        Edit,
    }

    public enum MonitoringActions
    {
        View,
    }

    public enum InterfacesActions
    {
        Export,
    }

    public enum SapFileListActions
    {
        Search,
        Create,
    }

    public enum SapFileActions
    {
        View,
        Edit,
        Import,
    }

    public enum ProjectDossierActions
    {
        View,
    }

    public enum RegixActions
    {
        View,
    }
}
