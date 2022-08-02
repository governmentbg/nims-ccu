using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.OperationalMap.ProgrammePriorities
{
    public class ProgrammePrioritiesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProgrammePriorityMap());
            modelBuilder.Configurations.Add(new ProgrammePriorityCompanyMap());
        }
    }
}
