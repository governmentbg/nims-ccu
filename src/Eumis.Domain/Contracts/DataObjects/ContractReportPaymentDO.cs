using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportPaymentDO
    {
        public ContractReportPaymentDO()
        {
        }

        public ContractReportPaymentDO(ContractReportPayment contractReportPayment)
        {
            this.ContractReportPaymentId = contractReportPayment.ContractReportPaymentId;
            this.ContractReportId = contractReportPayment.ContractReportId;
            this.ContractId = contractReportPayment.ContractId;
            this.XmlGid = contractReportPayment.Gid;
            this.VersionNum = contractReportPayment.VersionNum;
            this.VersionSubNum = contractReportPayment.VersionSubNum;
            this.Status = contractReportPayment.Status;
            this.StatusNote = contractReportPayment.StatusNote;

            this.PaymentType = contractReportPayment.PaymentType;
            this.RegDate = contractReportPayment.RegDate;
            this.OtherRegistration = contractReportPayment.OtherRegistration;
            this.SubmitDate = contractReportPayment.SubmitDate;
            this.SubmitDeadline = contractReportPayment.SubmitDeadline;
            this.DateFrom = contractReportPayment.DateFrom;
            this.DateTo = contractReportPayment.DateTo;
            this.RequestedAmount = contractReportPayment.RequestedAmount;

            this.CreateDate = contractReportPayment.CreateDate;
            this.Version = contractReportPayment.Version;
        }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid XmlGid { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        public ContractReportPaymentStatus Status { get; set; }

        public string StatusNote { get; set; }

        public ContractReportPaymentType? PaymentType { get; set; }

        public DateTime? RegDate { get; set; }

        public string OtherRegistration { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? SubmitDeadline { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RequestedAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
