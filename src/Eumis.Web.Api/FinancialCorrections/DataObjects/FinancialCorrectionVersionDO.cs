using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.FinancialCorrections.DataObjects
{
    public class FinancialCorrectionVersionDO
    {
        public FinancialCorrectionVersionDO()
        {
            this.ViolationIds = new List<int>().ToArray();
        }

        public FinancialCorrectionVersionDO(FinancialCorrectionVersion financialCorrectionVersion, FinancialCorrection financialCorrection)
        {
            this.FinancialCorrectionVersionId = financialCorrectionVersion.FinancialCorrectionVersionId;
            this.FinancialCorrectionId = financialCorrectionVersion.FinancialCorrectionId;

            this.ContractId = financialCorrection.ContractId;
            this.ContractContractId = financialCorrection.ContractContractId;
            this.ContractBudgetLevel3AmountId = financialCorrection.ContractBudgetLevel3AmountId;

            this.OrderNum = financialCorrectionVersion.OrderNum;
            this.Status = financialCorrectionVersion.Status;
            this.Percent = financialCorrectionVersion.Percent;
            this.EuAmount = financialCorrectionVersion.EuAmount;
            this.BgAmount = financialCorrectionVersion.BgAmount;
            this.BfpAmount = financialCorrectionVersion.BfpAmount;
            this.SelfAmount = financialCorrectionVersion.SelfAmount;
            this.TotalAmount = financialCorrectionVersion.TotalAmount;
            this.FinancialCorrectionImposingReasonId = financialCorrectionVersion.FinancialCorrectionImposingReasonId;
            this.Description = financialCorrectionVersion.Description;
            this.ViolationFoundBy = financialCorrectionVersion.ViolationFoundBy;
            this.AmendmentReason = financialCorrectionVersion.AmendmentReason;
            this.CorrectionBearer = financialCorrectionVersion.CorrectionBearer;

            if (financialCorrectionVersion.File != null)
            {
                this.File = new FileDO
                {
                    Key = financialCorrectionVersion.File.Key,
                    Name = financialCorrectionVersion.File.FileName,
                };
            }

            this.ViolationIds = financialCorrectionVersion.FinancialCorrectionVersionViolations.Select(t => t.OtherViolationId).ToArray();

            this.IsFirstVersion = financialCorrectionVersion.IsFirstVersion;
            this.CreateDate = financialCorrectionVersion.CreateDate;
            this.ModifyDate = financialCorrectionVersion.ModifyDate;
            this.Version = financialCorrectionVersion.Version;
        }

        public int? FinancialCorrectionVersionId { get; set; }

        public int? FinancialCorrectionId { get; set; }

        public int? ContractId { get; set; }

        public int? ContractContractId { get; set; }

        public int? ContractBudgetLevel3AmountId { get; set; }

        public int? OrderNum { get; set; }

        public FinancialCorrectionVersionStatus Status { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? Percent { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? EuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? SelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TotalAmount { get; set; }

        public int? FinancialCorrectionImposingReasonId { get; set; }

        public string Description { get; set; }

        public FinancialCorrectionVersionViolationFoundBy? ViolationFoundBy { get; set; }

        public AmendmentReason? AmendmentReason { get; set; }

        public CorrectionBearer? CorrectionBearer { get; set; }

        public FileDO File { get; set; }

        public int[] ViolationIds { get; set; }

        public bool IsFirstVersion { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
