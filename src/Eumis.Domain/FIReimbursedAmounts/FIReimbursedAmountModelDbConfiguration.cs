using Eumis.Common.Db;
using System.Data.Entity;

namespace Eumis.Domain.FIReimbursedAmounts
{
    public class FIReimbursedAmountModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FIReimbursedAmountMap());
        }
    }
}
