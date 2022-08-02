namespace Eumis.Authentication.Authorization.ClaimsContexts.User
{
    internal delegate IUserClaimsContextInternal UserClaimsContextInternalFactory(int userId);

    internal interface IUserClaimsContextInternal : IUserClaimsContext
    {
        bool HasProgrammePermission<TEnum>(int programmeId, TEnum permission);

        bool HasAllProgrammePermissions<TEnum>(int programmeId, params TEnum[] permissions);

        bool HasProgrammePermissionForAnyProgramme<TEnum>(TEnum permission);

        bool HasCommonPermission<TEnum>(TEnum permission);

        bool HasAnyCommonPermission<TEnum>(params TEnum[] permissions);

        bool IsEvalSessionAdmin(int evalSessionId);

        bool IsEvalSessionAssessor(int evalSessionId);

        bool IsEvalSessionAssistantAssessor(int evalSessionId);

        bool IsEvalSessionObserver(int evalSessionId);

        bool IsEvalSessionProjectAdmin(int projectId);

        bool IsEvalSessionProjectAssessor(int projectId);

        bool IsEvalSessionProjectAssistantAssessor(int projectId);

        bool IsEvalSessionProjectObserver(int projectId);

        bool IsAssessorAssociatedWithEvalSessionSheet(int evalSessionSheetId);

        bool IsUserAssociatedWithEvalSessionStandpoint(int evalSessionStandpointId);

        bool IsContractExternalUser(int? contractId);

        bool HasAnyContractExternalUserPermission();
    }
}
