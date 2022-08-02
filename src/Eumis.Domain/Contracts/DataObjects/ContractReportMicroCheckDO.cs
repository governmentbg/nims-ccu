using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportMicroCheckDO
    {
        public ContractReportMicroCheckDO()
        {
        }

        public ContractReportMicroCheckDO(ContractReportMicroCheck contractReportMicroCheck, ContractReportMicro contractReportMicro, string checkedByUser)
        {
            this.ContractReportMicroCheckId = contractReportMicroCheck.ContractReportMicroCheckId;
            this.ContractReportMicroId = contractReportMicroCheck.ContractReportMicroId;
            this.ContractReportId = contractReportMicroCheck.ContractReportId;
            this.ContractId = contractReportMicroCheck.ContractId;

            this.OrderNum = contractReportMicroCheck.OrderNum;
            this.Status = contractReportMicroCheck.Status;
            this.Approval = contractReportMicroCheck.Approval;

            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportMicroCheck.CheckedDate;

            if (contractReportMicroCheck.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportMicroCheck.File.Key,
                    Name = contractReportMicroCheck.File.FileName,
                };
            }

            this.ContractReportMicro = new ContractReportMicroDO(contractReportMicro);

            this.Version = contractReportMicroCheck.Version;
        }

        public int ContractReportMicroCheckId { get; set; }

        public int ContractReportMicroId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportMicroCheckStatus Status { get; set; }

        public ContractReportMicroCheckApproval? Approval { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public FileDO File { get; set; }

        public ContractReportMicroDO ContractReportMicro { get; set; }

        public byte[] Version { get; set; }
    }
}
