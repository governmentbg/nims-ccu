using System;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.Data.ContractReports.PortalViewObjects
{
    public class ContractReportMicroPVO
    {
        public ContractReportMicroPVO(ContractReportMicro contractReportMicro)
        {
            this.CreateDate = contractReportMicro.CreateDate;
            this.ModifyDate = contractReportMicro.ModifyDate;
            this.VersionNum = contractReportMicro.VersionNum;
            this.VersionSubNum = contractReportMicro.VersionSubNum;
            this.Status = new EnumPVO<ContractReportMicroStatus>()
            {
                Description = contractReportMicro.Status,
                Value = contractReportMicro.Status,
            };
            this.StatusNote = contractReportMicro.StatusNote;
            this.Gid = contractReportMicro.Gid;
            this.ExcelBlobKey = contractReportMicro.ExcelBlobKey;
            this.ExcelName = contractReportMicro.ExcelName;
            this.IsFromExternalSystem = contractReportMicro.IsFromExternalSystem;
            this.Version = contractReportMicro.Version;
        }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public EnumPVO<ContractReportMicroStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public Guid Gid { get; set; }

        public Guid? ExcelBlobKey { get; set; }

        public string ExcelName { get; set; }

        public bool IsFromExternalSystem { get; set; }

        public byte[] Version { get; set; }
    }
}
