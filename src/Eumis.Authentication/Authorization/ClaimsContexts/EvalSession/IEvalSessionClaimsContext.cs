using Eumis.Domain.EvalSessions;

namespace Eumis.Authentication.Authorization.ClaimsContexts.EvalSession
{
    internal delegate IEvalSessionClaimsContext EvalSessionClaimsContextFactory(int evalSessionId);

    internal interface IEvalSessionClaimsContext
    {
        int EvalSessionId { get; }

        EvalSessionStatus EvalSessionStatus { get; }

        int ProgrammeId { get; }
    }
}
