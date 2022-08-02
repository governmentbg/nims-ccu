using Eumis.Data.OperationalMap.Directions.ViewObjects;
using Eumis.Domain.OperationalMap.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.OperationalMap.Directions.Repositories
{
    public interface IDirectionsRepository : IAggregateRepository<Direction>
    {
        DirectionInfoVO GetDirectionInfo(int directionId);

        IList<DirectionVO> GetDirectionItems();

        IList<SubDirectionVO> GetSubDirections(int directionId);

        IList<string> CanDeleteSubDirection(int directionId, int subDirectionId);

        IList<string> CanDeleteDirection(int directionId);

        Direction GetDirectionByGid(Guid gid);

        SubDirection GetSubDirectionByGid(Guid gid);
    }
}
