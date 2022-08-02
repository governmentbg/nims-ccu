using Eumis.Public.Domain.Entities.Umis.Core;
using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public partial class ContractReportFinancialCSDBudgetItem : IAggregateRoot
    {

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
        public decimal EuAmount { get; set; }
        public decimal BgAmount { get; set; }
        public decimal BfpTotalAmount { get; set; }
        public decimal SelfAmount { get; set; }
        public YesNoNonApplicable CrossFinancing { get; set; }
        public bool IsVatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitDefinition { get; set; }
        public int? ProducedUnitsCount { get; set; }
        public decimal? UnitCost { get; set; }
        public YesNoNonApplicable InsideEU { get; set; }
        public YesNoNonApplicable OutsideEU { get; set; }
        public YesNoNonApplicable AdvancePayment { get; set; }
        public YesNoNonApplicable ContributionNature { get; set; }

        public bool? CostSupportingDocumentApproved { get; set; }
        public ContractReportFinancialCSDBudgetItemStatus Status { get; set; }
        public string Notes { get; set; }
        public int? CheckedByUserId { get; set; }
        public DateTime? CheckedDate { get; set; }
        public int? TechCheckedByUserId { get; set; }
        public DateTime? TechCheckedDate { get; set; }

        public decimal? UnapprovedEuAmount { get; set; }
        public decimal? UnapprovedBgAmount { get; set; }
        public decimal? UnapprovedBfpTotalAmount { get; set; }
        public decimal? UnapprovedSelfAmount { get; set; }
        public decimal? UnapprovedTotalAmount { get; set; }

        public decimal? UnapprovedByCorrectionEuAmount { get; set; }
        public decimal? UnapprovedByCorrectionBgAmount { get; set; }
        public decimal? UnapprovedByCorrectionBfpTotalAmount { get; set; }
        public decimal? UnapprovedByCorrectionSelfAmount { get; set; }
        public decimal? UnapprovedByCorrectionTotalAmount { get; set; }

        public CorrectionType? CorrectionType { get; set; }
        public int? FinancialCorrectionId { get; set; }
        public int? IrregularityId { get; set; }

        public decimal? ApprovedEuAmount { get; set; }
        public decimal? ApprovedBgAmount { get; set; }
        public decimal? ApprovedBfpTotalAmount { get; set; }
        public decimal? ApprovedSelfAmount { get; set; }
        public decimal? ApprovedTotalAmount { get; set; }

        public int? CertReportId { get; set; }

        public ContractReportFinancialCSDBudgetItemCertStatus? CertStatus { get; set; }
        public int? CertCheckedByUserId { get; set; }
        public DateTime? CertCheckedDate { get; set; }
        public decimal? UncertifiedApprovedEuAmount { get; set; }
        public decimal? UncertifiedApprovedBgAmount { get; set; }
        public decimal? UncertifiedApprovedBfpTotalAmount { get; set; }
        public decimal? UncertifiedApprovedSelfAmount { get; set; }
        public decimal? UncertifiedApprovedTotalAmount { get; set; }

        public decimal? CertifiedApprovedEuAmount { get; set; }
        public decimal? CertifiedApprovedBgAmount { get; set; }
        public decimal? CertifiedApprovedBfpTotalAmount { get; set; }
        public decimal? CertifiedApprovedSelfAmount { get; set; }
        public decimal? CertifiedApprovedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }
    }

    public class ContractReportFinancialCSDBudgetItemMap : EntityTypeConfiguration<ContractReportFinancialCSDBudgetItem>
    {
        public ContractReportFinancialCSDBudgetItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportFinancialCSDBudgetItemId);

            // Properties
            this.Property(t => t.ContractReportFinancialCSDId)
                .IsRequired();

            this.Property(t => t.ContractReportFinancialId)
                .IsRequired();

            this.Property(t => t.ContractReportId)
                .IsRequired();

            this.Property(t => t.ContractId)
                .IsRequired();

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ContractBudgetLevel3AmountId)
                .IsRequired();

            this.Property(t => t.BudgetDetailGid)
                .IsRequired();

            this.Property(t => t.BudgetDetailName)
                .IsRequired();

            this.Property(t => t.EuAmount)
                .IsRequired();

            this.Property(t => t.BgAmount)
                .IsRequired();

            this.Property(t => t.BfpTotalAmount)
                .IsRequired();

            this.Property(t => t.SelfAmount)
                .IsRequired();

            this.Property(t => t.CrossFinancing)
                .IsRequired();

            this.Property(t => t.IsVatAmount)
                .IsRequired();

            this.Property(t => t.TotalAmount)
                .IsRequired();

            this.Property(t => t.InsideEU)
                .IsRequired();

            this.Property(t => t.OutsideEU)
                .IsRequired();

            this.Property(t => t.AdvancePayment)
                .IsRequired();

            this.Property(t => t.ContributionNature)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ContractReportFinancialCSDBudgetItems");
            this.Property(t => t.ContractReportFinancialCSDBudgetItemId).HasColumnName("ContractReportFinancialCSDBudgetItemId");
            this.Property(t => t.ContractReportFinancialId).HasColumnName("ContractReportFinancialId");
            this.Property(t => t.ContractReportId).HasColumnName("ContractReportId");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Gid).HasColumnName("Gid");

            this.Property(t => t.ContractBudgetLevel3AmountId).HasColumnName("ContractBudgetLevel3AmountId");

            this.Property(t => t.BudgetDetailGid).HasColumnName("BudgetDetailGid");
            this.Property(t => t.BudgetDetailName).HasColumnName("BudgetDetailName");
            this.Property(t => t.ContractActivityGid).HasColumnName("ContractActivityGid");
            this.Property(t => t.ContractActivityName).HasColumnName("ContractActivityName");
            this.Property(t => t.EuAmount).HasColumnName("EuAmount");
            this.Property(t => t.BgAmount).HasColumnName("BgAmount");
            this.Property(t => t.BfpTotalAmount).HasColumnName("BfpTotalAmount");
            this.Property(t => t.SelfAmount).HasColumnName("SelfAmount");
            this.Property(t => t.CrossFinancing).HasColumnName("CrossFinancing");
            this.Property(t => t.IsVatAmount).HasColumnName("IsVatAmount");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.UnitDefinition).HasColumnName("UnitDefinition");
            this.Property(t => t.ProducedUnitsCount).HasColumnName("ProducedUnitsCount");
            this.Property(t => t.UnitCost).HasColumnName("UnitCost");
            this.Property(t => t.InsideEU).HasColumnName("InsideEU");
            this.Property(t => t.OutsideEU).HasColumnName("OutsideEU");
            this.Property(t => t.AdvancePayment).HasColumnName("AdvancePayment");
            this.Property(t => t.ContributionNature).HasColumnName("ContributionNature");

            this.Property(t => t.CostSupportingDocumentApproved).HasColumnName("CostSupportingDocumentApproved");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CheckedByUserId).HasColumnName("CheckedByUserId");
            this.Property(t => t.CheckedDate).HasColumnName("CheckedDate");
            this.Property(t => t.TechCheckedByUserId).HasColumnName("TechCheckedByUserId");
            this.Property(t => t.TechCheckedDate).HasColumnName("TechCheckedDate");

            this.Property(t => t.UnapprovedEuAmount).HasColumnName("UnapprovedEuAmount");
            this.Property(t => t.UnapprovedBgAmount).HasColumnName("UnapprovedBgAmount");
            this.Property(t => t.UnapprovedBfpTotalAmount).HasColumnName("UnapprovedBfpTotalAmount");
            this.Property(t => t.UnapprovedSelfAmount).HasColumnName("UnapprovedSelfAmount");
            this.Property(t => t.UnapprovedTotalAmount).HasColumnName("UnapprovedTotalAmount");

            this.Property(t => t.UnapprovedByCorrectionEuAmount).HasColumnName("UnapprovedByCorrectionEuAmount");
            this.Property(t => t.UnapprovedByCorrectionBgAmount).HasColumnName("UnapprovedByCorrectionBgAmount");
            this.Property(t => t.UnapprovedByCorrectionBfpTotalAmount).HasColumnName("UnapprovedByCorrectionBfpTotalAmount");
            this.Property(t => t.UnapprovedByCorrectionSelfAmount).HasColumnName("UnapprovedByCorrectionSelfAmount");
            this.Property(t => t.UnapprovedByCorrectionTotalAmount).HasColumnName("UnapprovedByCorrectionTotalAmount");

            this.Property(t => t.CorrectionType).HasColumnName("CorrectionType");
            this.Property(t => t.FinancialCorrectionId).HasColumnName("FinancialCorrectionId");
            this.Property(t => t.IrregularityId).HasColumnName("IrregularityId");

            this.Property(t => t.ApprovedEuAmount).HasColumnName("ApprovedEuAmount");
            this.Property(t => t.ApprovedBgAmount).HasColumnName("ApprovedBgAmount");
            this.Property(t => t.ApprovedBfpTotalAmount).HasColumnName("ApprovedBfpTotalAmount");
            this.Property(t => t.ApprovedSelfAmount).HasColumnName("ApprovedSelfAmount");
            this.Property(t => t.ApprovedTotalAmount).HasColumnName("ApprovedTotalAmount");

            this.Property(t => t.CertReportId).HasColumnName("CertReportId");

            this.Property(t => t.CertStatus).HasColumnName("CertStatus");
            this.Property(t => t.CertCheckedByUserId).HasColumnName("CertCheckedByUserId");
            this.Property(t => t.CertCheckedDate).HasColumnName("CertCheckedDate");
            this.Property(t => t.UncertifiedApprovedEuAmount).HasColumnName("UncertifiedApprovedEuAmount");
            this.Property(t => t.UncertifiedApprovedBgAmount).HasColumnName("UncertifiedApprovedBgAmount");
            this.Property(t => t.UncertifiedApprovedBfpTotalAmount).HasColumnName("UncertifiedApprovedBfpTotalAmount");
            this.Property(t => t.UncertifiedApprovedSelfAmount).HasColumnName("UncertifiedApprovedSelfAmount");
            this.Property(t => t.UncertifiedApprovedTotalAmount).HasColumnName("UncertifiedApprovedTotalAmount");
            this.Property(t => t.CertifiedApprovedEuAmount).HasColumnName("CertifiedApprovedEuAmount");
            this.Property(t => t.CertifiedApprovedBgAmount).HasColumnName("CertifiedApprovedBgAmount");
            this.Property(t => t.CertifiedApprovedBfpTotalAmount).HasColumnName("CertifiedApprovedBfpTotalAmount");
            this.Property(t => t.CertifiedApprovedSelfAmount).HasColumnName("CertifiedApprovedSelfAmount");
            this.Property(t => t.CertifiedApprovedTotalAmount).HasColumnName("CertifiedApprovedTotalAmount");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}