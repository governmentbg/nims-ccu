using Autofac.Features.AttributeFilters;
using Eumis.Data.OperationalMap.MapNodes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Authorization.ClaimsContexts.MapNodeIndicator
{
    internal class MapNodeIndicatorClaimsContext : ClaimsContext, IMapNodeIndicatorClaimsContext
    {
        private readonly int mapNodeId;

        private IClaimsCache claimsCache;
        private IMapNodesRepository mapNodesRepository;

        public MapNodeIndicatorClaimsContext(
            int mapNodeId,
            [KeyFilter(ClaimsCaches.CertReport)]IClaimsCache claimsCache,
            IMapNodesRepository mapNodesRepository)
            : base(claimsCache)
        {
            this.mapNodeId = mapNodeId;
            this.claimsCache = claimsCache;
            this.mapNodesRepository = mapNodesRepository;
        }

        public int MapNodeId
        {
            get
            {
                return this.mapNodeId;
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
