using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialCertCorrectionDO
    {
        public ContractReportFinancialCertCorrectionDO()
        {
        }

        public ContractReportFinancialCertCorrectionDO(
            ContractReportFinancialCertCorrection contractReportFinancialCertCorrection,
            ContractReportFinancial contractReportFinancial,
            ContractReportPayment contractReportPayment,
            string username)
        {
            this.ContractReportFinancialCertCorrectionId = contractReportFinancialCertCorrection.ContractReportFinancialCertCorrectionId;
            this.ContractReportId = contractReportFinancialCertCorrection.ContractReportId;
            this.ContractId = contractReportFinancialCertCorrection.ContractId;

            this.OrderNum = contractReportFinancialCertCorrection.OrderNum;
            this.Status = contractReportFinancialCertCorrection.Status;
            this.CertCorrectionDate = contractReportFinancialCertCorrection.CertCorrectionDate;

            if (contractReportFinancialCertCorrection.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportFinancialCertCorrection.File.Key,
                    Name = contractReportFinancialCertCorrection.File.FileName,
                };
            }

            this.Notes = contractReportFinancialCertCorrection.Notes;
            this.CheckedByUser = username;
            this.CheckedDate = contractReportFinancialCertCorrection.CheckedDate;

            if (contractReportFinancial != null)
            {
                this.ContractReportFinancial = new ContractReportFinancialDO(contractReportFinancial);
            }

            if (contractReportPayment != null)
            {
                this.ContractReportPayment = new ContractReportPaymentDO(contractReportPayment);
            }

            this.CreateDate = contractReportFinancialCertCorrection.CreateDate;
            this.ModifyDate = contractReportFinancialCertCorrection.ModifyDate;
            this.Version = contractReportFinancialCertCorrection.Version;
        }

        public int ContractReportFinancialCertCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportFinancialCertCorrectionStatus Status { get; set; }

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
