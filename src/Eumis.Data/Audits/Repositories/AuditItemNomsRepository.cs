using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Audits;
using Eumis.Domain.Contracts;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Audits.Repositories
{
    internal class AuditItemNomsRepository : Repository, IAuditItemNomsRepository
    {
        private Func<AuditItemNom, EntityNomVO> getNomenclature = o =>
        {
            var result = new EntityNomVO
            {
                NomValueId = o.AuditItemId,
            };

            switch (o.Level)
            {
                case AuditItemLevel.ProgrammePriority:
                    result.Name = string.Format("{0} {1}", o.ProgrammePriorityCode, o.ProgrammePriorityName);
                    break;
                case AuditItemLevel.Procedure:
                    result.Name = o.ProcedureName;
                    break;
                case AuditItemLevel.Contract:
                    result.Name = o.ContractRegNumber;
                    break;
                case AuditItemLevel.ContractContract:
                    result.Name = o.ContractContractNumber;
                    break;
            }

            return result;
        };

        public AuditItemNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            var predicate = PredicateBuilder.True<AuditItemNom>()
                .And(scp => scp.AuditItemId == nomValueId);

            return this.GetNomenclatures(predicate)
                    .ToList()
                    .Select(this.getNomenclature).SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<AuditItemNom>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                var termPredicate = PredicateBuilder.False<AuditItemNom>()
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

        public IEnumerable<EntityNomVO> GetItemNoms(int[] ids, int auditId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<AuditItemNom>()
                .And(ali => ali.AuditId == auditId);

            if (!string.IsNullOrWhiteSpace(term))
            {
                var termPredicate = PredicateBuilder.False<AuditItemNom>()
                    .Or(p => p.ProgrammePriorityCode.Contains(term))
                    .Or(p => p.ProgrammePriorityName.Contains(term))
                    .Or(p => p.ProcedureName.Contains(term))
                    .Or(p => p.ContractRegNumber.Contains(term))
                    .Or(p => p.ContractContractNumber.Contains(term));

                predicate = predicate.And(termPredicate);
            }

            if (ids != null && ids.Length != 0)
            {
                predicate = predicate.And(p => ids.Contains(p.AuditItemId));
            }

            return this.GetNomenclatures(predicate)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList()
                    .Select(this.getNomenclature)
                    .ToList();
        }

        private IQueryable<AuditItemNom> GetNomenclatures(Expression<Func<AuditItemNom, bool>> predicate)
        {
            return (from ali in this.unitOfWork.DbContext.Set<AuditLevelItem>()
                    join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ali.ProgrammePriorityId equals pp.MapNodeId into g0
                    from pp in g0.DefaultIfEmpty()
                    join proc in this.unitOfWork.DbContext.Set<Procedure>() on ali.ProcedureId equals proc.ProcedureId into g1
                    from proc in g1.DefaultIfEmpty()
                    join c in this.unitOfWork.DbContext.Set<Contract>() on ali.ContractId equals c.ContractId into g2
                    from c in g2.DefaultIfEmpty()
                    join cc in this.unitOfWork.DbContext.Set<ContractContract>() on ali.ContractContractId equals cc.ContractContractId into g3
                    from cc in g3.DefaultIfEmpty()
                    orderby ali.AuditId
                    select new AuditItemNom
                    {
                        AuditItemId = ali.AuditLevelItemId,
                        AuditId = ali.AuditId,
                        Level = ali.Level,
                        ProgrammePriorityCode = pp.Code,
                        ProgrammePriorityName = pp.Name,
                        ProcedureName = proc.Name,
                        ContractRegNumber = c.RegNumber,
                        ContractContractNumber = cc.Number,
                    }).Where(predicate);
        }

        private class AuditItemNom
        {
            public int AuditItemId { get; set; }

            public int AuditId { get; set; }

            public AuditItemLevel Level { get; set; }

            public string ProgrammePriorityCode { get; set; }

            public string ProgrammePriorityName { get; set; }

            public string ProcedureName { get; set; }

            public string ContractRegNumber { get; set; }

            public string ContractContractNumber { get; set; }
        }
    }
}
