using System;

namespace Eumis.Data.Prognoses.ViewObjects
{
    public class PrognosisSummaryReportVO
    {
        public PrognosisSummaryReportVO(
            int procedureId,
            string procedureName,
            string procedureNameAlt,
            string programme,
            string programmePriority,
            decimal procedureBudget,
            decimal proceduresTotalBudget,
            int projectsCount,
            int approvedProjectsCount,
            decimal approvedProjectsBudget,
            int contractsCount,
            decimal contractsBfpBudget,
            decimal prognosedContractedBfpAmount,
            decimal paymentsBfpAmount,
            decimal approvedPaymentsBfpAmount,
            decimal approvedBfpAmount,
            decimal actuallyPaidAmount,
            decimal prognosedApprovedBfpAmount,
            decimal requestedBfpAmount,
            decimal certifiedBfpAmount,
            decimal prognosedCertifiedBfpAmount)
        {
            this.ProcedureId = procedureId;
            this.ProcedureName = procedureName;
            this.ProcedureNameAlt = procedureNameAlt;
            this.Programme = programme;
            this.ProgrammePriority = programmePriority;
            this.ProcedureBudget = procedureBudget;
            this.EuPercent = procedureBudget == 0 ? 0 : procedureBudget;
            this.ProcedurePercent = proceduresTotalBudget == 0 ? 0 : procedureBudget / proceduresTotalBudget;

            this.ProjectsCount = projectsCount;
            this.ApprovedProjectsCount = approvedProjectsCount;
            this.ApprovedProjectsBudget = approvedProjectsBudget;
            this.ApprovedProjectsPercent = procedureBudget == 0 ? 0 : approvedProjectsBudget / procedureBudget;

            this.ContractsCount = contractsCount;
            this.ContractsBfpBudget = contractsBfpBudget;
            this.PrognosedContractedBfpAmount = prognosedContractedBfpAmount;
            this.PrognosedContractedPercent = prognosedContractedBfpAmount == 0 ? 0 : contractsBfpBudget / prognosedContractedBfpAmount;

            this.PaymentsBfpAmount = paymentsBfpAmount;
            this.ApprovedPaymentsBfpAmount = approvedPaymentsBfpAmount;

            this.ApprovedBfpAmount = approvedBfpAmount;
            this.ActuallyPaidAmount = actuallyPaidAmount;
            this.PrognosedApprovedBfpAmount = prognosedApprovedBfpAmount;
            this.PrognosedApprovedPercent = prognosedApprovedBfpAmount == 0 ? 0 : approvedBfpAmount / prognosedApprovedBfpAmount;

            this.RequestedBfpAmount = requestedBfpAmount;
            this.CertifiedBfpAmount = certifiedBfpAmount;
            this.CertifiedPercent = proceduresTotalBudget == 0 ? 0 : certifiedBfpAmount / proceduresTotalBudget;
            this.PrognosedCertifiedBfpAmount = prognosedCertifiedBfpAmount;
            this.PrognosedCertifiedPercent = prognosedCertifiedBfpAmount == 0 ? 0 : certifiedBfpAmount / prognosedCertifiedBfpAmount;
        }

        public int ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string Programme { get; set; }

        public string ProgrammePriority { get; set; }

        public decimal ProcedureBudget { get; set; }

        public decimal EuPercent { get; set; }

        public decimal ProcedurePercent { get; set; }

        public int ProjectsCount { get; set; }

        public int ApprovedProjectsCount { get; set; }

        public decimal ApprovedProjectsBudget { get; set; }

        public decimal ApprovedProjectsPercent { get; set; }

        public int ContractsCount { get; set; }

        public decimal ContractsBfpBudget { get; set; }

        public decimal ContractsEuBudget { get; set; }

        public decimal ContractsEuPercent { get; set; }

        public decimal PrognosedContractedBfpAmount { get; set; }

        public decimal PrognosedContractedPercent { get; set; }

        public decimal PaymentsBfpAmount { get; set; }

        public decimal ApprovedPaymentsBfpAmount { get; set; }

        public decimal ApprovedBfpAmount { get; set; }

        public decimal ActuallyPaidAmount { get; set; }

        public decimal PrognosedApprovedBfpAmount { get; set; }

        public decimal PrognosedApprovedPercent { get; set; }

        public decimal RequestedBfpAmount { get; set; }

        public decimal CertifiedBfpAmount { get; set; }

        public decimal CertifiedPercent { get; set; }

        public decimal PrognosedCertifiedBfpAmount { get; set; }

        public decimal PrognosedCertifiedPercent { get; set; }
    }
}
