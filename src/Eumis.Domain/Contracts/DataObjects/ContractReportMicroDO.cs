using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportMicroDO
    {
        public ContractReportMicroDO()
        {
        }

        public ContractReportMicroDO(ContractReportMicro micro)
        {
            this.ContractReportMicroId = micro.ContractReportMicroId;
            this.ContractReportId = micro.ContractReportId;
            this.ContractId = micro.ContractId;
            this.Type = micro.Type;
            this.XmlGid = micro.Gid;
            this.VersionNum = micro.VersionNum;
            this.VersionSubNum = micro.VersionSubNum;
            this.Status = micro.Status;
            this.StatusNote = micro.StatusNote;
            this.IsFromExternalSystem = micro.IsFromExternalSystem;

            if (micro.ExcelBlobKey.HasValue)
            {
                this.File = new FileDO
                {
                    Key = micro.ExcelBlobKey.Value,
                    Name = micro.ExcelName,
                };
            }

            this.CreateDate = micro.CreateDate;
            this.Version = micro.Version;
        }

        public int ContractReportMicroId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public ContractReportMicroType? Type { get; set; }

        public Guid XmlGid { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public ContractReportMicroStatus Status { get; set; }

        public string StatusNote { get; set; }

        public bool IsFromExternalSystem { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public FileDO File { get; set; }
    }
}
