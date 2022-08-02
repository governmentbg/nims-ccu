using System;
using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Web.Api.CompensationDocuments.DataObjects
{
    public class CompensationDocumentDO
    {
        public CompensationDocumentDO()
        {
        }

        public CompensationDocumentDO(CompensationDocument compensationDoc)
        {
            this.CompensationDocumentId = compensationDoc.CompensationDocumentId;
            this.ContractId = compensationDoc.ContractId;
            this.ProgrammePriorityId = compensationDoc.ProgrammePriorityId;
            this.Type = compensationDoc.Type;
            this.CompensationSign = compensationDoc.CompensationSign;
            this.CompensationDocDate = compensationDoc.CompensationDocDate;
            this.Description = compensationDoc.Description;
            this.CompensationReason = compensationDoc.CompensationReason;
            this.BfpEuAmount = compensationDoc.BfpEuAmount;
            this.BfpBgAmount = compensationDoc.BfpBgAmount;
            this.BfpCrossAmount = compensationDoc.BfpCrossAmount;
            this.BfpTotalAmount = compensationDoc.BfpTotalAmount;
            this.SelfAmount = compensationDoc.SelfAmount;
            this.TotalAmount = compensationDoc.TotalAmount;
            this.Version = compensationDoc.Version;
        }

        public int CompensationDocumentId { get; set; }

        public int ContractId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public CompensationDocumentType? Type { get; set; }

        public CompensationSign? CompensationSign { get; set; }

        public DateTime? CompensationDocDate { get; set; }

        public string Description { get; set; }

        public string CompensationReason { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BfpEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BfpBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BfpCrossAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? SelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TotalAmount { get; set; }

        public byte[] Version { get; set; }
    }
}
