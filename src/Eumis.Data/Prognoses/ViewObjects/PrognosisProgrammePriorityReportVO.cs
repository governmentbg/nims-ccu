using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Prognoses.ViewObjects
{
    public class PrognosisProgrammePriorityReportVO
    {
        public PrognosisProgrammePriorityReportVO(
            string programmePriority,
            Year year,
            Quarter quarter,
            decimal nextThreeWithAdvances,
            decimal nextThreeWithoutAdvances,
            decimal prognosedContractedBfpAmount,
            decimal contractsBfpAmount,
            decimal prognosedApprovedBfpAmount,
            decimal approvedBfpAmount,
            decimal prognosedCertifiedBfpAmount,
            decimal certifiedBfpAmount)
        {
            this.ProgrammePriority = programmePriority;
            this.Year = year;
            this.Quarter = quarter;
            this.NextThreeWithAdvances = nextThreeWithAdvances;
            this.NextThreeWithoutAdvances = nextThreeWithoutAdvances;

            this.PrognosedContractedBfpAmount = prognosedContractedBfpAmount;
            this.ContractsBfpAmount = contractsBfpAmount;
            this.ContractedPercent = prognosedContractedBfpAmount == 0 ? 0 : contractsBfpAmount / prognosedContractedBfpAmount;

            this.PrognosedApprovedBfpAmount = prognosedApprovedBfpAmount;
            this.ApprovedBfpAmount = approvedBfpAmount;
            this.ApprovedPercent = prognosedApprovedBfpAmount == 0 ? 0 : approvedBfpAmount / prognosedApprovedBfpAmount;

            this.PrognosedCertifiedBfpAmount = prognosedCertifiedBfpAmount;
            this.CertifiedBfpAmount = certifiedBfpAmount;
            this.CertifiedPercent = prognosedCertifiedBfpAmount == 0 ? 0 : certifiedBfpAmount / prognosedCertifiedBfpAmount;
        }

        public string ProgrammePriority { get; set; }

        public Year Year { get; set; }

        public Quarter Quarter { get; set; }

        public decimal NextThreeWithAdvances { get; set; }

        public decimal NextThreeWithoutAdvances { get; set; }

        public decimal PrognosedContractedBfpAmount { get; set; }

        public decimal ContractsBfpAmount { get; set; }

        public decimal ContractedPercent { get; set; }

        public decimal PrognosedApprovedBfpAmount { get; set; }

        public decimal ApprovedBfpAmount { get; set; }

        public decimal ApprovedPercent { get; set; }

        public decimal PrognosedCertifiedBfpAmount { get; set; }

        public decimal CertifiedBfpAmount { get; set; }

        public decimal CertifiedPercent { get; set; }
    }
}
