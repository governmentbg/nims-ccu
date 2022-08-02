using Eumis.Common.Db;
using Eumis.Data.ExpenseTypes.ViewObjects;
using Eumis.Domain.ExpenseTypes;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.ExpenseTypes.Repositories
{
    internal class ExpenseTypesRepository : AggregateRepository<ExpenseType>, IExpenseTypesRepository
    {
        public ExpenseTypesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ExpenseType, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ExpenseType, object>>[]
                {
                    et => et.ExpenseSubTypes,
                };
            }
        }

        public IList<ExpenseTypeVO> GetExpenseTypes()
        {
            return (from et in this.unitOfWork.DbContext.Set<ExpenseType>()
                    orderby et.CreateDate descending
                    select new ExpenseTypeVO
                    {
                        ExpenseTypeId = et.ExpenseTypeId,
                        Name = et.Name,
                        NameAlt = et.NameAlt,
                        IsActive = et.IsActive,
                    })
                    .ToList();
        }

        public IList<string> CanDeleteExpenseType(int expenseTypeId)
        {
            var errors = new List<string>();

            if ((from b in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel1>()
                 where b.ExpenseTypeId == expenseTypeId
                 select b.ExpenseTypeId).Any())
            {
                errors.Add("Типът разход не може да бъде изтрит, защото е свързан с елемент от бюджета");
            }

            return errors;
        }
    }
}
