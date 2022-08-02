using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.OperationalMap.Programmes
{
    public class ProgrammesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProgrammeMap());
            modelBuilder.Configurations.Add(new ProgrammeApplicationDocumentMap());
        }
    }
}
