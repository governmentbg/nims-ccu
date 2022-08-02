using System.ComponentModel.DataAnnotations.Schema;

namespace Eumis.Web.Api.Monitoring.DataObjects
{
    public class ContractReportRow
    {
        [Column(Order = 1)]
        public string Programme { get; set; }

        [Column(Order = 2)]
        public string Procedure { get; set; }

        [Column(Order = 3)]
        public string RegNumber { get; set; }

        [Column(Order = 4)]
        public string Name { get; set; }

        [Column(Order = 5)]
        public string CompanyUin { get; set; }

        [Column(Order = 6)]
        public string CompanyName { get; set; }

        [Column(Order = 7)]
        public string CompanyType { get; set; }

        [Column(Order = 8)]
        public string CompanyLegalType { get; set; }

        [Column(Order = 9)]
        public string CompanyKidCode { get; set; }

        [Column(Order = 10)]
        public string CompanyAddress { get; set; }

        [Column(Order = 11)]
        public string CompanyCorrespondenceAddress { get; set; }

        [Column(Order = 12)]
        public string CompanyEmail { get; set; }

        [Column(Order = 13)]
        public string CompanySizeType { get; set; }

        [Column(Order = 14)]
        public int ProjectDuration { get; set; }

        [Column(Order = 15)]
        public string ProjectKidCode { get; set; }

        [Column(Order = 16)]
        public string InitialContractDate { get; set; }

        [Column(Order = 17)]
        public string ActualContractDate { get; set; }

        [Column(Order = 18)]
        public string InitialStartDate { get; set; }

        [Column(Order = 19)]
        public string InitialCompletionDate { get; set; }

        [Column(Order = 20)]
        public string ActualStartDate { get; set; }

        [Column(Order = 21)]
        public string ActualCompletionDate { get; set; }

        [Column(Order = 22)]
        public string ContractTerminationDate { get; set; }

        [Column(Order = 23)]
        public string ContractExecutionStatus { get; set; }

        [Column(Order = 24)]
        public string ContractBudgetLevel3AmountNutsFullPathName { get; set; }

        [Column(Order = 25)]
        public decimal InitialContractedTotalAmount { get; set; }

        [Column(Order = 26)]
        public decimal InitialContractedBfpTotalAmount { get; set; }

        [Column(Order = 27)]
        public decimal InitialContractedEuAmount { get; set; }

        [Column(Order = 28)]
        public decimal InitialContractedBgAmount { get; set; }

        [Column(Order = 29)]
        public decimal InitialContractedSelfAmount { get; set; }

        [Column(Order = 30)]
        public decimal ActualContractedTotalAmount { get; set; }

        [Column(Order = 31)]
        public decimal ActualContractedBfpTotalAmount { get; set; }

        [Column(Order = 32)]
        public decimal ActualContractedEuAmount { get; set; }

        [Column(Order = 33)]
        public decimal ActualContractedBgAmount { get; set; }

        [Column(Order = 34)]
        public decimal ActualContractedSelfAmount { get; set; }

        [Column(Order = 35)]
        public decimal ReportedTotalAmount { get; set; }

        [Column(Order = 36)]
        public decimal ReportedBfpTotalAmount { get; set; }

        [Column(Order = 37)]
        public decimal ReportedEuAmount { get; set; }

        [Column(Order = 38)]
        public decimal ReportedBgAmount { get; set; }

        [Column(Order = 39)]
        public decimal ReportedSelfAmount { get; set; }

        [Column(Order = 40)]
        public decimal ApprovedTotalAmount { get; set; }

        [Column(Order = 41)]
        public decimal ApprovedBfpTotalAmount { get; set; }

        [Column(Order = 42)]
        public decimal ApprovedEuAmount { get; set; }

        [Column(Order = 43)]
        public decimal ApprovedBgAmount { get; set; }

        [Column(Order = 44)]
        public decimal ApprovedSelfAmount { get; set; }

        [Column(Order = 45)]
        public decimal UnapprovedTotalAmount { get; set; }

        [Column(Order = 46)]
        public decimal UnapprovedBfpTotalAmount { get; set; }

        [Column(Order = 47)]
        public decimal UnapprovedEuAmount { get; set; }

        [Column(Order = 48)]
        public decimal UnapprovedBgAmount { get; set; }

        [Column(Order = 49)]
        public decimal UnapprovedSelfAmount { get; set; }

        [Column(Order = 50)]
        public decimal CorrectedTotalAmount { get; set; }

        [Column(Order = 51)]
        public decimal CorrectedBfpTotalAmount { get; set; }

        [Column(Order = 52)]
        public decimal CorrectedEuAmount { get; set; }

        [Column(Order = 53)]
        public decimal CorrectedBgAmount { get; set; }

        [Column(Order = 54)]
        public decimal CorrectedSelfAmount { get; set; }

        [Column(Order = 55)]
        public decimal CertifiedTotalAmount { get; set; }

        [Column(Order = 56)]
        public decimal CertifiedBfpTotalAmount { get; set; }

        [Column(Order = 57)]
        public decimal CertifiedEuAmount { get; set; }

        [Column(Order = 58)]
        public decimal CertifiedBgAmount { get; set; }

        [Column(Order = 59)]
        public decimal CertifiedSelfAmount { get; set; }

        [Column(Order = 60)]
        public decimal PaidAdvanceTotalAmount { get; set; }

        [Column(Order = 61)]
        public decimal PaidAdvanceEuAmount { get; set; }

        [Column(Order = 62)]
        public decimal PaidAdvanceBgAmount { get; set; }

        [Column(Order = 63)]
        public decimal PaidIntermediateTotalAmount { get; set; }

        [Column(Order = 64)]
        public decimal PaidIntermediateEuAmount { get; set; }

        [Column(Order = 65)]
        public decimal PaidIntermediateBgAmount { get; set; }

        [Column(Order = 66)]
        public decimal PaidFinalTotalAmount { get; set; }

        [Column(Order = 67)]
        public decimal PaidFinalEuAmount { get; set; }

        [Column(Order = 68)]
        public decimal PaidFinalBgAmount { get; set; }

        [Column(Order = 69)]
        public decimal ReimbursedPrincipalTotalAmount { get; set; }

        [Column(Order = 70)]
        public decimal ReimbursedPrincipalEuAmount { get; set; }

        [Column(Order = 71)]
        public decimal ReimbursedPrincipalBgAmount { get; set; }

        [Column(Order = 72)]
        public decimal ReimbursedInterestTotalAmount { get; set; }

        [Column(Order = 73)]
        public decimal ReimbursedInterestEuAmount { get; set; }

        [Column(Order = 74)]
        public decimal ReimbursedInterestBgAmount { get; set; }
    }
}
