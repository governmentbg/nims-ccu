namespace Eumis.Authentication.Authorization.ClaimsContexts.Prognosis
{
    internal delegate IProcedurePrognosisClaimsContext ProcedurePrognosisClaimsContextFactory(int prognosisId);

    internal interface IProcedurePrognosisClaimsContext
    {
        int PrognosistId { get; }

        int ProgrammeId { get; }
    }
}
