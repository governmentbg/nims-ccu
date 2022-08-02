using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportCertAuthorityFinancialCorrectionDO
    {
        public ContractReportCertAuthorityFinancialCorrectionDO()
        {
        }

        public ContractReportCertAuthorityFinancialCorrectionDO(
            ContractReportCertAuthorityFinancialCorrection contractReportCertAuthorityFinancialCorrection,
            ContractReportFinancial contractReportFinancial,
            ContractReportPayment contractReportPayment,
            string username)
        {
            this.ContractReportCertAuthorityFinancialCorrectionId = contractReportCertAuthorityFinancialCorrection.ContractReportCertAuthorityFinancialCorrectionId;
            this.ContractReportId = contractReportCertAuthorityFinancialCorrection.ContractReportId;
            this.ContractId = contractReportCertAuthorityFinancialCorrection.ContractId;

            this.OrderNum = contractReportCertAuthorityFinancialCorrection.OrderNum;
            this.Status = contractReportCertAuthorityFinancialCorrection.Status;
            this.CertCorrectionDate = contractReportCertAuthorityFinancialCorrection.CertCorrectionDate;

            if (contractReportCertAuthorityFinancialCorrection.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportCertAuthorityFinancialCorrection.File.Key,
                    Name = contractReportCertAuthorityFinancialCorrection.File.FileName,
                };
            }

            this.Notes = contractReportCertAuthorityFinancialCorrection.Notes;
            this.CheckedByUser = username;
            this.CheckedDate = contractReportCertAuthorityFinancialCorrection.CheckedDate;

            if (contractReportFinancial != null)
            {
                this.ContractReportFinancial = new ContractReportFinancialDO(contractReportFinancial);
            }

            if (contractReportPayment != null)
            {
                this.ContractReportPayment = new ContractReportPaymentDO(contractReportPayment);
            }

            this.CreateDate = contractReportCertAuthorityFinancialCorrection.CreateDate;
            this.ModifyDate = contractReportCertAuthorityFinancialCorrection.ModifyDate;
            this.Version = contractReportCertAuthorityFinancialCorrection.Version;
        }

        public int ContractReportCertAuthorityFinancialCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportCertAuthorityFinancialCorrectionStatus Status { get; set; }

        public DateTime? CertCorrectionDate { get; set; }

        public FileDO File { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public ContractReportFinancialDO ContractReportFinancial { get; set; }

        public ContractReportPaymentDO ContractReportPayment { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
