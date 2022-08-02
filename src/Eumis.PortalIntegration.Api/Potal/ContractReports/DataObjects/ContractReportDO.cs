using System;
using Eumis.Domain.Contracts;
using Eumis.PortalIntegration.Api.Core;

namespace Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects
{
    public class ContractReportDO
    {
        public ContractReportDO()
        {
        }

        public ContractReportDO(ContractReport contractReport)
        {
            this.ContractId = contractReport.ContractId;
            this.Gid = contractReport.Gid;
            this.ContractReportType = new EnumDO<ContractReportType>()
            {
                Value = contractReport.ReportType,
                Description = contractReport.ReportType,
            };
            this.OrderNum = contractReport.OrderNum;
            this.Status = new EnumDO<ContractReportStatus>()
            {
                Value = contractReport.Status,
                Description = contractReport.Status,
            };
            this.StatusNote = contractReport.StatusNote;

            this.Source = new EnumDO<Source>()
            {
                Value = contractReport.Source,
                Description = contractReport.Source,
            };
            this.OtherRegistration = contractReport.OtherRegistration;
            this.StoragePlace = contractReport.StoragePlace;
            this.SubmitDate = contractReport.SubmitDate;
            this.SubmitDeadline = contractReport.SubmitDeadline;

            this.CreateDate = contractReport.CreateDate;
            this.Version = contractReport.Version;
        }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public EnumDO<ContractReportType> ContractReportType { get; set; }

        public int? OrderNum { get; set; }

        public EnumDO<ContractReportStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public EnumDO<Source> Source { get; set; }

        public string OtherRegistration { get; set; }

        public string StoragePlace { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? SubmitDeadline { get; set; }

        public DateTime CreateDate { get; set; }

        public byte[] Version { get; set; }
    }
}
