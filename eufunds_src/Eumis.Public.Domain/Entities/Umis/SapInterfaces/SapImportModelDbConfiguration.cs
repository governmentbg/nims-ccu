using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces
{
    public class SapImportModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SapSchemaMap());
            modelBuilder.Configurations.Add(new SapFileMap());
            modelBuilder.Configurations.Add(new SapPaidAmountMap());
        }
    }
}
