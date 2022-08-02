namespace Eumis.Data.Monitoring.ViewObjects
{
    public class BeneficiaryProjectsContractsReportItem
    {
        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyType { get; set; }

        public string CompanyLegalType { get; set; }

        public int? ProjectsCount { get; set; }

        public decimal? ProjectsTotalAmount { get; set; }

        public decimal? ProjectsBfpAmount { get; set; }

        public decimal? ProjectsSelfAmount { get; set; }

        public int? ContractsCount { get; set; }

        public decimal? ContractsTotalAmount { get; set; }

        public decimal? ContractsEuAmount { get; set; }

        public decimal? ContractsBgAmount { get; set; }

        public decimal? ContractsSelfAmount { get; set; }

        public decimal? ActuallyPaidTotalAmount { get; set; }

        public decimal? ActuallyPaidEuAmount { get; set; }

        public decimal? ActuallyPaidBgAmount { get; set; }

        public int? IrregularitySignalsCount { get; set; }

        public int? IrregularitySignalsActiveCount { get; set; }

        public int? IrregularitiesCount { get; set; }

        public decimal? FinancialCorrectionTotalAmount { get; set; }

        public decimal? FinancialCorrectionBfpAmount { get; set; }

        public decimal? FinancialCorrectionSelfAmount { get; set; }

        public decimal? CorrectionsTotalAmount { get; set; }

        public decimal? CorrectionsBfpAmount { get; set; }

        public decimal? CorrectionsSelfAmount { get; set; }

        public bool HasProjects { get; set; }

        public bool HasContracts { get; set; }
    }
}