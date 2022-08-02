namespace Eumis.Authentication.Authorization.ClaimsContexts.EvalSessionStandpoint
{
    internal delegate IEvalSessionStandpointClaimsContext EvalSessionStandpointClaimsContextFactory(int standpointId);

    internal interface IEvalSessionStandpointClaimsContext
    {
        int EvalSessionStandpointId { get; }

        int ProjectId { get; }
    }
}
