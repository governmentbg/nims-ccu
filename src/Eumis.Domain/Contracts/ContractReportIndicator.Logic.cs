using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportIndicator
    {
        public void UpdateAttributes(
            ContractReportIndicatorApproval? approval,
            string notes,
            decimal? approvedPeriodAmountMen,
            decimal? approvedPeriodAmountWomen,
            decimal? approvedPeriodAmountTotal,
            decimal? approvedCumulativeAmountMen,
            decimal? approvedCumulativeAmountWomen,
            decimal? approvedCumulativeAmountTotal,
            decimal? approvedResidueAmountMen,
            decimal? approvedResidueAmountWomen,
            decimal? approvedResidueAmountTotal,
            decimal? lastReportCumulativeAmountMen,
            decimal? lastReportCumulativeAmountWomen,
            decimal lastReportCumulativeAmountTotal,
            ContractIndicator contractIndicator)
        {
            this.Approval = approval;
            this.Notes = notes;

            if (this.HasGenderDivision)
            {
                this.ApprovedPeriodAmountMen = approvedPeriodAmountMen;
                this.ApprovedPeriodAmountWomen = approvedPeriodAmountWomen;
                this.ApprovedCumulativeAmountMen = approvedCumulativeAmountMen;
                this.ApprovedCumulativeAmountWomen = approvedCumulativeAmountWomen;
                this.ApprovedResidueAmountMen = approvedResidueAmountMen;
                this.ApprovedResidueAmountWomen = approvedResidueAmountWomen;
            }

            this.ApprovedPeriodAmountTotal = approvedPeriodAmountTotal;
            this.ApprovedCumulativeAmountTotal = approvedCumulativeAmountTotal;
            this.ApprovedResidueAmountTotal = approvedResidueAmountTotal;

            this.ModifyDate = DateTime.Now;

            this.ValidateAmounts(
                this.ApprovedPeriodAmountMen,
                this.ApprovedPeriodAmountWomen,
                this.ApprovedPeriodAmountTotal.Value,
                this.ApprovedCumulativeAmountMen,
                this.ApprovedCumulativeAmountWomen,
                this.ApprovedCumulativeAmountTotal.Value,
                this.ApprovedResidueAmountMen,
                this.ApprovedResidueAmountWomen,
                this.ApprovedResidueAmountTotal.Value,
                lastReportCumulativeAmountMen,
                lastReportCumulativeAmountWomen,
                lastReportCumulativeAmountTotal,
                "Approved",
                contractIndicator);
        }

        public void ValidateTotalAmount(decimal amountMen, decimal amountWomen, decimal amountTotal, string type)
        {
            if ((amountMen + amountWomen) != amountTotal)
            {
                throw new DomainException($"ContractReportIndicator.ValidateTotalAmount: {type}AmountTotal is not correct!");
            }
        }

        public void ValidateAmount(
            decimal periodAmount,
            decimal cumulativeAmount,
            decimal residueAmount,
            decimal lastReportCorrectedCumulativeAmount,
            decimal baseValue,
            decimal targetValue,
            string type)
        {
            if (cumulativeAmount != periodAmount + lastReportCorrectedCumulativeAmount)
            {
                throw new DomainException($"ContractReportIndicator.ValidateAmount: CumulativeAmount{type} is not correct!");
            }

            if (residueAmount != targetValue - cumulativeAmount + baseValue)
            {
                throw new DomainException($"ContractReportIndicator.ValidateAmount: ResidueAmount{type} is not correct!");
            }
        }

        public void ValidateAmounts(
            decimal? periodAmountMen,
            decimal? periodAmountWomen,
            decimal? periodAmountTotal,
            decimal? cumulativeAmountMen,
            decimal? cumulativeAmountWomen,
            decimal? cumulativeAmountTotal,
            decimal? residueAmountMen,
            decimal? residueAmountWomen,
            decimal? residueAmountTotal,
            decimal? lastReportCumulativeAmountMen,
            decimal? lastReportCumulativeAmountWomen,
            decimal? lastReportCumulativeAmountTotal,
            string type,
            ContractIndicator contractIndicator)
        {
            if (!periodAmountTotal.HasValue ||
                !cumulativeAmountTotal.HasValue ||
                !residueAmountTotal.HasValue)
            {
                throw new DomainException($"ContractReportIndicator has no total amount data!");
            }

            if (this.HasGenderDivision)
            {
                if (!periodAmountMen.HasValue ||
                    !periodAmountWomen.HasValue ||
                    !cumulativeAmountMen.HasValue ||
                    !cumulativeAmountWomen.HasValue ||
                    !residueAmountMen.HasValue ||
                    !residueAmountWomen.HasValue ||
                    !this.LastReportCumulativeAmountMen.HasValue ||
                    !this.LastReportCumulativeAmountWomen.HasValue)
                {
                    throw new DomainException($"ContractReportIndicator has gender division but has no gender division data!");
                }

                if (!contractIndicator.BaseMenValue.HasValue ||
                    !contractIndicator.BaseWomenValue.HasValue ||
                    !contractIndicator.TargetMenValue.HasValue ||
                    !contractIndicator.TargetWomenValue.HasValue)
                {
                    throw new DomainException($"ContractReportIndicator has gender division but ContractIndicator has no gender division data!");
                }

                // we do not validate "amountMen + amountWomen = amountTotal" for cumulative and residue amounts as this might not hold
                // for contract report indicators whose parent indicator has changed from HasGenderDivision=false to HasGenderDivision=true
                this.ValidateTotalAmount(
                    periodAmountMen.Value,
                    periodAmountWomen.Value,
                    periodAmountTotal.Value,
                    type + "Period");

                this.ValidateAmount(
                    periodAmountMen.Value,
                    cumulativeAmountMen.Value,
                    residueAmountMen.Value,
                    lastReportCumulativeAmountMen.Value,
                    contractIndicator.BaseMenValue.Value,
                    contractIndicator.TargetMenValue.Value,
                    type + "Men");

                this.ValidateAmount(
                    periodAmountWomen.Value,
                    cumulativeAmountWomen.Value,
                    residueAmountWomen.Value,
                    lastReportCumulativeAmountWomen.Value,
                    contractIndicator.BaseWomenValue.Value,
                    contractIndicator.TargetWomenValue.Value,
                    type + "Women");
            }
            else
            {
                if (!contractIndicator.BaseTotalValue.HasValue ||
                    !contractIndicator.TargetTotalValue.HasValue)
                {
                    throw new DomainException($"ContractReportIndicator has no gender division but ContractIndicator has no total data!");
                }

                this.ValidateAmount(
                    periodAmountTotal.Value,
                    cumulativeAmountTotal.Value,
                    residueAmountTotal.Value,
                    lastReportCumulativeAmountTotal.Value,
                    contractIndicator.BaseTotalValue.Value,
                    contractIndicator.TargetTotalValue.Value,
                    type + "Total");
            }
        }
    }
}
