using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts
{
    public class ContractReimbursedAmountPayment
    {
        private ContractReimbursedAmountPayment()
        {
        }

        public ContractReimbursedAmountPayment(int contractReportPaymentId)
        {
            this.ContractReportPaymentId = contractReportPaymentId;
        }

        public int ReimbursedAmountPaymentId { get; set; }

        public int ReimbursedAmountId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public virtual ContractReimbursedAmount ContractReimbursedAmount { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReimbursedAmountPaymentMap : EntityTypeConfiguration<ContractReimbursedAmountPayment>
    {
        public ContractReimbursedAmountPaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.ReimbursedAmountPaymentId);

            // Properties
            this.Property(t => t.ReimbursedAmountPaymentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportPaymentId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ReimbursedAmountPayments");
            this.Property(t => t.ReimbursedAmountPaymentId).HasColumnName("ReimbursedAmountPaymentId");
            this.Property(t => t.ReimbursedAmountId).HasColumnName("ReimbursedAmountId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");

            this.HasRequired(t => t.ContractReimbursedAmount)
                .WithMany(t => t.ContractReimbursedAmountPayments)
                .HasForeignKey(t => t.ReimbursedAmountId)
                .WillCascadeOnDelete();
        }
    }
}
