using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.SapInterfaces
{
    public partial class SapPaidAmount
    {
        public const string ERRORS_SEPARATOR = "$$";

        public static readonly IList<SapPaymentType> PaidAmountTypes = new List<SapPaymentType>
        {
            SapPaymentType.Advance,
            SapPaymentType.Intermediate,
            SapPaymentType.Final,
            SapPaymentType.Transfer,
        };

        public static readonly IList<SapPaymentType> ReimbursementTypes = new List<SapPaymentType>
        {
            SapPaymentType.Interest,
            SapPaymentType.VoluntaryReimbursement,
            SapPaymentType.MistakeReimbursement,
            SapPaymentType.IrregularityReimbursement,
        };

        public int SapPaidAmountId { get; set; }

        public int SapFileId { get; set; }

        public bool IsImported { get; set; }

        public int? ActuallyPaidAmountId { get; set; }

        public int? ReimbursedAmountId { get; set; }

        public int? ProgrammeId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public string ContractSapNum { get; set; }

        public int? ContractId { get; set; }

        public int? ContractDebtId { get; set; }

        public SapPaidAmountFund? Fund { get; set; }

        public string ContractReportPaymentNum { get; set; }

        public int? ContractReportPaymentId { get; set; }

        public DateTime? ContractReportPaymentDate { get; set; }

        public decimal PaidBfpBgAmount { get; set; }

        public decimal PaidBfpEuAmount { get; set; }

        public SapPaidAmountCurrency? Currency { get; set; }

        public SapPaymentType? PaymentType { get; set; }

        public DateTime? AccDate { get; set; }

        public DateTime? BankDate { get; set; }

        public DateTime? SapDate { get; set; }

        public string Comment { get; set; }

        public string StornoCode { get; set; }

        public string StornoDescr { get; set; }

        public bool HasWarning { get; set; }

        public string Warnings { get; set; }

        public bool HasError { get; set; }

        public string Errors { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class SapPaidAmountMap : EntityTypeConfiguration<SapPaidAmount>
    {
        public SapPaidAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.SapPaidAmountId);

            // Properties
            this.Property(t => t.SapPaidAmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SapFileId)
                .IsRequired();
            this.Property(t => t.IsImported)
                .IsRequired();
            this.Property(t => t.ContractSapNum)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.ContractReportPaymentNum)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.StornoCode)
                .HasMaxLength(50)
                .IsOptional();
            this.Property(t => t.HasWarning)
                .IsRequired();
            this.Property(t => t.HasError)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SapPaidAmounts");
            this.Property(t => t.SapPaidAmountId).HasColumnName("SapPaidAmountId");
            this.Property(t => t.SapFileId).HasColumnName("SapFileId");
            this.Property(t => t.IsImported).HasColumnName("IsImported");
            this.Property(t => t.ActuallyPaidAmountId).HasColumnName("ActuallyPaidAmountId");
            this.Property(t => t.ReimbursedAmountId).HasColumnName("ReimbursedAmountId");

            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.ContractSapNum).HasColumnName("ContractSapNum");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractDebtId).HasColumnName("ContractDebtId");
            this.Property(t => t.Fund).HasColumnName("Fund");
            this.Property(t => t.ContractReportPaymentNum).HasColumnName("ContractReportPaymentNum");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");
            this.Property(t => t.ContractReportPaymentDate).HasColumnName("ContractReportPaymentDate");
            this.Property(t => t.PaidBfpBgAmount).HasColumnName("PaidBfpBgAmount");
            this.Property(t => t.PaidBfpEuAmount).HasColumnName("PaidBfpEuAmount");
            this.Property(t => t.Currency).HasColumnName("Currency");
            this.Property(t => t.PaymentType).HasColumnName("PaymentType");
            this.Property(t => t.AccDate).HasColumnName("AccDate");
            this.Property(t => t.BankDate).HasColumnName("BankDate");
            this.Property(t => t.SapDate).HasColumnName("SapDate");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.StornoCode).HasColumnName("StornoCode");
            this.Property(t => t.StornoDescr).HasColumnName("StornoDescr");
            this.Property(t => t.HasWarning).HasColumnName("HasWarning");
            this.Property(t => t.Warnings).HasColumnName("Warnings");
            this.Property(t => t.HasError).HasColumnName("HasError");
            this.Property(t => t.Errors).HasColumnName("Errors");
        }
    }
}
