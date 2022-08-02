namespace Eumis.Authentication.Authorization.ClaimsContexts.SpotCheck
{
    internal delegate ISpotCheckClaimsContext SpotCheckClaimsContextFactory(int spotCheckId);

    internal interface ISpotCheckClaimsContext
    {
        int SpotCheckId { get; }

        int ProgrammeId { get; }

        int? ContractId { get; }
    }
}
