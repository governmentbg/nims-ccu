using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.InterestSchemes
{
    public class InterestSchemesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new InterestSchemeMap());
        }
    }
}
