namespace Eumis.Authentication.Authorization.ClaimsContexts.Prognosis
{
    internal delegate IProgrammePriorityPrognosisClaimsContext ProgrammePriorityPrognosisClaimsContextFactory(int prognosisId);

    internal interface IProgrammePriorityPrognosisClaimsContext
    {
        int PrognosistId { get; }

        int ProgrammeId { get; }
    }
}
