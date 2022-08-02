using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.EuReimbursedAmounts
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
