using Eumis.Common.Json;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialRevalidationCSDDO
    {
        public ContractReportFinancialRevalidationCSDDO()
        {
        }

        public ContractReportFinancialRevalidationCSDDO(
            ContractReportFinancialRevalidationCSD contractReportFinancialRevalidationCSD,
            string checkedByUser,
            ContractReportFinancialCSDBudgetItem contractReportFinancialCSDBudgetItem,
            ContractReportFinancialCSD contractReportFinancialCSD,
            string budgetItemCheckedByUser,
            string budgetItemTechCheckedByUser,
            string certCheckedByUser = null)
        {
            this.ContractReportFinancialRevalidationCSDId = contractReportFinancialRevalidationCSD.ContractReportFinancialRevalidationCSDId;
            this.ContractReportFinancialRevalidationId = contractReportFinancialRevalidationCSD.ContractReportFinancialRevalidationId;
            this.ContractReportFinancialCSDBudgetItemId = contractReportFinancialRevalidationCSD.ContractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialId = contractReportFinancialRevalidationCSD.ContractReportFinancialId;
            this.ContractReportId = contractReportFinancialRevalidationCSD.ContractReportId;
            this.ContractId = contractReportFinancialRevalidationCSD.ContractId;
            this.Gid = contractReportFinancialRevalidationCSD.Gid;

            this.Sign = contractReportFinancialRevalidationCSD.Sign;
            this.Status = contractReportFinancialRevalidationCSD.Status;
            this.Notes = contractReportFinancialRevalidationCSD.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportFinancialRevalidationCSD.CheckedDate;

            this.RevalidatedEuAmount = contractReportFinancialRevalidationCSD.RevalidatedEuAmount;
            this.RevalidatedBgAmount = contractReportFinancialRevalidationCSD.RevalidatedBgAmount;
            this.RevalidatedBfpTotalAmount = contractReportFinancialRevalidationCSD.RevalidatedBfpTotalAmount;
            this.RevalidatedSelfAmount = contractReportFinancialRevalidationCSD.RevalidatedSelfAmount;
            this.RevalidatedTotalAmount = contractReportFinancialRevalidationCSD.RevalidatedTotalAmount;

            this.CreateDate = contractReportFinancialRevalidationCSD.CreateDate;
            this.ModifyDate = contractReportFinancialRevalidationCSD.ModifyDate;
            this.Version = contractReportFinancialRevalidationCSD.Version;

            this.ContractReportFinancialCSDBudgetItem = new ContractReportFinancialCSDBudgetItemDO(
                contractReportFinancialCSDBudgetItem,
                contractReportFinancialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);

            this.CertStatus = contractReportFinancialRevalidationCSD.CertStatus;
            this.CertCheckedByUser = certCheckedByUser;
            this.CertCheckedDate = contractReportFinancialRevalidationCSD.CertCheckedDate;
            this.UncertifiedRevalidatedEuAmount = contractReportFinancialRevalidationCSD.UncertifiedRevalidatedEuAmount;
            this.UncertifiedRevalidatedBgAmount = contractReportFinancialRevalidationCSD.UncertifiedRevalidatedBgAmount;
            this.UncertifiedRevalidatedBfpTotalAmount = contractReportFinancialRevalidationCSD.UncertifiedRevalidatedBfpTotalAmount;
            this.UncertifiedRevalidatedSelfAmount = contractReportFinancialRevalidationCSD.UncertifiedRevalidatedSelfAmount;
            this.UncertifiedRevalidatedTotalAmount = contractReportFinancialRevalidationCSD.UncertifiedRevalidatedTotalAmount;

            this.CertifiedRevalidatedEuAmount = contractReportFinancialRevalidationCSD.CertifiedRevalidatedEuAmount;
            this.CertifiedRevalidatedBgAmount = contractReportFinancialRevalidationCSD.CertifiedRevalidatedBgAmount;
            this.CertifiedRevalidatedBfpTotalAmount = contractReportFinancialRevalidationCSD.CertifiedRevalidatedBfpTotalAmount;
            this.CertifiedRevalidatedSelfAmount = contractReportFinancialRevalidationCSD.CertifiedRevalidatedSelfAmount;
            this.CertifiedRevalidatedTotalAmount = contractReportFinancialRevalidationCSD.CertifiedRevalidatedTotalAmount;
        }

        public int ContractReportFinancialRevalidationCSDId { get; set; }

        public int ContractReportFinancialRevalidationId { get; set; }

        public int ContractReportFinancialCSDBudgetItemId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public Sign? Sign { get; set; }

        public ContractReportFinancialRevalidationCSDStatus Status { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? RevalidatedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractReportFinancialCSDBudgetItemDO ContractReportFinancialCSDBudgetItem { get; set; }

        public ContractReportFinancialRevalidationCSDCertStatus? CertStatus { get; set; }

        public string CertCheckedByUser { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedRevalidatedTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedRevalidatedTotalAmount { get; set; }
    }
}
