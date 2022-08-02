using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.Debts
{
    public class DebtsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContractDebtMap());
            modelBuilder.Configurations.Add(new ContractDebtVersionMap());
            modelBuilder.Configurations.Add(new ContractDebtPaymentMap());
            modelBuilder.Configurations.Add(new ContractDebtInterestMap());
            modelBuilder.Configurations.Add(new CorrectionDebtMap());
            modelBuilder.Configurations.Add(new CorrectionDebtVersionMap());
        }
    }
}
