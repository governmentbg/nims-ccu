using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.ExpenseTypes;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.ExpenseTypes.Repositories
{
    internal class ExpenseTypeNomsRepository : EntityNomsRepository<ExpenseType, EntityNomVO>, IExpenseTypeNomsRepository
    {
        public ExpenseTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ExpenseTypeId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.ExpenseTypeId,
                    Name = t.Name,
                })
        {
        }

        public override IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<EntityNomVO> GetExpenseTypeNoms(int procedureId, int programmeId, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<ExpenseType>()
                .AndStringContains(this.nameSelector, term)
                .And(et => et.IsActive);

            var usedExpenseTypes = from e in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel1>()
                                   where e.ProcedureId == procedureId && e.ProgrammeId == programmeId
                                   select e.ExpenseTypeId;

            var expenseTypes = from e in this.unitOfWork.DbContext.Set<ExpenseType>().Where(predicate)
                               where !usedExpenseTypes.Contains(e.ExpenseTypeId)
                               select e;

            return expenseTypes
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
