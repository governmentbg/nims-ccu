using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialCorrectionDO
    {
        public ContractReportFinancialCorrectionDO()
        {
        }

        public ContractReportFinancialCorrectionDO(
            ContractReportFinancialCorrection contractReportFinancialCorrection,
            ContractReportFinancial contractReportFinancial,
            ContractReportPayment contractReportPayment,
            string username)
        {
            this.ContractReportFinancialCorrectionId = contractReportFinancialCorrection.ContractReportFinancialCorrectionId;
            this.ContractReportId = contractReportFinancialCorrection.ContractReportId;
            this.ContractId = contractReportFinancialCorrection.ContractId;

            this.OrderNum = contractReportFinancialCorrection.OrderNum;
            this.Status = contractReportFinancialCorrection.Status;
            this.CorrectionDate = contractReportFinancialCorrection.CorrectionDate;

            if (contractReportFinancialCorrection.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportFinancialCorrection.File.Key,
                    Name = contractReportFinancialCorrection.File.FileName,
                };
            }

            this.Notes = contractReportFinancialCorrection.Notes;
            this.CheckedByUser = username;
            this.CheckedDate = contractReportFinancialCorrection.CheckedDate;

            if (contractReportFinancial != null)
            {
                this.ContractReportFinancial = new ContractReportFinancialDO(contractReportFinancial);
            }

            if (contractReportPayment != null)
            {
                this.ContractReportPayment = new ContractReportPaymentDO(contractReportPayment);
            }

            this.CreateDate = contractReportFinancialCorrection.CreateDate;
            this.ModifyDate = contractReportFinancialCorrection.ModifyDate;
            this.Version = contractReportFinancialCorrection.Version;
        }

        public int ContractReportFinancialCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportFinancialCorrectionStatus Status { get; set; }

        public DateTime? CorrectionDate { get; set; }

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
