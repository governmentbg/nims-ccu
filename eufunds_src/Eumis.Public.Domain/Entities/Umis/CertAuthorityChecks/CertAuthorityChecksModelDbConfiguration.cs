using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public class CertAuthorityChecksModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CertAuthorityCheckMap());
            modelBuilder.Configurations.Add(new CertAuthorityCheckAscertainmentMap());
            modelBuilder.Configurations.Add(new CertAuthorityCheckLevelItemMap());
            modelBuilder.Configurations.Add(new CertAuthorityCheckDocumentMap());
        }
    }
}
