using Eumis.Common.Db;
using Eumis.Data.BasicInterestRates.ViewObjects;
using Eumis.Domain.BasicInterestRates;
using Eumis.Domain.InterestSchemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.BasicInterestRates.Repositories
{
    internal class BasicInterestRatesRepository : AggregateRepository<BasicInterestRate>, IBasicInterestRatesRepository
    {
        public BasicInterestRatesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<BasicInterestRate, object>>[] Includes
        {
            get
            {
                return new Expression<Func<BasicInterestRate, object>>[]
                {
                    et => et.Rates,
                };
            }
        }

        public IList<BasicInterestRateVO> GetBasicInterestRates()
        {
            return (from bir in this.unitOfWork.DbContext.Set<BasicInterestRate>()
                    select new BasicInterestRateVO
                    {
                        BasicInterestRateId = bir.BasicInterestRateId,
                        Name = bir.Name,
                    })
                .ToList();
        }

        public IList<string> CanDeleteBasicInterestRate(int basicInterestRateId)
        {
            var errors = new List<string>();

            if ((from s in this.unitOfWork.DbContext.Set<InterestScheme>()
                 where s.BasicInterestRateId == basicInterestRateId
                 select s.InterestSchemeId).Any())
            {
                errors.Add("Записът не може да бъде изтрит, защото е свързан със схема за олихвяване");
            }

            return errors;
        }
    }
}
