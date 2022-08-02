using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Debts
{
    public class ContractDebtVersionPayment
    {
        private ContractDebtVersionPayment()
        {
        }

        public ContractDebtVersionPayment(int contractReportPaymentId)
        {
            this.ContractReportPaymentId = contractReportPaymentId;
        }

        public int ContractDebtVersionPaymentId { get; set; }

        public int ContractDebtVersionId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public virtual ContractDebtVersion ContractDebtVersion { get; set; }
    }

    public class ContractDebtVersionPaymentMap : EntityTypeConfiguration<ContractDebtVersionPayment>
    {
        public ContractDebtVersionPaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractDebtVersionPaymentId);

            // Properties
            this.Property(t => t.ContractDebtVersionPaymentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ContractReportPaymentId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractDebtVersionPayments");
            this.Property(t => t.ContractDebtVersionPaymentId).HasColumnName("ContractDebtVersionPaymentId");
            this.Property(t => t.ContractDebtVersionId).HasColumnName("ContractDebtVersionId");
            this.Property(t => t.ContractReportPaymentId).HasColumnName("ContractReportPaymentId");

            this.HasRequired(t => t.ContractDebtVersion)
                .WithMany(t => t.ContractDebtVersionPayments)
                .HasForeignKey(t => t.ContractDebtVersionId)
                .WillCascadeOnDelete();
        }
    }
}
