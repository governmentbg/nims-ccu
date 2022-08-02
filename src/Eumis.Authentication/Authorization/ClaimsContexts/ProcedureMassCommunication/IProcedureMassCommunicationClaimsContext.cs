namespace Eumis.Authentication.Authorization.ClaimsContexts.ProcedureMassCommunication
{
    internal delegate IProcedureMassCommunicationClaimsContext ProcedureMassCommunicationClaimsContextFactory(int procedureMassCommunicationId);

    internal interface IProcedureMassCommunicationClaimsContext
    {
        int ProcedureId { get; }

        int ProgrammeId { get; }
    }
}
