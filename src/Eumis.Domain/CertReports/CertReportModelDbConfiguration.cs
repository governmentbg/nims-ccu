using Eumis.Common.Db;
using System.Data.Entity;

namespace Eumis.Domain.CertReports
{
    public class CertReportModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CertReportMap());
            modelBuilder.Configurations.Add(new CertReportDocumentMap());
            modelBuilder.Configurations.Add(new CertReportCertificationDocumentMap());
            modelBuilder.Configurations.Add(new CertReportAttachedCertReportMap());
            modelBuilder.Configurations.Add(new CertReportSnapshotMap());
            modelBuilder.Configurations.Add(new CertReportSnapshotFileMap());
        }
    }
}
