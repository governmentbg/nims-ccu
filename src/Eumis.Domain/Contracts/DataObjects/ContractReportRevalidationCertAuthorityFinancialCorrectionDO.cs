using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionDO
    {
        public ContractReportRevalidationCertAuthorityFinancialCorrectionDO()
        {
        }

        public ContractReportRevalidationCertAuthorityFinancialCorrectionDO(
            ContractReportRevalidationCertAuthorityFinancialCorrection contractReportRevalidationCertAuthorityFinancialCorrection,
            ContractReportFinancial contractReportFinancial,
            ContractReportPayment contractReportPayment)
        {
            this.ContractReportRevalidationCertAuthorityFinancialCorrectionId = contractReportRevalidationCertAuthorityFinancialCorrection.ContractReportRevalidationCertAuthorityFinancialCorrectionId;
            this.ContractReportId = contractReportRevalidationCertAuthorityFinancialCorrection.ContractReportId;
            this.ContractId = contractReportRevalidationCertAuthorityFinancialCorrection.ContractId;

            this.OrderNum = contractReportRevalidationCertAuthorityFinancialCorrection.OrderNum;
            this.Status = contractReportRevalidationCertAuthorityFinancialCorrection.Status;
            this.CertCorrectionDate = contractReportRevalidationCertAuthorityFinancialCorrection.CertCorrectionDate;

            if (contractReportRevalidationCertAuthorityFinancialCorrection.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportRevalidationCertAuthorityFinancialCorrection.File.Key,
                    Name = contractReportRevalidationCertAuthorityFinancialCorrection.File.FileName,
                };
            }

            this.Notes = contractReportRevalidationCertAuthorityFinancialCorrection.Notes;
            this.CheckedByUserId = contractReportRevalidationCertAuthorityFinancialCorrection.CheckedByUserId;
            this.CheckedDate = contractReportRevalidationCertAuthorityFinancialCorrection.CheckedDate;

            if (contractReportFinancial != null)
            {
                this.ContractReportFinancial = new ContractReportFinancialDO(contractReportFinancial);
            }

            if (contractReportPayment != null)
            {
                this.ContractReportPayment = new ContractReportPaymentDO(contractReportPayment);
            }

            this.CreateDate = contractReportRevalidationCertAuthorityFinancialCorrection.CreateDate;
            this.ModifyDate = contractReportRevalidationCertAuthorityFinancialCorrection.ModifyDate;
            this.Version = contractReportRevalidationCertAuthorityFinancialCorrection.Version;
        }

        public int ContractReportRevalidationCertAuthorityFinancialCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportRevalidationCertAuthorityFinancialCorrectionStatus Status { get; set; }

        public DateTime? CertCorrectionDate { get; set; }

        public FileDO File { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public ContractReportFinancialDO ContractReportFinancial { get; set; }

        public ContractReportPaymentDO ContractReportPayment { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
