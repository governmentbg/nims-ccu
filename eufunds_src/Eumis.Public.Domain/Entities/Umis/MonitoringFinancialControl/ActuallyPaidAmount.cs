using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public partial class ActuallyPaidAmount : IAggregateRoot
    {
        public int ActuallyPaidAmountId { get; set; }

        public int? SapFileId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public FinanceSource FinanceSource { get; set; }

        public int ContractId { get; set; }

        public int? ContractReportPaymentId { get; set; }

        public string RegNumber { get; set; }

        public ActuallyPaidAmountStatus Status { get; set; }

        public PaymentReason PaymentReason { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string Comment { get; set; }

        public decimal? PaidBfpEuAmount { get; set; }

        public decimal? PaidBfpBgAmount { get; set; }

        public decimal? PaidBfpTotalAmount { get; set; }

        public decimal? PaidSelfAmount { get; set; }

        public decimal? PaidTotalAmount { get; set; }

        public decimal? PaidBfpCrossAmount { get; set; }

        public bool IsActivated { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }

    public class ActuallyPaidAmountMap : EntityTypeConfiguration<ActuallyPaidAmount>
    {
        public ActuallyPaidAmountMap()
        {
            // Primary Key
            this.HasKey(t => t.ActuallyPaidAmountId);

            // Properties
            this.Property(t => t.ActuallyPaidAmountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProgrammeId)
                .IsRequired();
            this.Property(t => t.ProgrammePriorityId)
                .IsRequired();
            this.Property(t => t.FinanceSource)
                .IsRequired();
            this.Property(t => t.ContractId)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.PaymentReason)
                .IsRequired();
            this.Property(t => t.IsActivated)
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
            this.ToTable("ActuallyPaidAmounts");
            this.Property(t => t.ActuallyPaidAmountId).HasColumnName("ActuallyPaidAmountId");
            this.Property(t => t.SapFileId).HasColumnName("SapFileId");
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");
            this.Property(t => t.ProgrammePriorityId).HasColumnName("ProgrammePriorityId");
            this.Property(t => t.FinanceSource).HasColumnName("FinanceSource");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.PaymentReason).HasColumnName("PaymentReason");
            this.Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            this.Property(t => t.Comment).HasColumnName("Comment");

            this.Property(t => t.PaidBfpEuAmount).HasColumnName("PaidBfpEuAmount");
            this.Property(t => t.PaidBfpBgAmount).HasColumnName("PaidBfpBgAmount");
            this.Property(t => t.PaidBfpTotalAmount).HasColumnName("PaidBfpTotalAmount");
            this.Property(t => t.PaidSelfAmount).HasColumnName("PaidSelfAmount");
            this.Property(t => t.PaidTotalAmount).HasColumnName("PaidTotalAmount");
            this.Property(t => t.PaidBfpCrossAmount).HasColumnName("PaidBfpCrossAmount");

            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsDeletedNote).HasColumnName("IsDeletedNote");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
