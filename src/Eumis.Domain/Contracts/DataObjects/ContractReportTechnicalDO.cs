using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportTechnicalDO
    {
        public ContractReportTechnicalDO()
        {
        }

        public ContractReportTechnicalDO(ContractReportTechnical contractReportTechnical)
        {
            this.ContractReportTechnicalId = contractReportTechnical.ContractReportTechnicalId;
            this.ContractReportId = contractReportTechnical.ContractReportId;
            this.ContractId = contractReportTechnical.ContractId;
            this.XmlGid = contractReportTechnical.Gid;
            this.VersionNum = contractReportTechnical.VersionNum;
            this.VersionSubNum = contractReportTechnical.VersionSubNum;
            this.Status = contractReportTechnical.Status;
            this.StatusNote = contractReportTechnical.StatusNote;

            this.Type = contractReportTechnical.Type;
            this.RegDate = contractReportTechnical.RegDate;
            this.SubmitDate = contractReportTechnical.SubmitDate;
            this.DateFrom = contractReportTechnical.DateFrom;
            this.DateTo = contractReportTechnical.DateTo;

            this.CreateDate = contractReportTechnical.CreateDate;
            this.Version = contractReportTechnical.Version;
        }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid XmlGid { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public ContractReportTechnicalStatus Status { get; set; }

        public string StatusNote { get; set; }

        public ContractReportTechnicalType? Type { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
