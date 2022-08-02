namespace Eumis.Authentication.Authorization.ClaimsContexts.SpotCheck
{
    internal delegate ISpotCheckPlanClaimsContext SpotCheckPlanClaimsContextFactory(int spotCheckPlanId);

    internal interface ISpotCheckPlanClaimsContext
    {
        int SpotCheckPlanId { get; }

        int ProgrammeId { get; }

        int? ContractId { get; }
    }
}
