using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialCheckDO
    {
        public ContractReportFinancialCheckDO()
        {
        }

        public ContractReportFinancialCheckDO(ContractReportFinancialCheck contractReportFinancialCheck, ContractReportFinancial contractReportFinancial, string checkedByUser)
        {
            this.ContractReportFinancialCheckId = contractReportFinancialCheck.ContractReportFinancialCheckId;
            this.ContractReportFinancialId = contractReportFinancialCheck.ContractReportFinancialId;
            this.ContractReportId = contractReportFinancialCheck.ContractReportId;
            this.ContractId = contractReportFinancialCheck.ContractId;

            this.OrderNum = contractReportFinancialCheck.OrderNum;
            this.Status = contractReportFinancialCheck.Status;
            this.Approval = contractReportFinancialCheck.Approval;

            if (contractReportFinancialCheck.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportFinancialCheck.File.Key,
                    Name = contractReportFinancialCheck.File.FileName,
                };
            }

            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportFinancialCheck.CheckedDate;

            this.ContractReportFinancial = new ContractReportFinancialDO(contractReportFinancial);

            this.CreateDate = contractReportFinancialCheck.CreateDate;
            this.Version = contractReportFinancialCheck.Version;
        }

        public int ContractReportFinancialCheckId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportFinancialCheckStatus Status { get; set; }

        public ContractReportFinancialCheckApproval? Approval { get; set; }

        public FileDO File { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public ContractReportFinancialDO ContractReportFinancial { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
