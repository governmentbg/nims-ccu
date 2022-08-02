using System.Collections.Generic;
using Eumis.Data.Core;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Data.OperationalMap.MapNodes.Repositories
{
    public interface IMapNodeIndicatorsRepository<TMapNode, TMapNodeIndicator> : IRepository
        where TMapNode : MapNode
    {
        IList<MapNodeIndicatorVO> GetMapNodeIndicators(int mapNodeId);

        bool HasAvailableIndicatorsForAttach(int mapNodeId);
    }
}
