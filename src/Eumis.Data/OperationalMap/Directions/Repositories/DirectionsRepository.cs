using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Directions.ViewObjects;
using Eumis.Domain.OperationalMap.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.OperationalMap.Directions.Repositories
{
    internal class DirectionsRepository : AggregateRepository<Direction>, IDirectionsRepository
    {
        public DirectionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Direction, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Direction, object>>[]
                {
                    e => e.SubDirections,
                };
            }
        }

        public DirectionInfoVO GetDirectionInfo(int directionId)
        {
            var result = this.Set()
                .Where(t => t.DirectionId == directionId)
                .Select(t => new DirectionInfoVO { Status = t.Status })
                .FirstOrDefault();

            return result;
        }

        public IList<DirectionVO> GetDirectionItems()
        {
            var result = this.Set()
                .Select(t => new DirectionVO
                {
                    DirectionId = t.DirectionId,
                    Name = t.Name,
                    NameAlt = t.NameAlt,
                    Status = t.Status,
                })
                .ToList();

            return result;
        }

        public IList<SubDirectionVO> GetSubDirections(int directionId)
        {
            var result = this.unitOfWork.DbContext.Set<SubDirection>()
                .Where(t => t.DirectionId == directionId)
                .Select(t => new SubDirectionVO
                {
                    DirectionId = t.DirectionId,
                    SubDirectionId = t.SubDirectionId,
                    Name = t.Name,
                    NameAlt = t.NameAlt,
                })
                .ToList();

            return result;
        }

        public IList<string> CanDeleteDirection(int directionId)
        {
            // TODO Add more validation logic
            var errors = this.CanDeleteDirectionInternal(directionId);

            return errors;
        }

        public IList<string> CanDeleteSubDirection(int directionId, int subDirectionId)
        {
            // TODO Add more validation logic
            var errors = this.CanDeleteDirectionInternal(directionId);

            return errors;
        }

        private IList<string> CanDeleteDirectionInternal(int directionId)
        {
            var errors = new List<string>();
            var direction = this.Set().Single(t => t.DirectionId == directionId);

            if (direction.Status != DirectionStatus.Draft)
            {
                errors.Add("Статуса на направлението е различен от 'Чернова'");
            }

            return errors;
        }

        public Direction GetDirectionByGid(Guid gid)
        {
            var direction = this.Set().Where(x => x.Gid == gid).Single();
            return direction;
        }

        public SubDirection GetSubDirectionByGid(Guid gid)
        {
            var subDirection = this.unitOfWork.DbContext.Set<SubDirection>().Where(x => x.Gid == gid).Single();
            return subDirection;
        }
    }
}
