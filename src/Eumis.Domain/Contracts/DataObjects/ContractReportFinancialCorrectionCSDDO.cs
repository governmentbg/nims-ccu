using Eumis.Common.Json;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialCorrectionCSDDO
    {
        public ContractReportFinancialCorrectionCSDDO()
        {
        }

        public ContractReportFinancialCorrectionCSDDO(
            ContractReportFinancialCorrectionCSD contractReportFinancialCorrectionCSD,
            string checkedByUser,
            ContractReportFinancialCSDBudgetItem contractReportFinancialCSDBudgetItem,
            ContractReportFinancialCSD contractReportFinancialCSD,
            string budgetItemCheckedByUser,
            string budgetItemTechCheckedByUser,
            string certCheckedByUser = null)
        {
            this.ContractReportFinancialCorrectionCSDId = contractReportFinancialCorrectionCSD.ContractReportFinancialCorrectionCSDId;
            this.ContractReportFinancialCorrectionId = contractReportFinancialCorrectionCSD.ContractReportFinancialCorrectionId;
            this.ContractReportFinancialCSDBudgetItemId = contractReportFinancialCorrectionCSD.ContractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialId = contractReportFinancialCorrectionCSD.ContractReportFinancialId;
            this.ContractReportId = contractReportFinancialCorrectionCSD.ContractReportId;
            this.ContractId = contractReportFinancialCorrectionCSD.ContractId;
            this.Gid = contractReportFinancialCorrectionCSD.Gid;

            this.Sign = contractReportFinancialCorrectionCSD.Sign;
            this.Status = contractReportFinancialCorrectionCSD.Status;
            this.Notes = contractReportFinancialCorrectionCSD.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportFinancialCorrectionCSD.CheckedDate;

            this.CorrectedUnapprovedEuAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedEuAmount;
            this.CorrectedUnapprovedBgAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedBgAmount;
            this.CorrectedUnapprovedBfpTotalAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedBfpTotalAmount;
            this.CorrectedUnapprovedSelfAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedSelfAmount;
            this.CorrectedUnapprovedTotalAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedTotalAmount;

            this.CorrectedUnapprovedByCorrectionEuAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionEuAmount;
            this.CorrectedUnapprovedByCorrectionBgAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionBgAmount;
            this.CorrectedUnapprovedByCorrectionBfpTotalAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionBfpTotalAmount;
            this.CorrectedUnapprovedByCorrectionSelfAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionSelfAmount;
            this.CorrectedUnapprovedByCorrectionTotalAmount = contractReportFinancialCorrectionCSD.CorrectedUnapprovedByCorrectionTotalAmount;

            this.CorrectionType = contractReportFinancialCorrectionCSD.CorrectionType;
            this.FinancialCorrectionId = contractReportFinancialCorrectionCSD.FinancialCorrectionId;
            this.IrregularityId = contractReportFinancialCorrectionCSD.IrregularityId;

            this.CorrectedApprovedEuAmount = contractReportFinancialCorrectionCSD.CorrectedApprovedEuAmount;
            this.CorrectedApprovedBgAmount = contractReportFinancialCorrectionCSD.CorrectedApprovedBgAmount;
            this.CorrectedApprovedBfpTotalAmount = contractReportFinancialCorrectionCSD.CorrectedApprovedBfpTotalAmount;
            this.CorrectedApprovedSelfAmount = contractReportFinancialCorrectionCSD.CorrectedApprovedSelfAmount;
            this.CorrectedApprovedTotalAmount = contractReportFinancialCorrectionCSD.CorrectedApprovedTotalAmount;

            this.CreateDate = contractReportFinancialCorrectionCSD.CreateDate;
            this.ModifyDate = contractReportFinancialCorrectionCSD.ModifyDate;
            this.Version = contractReportFinancialCorrectionCSD.Version;

            this.ContractReportFinancialCSDBudgetItem = new ContractReportFinancialCSDBudgetItemDO(
                contractReportFinancialCSDBudgetItem,
                contractReportFinancialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);

            this.CertStatus = contractReportFinancialCorrectionCSD.CertStatus;
            this.CertCheckedByUser = certCheckedByUser;
            this.CertCheckedDate = contractReportFinancialCorrectionCSD.CertCheckedDate;
            this.UncertifiedCorrectedApprovedEuAmount = contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedEuAmount;
            this.UncertifiedCorrectedApprovedBgAmount = contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedBgAmount;
            this.UncertifiedCorrectedApprovedBfpTotalAmount = contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedBfpTotalAmount;
            this.UncertifiedCorrectedApprovedSelfAmount = contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedSelfAmount;
            this.UncertifiedCorrectedApprovedTotalAmount = contractReportFinancialCorrectionCSD.UncertifiedCorrectedApprovedTotalAmount;

            this.CertifiedCorrectedApprovedEuAmount = contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedEuAmount;
            this.CertifiedCorrectedApprovedBgAmount = contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedBgAmount;
            this.CertifiedCorrectedApprovedBfpTotalAmount = contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedBfpTotalAmount;
            this.CertifiedCorrectedApprovedSelfAmount = contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedSelfAmount;
            this.CertifiedCorrectedApprovedTotalAmount = contractReportFinancialCorrectionCSD.CertifiedCorrectedApprovedTotalAmount;
        }

        public int ContractReportFinancialCorrectionCSDId { get; set; }

        public int ContractReportFinancialCorrectionId { get; set; }

        public int ContractReportFinancialCSDBudgetItemId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public Sign? Sign { get; set; }

        public ContractReportFinancialCorrectionCSDStatus Status { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedByCorrectionEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedByCorrectionBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedByCorrectionBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedByCorrectionSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedUnapprovedByCorrectionTotalAmount { get; set; }

        public CorrectionType? CorrectionType { get; set; }

        public int? FinancialCorrectionId { get; set; }

        public int? IrregularityId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractReportFinancialCSDBudgetItemDO ContractReportFinancialCSDBudgetItem { get; set; }

        public ContractReportFinancialCorrectionCSDCertStatus? CertStatus { get; set; }

        public string CertCheckedByUser { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedCorrectedApprovedTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedCorrectedApprovedTotalAmount { get; set; }
    }
}
