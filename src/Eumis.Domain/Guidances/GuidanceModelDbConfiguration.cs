using Eumis.Common.Db;
using System.Data.Entity;

namespace Eumis.Domain.Guidances
{
    public class GuidanceModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GuidanceMap());
        }
    }
}
