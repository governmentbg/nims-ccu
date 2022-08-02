using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Debts
{
    public class ContractDebtPayment
    {
        private ContractDebtPayment()
        {
        }

        public ContractDebtPayment(int contractReportPaymentId)
        {
            this.ContractReportPaymentId = contractReportPaymentId;
        }

        public int ContractDebtPaymentId { get; set; }

        public int ContractDebtId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public virtual ContractDebt ContractDebt { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractDebtPaymentMap : EntityTypeConfiguration<ContractDebtPayment>
    {
        public ContractDebtPaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractDebtPaymentId);

            // Properties
            this.Property(t => t.ContractDebtPaymentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportPaymentId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractDebtPayments");
            this.Property(t => t.ContractDebtPaymentId).HasColumnName("ContractDebtPaymentId");
            this.Property(t => t.ContractDebtId).HasColumnName("ContractDebtId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");

            this.HasRequired(t => t.ContractDebt)
                .WithMany(t => t.ContractDebtPayments)
                .HasForeignKey(t => t.ContractDebtId)
                .WillCascadeOnDelete();
        }
    }
}
