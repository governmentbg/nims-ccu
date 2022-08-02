using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.Procurements
{
    public class ProcurementsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProcurementMap());
            modelBuilder.Configurations.Add(new ProcurementDifferentiatedPositionMap());
            modelBuilder.Configurations.Add(new ProcurementDocumentMap());
        }
    }
}
