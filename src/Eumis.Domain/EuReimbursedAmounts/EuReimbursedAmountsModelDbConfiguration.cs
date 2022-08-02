using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.EuReimbursedAmounts
{
    public class EuReimbursedAmountsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EuReimbursedAmountMap());
            modelBuilder.Configurations.Add(new EuReimbursedAmountCertReportMap());
        }
    }
}
