namespace Eumis.Authentication.Authorization.ClaimsContexts.Project
{
    internal delegate IProjectClaimsContext ProjectClaimsContextFactory(int projectId);

    internal interface IProjectClaimsContext
    {
        int ProjectId { get; }

        int ProgrammeId { get; }

        bool IsProjectInFinishedEvalSession();
    }
}
