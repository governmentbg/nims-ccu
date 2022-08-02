namespace Eumis.PortalIntegration.Api.Core
{
    public enum PortalIntegrationErrors
    {
        // registrations
        InvalidActivationCode,
        InvalidPasswordRecoveryCode,
        RegistrationEmailExists,
        RegistrationEmailDoesNotExist,
        WrongOldPassword,

        // drafts
        UpdateConcurrencyError,
        ObjectNotFound,

        // messages
        MessageCanceled,
        MessageTimedOut,

        // contract registrations
        AccessCodeEmailNotUnique,

        // contract versions
        ContractVersionInProgress,

        // contract procurement
        ExistsProcurementInProgress,

        // contract spendingPlan
        ExistsSpendingPlanInProgress,

        // contract reports
        ExistsDraftReport,

        // procedures
        ProcedureInactive,
    }
}
