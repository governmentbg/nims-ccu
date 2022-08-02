using System;
using System.Linq;

namespace Eumis.Domain.Allowances
{
    public partial class Allowance
    {
        public void UpdateAllowance(string name)
        {
            this.Name = name;
            this.ModifyDate = DateTime.Now;
        }

        #region AllowanceRates

        public AllowanceRate FindAllowanceRate(int allowanceRateId)
        {
            var allowanceRate = this.Rates.Where(r => r.AllowanceRateId == allowanceRateId).SingleOrDefault();

            if (allowanceRate == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AllowanceRate with AllowanceRateId " + allowanceRateId);
            }

            return allowanceRate;
        }

        public void UpdateAllowanceRate(int allowanceRateId, decimal rate)
        {
            var allowanceRate = this.FindAllowanceRate(allowanceRateId);

            allowanceRate.SetAttributes(rate);

            this.ModifyDate = DateTime.Now;
        }

        public void AddAllowanceRate(DateTime date, decimal rate)
        {
            if (!this.IsCorrectAllowanceRateDate(date))
            {
                throw new DomainException("AllowanceRate date is not valid");
            }

            this.Rates.Add(new AllowanceRate()
            {
                AllowanceId = this.AllowanceId,
                Date = date,
                Rate = rate,
            });

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveAllowanceRate(int allowanceRateId)
        {
            var allowanceRate = this.FindAllowanceRate(allowanceRateId);

            this.Rates.Remove(allowanceRate);

            this.ModifyDate = DateTime.Now;
        }

        public bool IsCorrectAllowanceRateDate(DateTime date)
        {
            if (this.Rates.Any())
            {
                return date > this.Rates.OrderByDescending(t => t.Date).First().Date;
            }
            else
            {
                return true;
            }
        }

        #endregion //AllowanceRates
    }
}
