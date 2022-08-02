namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectDossier
{
    internal delegate IProjectDossierClaimsContext ProjectDossierClaimsContextFactory(int projectId);

    internal interface IProjectDossierClaimsContext
    {
        int ProjectId { get; }

        int ProgrammeId { get; }

        int ProcedureId { get; }
    }
}
