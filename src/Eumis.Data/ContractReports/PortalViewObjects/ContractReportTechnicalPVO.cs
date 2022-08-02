using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using System;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractReportTechnicalPVO
    {
        public ContractReportTechnicalPVO(ContractReportTechnical contractReportTechnical)
        {
            this.CreateDate = contractReportTechnical.CreateDate;
            this.ModifyDate = contractReportTechnical.ModifyDate;
            this.VersionNum = contractReportTechnical.VersionNum;
            this.VersionSubNum = contractReportTechnical.VersionSubNum;
            this.Status = new EnumPVO<ContractReportTechnicalStatus>()
            {
                Description = contractReportTechnical.Status,
                Value = contractReportTechnical.Status,
            };
            this.StatusNote = contractReportTechnical.StatusNote;
            this.Gid = contractReportTechnical.Gid;
            this.Version = contractReportTechnical.Version;
        }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public EnumPVO<ContractReportTechnicalStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public Guid Gid { get; set; }

        public byte[] Version { get; set; }
    }
}
