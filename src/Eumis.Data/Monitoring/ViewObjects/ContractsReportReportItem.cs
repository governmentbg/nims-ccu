using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class ContractsReportReportItem
    {
        public ContractsReportReportItem(
            string programme,
            string programmePriority,
            string procedure,
            int orderNum,
            string contractRegNumber,
            string contractName,
            ContractExecutionStatus? contractExecutionStatus,
            string companyUin,
            string companyName,
            decimal? requestedAmount,
            decimal? reportedEuAmount,
            decimal? reportedBgAmount,
            decimal? reportedSelfAmount,
            decimal? verifiedEuAmount,
            decimal? verifiedBgAmount,
            decimal? verifiedSelfAmount,
            decimal? verifiedEuAdvancePaymentAmount,
            decimal? verifiedBgAdvancePaymentAmount,
            decimal? verifiedSelfAdvancePaymentAmount,
            decimal? certEuAmount,
            decimal? certBgAmount,
            decimal? certSelfAmount,
            decimal? certEuAdvancePaymentAmount,
            decimal? certBgAdvancePaymentAmount,
            decimal? certSelfAdvancePaymentAmount,
            decimal? paidEuAmount,
            decimal? paidBgAmount,
            decimal? paidSelfAmount,
            string certReportNumber,
            ContractReportStatus? status,
            ContractReportType? reportType,
            DateTime? submitDate,
            DateTime? dateFrom,
            DateTime? dateTo)
        {
            this.Programme = programme;
            this.ProgrammePriority = programmePriority;
            this.Procedure = procedure;
            this.RegNumber = orderNum.ToString();
            this.ContractRegNumber = contractRegNumber;
            this.ContractName = contractName;
            this.ContractExecutionStatus = contractExecutionStatus;
            this.CompanyUin = companyUin;
            this.CompanyName = companyName;
            this.RequestedAmount = requestedAmount;
            this.ReportedTotalAmount = reportedEuAmount + reportedBgAmount + reportedSelfAmount;
            this.ReportedBfpAmount = reportedEuAmount + reportedBgAmount;
            this.ReportedEuAmount = reportedEuAmount;
            this.ReportedBgAmount = reportedBgAmount;
            this.ReportedSelfAmount = reportedSelfAmount;
            this.VerifiedTotalAmount = verifiedEuAmount + verifiedBgAmount + verifiedSelfAmount;
            this.VerifiedBfpAmount = verifiedEuAmount + verifiedBgAmount;
            this.VerifiedEuAmount = verifiedEuAmount;
            this.VerifiedBgAmount = verifiedBgAmount;
            this.VerifiedSelfAmount = verifiedSelfAmount;

            this.VerifiedTotalAdvancePaymentAmount = verifiedEuAdvancePaymentAmount + verifiedBgAdvancePaymentAmount + verifiedSelfAdvancePaymentAmount;
            this.VerifiedBfpAdvancePaymentAmount = verifiedEuAdvancePaymentAmount + verifiedBgAdvancePaymentAmount;
            this.VerifiedEuAdvancePaymentAmount = verifiedEuAdvancePaymentAmount;
            this.VerifiedBgAdvancePaymentAmount = verifiedBgAdvancePaymentAmount;
            this.VerifiedSelfAdvancePaymentAmount = verifiedSelfAdvancePaymentAmount;

            this.CertTotalAmount = certEuAmount + certBgAmount + certSelfAmount;
            this.CertBfpAmount = certEuAmount + certBgAmount;
            this.CertEuAmount = certEuAmount;
            this.CertBgAmount = certBgAmount;
            this.CertSelfAmount = certSelfAmount;

            this.CertTotalAdvancePaymentAmount = certEuAdvancePaymentAmount + certBgAdvancePaymentAmount + certSelfAdvancePaymentAmount;
            this.CertBfpAdvancePaymentAmount = certEuAdvancePaymentAmount + certBgAdvancePaymentAmount;
            this.CertEuAdvancePaymentAmount = certEuAdvancePaymentAmount;
            this.CertBgAdvancePaymentAmount = certBgAdvancePaymentAmount;
            this.CertSelfAdvancePaymentAmount = certSelfAdvancePaymentAmount;

            this.PaidTotalAmount = paidEuAmount + paidBgAmount + paidSelfAmount;
            this.PaidBfpAmount = paidEuAmount + paidBgAmount;
            this.PaidEuAmount = paidEuAmount;
            this.PaidBgAmount = paidBgAmount;
            this.PaidSelfAmount = paidSelfAmount;
            this.CertReportNumber = certReportNumber;
            this.Status = status;
            this.ReportType = reportType;
            this.SubmitDate = submitDate;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
        }

        public string Programme { get; set; }

        public string ProgrammePriority { get; set; }

        public string Procedure { get; set; }

        public string RegNumber { get; set; }

        public string ContractRegNumber { get; set; }

        public string ContractName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractExecutionStatus? ContractExecutionStatus { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyName { get; set; }

        public decimal? RequestedAmount { get; set; }

        public decimal? ReportedTotalAmount { get; set; }

        public decimal? ReportedBfpAmount { get; set; }

        public decimal? ReportedEuAmount { get; set; }

        public decimal? ReportedBgAmount { get; set; }

        public decimal? ReportedSelfAmount { get; set; }

        public decimal? VerifiedTotalAmount { get; set; }

        public decimal? VerifiedBfpAmount { get; set; }

        public decimal? VerifiedEuAmount { get; set; }

        public decimal? VerifiedBgAmount { get; set; }

        public decimal? VerifiedSelfAmount { get; set; }

        public decimal? VerifiedTotalAdvancePaymentAmount { get; set; }

        public decimal? VerifiedBfpAdvancePaymentAmount { get; set; }

        public decimal? VerifiedEuAdvancePaymentAmount { get; set; }

        public decimal? VerifiedBgAdvancePaymentAmount { get; set; }

        public decimal? VerifiedSelfAdvancePaymentAmount { get; set; }

        public decimal? CertTotalAmount { get; set; }

        public decimal? CertBfpAmount { get; set; }

        public decimal? CertEuAmount { get; set; }

        public decimal? CertBgAmount { get; set; }

        public decimal? CertSelfAmount { get; set; }

        public decimal? CertTotalAdvancePaymentAmount { get; set; }

        public decimal? CertBfpAdvancePaymentAmount { get; set; }

        public decimal? CertEuAdvancePaymentAmount { get; set; }

        public decimal? CertBgAdvancePaymentAmount { get; set; }

        public decimal? CertSelfAdvancePaymentAmount { get; set; }

        public decimal? PaidTotalAmount { get; set; }

        public decimal? PaidBfpAmount { get; set; }

        public decimal? PaidEuAmount { get; set; }

        public decimal? PaidBgAmount { get; set; }

        public decimal? PaidSelfAmount { get; set; }

        public string CertReportNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportStatus? Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportType? ReportType { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
