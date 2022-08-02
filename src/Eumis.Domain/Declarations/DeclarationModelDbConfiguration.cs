using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain
{
    public class DeclarationModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DeclarationMap());
            modelBuilder.Configurations.Add(new DeclarationFileMap());
        }
    }
}
