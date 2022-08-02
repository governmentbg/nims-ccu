using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialDO
    {
        public ContractReportFinancialDO()
        {
        }

        public ContractReportFinancialDO(ContractReportFinancial contractReportFinancial)
        {
            this.ContractReportFinancialId = contractReportFinancial.ContractReportFinancialId;
            this.ContractReportId = contractReportFinancial.ContractReportId;
            this.ContractId = contractReportFinancial.ContractId;
            this.XmlGid = contractReportFinancial.Gid;
            this.VersionNum = contractReportFinancial.VersionNum;
            this.VersionSubNum = contractReportFinancial.VersionSubNum;
            this.Status = contractReportFinancial.Status;
            this.StatusNote = contractReportFinancial.StatusNote;

            this.StartDate = contractReportFinancial.StartDate;
            this.EndDate = contractReportFinancial.EndDate;
            this.SubmitDate = contractReportFinancial.SubmitDate;

            this.CreateDate = contractReportFinancial.CreateDate;
            this.Version = contractReportFinancial.Version;
        }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid XmlGid { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public ContractReportFinancialStatus Status { get; set; }

        public string StatusNote { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
