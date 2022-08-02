using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Programme
{
    internal delegate IProgrammeClaimsContext ProgrammeClaimsContextFactory(int mapNodeId);

    internal interface IProgrammeClaimsContext
    {
        MapNodeType MapNodeType { get; }

        int ProgrammeId { get; }
    }
}
