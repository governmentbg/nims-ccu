namespace Eumis.Authentication.Authorization.ClaimsContexts.Procedure
{
    internal delegate IProcedureClaimsContext ProcedureClaimsContextFactory(int procedureId);

    internal interface IProcedureClaimsContext
    {
        int ProgrammeId { get; }
    }
}
