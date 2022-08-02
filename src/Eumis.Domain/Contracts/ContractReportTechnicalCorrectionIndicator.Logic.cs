using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportTechnicalCorrectionIndicator
    {
        public void UpdateAttributes(
            string notes,
            decimal? correctedApprovedPeriodAmountMen,
            decimal? correctedApprovedPeriodAmountWomen,
            decimal? correctedApprovedPeriodAmountTotal,
            decimal? correctedApprovedCumulativeAmountMen,
            decimal? correctedApprovedCumulativeAmountWomen,
            decimal? correctedApprovedCumulativeAmountTotal,
            decimal? correctedApprovedResidueAmountMen,
            decimal? correctedApprovedResidueAmountWomen,
            decimal correctedApprovedResidueAmountTotal,
            ContractReportIndicator contractReportIndicator,
            ContractIndicator contractIndicator)
        {
            this.Notes = notes;

            this.CorrectedApprovedPeriodAmountMen = correctedApprovedPeriodAmountMen;
            this.CorrectedApprovedPeriodAmountWomen = correctedApprovedPeriodAmountWomen;
            this.CorrectedApprovedPeriodAmountTotal = correctedApprovedPeriodAmountTotal;

            this.CorrectedApprovedCumulativeAmountMen = correctedApprovedCumulativeAmountMen;
            this.CorrectedApprovedCumulativeAmountWomen = correctedApprovedCumulativeAmountWomen;
            this.CorrectedApprovedCumulativeAmountTotal = correctedApprovedCumulativeAmountTotal;

            this.CorrectedApprovedResidueAmountMen = correctedApprovedResidueAmountMen;
            this.CorrectedApprovedResidueAmountWomen = correctedApprovedResidueAmountWomen;
            this.CorrectedApprovedResidueAmountTotal = correctedApprovedResidueAmountTotal;

            this.ModifyDate = DateTime.Now;

            contractReportIndicator.ValidateAmounts(
                this.CorrectedApprovedPeriodAmountMen,
                this.CorrectedApprovedPeriodAmountWomen,
                this.CorrectedApprovedPeriodAmountTotal,
                this.CorrectedApprovedCumulativeAmountMen,
                this.CorrectedApprovedCumulativeAmountWomen,
                this.CorrectedApprovedCumulativeAmountTotal,
                this.CorrectedApprovedResidueAmountMen,
                this.CorrectedApprovedResidueAmountWomen,
                this.CorrectedApprovedResidueAmountTotal,
                this.LastReportCorrectedCumulativeAmountMen,
                this.LastReportCorrectedCumulativeAmountWomen,
                this.LastReportCorrectedCumulativeAmountTotal,
                "CorrectedApproved",
                contractIndicator);
        }
    }
}
