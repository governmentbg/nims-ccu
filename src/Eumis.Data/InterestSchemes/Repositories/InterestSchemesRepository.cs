using Eumis.Common.Db;
using Eumis.Data.InterestSchemes.ViewObjects;
using Eumis.Domain.Allowances;
using Eumis.Domain.BasicInterestRates;
using Eumis.Domain.Debts;
using Eumis.Domain.InterestSchemes;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.InterestSchemes.Repositories
{
    internal class InterestSchemesRepository : AggregateRepository<InterestScheme>, IInterestSchemesRepository
    {
        public InterestSchemesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<InterestSchemeVO> GetInterestSchemes()
        {
            return (from s in this.unitOfWork.DbContext.Set<InterestScheme>()
                    join bir in this.unitOfWork.DbContext.Set<BasicInterestRate>() on s.BasicInterestRateId equals bir.BasicInterestRateId
                    join a in this.unitOfWork.DbContext.Set<Allowance>() on s.AllowanceId equals a.AllowanceId
                    select new InterestSchemeVO
                    {
                        InterestSchemeId = s.InterestSchemeId,
                        Name = s.Name,
                        BasicInterestRateName = bir.Name,
                        AllowanceName = a.Name,
                        AnnualBasis = s.AnnualBasis,
                        IsActive = s.IsActive,
                    })
                .ToList();
        }

        public IList<string> CanDelete(int interestSchemeId)
        {
            var errors = new List<string>();

            if ((from cdi in this.unitOfWork.DbContext.Set<ContractDebtInterest>()
                 where cdi.InterestSchemeId == interestSchemeId
                 select cdi.InterestSchemeId).Any())
            {
                errors.Add("Схемата за олихвяване не може да бъде изтрита, защото е свързана с дълг");
            }

            return errors;
        }
    }
}
