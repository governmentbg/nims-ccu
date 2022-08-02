using System;
using System.Collections.Generic;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Data.OperationalMap.MapNodes.Repositories
{
    public interface IMapNodesRepository : IAggregateRepository<MapNode>
    {
        IList<MapNodeDocumentVO> GetMapNodeDocuments(int mapNodeId);

        int GetMapNodeProgrammeId(int mapNodeId);

        MapNodeType GetMapNodeType(int mapNodeId);

        MapNodeStatus GetMapNodeStatus(int mapNodeId);

        MapNodeInfoVO GetMapNodePosition(int mapNodeId);

        int GetMapNodeIdByGid(Guid gid);
    }
}
