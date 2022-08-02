using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.InterestSchemes
{
    public class InterestSchemesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new InterestSchemeMap());
        }
    }
}
