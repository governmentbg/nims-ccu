using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportDO
    {
        public ContractReportDO()
        {
        }

        public ContractReportDO(ContractReport contractReport)
        {
            this.ContractReportId = contractReport.ContractReportId;
            this.ContractId = contractReport.ContractId;
            this.Gid = contractReport.Gid;
            this.ReportType = contractReport.ReportType;
            this.OrderNum = contractReport.OrderNum;
            this.Status = contractReport.Status;
            this.StatusNote = contractReport.StatusNote;

            this.Source = contractReport.Source;
            this.OtherRegistration = contractReport.OtherRegistration;
            this.StoragePlace = contractReport.StoragePlace;
            this.SubmitDate = contractReport.SubmitDate;
            this.SubmitDeadline = contractReport.SubmitDeadline;
            this.CheckedDate = contractReport.CheckedDate;

            this.CreateDate = contractReport.CreateDate;
            this.Version = contractReport.Version;
        }

        public int? ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public ContractReportType? ReportType { get; set; }

        public int? OrderNum { get; set; }

        public ContractReportStatus? Status { get; set; }

        public string StatusNote { get; set; }

        public Source Source { get; set; }

        public string OtherRegistration { get; set; }

        public string StoragePlace { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? SubmitDeadline { get; set; }

        public DateTime? CheckedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public byte[] Version { get; set; }
    }
}
