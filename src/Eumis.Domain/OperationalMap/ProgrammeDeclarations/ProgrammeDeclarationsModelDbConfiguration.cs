using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.OperationalMap.ProgrammeDeclarations
{
    public class ProgrammeDeclarationsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProgrammeDeclarationMap());
            modelBuilder.Configurations.Add(new ProgrammeDeclarationItemMap());
            modelBuilder.Configurations.Add(new ProgrammeAppFormDeclarationMap());
        }
    }
}
