using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportInfoDO
    {
        public ContractReportInfoDO()
        {
        }

        public ContractReportInfoDO(ContractReport contractReport, bool hasActualPaymentCheck, bool hasReturnedDocuments)
        {
            this.ReportType = contractReport.ReportType;
            this.OrderNum = contractReport.OrderNum;
            this.Source = contractReport.Source;
            this.Status = contractReport.Status;
            this.StatusDescription = contractReport.Status;
            this.Version = contractReport.Version;

            this.HasActualPaymentCheck = hasActualPaymentCheck;
            this.HasReturnedDocuments = hasReturnedDocuments;
        }

        public ContractReportType? ReportType { get; set; }

        public int? OrderNum { get; set; }

        public Source? Source { get; set; }

        public ContractReportStatus? Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportStatus? StatusDescription { get; set; }

        public byte[] Version { get; set; }

        public bool HasActualPaymentCheck { get; set; }

        public bool HasReturnedDocuments { get; set; }
    }
}
