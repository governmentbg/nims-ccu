namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectVersion
{
    internal delegate IProjectVersionClaimsContext ProjectVersionClaimsContextFactory(int versionId);

    internal interface IProjectVersionClaimsContext
    {
        int ProjectVersionId { get; }

        int ProjectId { get; }

        int ProgrammeId { get; }

        bool IsProjectInFinishedEvalSession();
    }
}
