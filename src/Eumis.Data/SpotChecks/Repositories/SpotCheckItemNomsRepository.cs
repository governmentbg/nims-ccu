using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.Procedures;
using Eumis.Domain.SpotChecks;

namespace Eumis.Data.SpotChecks.Repositories
{
    internal class SpotCheckItemNomsRepository : Repository, ISpotCheckItemNomsRepository
    {
        private Func<SpotCheckItemNom, EntityNomVO> getNomenclature = o =>
        {
            var result = new EntityNomVO
            {
                NomValueId = o.SpotCheckItemId,
            };

            switch (o.Level)
            {
                case SpotCheckItemLevel.ProgrammePriority:
                    result.Name = string.Format("{0} {1}", o.ProgrammePriorityCode, o.ProgrammePriorityName);
                    break;
                case SpotCheckItemLevel.Procedure:
                    result.Name = o.ProcedureName;
                    break;
                case SpotCheckItemLevel.Contract:
                    result.Name = o.ContractRegNumber;
                    break;
                case SpotCheckItemLevel.ContractContract:
                    result.Name = o.ContractContractNumber;
                    break;
            }

            return result;
        };

        public SpotCheckItemNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            var predicate = PredicateBuilder.True<SpotCheckItemNom>()
                .And(scp => scp.SpotCheckItemId == nomValueId);

            return this.GetNomenclatures(predicate)
                    .ToList()
                    .Select(this.getNomenclature).SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<SpotCheckItemNom>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                var termPredicate = PredicateBuilder.False<SpotCheckItemNom>()
                    .Or(p => p.ProgrammePriorityCode.Contains(term))
                    .Or(p => p.ProgrammePriorityName.Contains(term))
                    .Or(p => p.ProcedureName.Contains(term))
                    .Or(p => p.ContractRegNumber.Contains(term))
                    .Or(p => p.ContractContractNumber.Contains(term));

                predicate = predicate.And(termPredicate);
            }

            return this.GetNomenclatures(predicate)
                .WithOffsetAndLimit(offset, limit)
                .ToList()
                .Select(this.getNomenclature)
                .ToList();
        }

        public IEnumerable<EntityNomVO> GetItemNoms(int[] ids, int spotCheckId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<SpotCheckItemNom>()
                .And(scp => scp.SpotCheckId == spotCheckId);

            if (!string.IsNullOrWhiteSpace(term))
            {
                var termPredicate = PredicateBuilder.False<SpotCheckItemNom>()
                    .Or(p => p.ProgrammePriorityCode.Contains(term))
                    .Or(p => p.ProgrammePriorityName.Contains(term))
                    .Or(p => p.ProcedureName.Contains(term))
                    .Or(p => p.ContractRegNumber.Contains(term))
                    .Or(p => p.ContractContractNumber.Contains(term));

                predicate = predicate.And(termPredicate);
            }

            if (ids != null && ids.Length != 0)
            {
                predicate = predicate.And(p => ids.Contains(p.SpotCheckItemId));
            }

            return this.GetNomenclatures(predicate)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList()
                    .Select(this.getNomenclature)
                    .ToList();
        }

        private IQueryable<SpotCheckItemNom> GetNomenclatures(Expression<Func<SpotCheckItemNom, bool>> predicate)
        {
            return (from scp in this.unitOfWork.DbContext.Set<SpotCheckItem>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on scp.ProgrammePriorityId equals pp.MapNodeId into g0
                    from pp in g0.DefaultIfEmpty()
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on scp.ProcedureId equals proc.ProcedureId into g1
                    from proc in g1.DefaultIfEmpty()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on scp.ContractId equals c.ContractId into g2
                    from c in g2.DefaultIfEmpty()
                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on scp.ContractContractId equals cc.ContractContractId into g3
                    from cc in g3.DefaultIfEmpty()
                    orderby scp.SpotCheckId
                    select new SpotCheckItemNom
                    {
                        SpotCheckItemId = scp.SpotCheckItemId,
                        SpotCheckId = scp.SpotCheckId,
                        Level = scp.Level,
                        ProgrammePriorityCode = pp.Code,
                        ProgrammePriorityName = pp.Name,
                        ProcedureName = proc.Name,
                        ContractRegNumber = c.RegNumber,
                        ContractContractNumber = cc.Number,
                    }).Where(predicate);
        }

        private class SpotCheckItemNom
        {
            public int SpotCheckItemId { get; set; }

            public int SpotCheckId { get; set; }

            public SpotCheckItemLevel Level { get; set; }

            public string ProgrammePriorityCode { get; set; }

            public string ProgrammePriorityName { get; set; }

            public string ProcedureName { get; set; }

            public string ContractRegNumber { get; set; }

            public string ContractContractNumber { get; set; }
        }
    }
}
