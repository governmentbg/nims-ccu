using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialRevalidationDO
    {
        public ContractReportFinancialRevalidationDO()
        {
        }

        public ContractReportFinancialRevalidationDO(
            ContractReportFinancialRevalidation contractReportFinancialRevalidation,
            ContractReportFinancial contractReportFinancial,
            ContractReportPayment contractReportPayment,
            string username)
        {
            this.ContractReportFinancialRevalidationId = contractReportFinancialRevalidation.ContractReportFinancialRevalidationId;
            this.ContractReportId = contractReportFinancialRevalidation.ContractReportId;
            this.ContractId = contractReportFinancialRevalidation.ContractId;

            this.OrderNum = contractReportFinancialRevalidation.OrderNum;
            this.Status = contractReportFinancialRevalidation.Status;
            this.RevalidationDate = contractReportFinancialRevalidation.RevalidationDate;

            if (contractReportFinancialRevalidation.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportFinancialRevalidation.File.Key,
                    Name = contractReportFinancialRevalidation.File.FileName,
                };
            }

            this.Notes = contractReportFinancialRevalidation.Notes;
            this.CheckedByUser = username;
            this.CheckedDate = contractReportFinancialRevalidation.CheckedDate;

            if (contractReportFinancial != null)
            {
                this.ContractReportFinancial = new ContractReportFinancialDO(contractReportFinancial);
            }

            if (contractReportPayment != null)
            {
                this.ContractReportPayment = new ContractReportPaymentDO(contractReportPayment);
            }

            this.CreateDate = contractReportFinancialRevalidation.CreateDate;
            this.ModifyDate = contractReportFinancialRevalidation.ModifyDate;
            this.Version = contractReportFinancialRevalidation.Version;
        }

        public int ContractReportFinancialRevalidationId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportFinancialRevalidationStatus Status { get; set; }

        public DateTime? RevalidationDate { get; set; }

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
