using Eumis.Common.Db;
using Eumis.Data.Allowances.ViewObjects;
using Eumis.Domain.Allowances;
using Eumis.Domain.InterestSchemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Allowances.Repositories
{
    internal class AllowancesRepository : AggregateRepository<Allowance>, IAllowancesRepository
    {
        public AllowancesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Allowance, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Allowance, object>>[]
                {
                    et => et.Rates,
                };
            }
        }

        public IList<AllowanceVO> GetAllowances()
        {
            return (from a in this.unitOfWork.DbContext.Set<Allowance>()
                    select new AllowanceVO
                    {
                        AllowanceId = a.AllowanceId,
                        Name = a.Name,
                    })
                .ToList();
        }

        public IList<string> CanDeleteAllowance(int allowanceId)
        {
            var errors = new List<string>();

            if ((from s in this.unitOfWork.DbContext.Set<InterestScheme>()
                 where s.AllowanceId == allowanceId
                 select s.InterestSchemeId).Any())
            {
                errors.Add("Типът разход не може да бъде изтрит, защото е свързан със схема за олихвяване");
            }

            return errors;
        }
    }
}
