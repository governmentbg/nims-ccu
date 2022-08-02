using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.Debts
{
    public class DebtsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContractDebtMap());
            modelBuilder.Configurations.Add(new ContractDebtVersionMap());
            modelBuilder.Configurations.Add(new ContractDebtVersionPaymentMap());
            modelBuilder.Configurations.Add(new ContractDebtInterestMap());
            modelBuilder.Configurations.Add(new CorrectionDebtMap());
            modelBuilder.Configurations.Add(new CorrectionDebtVersionMap());
        }
    }
}
