using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using System;

namespace Eumis.Data.Contracts.PortalViewObjects
{
    public class ContractReportPaymentPVO
    {
        public ContractReportPaymentPVO(ContractReportPayment contractReportPayment)
        {
            this.CreateDate = contractReportPayment.CreateDate;
            this.ModifyDate = contractReportPayment.ModifyDate;
            this.VersionNum = contractReportPayment.VersionNum;
            this.VersionSubNum = contractReportPayment.VersionSubNum;
            this.Status = new EnumPVO<ContractReportPaymentStatus>()
            {
                Description = contractReportPayment.Status,
                Value = contractReportPayment.Status,
            };
            this.StatusNote = contractReportPayment.StatusNote;
            this.Gid = contractReportPayment.Gid;
            this.Version = contractReportPayment.Version;
        }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public EnumPVO<ContractReportPaymentStatus> Status { get; set; }

        public string StatusNote { get; set; }

        public Guid Gid { get; set; }

        public byte[] Version { get; set; }
    }
}
