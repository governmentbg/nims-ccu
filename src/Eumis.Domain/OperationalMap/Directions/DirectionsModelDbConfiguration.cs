using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.OperationalMap.Directions
{
    public class DirectionsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DirectionMap());
            modelBuilder.Configurations.Add(new SubDirectionMap());
        }
    }
}
