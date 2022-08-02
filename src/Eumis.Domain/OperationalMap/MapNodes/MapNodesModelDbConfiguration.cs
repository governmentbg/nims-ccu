using System.Data.Entity;
using Eumis.Common.Db;
using Eumis.Domain.Contracts.Views;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public class MapNodesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MapNodeMap());
            modelBuilder.Configurations.Add(new MapNodeWithDirectionsMap());
            modelBuilder.Configurations.Add(new MapNodeRelationMap());
            modelBuilder.Configurations.Add(new MapNodeBudgetMap());
            modelBuilder.Configurations.Add(new MapNodeDocumentMap());
            modelBuilder.Configurations.Add(new MapNodeDirectionMap());
        }
    }
}
