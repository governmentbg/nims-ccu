namespace Eumis.Authentication.Authorization.ClaimsContexts.Prognosis
{
    internal delegate IProgrammePrognosisClaimsContext ProgrammePrognosisClaimsContextFactory(int prognosisId);

    internal interface IProgrammePrognosisClaimsContext
    {
        int PrognosistId { get; }

        int ProgrammeId { get; }
    }
}
