using Eumis.Common.Json;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionCSDDO
    {
        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDDO()
        {
        }

        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDDO(
            ContractReportRevalidationCertAuthorityFinancialCorrectionCSD contractReportRevalidationCertAuthorityFinancialCorrectionCSD,
            ContractReportFinancialRevalidationCSD contractReportFinancialRevalidationCSD,
            string financialRevalidationCSDCheckedByUser,
            ContractReportFinancialCSDBudgetItem contractReportFinancialCSDBudgetItem,
            ContractReportFinancialCSD contractReportFinancialCSD,
            string budgetItemCheckedByUser,
            string budgetItemTechCheckedByUser,
            string financialRevalidationCSDCertCheckedByUser = null)
        {
            this.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId;
            this.Sign = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.Sign;
            this.Status = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.Status;
            this.Notes = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.Notes;
            this.CheckedByUserId = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.CheckedByUserId;
            this.CheckedDate = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.CheckedDate;

            this.RevalidatedEuAmount = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedEuAmount;
            this.RevalidatedBgAmount = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedBgAmount;
            this.RevalidatedBfpTotalAmount = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedBfpTotalAmount;
            this.RevalidatedSelfAmount = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedSelfAmount;
            this.RevalidatedTotalAmount = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.RevalidatedTotalAmount;

            this.CreateDate = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.CreateDate;
            this.ModifyDate = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.ModifyDate;
            this.Version = contractReportRevalidationCertAuthorityFinancialCorrectionCSD.Version;

            this.ContractReportFinancialRevalidationCSD = new ContractReportFinancialRevalidationCSDDO(
                contractReportFinancialRevalidationCSD,
                financialRevalidationCSDCheckedByUser,
                contractReportFinancialCSDBudgetItem,
                contractReportFinancialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser,
                financialRevalidationCSDCertCheckedByUser);
        }

        public int ContractReportRevalidationCertAuthorityFinancialCorrectionCSDId { get; set; }

        public Sign? Sign { get; set; }

        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus Status { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

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

        public ContractReportFinancialRevalidationCSDDO ContractReportFinancialRevalidationCSD { get; set; }
    }
}
