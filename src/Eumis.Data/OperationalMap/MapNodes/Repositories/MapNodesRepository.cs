using Eumis.Common.Db;
using Eumis.Data.OperationalMap.MapNodes.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.OperationalMap.MapNodes.Repositories
{
    internal class MapNodesRepository : AggregateRepository<MapNode>, IMapNodesRepository
    {
        public MapNodesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<MapNode, object>>[] Includes
        {
            get
            {
                return new Expression<Func<MapNode, object>>[]
                {
                    e => e.MapNodeRelation,
                    e => e.MapNodeDocuments.Select(f => f.File),
                };
            }
        }

        public override MapNode Find(int id)
        {
            var mn = base.Find(id);

            return mn;
        }

        public int GetMapNodeProgrammeId(int mapNodeId)
        {
            return (from mr in this.unitOfWork.DbContext.Set<MapNodeRelation>()
                    where mr.MapNodeId == mapNodeId
                    select mr.ProgrammeId.Value)
                   .Single();
        }

        public MapNodeType GetMapNodeType(int mapNodeId)
        {
            return this.Set()
                .Single(mn => mn.MapNodeId == mapNodeId)
                .Type;
        }

        public MapNodeStatus GetMapNodeStatus(int mapNodeId)
        {
            return (from mn in this.unitOfWork.DbContext.Set<MapNode>()
                    where mn.MapNodeId == mapNodeId
                    select mn.Status).Single();
        }

        public IList<MapNodeDocumentVO> GetMapNodeDocuments(int mapNodeId)
        {
            return (from mnd in this.unitOfWork.DbContext.Set<MapNodeDocument>()
                    join b in this.unitOfWork.DbContext.Set<Blob>() on mnd.BlobKey equals b.Key into g1
                    from b in g1.DefaultIfEmpty()
                    where mnd.MapNodeId == mapNodeId
                    select new { mnd = mnd, b })
                    .Select(p => new MapNodeDocumentVO
                    {
                        MapNodeId = p.mnd.MapNodeId,
                        MapNodeDocumentId = p.mnd.MapNodeDocumentId,
                        Name = p.mnd.Name,
                        Description = p.mnd.Description,
                        File = (p.b.Key == null) ? null : new FileVO
                        {
                            Key = p.b.Key,
                            Name = p.b.FileName,
                        },
                    })
                   .ToList();
        }

        public MapNodeInfoVO GetMapNodePosition(int mapNodeId)
        {
            return (from mn in this.unitOfWork.DbContext.Set<MapNode>()
                    join pmn in this.unitOfWork.DbContext.Set<MapNode>() on mn.MapNodeRelation.ParentMapNodeId equals pmn.MapNodeId into g1
                    from pmn in g1.DefaultIfEmpty()
                    where mn.MapNodeId == mapNodeId
                    select new MapNodeInfoVO()
                    {
                        Status = mn.Status,
                        Name = mn.Name,
                        ShortName = mn.ShortName,
                        ParentId = pmn.MapNodeId,
                        ParentName = pmn.Name,
                        ParentShortName = pmn.ShortName,
                    })
                    .SingleOrDefault();
        }

        public int GetMapNodeIdByGid(Guid gid)
        {
            return (from mn in this.unitOfWork.DbContext.Set<MapNode>()
                    where mn.Gid == gid
                    select mn.MapNodeId)
                   .Single();
        }

        #region IAggregateRepository Not Implemented Methods

        public new void Add(MapNode entity)
        {
            throw new NotImplementedException();
        }

        public new void Remove(MapNode entity)
        {
            throw new NotImplementedException();
        }

        public new byte[] GetVersion(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
