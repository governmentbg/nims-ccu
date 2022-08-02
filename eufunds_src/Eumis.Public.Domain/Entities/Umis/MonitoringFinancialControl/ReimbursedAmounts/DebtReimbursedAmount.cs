using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts
{
    public class DebtReimbursedAmount : ReimbursedAmount
    {
        public int? SapFileId { get; set; }

        public int ContractDebtId { get; set; }

        public override ReimbursedAmountDiscriminator Discriminator
        {
            get
            {
                return ReimbursedAmountDiscriminator.Debt;
            }
        }
    }

    public class DebtReimbursedAmountMap : EntityTypeConfiguration<DebtReimbursedAmount>
    {
        public DebtReimbursedAmountMap()
        {
            // Properties
            this.Property(t => t.ContractDebtId)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.SapFileId).HasColumnName("SapFileId");

            this.Property(t => t.ContractDebtId).HasColumnName("ContractDebtId");
        }
    }
}
