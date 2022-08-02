using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.SpotChecks;

namespace Eumis.Data.SpotChecks.Repositories
{
    internal class SpotCheckPlanNomsRepository : Repository, ISpotCheckPlanNomsRepository
    {
        public SpotCheckPlanNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from scp in this.unitOfWork.DbContext.Set<SpotCheckPlan>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on scp.ProgrammeId equals p.MapNodeId
                    where scp.SpotCheckPlanId == nomValueId
                    select new { p.Code, scp.SpotCheckPlanId, scp.Month, scp.Year })
                    .ToList()
                    .Select(o => new EntityNomVO
                    {
                        NomValueId = o.SpotCheckPlanId,
                        Name = string.Format("{0} ({1} {2})", o.Code, o.Month.GetEnumDescription(), o.Year.GetEnumDescription()),
                    }).SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetPlanNoms(term, offset, limit);
        }

        public IEnumerable<EntityNomVO> GetPlanNoms(string term, int offset = 0, int? limit = null, int[] programmeIds = null)
        {
            var predicate = PredicateBuilder.True<PlanNom>();

            if (programmeIds != null)
            {
                predicate = predicate.And(scp => programmeIds.Contains(scp.ProgrammeId));
            }

            if (!string.IsNullOrWhiteSpace(term))
            {
                var termPredicate = PredicateBuilder.False<PlanNom>()
                    .Or(p => p.ProgrammeCode.Contains(term));

                termPredicate = termPredicate.Or(this.GetMonthTermPred(term));
                termPredicate = termPredicate.Or(this.GetYearTermPred(term));

                predicate = predicate.And(termPredicate);
            }

            return (from scp in this.unitOfWork.DbContext.Set<SpotCheckPlan>()
                    join p in this.unitOfWork.DbContext.Set<Programme>() on scp.ProgrammeId equals p.MapNodeId

                    select new PlanNom
                    {
                        SpotCheckPlanId = scp.SpotCheckPlanId,
                        ProgrammeId = scp.ProgrammeId,
                        ProgrammeCode = p.Code,
                        Month = scp.Month,
                        Year = scp.Year,
                    }).Where(predicate)
                    .OrderByDescending(p => p.Year)
                    .ThenByDescending(p => p.Month)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList()
                    .Select(p => new EntityNomVO
                    {
                        NomValueId = p.SpotCheckPlanId,
                        Name = string.Format("{0} ({1} {2})", p.ProgrammeCode, p.Month.GetEnumDescription(), p.Year.GetEnumDescription()),
                    }).ToList();
        }

        private Expression<Func<PlanNom, bool>> GetMonthTermPred(string term)
        {
            var pred = PredicateBuilder.False<PlanNom>();
            var modifiedTerm = term.ToLower();

            foreach (var obj in Enum.GetValues(typeof(Month)))
            {
                var month = (Month)obj;
                if (month.GetEnumDescription().ToLower().Contains(modifiedTerm))
                {
                    pred = pred.Or(p => p.Month == month);
                }
            }

            return pred;
        }

        private Expression<Func<PlanNom, bool>> GetYearTermPred(string term)
        {
            var pred = PredicateBuilder.False<PlanNom>();

            foreach (var obj in Enum.GetValues(typeof(Year)))
            {
                var year = (Year)obj;
                if (year.GetEnumDescription().Contains(term))
                {
                    pred = pred.Or(p => p.Year == year);
                }
            }

            return pred;
        }

        private class PlanNom
        {
            public int SpotCheckPlanId { get; set; }

            public int ProgrammeId { get; set; }

            public string ProgrammeCode { get; set; }

            public Month Month { get; set; }

            public Year Year { get; set; }
        }
    }
}
