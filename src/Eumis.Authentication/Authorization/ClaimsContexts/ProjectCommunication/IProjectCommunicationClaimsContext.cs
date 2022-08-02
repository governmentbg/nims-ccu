namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectCommunication
{
    internal delegate IProjectCommunicationClaimsContext ProjectCommunicationClaimsContextFactory(int communicationId);

    internal interface IProjectCommunicationClaimsContext
    {
        int ProjectCommunicationId { get; }

        int ProjectId { get; }

        int ProgrammeId { get; }

        bool IsProjectInFinishedEvalSession();
    }
}
