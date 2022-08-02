using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts
{
    public class ContractReimbursedAmount : ReimbursedAmount
    {
        public override ReimbursedAmountDiscriminator Discriminator
        {
            get
            {
                return ReimbursedAmountDiscriminator.Contract;
            }
        }
    }

    public class ContractReimbursedAmountMap : EntityTypeConfiguration<ContractReimbursedAmount>
    {
        public ContractReimbursedAmountMap()
        {
        }
    }
}
