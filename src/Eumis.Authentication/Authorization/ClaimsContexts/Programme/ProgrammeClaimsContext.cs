using Autofac.Features.AttributeFilters;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Programme
{
    internal class ProgrammeClaimsContext : ClaimsContext, IProgrammeClaimsContext
    {
        private int mapNodeId;

        private IClaimsCache claimsCache;
        private IMapNodesRepository mapNodesRepository;

        public ProgrammeClaimsContext(
            int mapNodeId,
            [KeyFilter(ClaimsCaches.Programme)]IClaimsCache claimsCache,
            IMapNodesRepository mapNodesRepository)
            : base(claimsCache)
        {
            this.mapNodeId = mapNodeId;
            this.claimsCache = claimsCache;
            this.mapNodesRepository = mapNodesRepository;
        }

        public MapNodeType MapNodeType
        {
            get
            {
                return this.GetClaim(
                    this.mapNodeId,
                    new ClaimKey("MapNodeType"),
                    () => this.mapNodesRepository.GetMapNodeType(this.mapNodeId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.mapNodeId,
                    new ClaimKey("ProgrammeId"),
                    () => this.mapNodesRepository.GetMapNodeProgrammeId(this.mapNodeId));
            }
        }
    }
}
