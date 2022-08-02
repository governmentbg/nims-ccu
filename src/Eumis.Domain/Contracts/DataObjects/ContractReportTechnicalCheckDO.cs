using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportTechnicalCheckDO
    {
        public ContractReportTechnicalCheckDO()
        {
        }

        public ContractReportTechnicalCheckDO(ContractReportTechnicalCheck contractReportTechnicalCheck, ContractReportTechnical contractReportTechnical, string checkedByUser)
        {
            this.ContractReportTechnicalCheckId = contractReportTechnicalCheck.ContractReportTechnicalCheckId;
            this.ContractReportTechnicalId = contractReportTechnicalCheck.ContractReportTechnicalId;
            this.ContractReportId = contractReportTechnicalCheck.ContractReportId;
            this.ContractId = contractReportTechnicalCheck.ContractId;

            this.OrderNum = contractReportTechnicalCheck.OrderNum;
            this.Status = contractReportTechnicalCheck.Status;
            this.Approval = contractReportTechnicalCheck.Approval;

            if (contractReportTechnicalCheck.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportTechnicalCheck.File.Key,
                    Name = contractReportTechnicalCheck.File.FileName,
                };
            }

            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportTechnicalCheck.CheckedDate;

            this.ContractReportTechnical = new ContractReportTechnicalDO(contractReportTechnical);

            this.CreateDate = contractReportTechnicalCheck.CreateDate;
            this.Version = contractReportTechnicalCheck.Version;
        }

        public int ContractReportTechnicalCheckId { get; set; }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportTechnicalCheckStatus Status { get; set; }

        public ContractReportTechnicalCheckApproval? Approval { get; set; }

        public FileDO File { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public ContractReportTechnicalDO ContractReportTechnical { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
