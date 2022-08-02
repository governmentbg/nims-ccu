using System;
using System.Linq;

namespace Eumis.Domain.BasicInterestRates
{
    public partial class BasicInterestRate
    {
        public void UpdateBasicInterestRate(string name)
        {
            this.Name = name;
            this.ModifyDate = DateTime.Now;
        }

        #region InterestRates

        public InterestRate FindInterestRate(int interestRateId)
        {
            var interestRate = this.Rates.Where(r => r.InterestRateId == interestRateId).SingleOrDefault();

            if (interestRate == null)
            {
                throw new DomainObjectNotFoundException("Cannot find InterestRate with InterestRateId " + interestRateId);
            }

            return interestRate;
        }

        public void UpdateInterestRate(int interestRateId, decimal rate)
        {
            var interestRate = this.FindInterestRate(interestRateId);

            interestRate.SetAttributes(rate);

            this.ModifyDate = DateTime.Now;
        }

        public void AddInterestRate(DateTime date, decimal rate)
        {
            if (!this.IsCorrectInterestRateDate(date))
            {
                throw new DomainException("InterestRate date is not valid");
            }

            this.Rates.Add(new InterestRate()
            {
                BasicInterestRateId = this.BasicInterestRateId,
                Date = date,
                Rate = rate,
            });

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveInterestRate(int interestRateId)
        {
            var interestRate = this.FindInterestRate(interestRateId);

            this.Rates.Remove(interestRate);

            this.ModifyDate = DateTime.Now;
        }

        public bool IsCorrectInterestRateDate(DateTime date)
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

        #endregion //InterestRates
    }
}
