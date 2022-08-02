using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.OperationalMap.ProcedureManuals
{
    public class ProcedureManualModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProcedureManualMap());
        }
    }
}
