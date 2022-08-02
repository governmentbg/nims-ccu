using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.CertAuthorityChecks
{
    public class CertAuthorityChecksModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CertAuthorityCheckMap());
            modelBuilder.Configurations.Add(new CertAuthorityCheckAscertainmentMap());
            modelBuilder.Configurations.Add(new CertAuthorityCheckLevelItemMap());
            modelBuilder.Configurations.Add(new CertAuthorityCheckProjectMap());
            modelBuilder.Configurations.Add(new CertAuthorityCheckDocumentMap());
        }
    }
}
