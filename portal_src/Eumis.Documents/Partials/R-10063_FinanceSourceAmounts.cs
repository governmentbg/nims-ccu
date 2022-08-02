using Eumis.Common.Helpers;
using Eumis.Documents.Contracts;
using Eumis.Documents.Partials;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10063
{
    public partial class FinanceSourceAmounts
    {
        public void Load(decimal bfpContractAmount, decimal currentReportAmount, decimal cumulativeAmount
            , decimal bfpContractTotalAmount, decimal currentReportTotalAmount, decimal cumulativeTotalAmount)
        {
            this.BFPContractAmount = bfpContractAmount;
            this.CurrentReportAmount = currentReportAmount;
            this.CumulativeAmount = cumulativeAmount;

            this.CalculatePercents(bfpContractTotalAmount, currentReportTotalAmount, cumulativeTotalAmount);
        }
        public void Sum(decimal bfpContractAmount, decimal currentReportAmount, decimal cumulativeAmount
            , decimal bfpContractTotalAmount, decimal currentReportTotalAmount, decimal cumulativeTotalAmount)
        {
            this.BFPContractAmount += bfpContractAmount;
            this.CurrentReportAmount += currentReportAmount;
            this.CumulativeAmount += cumulativeAmount;

            this.CalculatePercents(bfpContractTotalAmount, currentReportTotalAmount, cumulativeTotalAmount);
        }

        public void CalculatePercents(decimal bfpContractTotalAmount, decimal currentReportTotalAmount, decimal cumulativeTotalAmount)
        {
            if (bfpContractTotalAmount > 0)
                this.BFPContractAmountPercentage = (this.BFPContractAmount * 100) / bfpContractTotalAmount;

            if (currentReportTotalAmount > 0)
                this.CurrentReportAmountPercentage = (this.CurrentReportAmount * 100) / currentReportTotalAmount;

            if (cumulativeTotalAmount > 0)
                this.CumulativeAmountPercentage = (this.CumulativeAmount * 100) / cumulativeTotalAmount;

            // set to 2 digits after decimal point
            this.BFPContractAmountPercentage = Math.Round(this.BFPContractAmountPercentage, 2, MidpointRounding.AwayFromZero);
            this.CurrentReportAmountPercentage = Math.Round(this.CurrentReportAmountPercentage, 2, MidpointRounding.AwayFromZero);
            this.CumulativeAmountPercentage = Math.Round(this.CumulativeAmountPercentage, 2, MidpointRounding.AwayFromZero);
        }
    }
}
