using Eumis.Common.Json;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialCSDBudgetItemDO
    {
        public ContractReportFinancialCSDBudgetItemDO()
        {
        }

        public ContractReportFinancialCSDBudgetItemDO(
            ContractReportFinancialCSDBudgetItem contractReportFinancialCSDBudgetItem,
            ContractReportFinancialCSD contractReportFinancialCSD,
            string checkedByUser,
            string techCheckedByUser,
            string certCheckedByUser = null)
        {
            this.ContractReportFinancialCSDBudgetItemId = contractReportFinancialCSDBudgetItem.ContractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialCSDId = contractReportFinancialCSDBudgetItem.ContractReportFinancialCSDId;
            this.ContractReportFinancialId = contractReportFinancialCSDBudgetItem.ContractReportFinancialId;
            this.ContractReportId = contractReportFinancialCSDBudgetItem.ContractReportId;
            this.ContractId = contractReportFinancialCSDBudgetItem.ContractId;

            this.ContractBudgetLevel3AmountId = contractReportFinancialCSDBudgetItem.ContractBudgetLevel3AmountId;

            this.BudgetDetailGid = contractReportFinancialCSDBudgetItem.BudgetDetailGid;
            this.BudgetDetailName = contractReportFinancialCSDBudgetItem.BudgetDetailName;
            this.ContractActivityGid = contractReportFinancialCSDBudgetItem.ContractActivityGid;
            this.ContractActivityName = contractReportFinancialCSDBudgetItem.ContractActivityName;
            this.EuAmount = contractReportFinancialCSDBudgetItem.EuAmount;
            this.BgAmount = contractReportFinancialCSDBudgetItem.BgAmount;
            this.BfpTotalAmount = contractReportFinancialCSDBudgetItem.BfpTotalAmount;
            this.OriginalBfpTotalAmount = contractReportFinancialCSDBudgetItem.BfpTotalAmount;
            this.SelfAmount = contractReportFinancialCSDBudgetItem.SelfAmount;
            this.CrossFinancing = contractReportFinancialCSDBudgetItem.CrossFinancing;
            this.IsVatAmount = contractReportFinancialCSDBudgetItem.IsVatAmount;
            this.TotalAmount = contractReportFinancialCSDBudgetItem.TotalAmount;
            this.OriginalTotalAmount = contractReportFinancialCSDBudgetItem.TotalAmount;
            this.UnitDefinition = contractReportFinancialCSDBudgetItem.UnitDefinition;
            this.ProducedUnitsCount = contractReportFinancialCSDBudgetItem.ProducedUnitsCount;
            this.UnitCost = contractReportFinancialCSDBudgetItem.UnitCost;
            this.InsideEU = contractReportFinancialCSDBudgetItem.InsideEU;
            this.OutsideEU = contractReportFinancialCSDBudgetItem.OutsideEU;
            this.AdvancePayment = contractReportFinancialCSDBudgetItem.AdvancePayment;
            this.ContributionNature = contractReportFinancialCSDBudgetItem.ContributionNature;

            this.CostSupportingDocumentApproved = contractReportFinancialCSDBudgetItem.CostSupportingDocumentApproved;
            this.Status = contractReportFinancialCSDBudgetItem.Status;
            this.Notes = contractReportFinancialCSDBudgetItem.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportFinancialCSDBudgetItem.CheckedDate;

            this.TechCheckedByUser = techCheckedByUser;
            this.TechCheckedDate = contractReportFinancialCSDBudgetItem.TechCheckedDate;

            this.UnapprovedEuAmount = contractReportFinancialCSDBudgetItem.UnapprovedEuAmount;
            this.UnapprovedBgAmount = contractReportFinancialCSDBudgetItem.UnapprovedBgAmount;
            this.UnapprovedBfpTotalAmount = contractReportFinancialCSDBudgetItem.UnapprovedBfpTotalAmount;
            this.UnapprovedSelfAmount = contractReportFinancialCSDBudgetItem.UnapprovedSelfAmount;
            this.UnapprovedTotalAmount = contractReportFinancialCSDBudgetItem.UnapprovedTotalAmount;

            this.UnapprovedByCorrectionEuAmount = contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionEuAmount;
            this.UnapprovedByCorrectionBgAmount = contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionBgAmount;
            this.UnapprovedByCorrectionBfpTotalAmount = contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionBfpTotalAmount;
            this.UnapprovedByCorrectionSelfAmount = contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionSelfAmount;
            this.UnapprovedByCorrectionTotalAmount = contractReportFinancialCSDBudgetItem.UnapprovedByCorrectionTotalAmount;

            this.CorrectionType = contractReportFinancialCSDBudgetItem.CorrectionType;
            this.FinancialCorrectionId = contractReportFinancialCSDBudgetItem.FinancialCorrectionId;
            this.IrregularityId = contractReportFinancialCSDBudgetItem.IrregularityId;

            this.ApprovedEuAmount = contractReportFinancialCSDBudgetItem.ApprovedEuAmount;
            this.ApprovedBgAmount = contractReportFinancialCSDBudgetItem.ApprovedBgAmount;
            this.ApprovedBfpTotalAmount = contractReportFinancialCSDBudgetItem.ApprovedBfpTotalAmount;
            this.ApprovedSelfAmount = contractReportFinancialCSDBudgetItem.ApprovedSelfAmount;
            this.ApprovedTotalAmount = contractReportFinancialCSDBudgetItem.ApprovedTotalAmount;

            this.ContractReportFinancialCSD = new ContractReportFinancialCSDDO(contractReportFinancialCSD);

            this.ModifyDate = contractReportFinancialCSDBudgetItem.ModifyDate;
            this.CreateDate = contractReportFinancialCSDBudgetItem.CreateDate;
            this.Version = contractReportFinancialCSDBudgetItem.Version;

            this.CertStatus = contractReportFinancialCSDBudgetItem.CertStatus;
            this.CertCheckedByUser = certCheckedByUser;
            this.CertCheckedDate = contractReportFinancialCSDBudgetItem.CertCheckedDate;

            this.UncertifiedApprovedEuAmount = contractReportFinancialCSDBudgetItem.UncertifiedApprovedEuAmount;
            this.UncertifiedApprovedBgAmount = contractReportFinancialCSDBudgetItem.UncertifiedApprovedBgAmount;
            this.UncertifiedApprovedBfpTotalAmount = contractReportFinancialCSDBudgetItem.UncertifiedApprovedBfpTotalAmount;
            this.UncertifiedApprovedSelfAmount = contractReportFinancialCSDBudgetItem.UncertifiedApprovedSelfAmount;
            this.UncertifiedApprovedTotalAmount = contractReportFinancialCSDBudgetItem.UncertifiedApprovedTotalAmount;

            this.CertifiedApprovedEuAmount = contractReportFinancialCSDBudgetItem.CertifiedApprovedEuAmount;
            this.CertifiedApprovedBgAmount = contractReportFinancialCSDBudgetItem.CertifiedApprovedBgAmount;
            this.CertifiedApprovedBfpTotalAmount = contractReportFinancialCSDBudgetItem.CertifiedApprovedBfpTotalAmount;
            this.CertifiedApprovedSelfAmount = contractReportFinancialCSDBudgetItem.CertifiedApprovedSelfAmount;
            this.CertifiedApprovedTotalAmount = contractReportFinancialCSDBudgetItem.CertifiedApprovedTotalAmount;
        }

        public ContractReportFinancialCSDBudgetItemDO(
            ContractReportFinancialCSDBudgetItem contractReportFinancialCSDBudgetItem,
            ContractReportFinancialCSD contractReportFinancialCSD,
            string checkedByUser,
            string techCheckedByUser,
            ContractReportFinancialStatus contractReportFinancialStatus,
            string certCheckedByUser = null)
            : this(contractReportFinancialCSDBudgetItem, contractReportFinancialCSD, checkedByUser, techCheckedByUser, certCheckedByUser)
        {
            this.ContractReportFinancialStatus = contractReportFinancialStatus;
        }

        public int ContractReportFinancialCSDBudgetItemId { get; set; }

        public int ContractReportFinancialCSDId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public int ContractBudgetLevel3AmountId { get; set; }

        public Guid BudgetDetailGid { get; set; }

        public string BudgetDetailName { get; set; }

        public Guid? ContractActivityGid { get; set; }

        public string ContractActivityName { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal EuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal BgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal BfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal OriginalBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal SelfAmount { get; set; }

        public YesNoNonApplicable CrossFinancing { get; set; }

        public bool IsVatAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal TotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal OriginalTotalAmount { get; set; }

        public string UnitDefinition { get; set; }

        public int? ProducedUnitsCount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnitCost { get; set; }

        public YesNoNonApplicable InsideEU { get; set; }

        public YesNoNonApplicable OutsideEU { get; set; }

        public YesNoNonApplicable AdvancePayment { get; set; }

        public YesNoNonApplicable ContributionNature { get; set; }

        public bool? CostSupportingDocumentApproved { get; set; }

        public ContractReportFinancialCSDBudgetItemStatus Status { get; set; }

        public ContractReportFinancialStatus ContractReportFinancialStatus { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public string TechCheckedByUser { get; set; }

        public DateTime? TechCheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedByCorrectionEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedByCorrectionBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedByCorrectionBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedByCorrectionSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UnapprovedByCorrectionTotalAmount { get; set; }

        public CorrectionType? CorrectionType { get; set; }

        public int? FinancialCorrectionId { get; set; }

        public int? IrregularityId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractReportFinancialCSDDO ContractReportFinancialCSD { get; set; }

        public ContractReportFinancialCSDBudgetItemCertStatus? CertStatus { get; set; }

        public string CertCheckedByUser { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedApprovedTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedApprovedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedApprovedTotalAmount { get; set; }
    }
}