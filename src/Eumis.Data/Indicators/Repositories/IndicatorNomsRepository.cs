using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Indicators.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain.Indicators;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.Procedures;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Indicators.Repositories
{
    internal class IndicatorNomsRepository : Repository, IIndicatorNomsRepository
    {
        public IndicatorNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IndicatorNomVO GetNom(int nomValueId)
        {
            return (from i in this.unitOfWork.DbContext.Set<Indicator>()
                    where i.IndicatorId == nomValueId
                    select new IndicatorNomVO
                    {
                        NomValueId = i.IndicatorId,
                        Name = i.Name,
                        IndicatorId = i.IndicatorId,
                        ProgrammeId = i.ProgrammeId,
                        MeasureId = i.MeasureId,
                        HasGenderDivision = i.HasGenderDivision,
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<IndicatorNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Indicator>().AndStringContains(i => i.Name, term);

            var indicators =
                from i in this.unitOfWork.DbContext.Set<Indicator>().Where(predicate)
                select i;

            return (from i in indicators
                    select new IndicatorNomVO
                    {
                        NomValueId = i.IndicatorId,
                        Name = i.Name,
                        IndicatorId = i.IndicatorId,
                        ProgrammeId = i.ProgrammeId,
                        MeasureId = i.MeasureId,
                        HasGenderDivision = i.HasGenderDivision,
                    })
                    .OrderBy(e => e.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public IEnumerable<IndicatorNomVO> GetUnusedMapNodeIndicators(
            int mapNodeId,
            string term,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<Indicator>().AndStringContains(i => i.Name, term);

            var indicators =
                from i in this.unitOfWork.DbContext.Set<Indicator>().Where(predicate)
                join mn in this.unitOfWork.DbContext.Set<MapNodeRelation>() on i.ProgrammeId equals mn.ProgrammeId
                where mn.MapNodeId == mapNodeId
                select i;

            return (from i in indicators
                    select new IndicatorNomVO
                    {
                        NomValueId = i.IndicatorId,
                        Name = i.Name,
                        IndicatorId = i.IndicatorId,
                        ProgrammeId = i.ProgrammeId,
                        MeasureId = i.MeasureId,
                        HasGenderDivision = i.HasGenderDivision,
                    })
                    .OrderBy(e => e.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public IEnumerable<IndicatorNomVO> GetUnusedProcedureIndicators(
            int procedureId,
            string term,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<Indicator>().AndStringContains(i => i.Name, term);

            var indicators = Enumerable.Empty<int>().Select(i =>
                new
                {
                    indicator = (Indicator)null,
                    mapNodeId = (int?)null,
                });

            var usedIndicatorIds =
                from pi in this.unitOfWork.DbContext.Set<ProcedureIndicator>()
                join i in this.unitOfWork.DbContext.Set<Indicator>() on pi.IndicatorId equals i.IndicatorId
                where pi.ProcedureId == procedureId
                select i.IndicatorId;

            indicators = indicators.Where(e => !usedIndicatorIds.Contains(e.indicator.IndicatorId));

            return (from e in indicators
                    group e by new { e.indicator } into g
                    select new IndicatorNomVO
                    {
                        NomValueId = g.Key.indicator.IndicatorId,
                        Name = g.Key.indicator.Name,
                        IndicatorId = g.Key.indicator.IndicatorId,
                        ProgrammeId = g.Key.indicator.ProgrammeId,
                        MeasureId = g.Key.indicator.MeasureId,
                        HasGenderDivision = g.Key.indicator.HasGenderDivision,
                        SourceMapNodeId = g.First().mapNodeId,
                    })
                    .OrderBy(e => e.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}