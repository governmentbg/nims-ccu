using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using System;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractReportFinancialPVO
    {
        public ContractReportFinancialPVO(ContractReportFinancial contractReportFinancial)
        {
            this.CreateDate = contractReportFinancial.CreateDate;
            this.ModifyDate = contractReportFinancial.ModifyDate;
            this.VersionNum = contractReportFinancial.VersionNum;
            this.VersionSubNum = contractReportFinancial.VersionSubNum;
            this.Status = new EnumPVO<ContractReportFinancialStatus>()
            {
                Description = contractReportFinancial.Status,
                Value = contractReportFinancial.Status,
            };
            this.StatusNote = contractReportFinancial.StatusNote;
            this.Gid = contractReportFinancial.Gid;
            this.Version = contractReportFinancial.Version;
        }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public EnumPVO<ContractReportFinancialStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public Guid Gid { get; set; }

        public byte[] Version { get; set; }
    }
}
