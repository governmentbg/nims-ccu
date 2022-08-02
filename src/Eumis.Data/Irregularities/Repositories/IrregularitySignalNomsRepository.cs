using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Irregularities;
using Eumis.Domain.OperationalMap.Programmes;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularitySignalNomsRepository : Repository, IIrregularitySignalNomsRepository
    {
        public IrregularitySignalNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>()
                    select new EntityNomVO
                    {
                        NomValueId = irrs.IrregularitySignalId,
                        Name = irrs.RegNumber,
                    }).SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetSignalNoms(term, offset, limit);
        }

        public IEnumerable<EntityNomVO> GetSignalNoms(string term, int offset = 0, int? limit = null, int[] programmeIds = null, IrregularitySignalStatus? status = null, bool freeOnly = false)
        {
            var predicate = PredicateBuilder.True<IrregularitySignal>()
                .AndStringContains(irrs => irrs.RegNumber, term)
                .AndEquals(irrs => irrs.Status, status);

            if (freeOnly)
            {
                var associatedIrrs = from irr in this.unitOfWork.DbContext.Set<Irregularity>()
                                     select irr.IrregularitySignalId;

                predicate = predicate.And(irrs => !associatedIrrs.Contains(irrs.IrregularitySignalId));
            }

            if (programmeIds != null)
            {
                predicate = predicate.And(irrs => programmeIds.Contains(irrs.ProgrammeId));
            }

            return (from irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>().Where(predicate)
                    orderby irrs.Number descending
                    select new EntityNomVO
                    {
                        NomValueId = irrs.IrregularitySignalId,
                        Name = irrs.RegNumber,
                    })
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public IEnumerable<EntityNomVO> GetRegisterSignalNoms(string term, int offset = 0, int? limit = null, int[] programmeIds = null)
        {
            var predicate = PredicateBuilder.True<IrregularitySignal>()
                .And(irrs => irrs.Status == IrregularitySignalStatus.Active || irrs.Status == IrregularitySignalStatus.Ended);

            if (programmeIds != null)
            {
                predicate = predicate.And(irrs => programmeIds.Contains(irrs.ProgrammeId));
            }

            var result = (from irrs in this.unitOfWork.DbContext.Set<IrregularitySignal>().Where(predicate)
                          join pr in this.unitOfWork.DbContext.Set<Programme>() on irrs.ProgrammeId equals pr.MapNodeId
                          orderby irrs.Number descending
                          select new EntityNomVO
                          {
                              NomValueId = irrs.IrregularitySignalId,
                              Name = irrs.RegNumber + " - " + pr.Code + " " + pr.Name,
                          })
                          .WithOffsetAndLimit(offset, limit);

            if (!string.IsNullOrWhiteSpace(term))
            {
                result = result.Where(n => n.Name.ToLower().Contains(term.ToLower()));
            }

            return result.ToList();
        }
    }
}
