using Eumis.Common.Db;
using System.Data.Entity;

namespace Eumis.Domain
{
    public class NewsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NewsMap());
            modelBuilder.Configurations.Add(new NewsFileMap());
        }
    }
}
