using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.CertReports
{
    public class CertReportModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CertReportMap());
            modelBuilder.Configurations.Add(new CertReportDocumentMap());
            modelBuilder.Configurations.Add(new CertReportAttachedCertReportMap());
        }
    }
}
