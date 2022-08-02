using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.Audits
{
    public class AuditsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuditMap());
            modelBuilder.Configurations.Add(new AuditDocMap());
            modelBuilder.Configurations.Add(new AuditAscertainmentMap());
            modelBuilder.Configurations.Add(new AuditLevelItemMap());
            modelBuilder.Configurations.Add(new AuditProjectMap());
            modelBuilder.Configurations.Add(new AuditAscertainmentItemMap());
        }
    }
}
