using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.SapInterfaces
{
    public class SapImportModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SapSchemaMap());
            modelBuilder.Configurations.Add(new SapFileMap());
            modelBuilder.Configurations.Add(new SapPaidAmountMap());
            modelBuilder.Configurations.Add(new SapDistributedLimitMap());
        }
    }
}
